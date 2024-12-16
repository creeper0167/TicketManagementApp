namespace TicketManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Phonenumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "Phonenumber");
        }
    }
}
