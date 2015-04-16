using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TraceMe
{
    public partial class Form1 : Form
    {
        RenderDevice device;
        Sphere s;
        Mesh m;
        Task graph;
        Stopwatch sw = new Stopwatch();
        Progress<string> titleProgress;

        CancellationTokenSource tokenSource = new CancellationTokenSource();

        public Form1()
        {
            InitializeComponent();
        }

        private void InitUserComponents()
        {
            DoubleBuffered = true;
            device = new RenderDevice(CreateGraphics(), Width, Height, Math.PI / 4);
            device.FillColor = Color.CornflowerBlue;

            s = new Sphere(new Vector3(0, 0, 200), 100);
            s.Color = Color.Black;
            s.Reflection = 0.5;
            device.Objects.Add(s);

            Vector3[] vertices = new Vector3[] { new Vector3(100, -100, 150), new Vector3(-100, -100, 150), new Vector3(0, 100, 150) };
            int[] indices = new int[] { 0, 1, 2 };
            Color[] colors = new Color[] { Color.FromArgb(128, Color.Red), Color.FromArgb(128, Color.Green), Color.FromArgb(128, Color.Blue) };
            double[] reflections = new double[] { 0, 0, 0 };
            m = new Mesh(vertices, indices, colors, reflections);
            device.Objects.Add(m);

            this.FormClosing += Form1_FormClosing;

            titleProgress = new Progress<string>();
            titleProgress.ProgressChanged += titleProgress_ProgressChanged;

            graph = Task.Run(() => 
            {
                while (!tokenSource.Token.IsCancellationRequested)
                {
                    sw.Restart();
                    device.RenderScene();
                    sw.Stop();
                    ((IProgress<string>)titleProgress).Report((1 / sw.Elapsed.TotalSeconds).ToString("F2"));
                }
            });
        }

        void titleProgress_ProgressChanged(object sender, string e)
        {
            Text = e;
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            tokenSource.Cancel();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //s.Center = s.Center + Vector3.Forward * 20;
            m.Transformation = m.Transformation * Matrix4.RotationY(-Math.PI / 16);
        }

        //private int areaRadius = 10;
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //for (int x = e.X - areaRadius; x < e.X + areaRadius; x++)
            //{
            //    for (int y = e.Y - areaRadius; y < e.Y + areaRadius; y++)
            //    {
            //        device.Render(x, y);
            //    }
            //}
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            device.Render(e.X, e.Y);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitUserComponents();
        }
    }
}
