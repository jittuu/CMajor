using System.Data.Entity;

namespace CMajor.Models {
#if DEBUG
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<CMajorDbContext> {
#else
    public class DatabaseInitializer : CreateDatabaseIfNotExists<CMajorDbContext> {
#endif
        protected override void Seed(CMajorDbContext context) {
            base.Seed(context);
        }
    }
}