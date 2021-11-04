namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7eb66dd6-a784-4cee-9bd3-de8c447f7f3e', N'guest@vidly.com', 0, N'ANs2tK/VaBDsO+wQ4l8Bv8GQv6NpLj4hC7E8rqeofnuKOIuVtkzNzChFU6thUaqpJQ==', N'3f33006f-85b6-491f-a83d-5f513d476114', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'f8772bbd-c8dd-4115-9cc9-83a63c8840a1', N'admin@vidly.com', 0, N'AAk2Ss+GxHjM2DfrzMM1cJyWfRUCJAy+3Kg02ar51efoTBZyul7iyTMWzmr2Yiv+DA==', N'1ff67d3e-8b28-41ad-b090-db5c14de159f', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'7d3ac3bb-9ba5-4b9d-85c7-db7f74eab6cb', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f8772bbd-c8dd-4115-9cc9-83a63c8840a1', N'7d3ac3bb-9ba5-4b9d-85c7-db7f74eab6cb')

");
        }
        
        public override void Down()
        {
        }
    }
}
