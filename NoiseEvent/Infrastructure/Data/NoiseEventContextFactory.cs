using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data
{
    public class NoiseEventContextContextFactory
    {

        // This class allows EF migrations to work despite the obfuscation of the connection string via secrets.  
        //  replace the userId and password with correct values (change from '****') in the connection string below, perform the migration command from a cmd window, then UNDO the changes to this file (or hide the credentials again somewhow).  
        // DO NOT check in source control.
        // can't find the values?  check user secrets or set a breakpoint on the line in startup.cs with this code fragment  'options.UseSqlServer(connectionString));'
        // 
        // This article is the source of the code below.
        // https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation
        //
        // see this article for general guidance about EF migrations:  https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/
        //
        // in admin cmd window, navigate to Infrastructure project folder:
        //      \InformationDisplays\Infrastructure>
        // perform this command:
        //      dotnet ef migrations add DisplayEntity
        // then:
        //      dotnet ef database update


        public class NoiseEventContextFactory : Microsoft.EntityFrameworkCore.Design.IDesignTimeDbContextFactory<NoiseEventContext>
        {
            public NoiseEventContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<NoiseEventContext>();

                // this is for a 'test' db on Azure
                var connectionString = "Data Source=ace-xids-poc.database.windows.net;Initial Catalog=ACE-XIDS-POC;Persist Security Info=True;User ID=Xids;Password=Station32";


                // this should be the 'real' DB on azure - (at least for TEST env)  - this value was copied from AppService ConnectionStrings property
                //var connectionString = "Data Source=infodisplay-test-sqlsvr-1.database.windows.net;Initial Catalog=infodisplay-test-sqldb-1;Persist Security Info=True;User ID=infodisplay-test-user;Password=****;";

                optionsBuilder.UseSqlServer(connectionString);

                return new NoiseEventContext(optionsBuilder.Options);
            }
        }


    }
}
