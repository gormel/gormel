using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LineSplit
{
    public partial class Form1 : Form
    {
        private bool dragMode = false;

        Shape s = new Shape(4);
        ClipControl clip;

        public Form1()
        {
            InitializeComponent();

            clip = new ClipControl(s);
            clip.Resize += (s1, e) => clip.Refresh();
            panel1.Controls.Add(clip);

            clip.Shape.Points[0] = new Point(10, 10);
            clip.Shape.Points[1] = new Point(100, 10);
            clip.Shape.Points[2] = new Point(100, 100);
            clip.Shape.Points[3] = new Point(10, 100);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clip.ClipType = ClipType.Coin;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clip.Width += 10;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clip.Height += 10;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            clip.Height -= 10;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clip.Width -= 10;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            clip.ClipType = ClipType.Beck;
        }
    }
}
