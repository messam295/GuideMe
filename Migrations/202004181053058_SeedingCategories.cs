namespace GuideMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedingCategories : DbMigration
    {
        public override void Up()
        {

            Sql("insert into dbo.Categories values (1,'Hotel')," +
                "(2,'Restaurant')," +
                "(3,'Shopping')," +
                "(4,'Beauty')," +
                "(5,'Cinema')");
        }
        
        public override void Down()
        {
        }
    }
}
