namespace SportsStore.Models
{
    public class FakeProductRepository : IProductRepository
    {
        public IQueryable<Product> Products => new List<Product>
        {
            new Product { Name = "Футбольный мяч", Price =25},
            new Product { Name = "Штанга", Price =179},
            new Product { Name = "Труселя", Price =95},
        }.AsQueryable<Product>();
    }
}
