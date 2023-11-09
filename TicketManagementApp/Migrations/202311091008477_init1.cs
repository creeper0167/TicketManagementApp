namespace TicketManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "TicketAttachment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "TicketAttachment");
        }
    }
}
