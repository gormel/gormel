using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LineSplit
{
    public partial class ClipControl : UserControl
    {
        public Shape Shape { get; private set; }
        public ClipType ClipType { get; set; }
        private bool dragMode = false;
        Point lastMousePos;

        public ClipControl(Shape shape)
        {
            InitializeComponent();

            Shape = shape;

            MouseDown += new MouseEventHandler(ClipControl_MouseDown);
            MouseUp += new MouseEventHandler(ClipControl_MouseUp);
            MouseMove += new MouseEventHandler(ClipControl_MouseMove);
        }

        void ClipControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragMode)
            {
                int dx = -lastMousePos.X + e.X;
                int dy = -lastMousePos.Y + e.Y;

                Shape.Translate(dx, dy);
                Shape.Draw(CreateGraphics(), ClipType);
            }
            lastMousePos = e.Location;
        }

        void ClipControl_MouseUp(object sender, MouseEventArgs e)
        {
            dragMode = false;
        }

        void ClipControl_MouseDown(object sender, MouseEventArgs e)
        {
            dragMode = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Shape.Draw(CreateGraphics(), ClipType);
        }
    }
}
