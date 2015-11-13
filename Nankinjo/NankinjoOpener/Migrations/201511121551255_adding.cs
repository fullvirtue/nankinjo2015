namespace NankinjoOpener.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adding : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KeyInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KeyCode = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        UpdateDateTime = c.DateTime(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        CreateUser = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KeyLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KeyInfoId = c.Int(nullable: false),
                        KeyId = c.String(),
                        UserName = c.String(),
                        Status = c.Int(nullable: false),
                        Place = c.String(),
                        UpdateDateTime = c.DateTime(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        CreateUser = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KeyInfoes", t => t.KeyInfoId, cascadeDelete: true)
                .Index(t => t.KeyInfoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KeyLogs", "KeyInfoId", "dbo.KeyInfoes");
            DropIndex("dbo.KeyLogs", new[] { "KeyInfoId" });
            DropTable("dbo.KeyLogs");
            DropTable("dbo.KeyInfoes");
        }
    }
}
