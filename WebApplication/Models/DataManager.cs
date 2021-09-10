using Microsoft.Data.Sqlite;
using System;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using WebApplication.Helpers;
using WebApplication.Models;

namespace WebApplication
{
    public class DataManager
    {
        public Data data;
        public DataManager(string fileName)
        {
            this.data = Load(fileName);
        }

        public Data Load(string fileName)
        {
            string jsonString = File.ReadAllText(fileName, Encoding.UTF8);
            Data data = JsonSerializer.Deserialize<Data>(jsonString);
            return data;
        }

        public void Save(Data data, string fileName)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(data, options);
            File.WriteAllText(fileName, jsonString, Encoding.UTF8);
        }

        public void SaveToDB(Data data, string nameDB)
        {
            initializiteDB(nameDB);
            InsertData(data, nameDB);

        }

        public void initializiteDB(string nameDB)
        {
            if (File.Exists("Files\\DB.db"))
                File.Copy("Files\\DB.db", nameDB);
            else 
            using (var connection = new SqliteConnection("Data Source="+ nameDB))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = 
                "CREATE TABLE `Data` ( `Id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, `FileName` TEXT NOT NULL, `Discription` TEXT, `Version` REAL ); " +
                "CREATE TABLE `Element` ( `Id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, `Element` TEXT NOT NULL, `ElementInfo` TEXT, `IsElement` INTEGER, `DataId` INTEGER ) ";


                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);

                        Console.WriteLine($"Hello, {name}!");
                    }
                }
            }
        }

        public void InsertData(Data data , string nameDB)
        {
            using (var connection = new SqliteConnection("Data Source="+ nameDB))
            {
                connection.Open();

                var command = connection.CreateCommand();
                StringBuilder commandTextBuilder = new StringBuilder();
                foreach (var element in data.ElementsList)
                {
                    commandTextBuilder.Append($"INSERT INTO Element (" +
                        $"{nameof(element.Element)}, " +
                        $"{nameof(element.ElementInfo)}, " +
                        $"{nameof(element.IsElement)}) " +
                        $"VALUES (" +
                        $"\"{element.Element}\", " +
                        $"\"{element.ElementInfo}\", " +
                        $"{BooleanHelper.BoolToInt(element.IsElement)});");
                }
                command.CommandText = commandTextBuilder.ToString();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);

                        Console.WriteLine($"Hello, {name}!");
                    }
                }
            }

        }

        
    }
}
