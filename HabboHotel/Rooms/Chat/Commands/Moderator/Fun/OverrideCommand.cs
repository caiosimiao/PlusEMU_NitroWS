﻿namespace Plus.HabboHotel.Rooms.Chat.Commands.Moderator.Fun
{
    class OverrideCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "command_override"; }
        }

        public string Parameters
        {
            get { return ""; }
        }

        public string Description
        {
            get { return "Gives you the ability to walk over anything."; }
        }

        public void Execute(GameClients.GameClient Session, Room Room, string[] Params)
        {
            RoomUser User = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            if (User == null)
                return;

            User.AllowOverride = !User.AllowOverride;
            Session.SendWhisper("Override mode updated.");
        }
    }
}
