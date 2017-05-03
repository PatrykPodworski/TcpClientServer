using System;

namespace TcpClientServer
{
    [Serializable]
    public class DataType
    {
        public string Name { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
    }
}
