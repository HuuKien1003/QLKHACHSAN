namespace QLKHACHSANWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adasdas : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PhieuDatPhongs", "DichVuId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PhieuDatPhongs", "DichVuId", c => c.Int(nullable: false));
        }
    }
}
