namespace QLKHACHSANWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CTDichVus",
                c => new
                    {
                        CTDichVuId = c.Int(nullable: false, identity: true),
                        DichVuId = c.Int(nullable: false),
                        PhieuDatPhongId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CTDichVuId)
                .ForeignKey("dbo.DichVus", t => t.DichVuId, cascadeDelete: true)
                .ForeignKey("dbo.PhieuDatPhongs", t => t.PhieuDatPhongId, cascadeDelete: true)
                .Index(t => t.DichVuId)
                .Index(t => t.PhieuDatPhongId);
            
            CreateTable(
                "dbo.DichVus",
                c => new
                    {
                        DichVuId = c.Int(nullable: false, identity: true),
                        DichVuName = c.String(),
                        Gia = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.DichVuId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CTDichVus", "PhieuDatPhongId", "dbo.PhieuDatPhongs");
            DropForeignKey("dbo.CTDichVus", "DichVuId", "dbo.DichVus");
            DropIndex("dbo.CTDichVus", new[] { "PhieuDatPhongId" });
            DropIndex("dbo.CTDichVus", new[] { "DichVuId" });
            DropTable("dbo.DichVus");
            DropTable("dbo.CTDichVus");
        }
    }
}
