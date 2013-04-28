using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameplay;
using Packages;

namespace RoomsServer
{
	public class RoomSession : Session<RoomClientInfo>
	{
		ServerFiled filed = null;
		ServerPlayerQueue queue = null;
		public RoomSession(IEnumerable<RoomClientInfo> clients)
		{
			int w = Server.Instance.Random.Next(10, 15);
			int h = Server.Instance.Random.Next(10, 15);
			foreach (var client in clients)
			{
				YouJoinedRoomPackage yjrPack = new YouJoinedRoomPackage();
				yjrPack.Team = client.Team.Name;
				yjrPack.Image = client.Player.Image;
				yjrPack.Width = w;
				yjrPack.Height = h;
				client.Client.Send(yjrPack);
				foreach (var item in clients)
				{
					JoinedRoomPackage p = new JoinedRoomPackage();
					p.Name = item.Name;
					p.Team = item.Team.Name;
					p.Image = item.Player.Image;
					client.Client.Send(p);
				}
				Clients.Add(client);
			}

			filed = new ServerFiled(w, h);
			queue = new ServerPlayerQueue(from c in clients select c.Player);

			var points = from x in Enumerable.Range(0, w)
						 from y in Enumerable.Range(0, h)
						 select new Point(x, y);
			FiledDataUpdatedPackage fduPack = getUpdated(points, filed);
			foreach (var client in Clients)
				client.Client.Send(fduPack);

			foreach (var client in Clients)
			{
				PlayerStepPackage psPackage = new PlayerStepPackage();
				psPackage.Name = queue.PlayingPlayer.Name;
				client.Client.Send(psPackage);
			}
		}

		protected override void OnPackageRecive(RoomClientInfo info, Packages.Package pack)
		{
			switch (pack.ID)
			{
				case PackageType.PublicRoomMessage:
					foreach (var client in Clients)
					{
						client.Client.Send(pack);
					}
					break;
				case PackageType.TeamRoomMessage:
					foreach (var teamate in Clients.Where(i => i.Team == info.Team))
					{
						teamate.Client.Send(pack);
					}
					break;
				case PackageType.PrivateRoomMessage:
					PrivateRoomMessagePackage prmPack = (PrivateRoomMessagePackage)pack;
					Clients.First(i => i.Name == prmPack.To).Client.Send(pack);
					break;
				case PackageType.Step:
					StepPackage sPack = (StepPackage)pack;
					if (queue.PlayingPlayer.Name == info.Player.Name)
					{
						FiledCursor cursor = new FiledCursor() 
							{ X = sPack.X, Y = sPack.Y, Direction = sPack.Direction };
						if (filed.Step(queue.PlayingPlayer, cursor))
						{
							queue.Step();
							foreach (var client in Clients)
							{
								PlayerStepPackage psPackage = new PlayerStepPackage();
								psPackage.Name = queue.PlayingPlayer.Name;
								client.Client.Send(psPackage);
							}
						}
						var points = new[] { new Point(cursor.X, cursor.Y), new Point(cursor.X - 1, cursor.Y),
											new Point(cursor.X + 1, cursor.Y), new Point(cursor.X, cursor.Y - 1),
											new Point(cursor.X, cursor.Y + 1)};
						FiledDataUpdatedPackage fduPack = getUpdated(points, filed);

						foreach (var client in Clients)
							client.Client.Send(fduPack);

						if (filed.Complete)
						{
							var teams = Clients.GroupBy(c => c.Team);
							Dictionary<Team, int> score = 
								teams.ToDictionary(g => g.Key, g => g.Sum(c => filed.Score(c.Player)));

							int middle = score.Values.Sum() / score.Count;
							
							foreach (var team in teams)
							{
								RoomSessionEndPackage rsePack = new RoomSessionEndPackage();
								rsePack.EloAdded = score[team.Key] - middle;
								foreach (var client in team)
								{
									Server.Instance.ClientStorage[client.Name].Rating += rsePack.EloAdded;
									client.Client.Send(rsePack);
								}
							}
							Server.Instance.Rooms.Remove(this);
							while (Clients.Any())
							{
								Clients.RemoveAt(0);
							}
						}
					}
					break;
				case PackageType.Logout:
					var dTeams = Clients.GroupBy(c => c.Team);
					Dictionary<Team, int> dScore = 
						dTeams.ToDictionary(g => g.Key, g => g.Sum(c => filed.Score(c.Player)));
					int dMiddle = dScore.Sum(p => p.Value) / dScore.Count;
					Team loosers = info.Team;
					foreach (var team in dTeams)
					{
						RoomSessionEndPackage rsePack = new RoomSessionEndPackage();
						rsePack.EloAdded = dScore[team.Key];
						int toAdd = dScore[team.Key];
						if (team.Key == loosers)
							rsePack.EloAdded = -rsePack.EloAdded;

						foreach (var client in team)
						{
							Server.Instance.ClientStorage[client.Name].Rating += rsePack.EloAdded;
							client.Client.Send(rsePack);
						}
					}
					Server.Instance.Rooms.Remove(this);
					while (Clients.Any())
						Clients.RemoveAt(0);
					break;
				default:
					break;
			}
		}

		private static FiledDataUpdatedPackage getUpdated(IEnumerable<Point> points, ServerFiled filed)
		{
			FiledDataUpdatedPackage fduPack = new FiledDataUpdatedPackage();
			fduPack.X.AddRange(from p in points select p.X);
			fduPack.Y.AddRange(from p in points select p.Y);
			fduPack.Values.AddRange(from p in points 
									select (filed[p].Symbol == null ? "" : filed[p].Symbol.Name));
			fduPack.Right.AddRange(from p in points select filed[p].Right);
			fduPack.Down.AddRange(from p in points select filed[p].Down);
			fduPack.IsWall.AddRange(from p in points select filed[p].IsWall);
			return fduPack;
		}
	}
}
