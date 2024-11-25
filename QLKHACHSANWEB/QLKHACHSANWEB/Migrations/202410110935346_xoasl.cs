namespace QLKHACHSANWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xoasl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CTDichVus", "SoLuong", c => c.Int(nullable: false));
            DropColumn("dbo.ChiTietPhieuDatPhongs", "SoLuong");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChiTietPhieuDatPhongs", "SoLuong", c => c.Int(nullable: false));
            DropColumn("dbo.CTDichVus", "SoLuong");
        }
    }
}
