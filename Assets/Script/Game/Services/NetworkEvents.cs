using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Services
{
    public static class NetworkEvents
    {
        public static readonly byte PlayerConnectToRoom = 0;
        public static readonly byte LobbyCountDown = 7;
        public static readonly byte StartGame = 8;
        public static readonly byte FinishGame = 9;
        public static readonly byte PlayerAnswer = 10; 
        public static readonly byte FinishRound = 11;
        public static readonly byte StartRaund = 12;
    }
}
