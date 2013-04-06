using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packages
{
	public enum PackageType
	{
		Login = 1,
		LoginSuccess = 2,
		LoginFailed = 3,
		LoggedIn = 4,
		LoggedOut = 5,
		Logout = 6,
		JoinQueue = 7,
		JoinedRoom = 8,
		YouJoinedRoom = 9,
		ExitedRoom = 10,
		PublicRoomMessage = 11,
		TeamRoomMessage = 12,
		PrivateRoomMessage = 13,
		RoomSessionEnd = 14,
	}
}
