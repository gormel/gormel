using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UILib.Base
{
	public class UIContainer
	{
		protected UIControl BaseControl;
		public GraphicsDevice GraphicsDevice { get; private set; }
		protected object Controller { get; private set; }
		protected Dictionary<String, UIControl> Controls { get; private set; }

		protected UIContainer(Object controller, GraphicsDevice device)
		{
			GraphicsDevice = device;
			Controller = controller;
			Controls = new Dictionary<string, UIControl>();
		}

		public dynamic this[string name]
		{
			get { return Controls[name]; }
		}

		public T GetControl<T>(string name) where T : UIControl
		{
			return (T)Controls[name];
		}

		public virtual void Draw(GameTime time)
		{
			BaseControl.Draw(time);
		}

		protected static string ControlInfoString(UIControl c)
		{
			var result = string.Format("{0}({1}, {2}): [ ", c.Name, c.Activable, c.Active);

			foreach (var con in c.Controls)
			{
				result += ControlInfoString(con) + ", ";
			}

			return result + " ]";
		}

		public virtual void Update(GameTime time)
		{
			BaseControl.Update(time);
		}
	}
}
