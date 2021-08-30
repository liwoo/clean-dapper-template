using System;
using FluentMigrator;

namespace Infrastructure.Persistence.Migrations
{
    public class AutoVersioningMigrationAttribute : FluentMigrator.MigrationAttribute
    {
        public AutoVersioningMigrationAttribute(long version, string description) : base(CalculateVersion(),
            description)
        {
        }

        private static long CalculateVersion()
        {
            var baseDate = new DateTime(1970, 01, 01);
            var now =  DateTime.Now;
            return (long) now.Subtract(baseDate).TotalSeconds;
        }
    }
}