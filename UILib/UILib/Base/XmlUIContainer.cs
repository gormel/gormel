using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using UILib.Controls;
using UILib.Utils;

namespace UILib.Base
{
	public class XmlUIContainer : UIContainer
	{
		private Dictionary<Type, BaseStringParser> parsers = new Dictionary<Type, BaseStringParser>();

		public XmlUIContainer(object controller, GraphicsDevice device, ContentManager content, string UIScheme)
			: base(controller, device)
		{
			parsers[typeof(int)] = new IntStringParser();
			parsers[typeof(bool)] = new BoolStringParser();
			parsers[typeof(float)] = new FloatStringParser();
			parsers[typeof(string)] = new StringStringParser();

			parsers[typeof(Texture2D)] = new TextureStringParser(content);
			parsers[typeof(SpriteFont)] = new SpriteFontStringParser(content);

			parsers[typeof(HorisontalAlligment)] = new EnumStringParser<HorisontalAlligment>();
			parsers[typeof(VerticalAlligment)] = new EnumStringParser<VerticalAlligment>();

			parsers[typeof(Color)] = new ColorStringParser();

			var files = controller.GetType().Assembly.GetManifestResourceNames();
			var file = controller.GetType().Assembly.GetManifestResourceStream(files.First(s => s.EndsWith(UIScheme)));
			XElement xml = XElement.Load(file);

			BaseControl = ParseControl(xml, null);
			BaseControl.Activate();
		}

		private UIControl ParseControl(XElement xml, object baseControl)
		{
			var controlType = Assembly.GetAssembly(typeof(XmlUIContainer)).GetTypes()
								.Concat(Assembly.GetAssembly(Controller.GetType()).GetTypes())
								.First(s => s.Name == xml.Name.LocalName);
			var control = Activator.CreateInstance(controlType, baseControl, GraphicsDevice);
			foreach (var attr in xml.Attributes())
			{
				var property = controlType.GetProperty(attr.Name.LocalName);
				if (property != null)
				{
					property.SetValue(control, parsers[property.PropertyType].ParseString(attr.Value), null);
					continue;
				}

				var @event = controlType.GetEvent(attr.Name.LocalName);
				var method = Controller.GetType().GetMethod(attr.Value, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
				if (@event != null)
				{
					if (method == null)
						throw new Exception("Wrong xml, missing handler method in controller.");
					@event.AddEventHandler(control, Delegate.CreateDelegate(typeof(EventHandler), Controller, method));
					continue;
				}

				throw new Exception(string.Format("No property or event {0} in class {1}.", attr.Name, controlType.Name));
			}

			foreach (var child in xml.Elements())
			{
				if (child.Name.LocalName.StartsWith(controlType.Name + "."))
				{
					var propertyName = child.Name.LocalName.Substring(controlType.Name.Length + 1);
					var property = controlType.GetProperty(propertyName);
					if (property == null)
						throw new Exception(string.Format("No property {0} in class {1}.", propertyName, controlType.Name));
					property.SetValue(control, parsers[property.PropertyType].ParseString(child.Value), null);
					continue;
				}
				ParseControl(child, control);
			}
			var resultControl = (UIControl)control;
			if (resultControl.Name == "")
				resultControl.Name = string.Format("Control {0}", Controls.Count);
			Controls.Add(resultControl.Name, resultControl);

			return resultControl;
		}
	}
}
