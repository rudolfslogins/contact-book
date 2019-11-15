namespace contact_book.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seventh : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Address", "ContactId", "dbo.Contact");
            DropForeignKey("dbo.Email", "ContactId", "dbo.Contact");
            DropForeignKey("dbo.PhoneNumber", "ContactId", "dbo.Contact");
            DropIndex("dbo.Address", new[] { "ContactId" });
            DropIndex("dbo.Email", new[] { "ContactId" });
            DropIndex("dbo.PhoneNumber", new[] { "ContactId" });
            RenameColumn(table: "dbo.Address", name: "ContactId", newName: "Contact_Id");
            RenameColumn(table: "dbo.Email", name: "ContactId", newName: "Contact_Id");
            RenameColumn(table: "dbo.PhoneNumber", name: "ContactId", newName: "Contact_Id");
            AlterColumn("dbo.Address", "Contact_Id", c => c.Int());
            AlterColumn("dbo.Email", "Contact_Id", c => c.Int());
            AlterColumn("dbo.PhoneNumber", "Contact_Id", c => c.Int());
            CreateIndex("dbo.Address", "Contact_Id");
            CreateIndex("dbo.Email", "Contact_Id");
            CreateIndex("dbo.PhoneNumber", "Contact_Id");
            AddForeignKey("dbo.Address", "Contact_Id", "dbo.Contact", "Id");
            AddForeignKey("dbo.Email", "Contact_Id", "dbo.Contact", "Id");
            AddForeignKey("dbo.PhoneNumber", "Contact_Id", "dbo.Contact", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhoneNumber", "Contact_Id", "dbo.Contact");
            DropForeignKey("dbo.Email", "Contact_Id", "dbo.Contact");
            DropForeignKey("dbo.Address", "Contact_Id", "dbo.Contact");
            DropIndex("dbo.PhoneNumber", new[] { "Contact_Id" });
            DropIndex("dbo.Email", new[] { "Contact_Id" });
            DropIndex("dbo.Address", new[] { "Contact_Id" });
            AlterColumn("dbo.PhoneNumber", "Contact_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Email", "Contact_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Address", "Contact_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.PhoneNumber", name: "Contact_Id", newName: "ContactId");
            RenameColumn(table: "dbo.Email", name: "Contact_Id", newName: "ContactId");
            RenameColumn(table: "dbo.Address", name: "Contact_Id", newName: "ContactId");
            CreateIndex("dbo.PhoneNumber", "ContactId");
            CreateIndex("dbo.Email", "ContactId");
            CreateIndex("dbo.Address", "ContactId");
            AddForeignKey("dbo.PhoneNumber", "ContactId", "dbo.Contact", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Email", "ContactId", "dbo.Contact", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Address", "ContactId", "dbo.Contact", "Id", cascadeDelete: true);
        }
    }
}
