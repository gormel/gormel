using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gameplay;

namespace RoomsServer
{
	public class ServerPlayer
	{
		public string Name { get; private set; }
		public Images Image { get; private set; }
		public ServerPlayer(string name, Images image)
		{
			Name = name;
			Image = image;
		}
	}
}
