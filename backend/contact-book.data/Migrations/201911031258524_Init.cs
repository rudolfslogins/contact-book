namespace contact_book.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullAddress = c.String(),
                        AddressType_Id = c.Int(),
                        Contact_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Type", t => t.AddressType_Id)
                .ForeignKey("dbo.Contact", t => t.Contact_Id)
                .Index(t => t.AddressType_Id)
                .Index(t => t.Contact_Id);
            
            CreateTable(
                "dbo.Type",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Company = c.String(),
                        Notes = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Email",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmailName = c.String(),
                        EmailType_Id = c.Int(),
                        Contact_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Type", t => t.EmailType_Id)
                .ForeignKey("dbo.Contact", t => t.Contact_Id)
                .Index(t => t.EmailType_Id)
                .Index(t => t.Contact_Id);
            
            CreateTable(
                "dbo.PhoneNumber",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Prefix = c.String(),
                        Number = c.String(),
                        PhoneNumberType_Id = c.Int(),
                        Contact_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Type", t => t.PhoneNumberType_Id)
                .ForeignKey("dbo.Contact", t => t.Contact_Id)
                .Index(t => t.PhoneNumberType_Id)
                .Index(t => t.Contact_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhoneNumber", "Contact_Id", "dbo.Contact");
            DropForeignKey("dbo.PhoneNumber", "PhoneNumberType_Id", "dbo.Type");
            DropForeignKey("dbo.Email", "Contact_Id", "dbo.Contact");
            DropForeignKey("dbo.Email", "EmailType_Id", "dbo.Type");
            DropForeignKey("dbo.Address", "Contact_Id", "dbo.Contact");
            DropForeignKey("dbo.Address", "AddressType_Id", "dbo.Type");
            DropIndex("dbo.PhoneNumber", new[] { "Contact_Id" });
            DropIndex("dbo.PhoneNumber", new[] { "PhoneNumberType_Id" });
            DropIndex("dbo.Email", new[] { "Contact_Id" });
            DropIndex("dbo.Email", new[] { "EmailType_Id" });
            DropIndex("dbo.Address", new[] { "Contact_Id" });
            DropIndex("dbo.Address", new[] { "AddressType_Id" });
            DropTable("dbo.PhoneNumber");
            DropTable("dbo.Email");
            DropTable("dbo.Contact");
            DropTable("dbo.Type");
            DropTable("dbo.Address");
        }
    }
}
