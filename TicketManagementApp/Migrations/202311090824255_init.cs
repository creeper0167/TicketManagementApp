namespace TicketManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TicketGroups",
                c => new
                    {
                        TicketGroupID = c.Int(nullable: false, identity: true),
                        TicketGroupTitle = c.String(),
                        TicketGroup_TicketGroupID = c.Int(),
                    })
                .PrimaryKey(t => t.TicketGroupID)
                .ForeignKey("dbo.TicketGroups", t => t.TicketGroup_TicketGroupID)
                .Index(t => t.TicketGroup_TicketGroupID);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketID = c.Int(nullable: false, identity: true),
                        TicketGroupID = c.Int(nullable: false),
                        TicketSubject = c.String(),
                        TicketDescription = c.String(),
                    })
                .PrimaryKey(t => t.TicketID)
                .ForeignKey("dbo.TicketGroups", t => t.TicketGroupID, cascadeDelete: true)
                .Index(t => t.TicketGroupID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "TicketGroupID", "dbo.TicketGroups");
            DropForeignKey("dbo.TicketGroups", "TicketGroup_TicketGroupID", "dbo.TicketGroups");
            DropIndex("dbo.Tickets", new[] { "TicketGroupID" });
            DropIndex("dbo.TicketGroups", new[] { "TicketGroup_TicketGroupID" });
            DropTable("dbo.Tickets");
            DropTable("dbo.TicketGroups");
        }
    }
}
