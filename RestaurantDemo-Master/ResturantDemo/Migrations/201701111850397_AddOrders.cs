namespace ResturantDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        timeCreated = c.DateTime(nullable: false),
                        isFulfilled = c.Boolean(nullable: false),
                        userId = c.String(maxLength: 128),
                        destination = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.userId)
                .Index(t => t.userId);
            
            AddColumn("dbo.MenuItems", "Orders_Id", c => c.Int());
            CreateIndex("dbo.MenuItems", "Orders_Id");
            AddForeignKey("dbo.MenuItems", "Orders_Id", "dbo.Orders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MenuItems", "Orders_Id", "dbo.Orders");
            DropIndex("dbo.Orders", new[] { "userId" });
            DropIndex("dbo.MenuItems", new[] { "Orders_Id" });
            DropColumn("dbo.MenuItems", "Orders_Id");
            DropTable("dbo.Orders");
        }
    }
}
