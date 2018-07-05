using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using ApplicationCore.Entities;

namespace Infrastructure.Data
{    
    public class NoiseEventContext : DbContext
    {
        // use built in logging feature to capture SQL commands to console
        public static readonly LoggerFactory MyConsoleLoggerFactory
            = new LoggerFactory(new[]
            {
                new ConsoleLoggerProvider((category,level)
                => category == DbLoggerCategory.Database.Command.Name  // only show SQL commands
                && level == LogLevel.Information, true)
            });


        public NoiseEventContext(DbContextOptions<NoiseEventContext> options)
            : base(options)
        { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyConsoleLoggerFactory);

            //var connectionString = "Server = (localdb)\\mssqllocaldb; Database = NoiseEvent; Trusted_Connection = True";
            //optionsBuilder.UseSqlServer(connectionString);
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // many to many relationship


            //modelBuilder.Entity<NoiseEventEntity>()  // DisplayResource joins Displays and Resources
            //    .HasKey(s => new
            //    {
            //        s.DisplayId,
            //        s.ResourceId
            //    });


        }


        #region DbSets

        // DbSet represents a SQL Table

        public DbSet<NoiseEventEntity> NoiseEvent { get; set; }

        // Rules and Resources can be associated with any Display, so many to many relationship. The join tables handle this.
        //public DbSet<RuleEntity> Rule { get; set; }   // 1 to 1 relationship DisplayRule -> Rule
        //public DbSet<ResourceEntity> Resource { get; set; }   // 1 to many relationship DisplayResources -> Resource

        //// join tables
        //public DbSet<DisplayResourceEntity> DisplayResource { get; set; }   // 1 to many relationship DisplayEntity -> DisplayResource
        //public DbSet<DisplayRuleEntity> DisplayRule { get; set; }   // 1 to many relationship DisplayEntity -> DisplayRule


        #endregion
    }
}
