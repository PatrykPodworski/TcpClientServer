using System;

namespace TcpClientServer.DataTypes
{
    [Serializable]
    public class PlayerData
    {
        public string Name { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public PlayerData(string name)
        {
            Name = name;
        }
    }
}