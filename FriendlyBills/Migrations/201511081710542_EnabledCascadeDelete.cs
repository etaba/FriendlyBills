namespace FriendlyBills.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnabledCascadeDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroupMembership", "GroupID", "dbo.Group");
            DropForeignKey("dbo.GroupMembership", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transaction", "GroupID", "dbo.Group");
            DropForeignKey("dbo.Transaction", "SubmitterID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transaction", "TargetUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.GroupMembership", new[] { "UserID" });
            DropIndex("dbo.Transaction", new[] { "SubmitterID" });
            DropIndex("dbo.Transaction", new[] { "TargetUser_Id" });
            AlterColumn("dbo.GroupMembership", "UserID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Transaction", "SubmitterID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Transaction", "TargetUser_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.GroupMembership", "UserID");
            CreateIndex("dbo.Transaction", "SubmitterID");
            CreateIndex("dbo.Transaction", "TargetUser_Id");
            AddForeignKey("dbo.GroupMembership", "GroupID", "dbo.Group", "ID", cascadeDelete: true);
            AddForeignKey("dbo.GroupMembership", "UserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Transaction", "GroupID", "dbo.Group", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Transaction", "SubmitterID", "dbo.AspNetUsers", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Transaction", "TargetUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: false);
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Transaction", "TargetUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transaction", "SubmitterID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transaction", "GroupID", "dbo.Group");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupMembership", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupMembership", "GroupID", "dbo.Group");
            DropIndex("dbo.Transaction", new[] { "TargetUser_Id" });
            DropIndex("dbo.Transaction", new[] { "SubmitterID" });
            DropIndex("dbo.GroupMembership", new[] { "UserID" });
            AlterColumn("dbo.Transaction", "TargetUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Transaction", "SubmitterID", c => c.String(maxLength: 128));
            AlterColumn("dbo.GroupMembership", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Transaction", "TargetUser_Id");
            CreateIndex("dbo.Transaction", "SubmitterID");
            CreateIndex("dbo.GroupMembership", "UserID");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id");
            AddForeignKey("dbo.Transaction", "TargetUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Transaction", "SubmitterID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Transaction", "GroupID", "dbo.Group", "ID");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.GroupMembership", "UserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.GroupMembership", "GroupID", "dbo.Group", "ID");
        }
    }
}
