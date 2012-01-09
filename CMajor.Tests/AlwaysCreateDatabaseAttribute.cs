using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using System.Data.Entity;
using CMajor.Models;

namespace CMajor.Tests {
    internal class AlwaysCreateDatabaseAttribute : BeforeAfterTestAttribute {
        public override void Before(System.Reflection.MethodInfo methodUnderTest) {
            base.Before(methodUnderTest);

            InitDatabase();
        }
                
        private static void InitDatabase() {
            Database.SetInitializer<CMajorDbContext>(new DropCreateDatabaseAlways<CMajorDbContext>());

            using (var context = new CMajorDbContext()) {
                context.Database.Initialize(force: true);
            }
        }
    }
}
