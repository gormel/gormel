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
				Cursor cursor = GetBrainCursor();
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

		private Cursor GetBrainCursor()
		{
			Cursor cursor = new Cursor();
			var freeCells = from x in Enumerable.Range(0, serverFiled.Width)
							from y in Enumerable.Range(0, serverFiled.Height)
							where !serverFiled[x, y].IsWall
							select new Point(x, y);

			foreach (var p in freeCells)
			{
				Direction direction;
				if (OneLeft(p, out direction))
				{
					cursor.X = p.X;
					cursor.Y = p.Y;
					cursor.Direction = direction;
					return cursor;
				}
			}

			int willTake = freeCells.Count();
			Cursor temp = cursor;
			foreach (var p in freeCells)
			{
				for (int i = 0; i < 4; i++)
				{
					cursor.X = p.X;
					cursor.Y = p.Y;
					cursor.Direction = (Direction)i;
					if (!Free(cursor))
						continue;
					int take = HowManyTake(cursor);
					if (take == 0)
						return cursor;
					if (take < willTake)
					{
						willTake = take;
						temp = (Cursor)cursor.Clone();
					}
				}
			}

			return temp;
		}

		private bool Free(Cursor cursor)
		{
			switch (cursor.Direction)
			{
				case Direction.Up:
					return !serverFiled[cursor.X, cursor.Y - 1].Down;
				case Direction.Left:
					return !serverFiled[cursor.X - 1, cursor.Y].Right;
				case Direction.Down:
					return !serverFiled[cursor.Position].Down;
				case Direction.Right:
					return !serverFiled[cursor.Position].Right;
				default:
					return false;
			}
		}

		private bool OneLeft(Point position, out Direction direction)
		{
			bool left = serverFiled[position.X - 1, position.Y].Right;
			bool right = serverFiled[position].Right;
			bool top = serverFiled[position.X, position.Y - 1].Down;
			bool down = serverFiled[position].Down;

			if (!left && right && top && down)
			{
				direction = Direction.Left;
				return true;
			}
			else if (left && !right && top && down)
			{
				direction = Direction.Right;
				return true;
			}
			else if (left && right && !top && down)
			{
				direction = Direction.Up;
				return true;
			}
			else if (left && right && top && !down)
			{
				direction = Direction.Down;
				return true;
			}
			direction = Direction.Down;
			return false;
		}

		private int HowManyTake(Cursor cursor)
		{
			Cursor c = (Cursor)cursor.Clone();
			Cursor c1 = MirrorCursor(c);

			Point start = new[] { c.Position, c1.Position }.FirstOrDefault(p => NearCanGo(p).Count() < 3);
			HashSet<Point> visited = new HashSet<Point>();
			if (start != Point.Empty)
				visited.Add(start);

			foreach (var p in NearCanGo(start))
			{
				CalculateTake(visited, p);
			}
			return visited.Count;
		}

		private void CalculateTake(HashSet<Point> visited, Point watching)
		{
			if (visited.Contains(watching))
				return;
			var t = from n in NearCanGo(watching)
					where !visited.Contains(n)
					select n;
			if (t.Count() > 1)
				return;

			visited.Add(watching);
			foreach (var p in t)
				CalculateTake(visited, p);
		}

		private IEnumerable<Point> NearCanGo(Point pos)
		{
			if (pos == Point.Empty)
				yield break;

			bool left = serverFiled[pos.X - 1, pos.Y].Right;
			bool right = serverFiled[pos].Right;
			bool top = serverFiled[pos.X, pos.Y - 1].Down;
			bool down = serverFiled[pos].Down;

			if (!left)
			{
				yield return new Point(pos.X - 1, pos.Y);
			}
			if (!right)
			{
				yield return new Point(pos.X + 1, pos.Y);
			}
			if (!top)
			{
				yield return new Point(pos.X, pos.Y - 1);
			}
			if (!down)
			{
				yield return new Point(pos.X, pos.Y + 1);
			}
		}
		
		private Cursor MirrorCursor(Cursor cursor)
		{
			int free = (int)cursor.Direction + 1;
			Direction newDirection = (Direction)((free + 1) % 4 );
			int newX = cursor.X + Math.Abs(2 - free) - 1;
			int newY = cursor.Y - Math.Abs(3 - free) + 1;

			Cursor rv = new Cursor();
			rv.X = newX;
			rv.Y = newY;
			rv.Direction = newDirection;
			return rv;
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
			Text = string.Format("{0}", playerQueue.PlayingPlayer.Name);
		}
	}
}
