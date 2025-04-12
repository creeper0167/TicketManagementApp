namespace TicketManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLastDateTimeToReply : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "LastReplyDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "LastReplyDateTime");
        }
    }
}
