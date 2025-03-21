using Microsoft.EntityFrameworkCore;
using GoodsAPI.Models;

namespace GoodsAPI.Data
{
    public class GoodDbContext : DbContext
    {
        public GoodDbContext(DbContextOptions<GoodDbContext> options) : base(options) { }

        public DbSet<HangHoa> Goods { get; set; }
    }
}
