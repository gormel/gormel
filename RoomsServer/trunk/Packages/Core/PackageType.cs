using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packages
{
	public enum PackageType
	{	
		Login,
		LoginSuccess,
		LoginFailed,
		Disconnected,

		LoggedIn,
		LoggedOut,
		Logout,
		JoinQueue,
		GetPlayerInfo,
		PlayerInfo,

		JoinedRoom,
		YouJoinedRoom,
		ExitedRoom,
		PublicRoomMessage,
		TeamRoomMessage,
		PrivateRoomMessage,
		RoomSessionEnd,

		FiledDataUpdated,
		PlayerStep,
		Step,
		Stats,
	}
}
