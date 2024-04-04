using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Assignment3.Tests
{
    public static class SerializationHelper
    {
        public static void SerializeUsers(SLL users, string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = File.Create(fileName))
            {
                formatter.Serialize(stream, users);
            }
        }

        public static SLL DeserializeUsers(string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = File.OpenRead(fileName))
            {
                return (SLL)formatter.Deserialize(stream);
            }
        }
    }
}
