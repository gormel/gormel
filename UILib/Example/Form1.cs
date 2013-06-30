using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example
{
	public partial class Form1 : Form
	{	
		public Form1()
		{
			InitializeComponent();
			textBox1.WordWrap = false;
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			listBox1.Items.Clear();
			listBox1.Items.AddRange(textBox1.Lines);
		}
	}
}
