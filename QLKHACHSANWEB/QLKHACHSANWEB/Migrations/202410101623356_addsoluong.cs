namespace QLKHACHSANWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsoluong : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChiTietPhieuDatPhongs", "SoLuong", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChiTietPhieuDatPhongs", "SoLuong");
        }
    }
}
