namespace Core.Tests.DataAccess.EntityFramework
{
    internal class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public virtual Category Category { get; set; }
    }
}
