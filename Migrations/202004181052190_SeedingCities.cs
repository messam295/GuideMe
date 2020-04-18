namespace GuideMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedingCities : DbMigration
    {
        public override void Up()
        {

            Sql("insert into dbo.Cities values (1,'Cairo'),(2,'Alexandria'),(3,'Luxor'),(4,'Aswan'),(5,'Minia'),(6,'Hurgada'),(7,'Sinai'),(8,'Sharm EL-Sheik')");
        }
        
        public override void Down()
        {
        }
    }
}
