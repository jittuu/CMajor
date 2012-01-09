using System.Data.Entity;
using CMajor.Infrastructure;

namespace CMajor.Models {
    public class CMajorDbContext : DbContext, IUnitOfWork {

        public DbSet<CMajor.Models.Artist> Artists { get; set; }

        public int Commit() {
            return this.SaveChanges();
        }
    }
}