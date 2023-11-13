namespace TicketManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TicketReplies",
                c => new
                    {
                        ReplyId = c.Int(nullable: false, identity: true),
                        TicketId = c.Int(nullable: false),
                        Text = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ReplyId)
                .ForeignKey("dbo.Tickets", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.TicketId);
            
            AddColumn("dbo.Tickets", "TicketAttachment", c => c.String());
            AddColumn("dbo.Tickets", "TicketStatus", c => c.String());
            AddColumn("dbo.Tickets", "TicketDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketReplies", "TicketId", "dbo.Tickets");
            DropIndex("dbo.TicketReplies", new[] { "TicketId" });
            DropColumn("dbo.Tickets", "TicketDate");
            DropColumn("dbo.Tickets", "TicketStatus");
            DropColumn("dbo.Tickets", "TicketAttachment");
            DropTable("dbo.TicketReplies");
        }
    }
}
