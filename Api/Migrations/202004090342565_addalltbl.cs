namespace Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addalltbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TB_M_Department",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateDate = c.DateTimeOffset(precision: 7),
                        DeleteDate = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TB_M_Division",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nama = c.String(),
                        DepartmentId = c.Int(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTimeOffset(precision: 7),
                        DeleteDate = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TB_M_Department", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TB_M_Division", "DepartmentId", "dbo.TB_M_Department");
            DropIndex("dbo.TB_M_Division", new[] { "DepartmentId" });
            DropTable("dbo.TB_M_Division");
            DropTable("dbo.TB_M_Department");
        }
    }
}
