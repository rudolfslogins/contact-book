namespace contact_book.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ninth : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contact", "BirthDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contact", "BirthDate", c => c.DateTime(storeType: "date"));
        }
    }
}
