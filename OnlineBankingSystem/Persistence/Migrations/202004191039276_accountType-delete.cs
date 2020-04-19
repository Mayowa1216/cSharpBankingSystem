namespace OnlineBankingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accountTypedelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccountTypes", "isDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccountTypes", "isDelete");
        }
    }
}
