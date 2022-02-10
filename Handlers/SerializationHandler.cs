using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Takira.Objects;

namespace Takira.Handlers
{
    // TODO: Сделать отлов ошибок
    public static class SerializationHandler
    {
        public static void Save(Dictionary<string, QuestPage> story)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Directory.GetCurrentDirectory() + "/Export/" + story.ElementAt(0).Key + ".cqt", FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, story);
            stream.Close();
        }
        
        public static Dictionary<string, QuestPage> Load(string filename)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Directory.GetCurrentDirectory() + "/Import/" + filename + ".cqt", FileMode.Open, FileAccess.Read);
            Dictionary<string, QuestPage> story = (Dictionary<string,QuestPage>)formatter.Deserialize(stream);
            stream.Close();
            return story;
        }
    }
}