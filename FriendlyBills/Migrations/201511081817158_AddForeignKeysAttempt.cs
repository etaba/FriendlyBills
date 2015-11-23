namespace FriendlyBills.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeysAttempt : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Transaction", name: "TargetID", newName: "TargetUserID");
            RenameIndex(table: "dbo.Transaction", name: "IX_TargetID", newName: "IX_TargetUserID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Transaction", name: "IX_TargetUserID", newName: "IX_TargetID");
            RenameColumn(table: "dbo.Transaction", name: "TargetUserID", newName: "TargetID");
        }
    }
}
