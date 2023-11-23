namespace TicketManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
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
                "dbo.TicketReplies",
                c => new
                    {
                        ReplyId = c.Int(nullable: false, identity: true),
                        TicketID = c.Int(nullable: false),
                        AccountID = c.Int(),
                        Text = c.String(nullable: false),
                        ReplyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReplyId)
                .ForeignKey("dbo.Accounts", t => t.AccountID)
                .ForeignKey("dbo.Tickets", t => t.TicketID, cascadeDelete: true)
                .Index(t => t.TicketID)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketID = c.Int(nullable: false, identity: true),
                        TicketGroupID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        TicketSubject = c.String(),
                        TicketDescription = c.String(),
                        TicketAttachment = c.String(),
                        TicketStatus = c.String(),
                        TicketDate = c.DateTime(nullable: false),
                        UserGroup_UserGroupID = c.Int(),
                    })
                .PrimaryKey(t => t.TicketID)
                .ForeignKey("dbo.Accounts", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("dbo.TicketGroups", t => t.TicketGroupID, cascadeDelete: true)
                .ForeignKey("dbo.UserGroups", t => t.UserGroup_UserGroupID)
                .Index(t => t.TicketGroupID)
                .Index(t => t.AccountID)
                .Index(t => t.UserGroup_UserGroupID);
            
            CreateTable(
                "dbo.TicketGroups",
                c => new
                    {
                        TicketGroupID = c.Int(nullable: false, identity: true),
                        TicketGroupTitle = c.String(),
                    })
                .PrimaryKey(t => t.TicketGroupID);
            
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
                "dbo.UserGroups",
                c => new
                    {
                        UserGroupID = c.Int(nullable: false, identity: true),
                        UserGroupTitle = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserGroupID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "UserGroup_UserGroupID", "dbo.UserGroups");
            DropForeignKey("dbo.Accounts", "UserGroupID", "dbo.UserGroups");
            DropForeignKey("dbo.Accounts", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.TicketReplies", "TicketID", "dbo.Tickets");
            DropForeignKey("dbo.Tickets", "TicketGroupID", "dbo.TicketGroups");
            DropForeignKey("dbo.Tickets", "AccountID", "dbo.Accounts");
            DropForeignKey("dbo.TicketReplies", "AccountID", "dbo.Accounts");
            DropIndex("dbo.Tickets", new[] { "UserGroup_UserGroupID" });
            DropIndex("dbo.Tickets", new[] { "AccountID" });
            DropIndex("dbo.Tickets", new[] { "TicketGroupID" });
            DropIndex("dbo.TicketReplies", new[] { "AccountID" });
            DropIndex("dbo.TicketReplies", new[] { "TicketID" });
            DropIndex("dbo.Accounts", new[] { "Role_Id" });
            DropIndex("dbo.Accounts", new[] { "UserGroupID" });
            DropTable("dbo.UserGroups");
            DropTable("dbo.Roles");
            DropTable("dbo.TicketGroups");
            DropTable("dbo.Tickets");
            DropTable("dbo.TicketReplies");
            DropTable("dbo.Accounts");
        }
    }
}
