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
			get { return Container.GetX(this); }
			set { }
		}

		public override float Width
		{
			get { return Container.GetWidth(this); }
			set { ValueWidth = value; }
		}

		public float ProcentWidth { get; set; }
		public float ValueWidth { get; set; }

		public ColItem(Table baseControl, GraphicsDevice device)
			: base(baseControl, device)
		{
			ValueWidth = -1;
			ProcentWidth = -1;
			Index = -1;

			baseControl.Cols.Add(this);
		}
	}
}
