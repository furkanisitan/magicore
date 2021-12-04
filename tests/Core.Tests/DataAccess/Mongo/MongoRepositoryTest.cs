using Core.DataAccess.Mongo;
using MongoDB.Driver;
using NUnit.Framework;
using System.Collections.Generic;

// NOTE: Up docker-compose.yml file with `docker-compose up -d` command before running tests in this class.

namespace Core.Tests.DataAccess.Mongo;

[TestFixture]
internal class MongoRepositoryTests
{
    private MongoOptions _options;

    [SetUp]
    public void SetUp()
    {
        _options = new MongoOptions
        {
            ConnectionString = "mongodb://localhost:27017",
            DatabaseName = "test_db",
            CollectionNames = new Dictionary<string, string> { { nameof(Category), "categories" } }
        };

        var client = new MongoClient(_options.ConnectionString);
        var database = client.GetDatabase(_options.DatabaseName);
        var categories = database.GetCollection<Category>(_options.CollectionNames[nameof(Category)]);

        client.DropDatabase(_options.DatabaseName);

        categories.InsertMany(new[]
        {
            new Category { Id = "61a693edf0e7ec8217dc42c8", Name = "C1" },
            new Category { Id = "61a69416437d52f7a585954c", Name = "C2" }
        });
    }

    [Test]
    public void FindAll_DoesNotReturnEmptyList()
    {
        var repository = new CategoryRepository(_options);

        var list = repository.FindAll();

        Assert.IsNotEmpty(list);
    }

    [Test]
    public void Find_DoesNotReturnNull_IdIsExists()
    {
        var repository = new CategoryRepository(_options);

        var entity = repository.Find(x => x.Id.Equals("61a693edf0e7ec8217dc42c8"));

        Assert.IsNotNull(entity);
    }

    [Test]
    public void Add_EntityIdIsNot0()
    {
        var repository = new CategoryRepository(_options);
        var entity = new Category { Name = "NewCategory" };

        repository.Add(entity);

        Assert.NotNull(entity.Id);
        Assert.IsNotEmpty(entity.Id);
    }

    private class CategoryRepository : MongoRepository<Category, string>
    {
        public CategoryRepository(MongoOptions options) : base(options, nameof(Category))
        {
        }
    }

}