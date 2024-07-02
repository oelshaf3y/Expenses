using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Windows;

namespace Expenses
{
    class DB
    {
        private const string DbFileName = "DB.json";
        private static readonly Lazy<DB> instance = new Lazy<DB>(() => new DB());
        public static DB Instance => instance.Value;
        public List<Category> categories { get; set; } = new List<Category>();
        public List<User> users { get; set; } = new List<User>();

        public User currentUser { get; set; }
        private DB() { }

        [JsonConstructor]
        private DB(List<Category> categories, List<User> users,User currentUser)
        {
            this.categories = categories;
            this.users = users;
            this.currentUser = currentUser;
        }

        public async void Reload()
        {
            if (File.Exists(DbFileName))
            {
                //JsonSerializerOptions options = new JsonSerializerOptions
                //{
                //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                //    IgnoreNullValues = true,
                //    PropertyNameCaseInsensitive = true,
                //    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
                //};
                string jsonString = await File.ReadAllTextAsync(DbFileName, Encoding.UTF8);
                var loadedDB = JsonSerializer.Deserialize<DB>(jsonString);
                categories = loadedDB.categories;
                users = loadedDB.users;
                currentUser = loadedDB.currentUser;
            }
        }

        public async Task sync()
        {
            string jsonString;
            JsonWriterOptions opt = new JsonWriterOptions
            {
                Indented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            using (var stream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(stream, opt))
                {
                    JsonSerializer.Serialize(writer, this);
                }
                jsonString = Encoding.UTF8.GetString(stream.ToArray());
            }
            await File.WriteAllTextAsync(DbFileName, jsonString, Encoding.UTF8);
        }

        public static List<Category> flatten(List<Category> CATS)
        {
            List<Category> flattenList = new List<Category>();

            void flattenSub(Category cat)
            {
                flattenList.Add(cat);
                foreach (Category child in cat.children)
                {
                    flattenSub(child);
                }
            }
            foreach (Category child in CATS)
            {
                flattenSub(child);
            }
            return flattenList;
        }
    }
}
