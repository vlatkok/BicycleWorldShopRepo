namespace BicycleWorldShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CartId = c.String(),
                        ProductId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category_Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        Feature = c.String(),
                        Video = c.String(),
                        PartNumber = c.String(),
                        Color = c.String(),
                        Availability = c.String(),
                        Brand = c.String(nullable: false, maxLength: 100),
                        Size = c.String(),
                        Gender = c.String(maxLength: 6),
                        LastUpdated = c.DateTime(precision: 7, storeType: "datetime2"),
                        Category_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id1)
                .Index(t => t.Category_Id1);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45),
                        SubCategoryName = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderedProducts",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        CustomerOrderId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.CustomerOrderId })
                .ForeignKey("dbo.CustomerOrders", t => t.CustomerOrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CustomerOrderId);
            
            CreateTable(
                "dbo.CustomerOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 160),
                        LastName = c.String(nullable: false, maxLength: 160),
                        Address = c.String(nullable: false, maxLength: 70),
                        City = c.String(nullable: false, maxLength: 40),
                        State = c.String(nullable: false, maxLength: 40),
                        PostalCode = c.String(nullable: false, maxLength: 10),
                        Country = c.String(nullable: false, maxLength: 40),
                        Phone = c.String(nullable: false, maxLength: 24),
                        Email = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerUserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Headline = c.String(nullable: false, maxLength: 100),
                        Rating = c.Int(nullable: false),
                        Pros = c.String(),
                        Cons = c.String(),
                        BestUses = c.String(),
                        DescribeYourself = c.String(),
                        CyclingStyle = c.String(),
                        Gift = c.String(),
                        Comments = c.String(nullable: false, maxLength: 1000),
                        BottomLine = c.String(),
                        Nickname = c.String(nullable: false, maxLength: 60),
                        Location = c.String(nullable: false, maxLength: 100),
                        Email = c.String(),
                        DatePosted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Reviews", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderedProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderedProducts", "CustomerOrderId", "dbo.CustomerOrders");
            DropForeignKey("dbo.Products", "Category_Id1", "dbo.Categories");
            DropIndex("dbo.Reviews", new[] { "ProductId" });
            DropIndex("dbo.OrderedProducts", new[] { "CustomerOrderId" });
            DropIndex("dbo.OrderedProducts", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "Category_Id1" });
            DropIndex("dbo.Carts", new[] { "ProductId" });
            DropTable("dbo.Customers");
            DropTable("dbo.Reviews");
            DropTable("dbo.CustomerOrders");
            DropTable("dbo.OrderedProducts");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Carts");
        }
    }
}
