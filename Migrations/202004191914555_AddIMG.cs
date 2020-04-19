namespace GuideMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIMG : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cities", "Img", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cities", "Img");
        }
    }
}
