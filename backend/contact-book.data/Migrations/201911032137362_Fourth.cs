namespace contact_book.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fourth : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contact", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Contact", "LastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contact", "LastName", c => c.String());
            AlterColumn("dbo.Contact", "FirstName", c => c.String());
        }
    }
}
