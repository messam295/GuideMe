namespace GuideMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedingServices : DbMigration
    {
        public override void Up()
        {
            Sql("insert into dbo.Services values (1,'Parking')," +
                "(2,'Wireless internet')," +
                "(3,'Smoking area')," +
                "(4,'Wheelchair Accessible')," +
                "(5,'Reservations')," +
                "(6,'Accepts Credit Cards')," +
                "(7,'Bike Parking')");
        }
        
        public override void Down()
        {
        }
    }
}
