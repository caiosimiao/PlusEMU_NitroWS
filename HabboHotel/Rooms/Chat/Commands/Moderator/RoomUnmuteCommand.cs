﻿using System.Collections.Generic;

namespace Plus.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class RoomUnmuteCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "command_unroommute"; }
        }

        public string Parameters
        {
            get { return ""; }
        }

        public string Description
        {
            get { return "Unmute the room."; }
        }

        public void Execute(GameClients.GameClient Session, Room Room, string[] Params)
        {
            if (!Room.RoomMuted)
            {
                Session.SendWhisper("This room isn't muted.");
                return;
            }

            Room.RoomMuted = false;

            List<RoomUser> RoomUsers = Room.GetRoomUserManager().GetRoomUsers();
            if (RoomUsers.Count > 0)
            {
                foreach (RoomUser User in RoomUsers)
                {
                    if (User == null || User.GetClient() == null || User.GetClient().GetHabbo() == null || User.GetClient().GetHabbo().Username == Session.GetHabbo().Username)
                        continue;

                    User.GetClient().SendWhisper("This room has been un-muted .");
                }
            }
        }
    }
}