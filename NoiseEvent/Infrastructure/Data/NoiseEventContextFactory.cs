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

                // localDb
               // var connectionString = "Server=(localdb)\\mssqllocaldb;Database=NoiseEvent;Trusted_Connection=True;Integrated Security=True;MultipleActiveResultSets=true";
                var connectionString = "Data Source=tcp:noiseeventsqlserver.database.windows.net,1433;Initial Catalog=NoiseEventDb;User Id=NoiseEventAdmin@noiseeventsqlserver.database.windows.net;Password=Gsts65shw@6%7;";
                optionsBuilder.UseSqlServer(connectionString);

                return new NoiseEventContext(optionsBuilder.Options);
            }
        }


    }
}
