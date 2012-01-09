using System.Data.Entity;
using CMajor.Infrastructure;

namespace CMajor.Models {
    public class CMajorDbContext : DbContext, IUnitOfWork {

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Album> Albums { get; set; }

        public int Commit() {
            return this.SaveChanges();
        }

    }
}