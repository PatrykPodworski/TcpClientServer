using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TcpClientServer
{
    [Serializable]
    public class DataType
    {
        public GameState GameState { get; set; }
        public string Name { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public byte[] Serialize()
        {
            IFormatter formatter = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, this);
                return ms.ToArray();
            }
        }

        public static DataType Deserialize(byte[] data)
        {
            IFormatter formatter = new BinaryFormatter();
            using (var ms = new MemoryStream(data))
            {
                var deserialized = formatter.Deserialize(ms) as DataType;
                return deserialized;
            }
        }
    }

    public enum GameState
    {
        Accepted,
        Rejected,
        Waiting,
        Playing
    }
}
