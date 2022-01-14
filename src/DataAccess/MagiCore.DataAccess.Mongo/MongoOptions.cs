namespace MagiCore.DataAccess.Mongo;

public class MongoOptions
{
    public const string Mongo = "MongoOptions";

    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public Dictionary<string, string> CollectionNames { get; set; }

    public MongoOptions()
    {
        ConnectionString = string.Empty;
        DatabaseName = string.Empty;
        CollectionNames = new Dictionary<string, string>();
    }
}