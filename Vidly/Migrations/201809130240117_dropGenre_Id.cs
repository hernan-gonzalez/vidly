namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropGenre_Id : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Movies", new[] { "Genre_Id" });
            DropForeignKey("dbo.Movies", "Genre_Id", "dbo.Genres");
           // AddForeignKey("dbo.Movies", "GenreId", "dbo.Genres", "Id");

            Sql("ALTER TABLE Movies DROP COLUMN Genre_Id");
            //CreateIndex("dbo.Movies", "GenreId");

        }
        
        public override void Down()
        {
        }
    }
}
