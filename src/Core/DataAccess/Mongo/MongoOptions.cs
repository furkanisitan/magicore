using System.Collections.Generic;

namespace Core.DataAccess.Mongo
{
    public class MongoOptions
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public Dictionary<string, string> CollectionNames { get; set; }
    }
}
