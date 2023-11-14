namespace TicketManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TicketGroups", "TicketGroup_TicketGroupID", "dbo.TicketGroups");
            DropIndex("dbo.TicketGroups", new[] { "TicketGroup_TicketGroupID" });
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountID = c.Int(nullable: false, identity: true),
                        UserGroupID = c.Int(nullable: false),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AccountID)
                .ForeignKey("dbo.UserGroups", t => t.UserGroupID, cascadeDelete: true)
                .Index(t => t.UserGroupID);
            
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
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        UserGroupID = c.Int(nullable: false, identity: true),
                        UserGroupTitle = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserGroupID);
            
            AddColumn("dbo.Tickets", "AccountID", c => c.Int(nullable: false));
            AddColumn("dbo.Tickets", "TicketAttachment", c => c.String());
            AddColumn("dbo.Tickets", "TicketStatus", c => c.String());
            AddColumn("dbo.Tickets", "TicketDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tickets", "UserGroup_UserGroupID", c => c.Int());
            CreateIndex("dbo.Tickets", "AccountID");
            CreateIndex("dbo.Tickets", "UserGroup_UserGroupID");
            AddForeignKey("dbo.Tickets", "AccountID", "dbo.Accounts", "AccountID", cascadeDelete: true);
            AddForeignKey("dbo.Tickets", "UserGroup_UserGroupID", "dbo.UserGroups", "UserGroupID");
            DropColumn("dbo.TicketGroups", "TicketGroup_TicketGroupID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TicketGroups", "TicketGroup_TicketGroupID", c => c.Int());
            DropForeignKey("dbo.Tickets", "UserGroup_UserGroupID", "dbo.UserGroups");
            DropForeignKey("dbo.Accounts", "UserGroupID", "dbo.UserGroups");
            DropForeignKey("dbo.TicketReplies", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.Tickets", "AccountID", "dbo.Accounts");
            DropIndex("dbo.TicketReplies", new[] { "TicketId" });
            DropIndex("dbo.Tickets", new[] { "UserGroup_UserGroupID" });
            DropIndex("dbo.Tickets", new[] { "AccountID" });
            DropIndex("dbo.Accounts", new[] { "UserGroupID" });
            DropColumn("dbo.Tickets", "UserGroup_UserGroupID");
            DropColumn("dbo.Tickets", "TicketDate");
            DropColumn("dbo.Tickets", "TicketStatus");
            DropColumn("dbo.Tickets", "TicketAttachment");
            DropColumn("dbo.Tickets", "AccountID");
            DropTable("dbo.UserGroups");
            DropTable("dbo.TicketReplies");
            DropTable("dbo.Accounts");
            CreateIndex("dbo.TicketGroups", "TicketGroup_TicketGroupID");
            AddForeignKey("dbo.TicketGroups", "TicketGroup_TicketGroupID", "dbo.TicketGroups", "TicketGroupID");
        }
    }
}
