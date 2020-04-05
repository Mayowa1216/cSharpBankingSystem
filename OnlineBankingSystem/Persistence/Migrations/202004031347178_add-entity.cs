namespace OnlineBankingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addentity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActNo = c.String(),
                        AcctName = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransLimit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        user_Id = c.String(maxLength: 128),
                        Transaction_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.user_Id)
                .ForeignKey("dbo.Transactions", t => t.Transaction_Id)
                .Index(t => t.user_Id)
                .Index(t => t.Transaction_Id);
            
            CreateTable(
                "dbo.AccountTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountTypeName = c.String(),
                        accounts_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.accounts_Id)
                .Index(t => t.accounts_Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionName = c.String(),
                        TansactionDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TransactionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionTypeName = c.String(),
                        Transaction_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Transactions", t => t.Transaction_Id)
                .Index(t => t.Transaction_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionTypes", "Transaction_Id", "dbo.Transactions");
            DropForeignKey("dbo.Accounts", "Transaction_Id", "dbo.Transactions");
            DropForeignKey("dbo.Accounts", "user_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AccountTypes", "accounts_Id", "dbo.Accounts");
            DropIndex("dbo.TransactionTypes", new[] { "Transaction_Id" });
            DropIndex("dbo.AccountTypes", new[] { "accounts_Id" });
            DropIndex("dbo.Accounts", new[] { "Transaction_Id" });
            DropIndex("dbo.Accounts", new[] { "user_Id" });
            DropTable("dbo.TransactionTypes");
            DropTable("dbo.Transactions");
            DropTable("dbo.AccountTypes");
            DropTable("dbo.Accounts");
        }
    }
}
