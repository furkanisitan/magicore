using Core.Entities;
using System.Collections.Generic;

namespace Core.Tests.DataAccess.EntityFramework
{
    internal class Category : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
