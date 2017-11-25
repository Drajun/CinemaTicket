namespace CinemaTicket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieModels",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        type = c.String(),
                        name = c.String(),
                        poster = c.String(),
                        area = c.String(),
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
