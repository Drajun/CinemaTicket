namespace CinemaTicket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        type = c.String(maxLength: 40),
                        name = c.String(maxLength: 40),
                        poster = c.String(nullable: false),
                        area = c.String(nullable: false),
                        releaseTime = c.DateTime(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        remarks = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MovieModels");
        }
    }
}
