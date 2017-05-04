using System;
using System.Collections.Generic;

namespace TcpClientServer.DataTypes
{
    [Serializable]
    public class GameData
    {
        public List<PlayerData> Players { get; set; }
        public GameState GameState { get; set; }
        private readonly int _maxPlayers;

        public GameData(int maxPlayers)
        {
            _maxPlayers = maxPlayers;
            Players = new List<PlayerData>();
        }

        public void AddPlayer(PlayerData player)
        {
            if (Players.Count < _maxPlayers)
            {
                Players.Add(player);
            }
        }
    }
}
