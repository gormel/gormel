using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OfflineRoomClient
{
	public partial class Form1 : Form
	{
		private ClientFiledControl ctrl;
		private List<ServerPlayer> serverPlayers = new List<ServerPlayer>();
		private List<ClientPlayer> clientPlayers = new List<ClientPlayer>();
		private ServerFiled serverFiled;
		private Brain brain;

		private static int w = 10;
		private static int h = 15;
		private static int k = 30;

		private Random r = new Random();

		ServerPlayer me = new ServerPlayer("Me", Images.Cross);

		private ServerPlayerQueue playerQueue;
		public Form1()
		{
			InitializeComponent();

			serverFiled = new ServerFiled(w, h);
			brain = new NormalBrain<ServerCell, ServerPlayer>(serverFiled);

			InitializeUserComponents();
			
			serverPlayers.Add(me);
			serverPlayers.Add(new ServerPlayer("player 2", Images.Zero));

			clientPlayers.AddRange(GeneratePlayers(serverPlayers));

			playerQueue = new ServerPlayerQueue(serverPlayers);
		}

		private void InitializeUserComponents()
		{
			ctrl = new ClientFiledControl(new ClientFiled(w, h));
			this.SetBounds(Bounds.X, Bounds.Y, w * k, h * k);
			ctrl.Dock = DockStyle.Fill;
			UpdateClientFiled(from x in Enumerable.Range(0, w)
							  from y in Enumerable.Range(0, h)
							  select new Point(x, y));
			ctrl.Clicked += Filed_Clicked;
			Controls.Add(ctrl);

			Timer t = new Timer();
			t.Interval = 100;
			t.Tick += t_Tick;
			t.Enabled = true;
			t.Start();
		}

		private IEnumerable<Point> DoSteps()
		{
			while (playerQueue.PlayingPlayer != me && !serverFiled.Complete)
			{
				Cursor cursor = brain.Step();
				if (serverFiled.Step(playerQueue.PlayingPlayer, cursor))
				{
					playerQueue.Step();
				}
				yield return cursor.Position;
				yield return new Point(cursor.X - 1, cursor.Y);
				yield return new Point(cursor.X + 1, cursor.Y);
				yield return new Point(cursor.X, cursor.Y - 1);
				yield return new Point(cursor.X, cursor.Y + 1);
			}
		}

		private IEnumerable<ClientPlayer> GeneratePlayers(IEnumerable<ServerPlayer> players)
		{
			foreach (var p in players)
			{
				var cPlayer = new ClientPlayer(p.Name, Image.FromFile(string.Format("{0}.png", p.Image.ToString())));
				yield return cPlayer;
			}
		}

		void Filed_Clicked(object sender, Cursor e)
		{
			var where = new List<Point>() { new Point(e.X, e.Y), new Point(e.X - 1, e.Y), new Point(e.X, e.Y - 1), 
				new Point(e.X + 1, e.Y), new Point(e.X, e.Y + 1) };
			if (serverFiled.Step(playerQueue.PlayingPlayer, e))
			{
				playerQueue.Step();
				where.AddRange(DoSteps());
			}
			UpdateClientFiled(where);
		}

		private void UpdateClientFiled(IEnumerable<Point> where)
		{
			var serverData = serverFiled.GetData(where);
			FiledData<ClientCell, ClientPlayer> clientData = new FiledData<ClientCell, ClientPlayer>();

			foreach (var p in serverData.Points)
			{
				ClientCell cell = new ClientCell();
				cell.Down = serverData[p].Down;
				cell.Right = serverData[p].Right;
				cell.IsWall = serverData[p].IsWall;
				cell.Symbol = clientPlayers.FirstOrDefault(pl => serverData[p].Symbol == null ? false : pl.Name == serverData[p].Symbol.Name);
				clientData.Add(p, cell);
			}
			ctrl.Filed.Update(clientData);
			ctrl.UpdateCells(where);
		}

		void t_Tick(object sender, EventArgs e)
		{
			Text = string.Format("{0}: {1}", playerQueue.PlayingPlayer.Name, serverFiled.Score(me));
		}
	}
}
