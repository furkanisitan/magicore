using Core.DataAccess.EntityFramework;
using NUnit.Framework;

namespace Core.Tests.DataAccess.EntityFramework
{
    [TestFixture]
    public class EfRepositoryTests
    {
        [SetUp]
        public void SetUp()
        {
            using var context = new InMemoryDbContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }


        [Test]
        public void FindAll_DoesNotReturnEmptyList()
        {
            var repository = new CategoryRepository();
            var list = repository.FindAll();

            Assert.IsNotEmpty(list);
        }

        [Test]
        public void Find_DoesNotReturnNull_IdIs1()
        {
            var repository = new CategoryRepository();
            var entity = repository.Find(x => x.Id == 1);

            Assert.IsNotNull(entity);
        }

        [Test]
        public void Find_NavigationPropertyOfReturnedEntityIsNotNull_PassNavigationPropertyParameter()
        {
            var repository = new CategoryRepository();
            var entity = repository.Find(x => x.Id == 1, nameof(Category.Products));

            Assert.IsNotNull(entity.Products);
        }

        [Test]
        public void Add_EntityIdIsNot0()
        {
            var repository = new CategoryRepository();
            var entity = new Category { Name = "NewCategory" };
            repository.Add(entity);

            Assert.NotZero(entity.Id);
        }

        [Test]
        public void IsPropertiesModified_CategoryNameIsChanged()
        {
            var repository = new CategoryRepository();
            var entity = new Category { Id = 1, Name = "ModifiedCategory" };
            var result = repository.IsPropertiesModified(entity, nameof(Category.Name));

            Assert.True(result);
        }
    }


    internal class CategoryRepository : EfRepository<Category, InMemoryDbContext, int>
    {
    }

}
