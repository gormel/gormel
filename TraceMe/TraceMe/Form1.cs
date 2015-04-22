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

        Mesh cube;

        Task graph;
        Stopwatch sw = new Stopwatch();
        Progress<string> titleProgress;

        object locking = new object();

        CancellationTokenSource tokenSource;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitUserComponents()
        {
            DoubleBuffered = true;
            device = new RenderDevice(CreateGraphics(), Width, Height, Math.PI / 4);
            device.Randomizer = 10;
            device.FillColor = Color.CornflowerBlue;

            Vector3[] vertices = new Vector3[] 
            {
                new Vector3(-100, -100, -100),
                new Vector3(-100, -100, 100),
                new Vector3(-100, 100, -100),
                new Vector3(-100, 100, 100),
                new Vector3(100, -100, -100),
                new Vector3(100, -100, 100),
                new Vector3(100, 100, -100),
                new Vector3(100, 100, 100),
            };
            int[] indices = new int[] { 
                0, 1, 3, 3, 2, 0, 
                0, 2, 6, 6, 4, 0, 
                0, 1, 5, 5, 4, 0, 
                4, 5, 7, 7, 6, 4, 
                1, 3, 7, 7, 5, 1, 
                3, 7, 6, 6, 2, 3 };
            Color[] colors = new Color[] { Color.Black, Color.Blue, Color.Green, Color.Cyan, Color.Red, Color.Magenta, Color.Yellow, Color.White };
            double[] reflections = new double[] { 0, 0, 0, 0, 0, 0, 0, 0 };

            cube = new Mesh(vertices, indices, colors, reflections);
            cube.Transformation = cube.Transformation * Matrix4.Translation(new Vector3(-100, -100, 100));
            device.Objects.Add(cube);

            this.FormClosing += Form1_FormClosing;

            titleProgress = new Progress<string>();
            titleProgress.ProgressChanged += titleProgress_ProgressChanged;

            RestartRendering();
        }

        async void RestartRendering()
        {
            if (graph != null)
            {
                tokenSource.Cancel();
                await graph;
            }

            tokenSource = new CancellationTokenSource();

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
            lock(locking) 
            {
                if (e.KeyCode == Keys.F2)
                {
                    Vector3 t = cube.Transformation.GetTranslation();
                    cube.Transformation = Matrix4.Translation(t) * Matrix4.RotationY(Math.PI / 16) * Matrix4.Translation(-t) * cube.Transformation;
                }
                if (e.KeyCode == Keys.S)
                    tokenSource.Cancel();
                if (e.KeyCode == Keys.R)
                    RestartRendering();
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Color c = device.Render(e.X, e.Y);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitUserComponents();
        }
    }
}
