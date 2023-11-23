namespace TicketManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
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
                        FullName = c.String(),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.AccountID)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .ForeignKey("dbo.UserGroups", t => t.UserGroupID, cascadeDelete: true)
                .Index(t => t.UserGroupID)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TicketReplies",
                c => new
                    {
                        ReplyId = c.Int(nullable: false, identity: true),
                        TicketID = c.Int(nullable: false),
                        AccountID = c.Int(),
                        Text = c.String(nullable: false),
                        TicketDate = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ReplyId)
                .ForeignKey("dbo.Tickets", t => t.TicketID, cascadeDelete: true)
                .Index(t => t.TicketID);
            
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
            DropForeignKey("dbo.TicketReplies", "TicketID", "dbo.Tickets");
            DropForeignKey("dbo.Tickets", "AccountID", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "Role_Id", "dbo.Roles");
            DropIndex("dbo.TicketReplies", new[] { "TicketID" });
            DropIndex("dbo.Tickets", new[] { "UserGroup_UserGroupID" });
            DropIndex("dbo.Tickets", new[] { "AccountID" });
            DropIndex("dbo.Accounts", new[] { "Role_Id" });
            DropIndex("dbo.Accounts", new[] { "UserGroupID" });
            DropColumn("dbo.Tickets", "UserGroup_UserGroupID");
            DropColumn("dbo.Tickets", "TicketDate");
            DropColumn("dbo.Tickets", "TicketStatus");
            DropColumn("dbo.Tickets", "TicketAttachment");
            DropColumn("dbo.Tickets", "AccountID");
            DropTable("dbo.UserGroups");
            DropTable("dbo.TicketReplies");
            DropTable("dbo.Roles");
            DropTable("dbo.Accounts");
            CreateIndex("dbo.TicketGroups", "TicketGroup_TicketGroupID");
            AddForeignKey("dbo.TicketGroups", "TicketGroup_TicketGroupID", "dbo.TicketGroups", "TicketGroupID");
        }
    }
}
