using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using UILib.Base;

namespace UILib.Controls
{
	public class ColItem : TableItem
	{
		public override float X
		{
			get
			{
				var beforerelative = from r in Container.Cols
									 where r.ValueWidth < 0
									 where r.Index < Index
									 select r;
				var beforevalue = from r in Container.Cols
								  where r.ValueWidth >= 0
								  where r.Index < Index
								  select r;

				return beforerelative.Sum(r => r.Width) + beforevalue.Sum(r => r.ValueWidth) + BaseControl.X;
			}
			set { }
		}

		public override float Width
		{
			get
			{
				if (ValueWidth >= 0)
				{
					return Math.Max(0, Math.Min(ValueWidth, BaseControl.X + BaseControl.Width - X));
				}
				var nonrelative = from r in Container.Cols
								  where r.ValueWidth != -1
								  select r;

				var relative = from r in Container.Cols
							   where r.ValueWidth < 0
							   select r;

				return (Container.Width - nonrelative.Sum(r => r.ValueWidth)) *
						RelativeWidth / relative.Sum(r => r.RelativeWidth);
			}
			set { ValueWidth = value; }
		}

		public float RelativeWidth { get; set; }
		public float ValueWidth { get; set; }

		public ColItem(Table baseControl, GraphicsDevice device)
			: base(baseControl, device)
		{
			ValueWidth = -1;
			RelativeWidth = 1;
			Index = -1;

			baseControl.Cols.Add(this);
		}
	}
}
