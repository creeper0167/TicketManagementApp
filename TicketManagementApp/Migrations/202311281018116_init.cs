﻿namespace TicketManagementApp.Migrations
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
                        DepartmentId = c.Int(nullable: false),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        FullName = c.String(),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.AccountID)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.UserGroups", t => t.UserGroupID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .Index(t => t.UserGroupID)
                .Index(t => t.DepartmentId)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentTitle = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketID = c.Int(nullable: false, identity: true),
                        UserGroupID = c.Int(),
                        TicketGroupID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        TicketSubject = c.String(),
                        TicketDescription = c.String(),
                        TicketAttachment = c.String(),
                        TicketStatus = c.String(),
                        TicketDate = c.DateTime(nullable: false),
                        TrackCode = c.String(),
                        DepartmentId = c.Int(),
                    })
                .PrimaryKey(t => t.TicketID)
                .ForeignKey("dbo.Accounts", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.TicketGroups", t => t.TicketGroupID, cascadeDelete: true)
                .ForeignKey("dbo.UserGroups", t => t.UserGroupID)
                .Index(t => t.UserGroupID)
                .Index(t => t.TicketGroupID)
                .Index(t => t.AccountID)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.TicketGroups",
                c => new
                    {
                        TicketGroupID = c.Int(nullable: false, identity: true),
                        TicketGroupTitle = c.String(),
                    })
                .PrimaryKey(t => t.TicketGroupID);
            
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
                "dbo.UserGroups",
                c => new
                    {
                        UserGroupID = c.Int(nullable: false, identity: true),
                        UserGroupTitle = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserGroupID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Tickets", "UserGroupID", "dbo.UserGroups");
            DropForeignKey("dbo.Accounts", "UserGroupID", "dbo.UserGroups");
            DropForeignKey("dbo.TicketReplies", "TicketID", "dbo.Tickets");
            DropForeignKey("dbo.TicketReplies", "AccountID", "dbo.Accounts");
            DropForeignKey("dbo.Tickets", "TicketGroupID", "dbo.TicketGroups");
            DropForeignKey("dbo.Tickets", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Tickets", "AccountID", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.TicketReplies", new[] { "AccountID" });
            DropIndex("dbo.TicketReplies", new[] { "TicketID" });
            DropIndex("dbo.Tickets", new[] { "DepartmentId" });
            DropIndex("dbo.Tickets", new[] { "AccountID" });
            DropIndex("dbo.Tickets", new[] { "TicketGroupID" });
            DropIndex("dbo.Tickets", new[] { "UserGroupID" });
            DropIndex("dbo.Accounts", new[] { "Role_Id" });
            DropIndex("dbo.Accounts", new[] { "DepartmentId" });
            DropIndex("dbo.Accounts", new[] { "UserGroupID" });
            DropTable("dbo.Roles");
            DropTable("dbo.UserGroups");
            DropTable("dbo.TicketReplies");
            DropTable("dbo.TicketGroups");
            DropTable("dbo.Tickets");
            DropTable("dbo.Departments");
            DropTable("dbo.Accounts");
        }
    }
}