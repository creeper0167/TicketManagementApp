namespace TicketManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TicketReplies", "TicketDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TicketReplies", "TicketDate", c => c.String(nullable: false));
        }
    }
}
