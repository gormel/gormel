using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using UILib.Base;

namespace UILib.Controls
{
	public class ColItem : Table
	{
		private float width;
		private Table container;

		public override float X
		{
			get
			{
				var upper = from c in BaseControl.Controls
							where c is ColItem
							where ((ColItem)c).Index < Index
							select (ColItem)c;
				return BaseControl.X + upper.Sum(r => r.Width);
			}
			set { }
		}

		public override float Width
		{
			get
			{
				float exceptedWidth = -1;
				if (ProcentWidth != -1)
					exceptedWidth = BaseControl.Width * ProcentWidth;
				else if (width != -1)
					exceptedWidth = width;
				else
				{
					var setted = BaseControl.Controls.Where(c => c is ColItem && (((ColItem)c).width != -1 || ((ColItem)c).ProcentWidth != -1));
					exceptedWidth = (BaseControl.Width - setted.Sum(c => c.Width)) / (BaseControl.Controls.Count(c => c is ColItem) - setted.Count());
				}
				return Math.Max(0, Math.Min(exceptedWidth, BaseControl.X + BaseControl.Width - X));
			}
			set { width = value; }
		}

		public float ProcentWidth { get; set; }
		public int Index { get; set; }

		public ColItem(Table baseControl, GraphicsDevice device)
			: base(baseControl, device)
		{
			width = -1;
			ProcentWidth = -1;
			Index = -1;

			baseControl.Cols.Add(this);
			container = baseControl;
		}
	}
}
