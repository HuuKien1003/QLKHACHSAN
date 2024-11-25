namespace QLKHACHSANWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aa : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DatPhongs", "PhieuDatPhongId", "dbo.PhieuDatPhongs");
            DropIndex("dbo.DatPhongs", new[] { "PhieuDatPhongId" });
            DropTable("dbo.DatPhongs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DatPhongs",
                c => new
                    {
                        DatPhongId = c.Int(nullable: false, identity: true),
                        PhieuDatPhongId = c.Int(nullable: false),
                        NgayDat = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DatPhongId);
            
            CreateIndex("dbo.DatPhongs", "PhieuDatPhongId");
            AddForeignKey("dbo.DatPhongs", "PhieuDatPhongId", "dbo.PhieuDatPhongs", "PhieuDatPhongId", cascadeDelete: true);
        }
    }
}
