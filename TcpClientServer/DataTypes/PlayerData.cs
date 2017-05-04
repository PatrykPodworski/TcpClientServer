using System;

namespace TcpClientServer.DataTypes
{
    [Serializable]
    public class PlayerData
    {
        public string Name { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Id { get; set; }

        public PlayerData(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Id: {Id}, X: {PositionX}, Y: {PositionY}";
        }
    }
}