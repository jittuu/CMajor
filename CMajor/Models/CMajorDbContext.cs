using System.Data.Entity;
using CMajor.Infrastructure;

namespace CMajor.Models {
    public class CMajorDbContext : DbContext, IUnitOfWork {

        public int Commit() {
            return this.SaveChanges();
        }
    }
}