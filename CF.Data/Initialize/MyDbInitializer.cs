using CF.Data.Initialize.Default;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Data
{
    //internal class MyDbInitializer : DropCreateDatabaseAlways<MyDbContext>
    //internal class MyDbInitializer : CreateDatabaseIfNotExists<MyDbContext>
    //internal class MyDbInitializer : DropCreateDatabaseIfModelChanges<MyDbContext>
    internal class MyDbInitializer : CreateDatabaseIfNotExists<MyDbContext>
    {
        protected override void Seed(MyDbContext context)
        {
            base.Seed(context);
        }

        private void SetUpDefaltDb(MyDbContext context)
        {
            DefaultInitializer.Setup(context);
        }
    }
}
