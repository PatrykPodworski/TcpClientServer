using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TcpClientServer.DataTypes
{
    [Serializable]
    public class DataToSend
    {
        public GameData GameData { get; set; }
        public int ClientId { get; set; }
        public ClientState ClientState { get; set; }

        public byte[] Serialize()
        {
            IFormatter formatter = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, this);
                return ms.ToArray();
            }
        }

        public static DataToSend Deserialize(byte[] data)
        {
            IFormatter formatter = new BinaryFormatter();
            using (var ms = new MemoryStream(data))
            {
                var deserialized = formatter.Deserialize(ms) as DataToSend;
                return deserialized;
            }
        }

        public override string ToString()
        {
            return $"Client state: {ClientState}, Game state: {GameData.GameState},  Players: {GameData.Players.Count}.";
        }
    }
}
