namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenres : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres Values('Comedy')");
            Sql("INSERT INTO Genres Values('Action')");
            Sql("INSERT INTO Genres Values('Family')");
            Sql("INSERT INTO Genres Values('Romance')");
        }
        
        public override void Down()
        {
        }
    }
}
