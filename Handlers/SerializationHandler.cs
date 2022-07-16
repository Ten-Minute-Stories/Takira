using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using Takira.Objects;

namespace Takira.Handlers
{
    // TODO: Сделать отлов ошибок
    public static class SerializationHandler
    {
        public static void Save(Dictionary<string, QuestPage> story)
        {
            var json = JsonConvert.SerializeObject(story, Formatting.Indented);
            if (!Directory.Exists("Export"))
                Directory.CreateDirectory("Export");
            Stream stream = new FileStream(Directory.GetCurrentDirectory() + "/Export/" + story.ElementAt(0).Key + ".json", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(json);
            stream.Close();
        }
        
        public static Dictionary<string, QuestPage> Load(string filename)
        {
            // TODO: Добавить загрузку файла через проводник и реализовать импорт JSON.
            throw new NotImplementedException("Не реализовано");
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Directory.GetCurrentDirectory() + "/Import/" + filename + ".cqt", FileMode.Open, FileAccess.Read);
            Dictionary<string, QuestPage> story = (Dictionary<string,QuestPage>)formatter.Deserialize(stream);
            stream.Close();
            return story;
        }
    }
}