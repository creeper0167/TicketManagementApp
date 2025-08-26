namespace TicketManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changingTicketSubject : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tickets", "TicketSubject", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickets", "TicketSubject", c => c.String(nullable: false));
        }
    }
}
