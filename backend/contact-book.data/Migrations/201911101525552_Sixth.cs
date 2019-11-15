namespace contact_book.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sixth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Address", "Contact_Id", "dbo.Contact");
            DropForeignKey("dbo.Email", "Contact_Id", "dbo.Contact");
            DropForeignKey("dbo.PhoneNumber", "Contact_Id", "dbo.Contact");
            DropIndex("dbo.Address", new[] { "Contact_Id" });
            DropIndex("dbo.Email", new[] { "Contact_Id" });
            DropIndex("dbo.PhoneNumber", new[] { "Contact_Id" });
            RenameColumn(table: "dbo.Address", name: "Contact_Id", newName: "ContactId");
            RenameColumn(table: "dbo.Email", name: "Contact_Id", newName: "ContactId");
            RenameColumn(table: "dbo.PhoneNumber", name: "Contact_Id", newName: "ContactId");
            AlterColumn("dbo.Address", "ContactId", c => c.Int(nullable: true));
            AlterColumn("dbo.Email", "ContactId", c => c.Int(nullable: true));
            AlterColumn("dbo.PhoneNumber", "ContactId", c => c.Int(nullable: true));
            CreateIndex("dbo.Address", "ContactId");
            CreateIndex("dbo.Email", "ContactId");
            CreateIndex("dbo.PhoneNumber", "ContactId");
            AddForeignKey("dbo.Address", "ContactId", "dbo.Contact", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Email", "ContactId", "dbo.Contact", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PhoneNumber", "ContactId", "dbo.Contact", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhoneNumber", "ContactId", "dbo.Contact");
            DropForeignKey("dbo.Email", "ContactId", "dbo.Contact");
            DropForeignKey("dbo.Address", "ContactId", "dbo.Contact");
            DropIndex("dbo.PhoneNumber", new[] { "ContactId" });
            DropIndex("dbo.Email", new[] { "ContactId" });
            DropIndex("dbo.Address", new[] { "ContactId" });
            AlterColumn("dbo.PhoneNumber", "ContactId", c => c.Int());
            AlterColumn("dbo.Email", "ContactId", c => c.Int());
            AlterColumn("dbo.Address", "ContactId", c => c.Int());
            RenameColumn(table: "dbo.PhoneNumber", name: "ContactId", newName: "Contact_Id");
            RenameColumn(table: "dbo.Email", name: "ContactId", newName: "Contact_Id");
            RenameColumn(table: "dbo.Address", name: "ContactId", newName: "Contact_Id");
            CreateIndex("dbo.PhoneNumber", "Contact_Id");
            CreateIndex("dbo.Email", "Contact_Id");
            CreateIndex("dbo.Address", "Contact_Id");
            AddForeignKey("dbo.PhoneNumber", "Contact_Id", "dbo.Contact", "Id");
            AddForeignKey("dbo.Email", "Contact_Id", "dbo.Contact", "Id");
            AddForeignKey("dbo.Address", "Contact_Id", "dbo.Contact", "Id");
        }
    }
}
