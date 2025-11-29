using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace MyApp.DAL
{
   
    public class EntityContext
    {
        public string DbPath { get; }

        public EntityContext(string dbPath)
        {
            DbPath = dbPath;
        }

        public void CreateDatabase()
        {
            
            var defaults = new List<Vector>
            {
                new Vector("red", 3, 4),
                new Vector("green", -1, 2),
                new Vector("blue", 0, -5)
            };
            Update(defaults);
        }

        public List<Vector> Open()
        {
            if (!File.Exists(DbPath))
                return new List<Vector>();
            var xs = new XmlSerializer(typeof(List<Vector>));
            using var fs = File.OpenRead(DbPath);
            return (List<Vector>)xs.Deserialize(fs);
        }

        public void Update(List<Vector> items)
        {
            var xs = new XmlSerializer(typeof(List<Vector>));
            using var fs = File.Create(DbPath);
            xs.Serialize(fs, items);
        }

        public void Close()
        {
           
        }

        public void DeleteDatabase()
        {
            if (File.Exists(DbPath)) File.Delete(DbPath);
        }
    }
}

