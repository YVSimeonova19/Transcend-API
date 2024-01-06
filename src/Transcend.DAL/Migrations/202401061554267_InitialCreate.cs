namespace Transcend.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carriers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.String(),
                        ExpectedDeliveryDate = c.DateTime(nullable: false),
                        CarrierId = c.Int(nullable: false),
                        UserPlaceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carriers", t => t.CarrierId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserPlaceId, cascadeDelete: true)
                .Index(t => t.CarrierId)
                .Index(t => t.UserPlaceId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        PasswordHash = c.String(),
                        Email = c.String(),
                        CarrierId = c.Int(nullable: false),
                        UserDetailsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Carriers", t => t.CarrierId, cascadeDelete: true)
                .ForeignKey("dbo.UserDetails", t => t.UserDetailsId, cascadeDelete: true)
                .Index(t => t.CarrierId)
                .Index(t => t.UserDetailsId);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        ShippingAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserPlaceId", "dbo.Users");
            DropForeignKey("dbo.Users", "UserDetailsId", "dbo.UserDetails");
            DropForeignKey("dbo.Users", "CarrierId", "dbo.Carriers");
            DropForeignKey("dbo.Orders", "CarrierId", "dbo.Carriers");
            DropIndex("dbo.Users", new[] { "UserDetailsId" });
            DropIndex("dbo.Users", new[] { "CarrierId" });
            DropIndex("dbo.Orders", new[] { "UserPlaceId" });
            DropIndex("dbo.Orders", new[] { "CarrierId" });
            DropTable("dbo.UserDetails");
            DropTable("dbo.Users");
            DropTable("dbo.Orders");
            DropTable("dbo.Carriers");
        }
    }
}
