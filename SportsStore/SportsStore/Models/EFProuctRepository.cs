namespace SportsStore.Models
{
    public class EFProuctRepository : IProductRepository
    {
        private ApplicationDbContext _context;

        public EFProuctRepository(ApplicationDbContext ctx) {
            _context = ctx;
        }
        public IQueryable<Product> Products => _context.Products;
    }
}
