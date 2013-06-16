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
			get { return Container.GetY(this); }
			set { }
		}

		public override float Height
		{
			get { return Container.GetHeight(this); }
			set { ValueHeight = value; }
		}

		public float ProcentHeight { get; set; }
		public float ValueHeight { get; set; }

		public RowItem(Table baseControl, GraphicsDevice device)
			: base(baseControl, device)
		{
			ProcentHeight = -1;
			ValueHeight = -1;
			Index = -1;

			baseControl.Rows.Add(this);
		}

	}
}
