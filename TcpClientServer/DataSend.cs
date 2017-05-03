using System;

namespace TcpClientServer
{
    [Serializable]
    public class DataSend
    {
        public string Name { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
    }
}
