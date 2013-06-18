using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using UILib.Base;

namespace UILib.Controls
{
	public class RowItem : TableItem
	{
		public override float Y
		{
			get 
			{
				var beforerelative = from r in Container.Rows
									 where r.ValueHeight < 0
									 where r.Index < Index
									 select r;
				var beforevalue = from r in Container.Rows
								  where r.ValueHeight >= 0
								  where r.Index < Index
								  select r;

				return beforerelative.Sum(r => r.Height) + beforevalue.Sum(r => r.ValueHeight) + BaseControl.Y;
			}
			set { }
		}

		public override float Height
		{
			get 
			{
				if (ValueHeight >= 0)
				{
					return Math.Max(0, Math.Min(ValueHeight, BaseControl.Y + BaseControl.Height - Y));
				}
				var nonrelative = from r in Container.Rows
								  where r.ValueHeight != -1
								  select r;

				var relative = from r in Container.Rows
							   where r.ValueHeight < 0
							   select r;

				return (Container.Height - nonrelative.Sum(r => r.ValueHeight)) * 
						RelativeHeight / relative.Sum(r => r.RelativeHeight);
			}
			set { ValueHeight = value; }
		}

		public float RelativeHeight { get; set; }
		public float ValueHeight { get; set; }

		public RowItem(Table baseControl, GraphicsDevice device)
			: base(baseControl, device)
		{
			RelativeHeight = 1;
			ValueHeight = -1;
			Index = -1;

			baseControl.Rows.Add(this);
		}

	}
}
