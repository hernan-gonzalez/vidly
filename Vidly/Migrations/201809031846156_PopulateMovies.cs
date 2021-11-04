namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMovies : DbMigration
    {
        public override void Up()
        {

            Sql("INSERT INTO MOVIES (Name,ReleaseDate,DateAdded,NumberInStock,Genre_Id)VALUES('Hangover','2009/11/06','2018/09/03',5,1)");
            Sql("INSERT INTO MOVIES (Name,ReleaseDate,DateAdded,NumberInStock,Genre_Id)VALUES('Die Hard','1988/07/15','2018/09/03',4,2)");
            Sql("INSERT INTO MOVIES (Name,ReleaseDate,DateAdded,NumberInStock,Genre_Id)VALUES('Terminator','1984/10/26','2018/09/03',3,2)");
            Sql("INSERT INTO MOVIES (Name,ReleaseDate,DateAdded,NumberInStock,Genre_Id)VALUES('Toy Story','1995/11/22','2018/09/03',2,3)");
            Sql("INSERT INTO MOVIES (Name,ReleaseDate,DateAdded,NumberInStock,Genre_Id)VALUES('Titanic','1997/12/19','2018/09/03',1,4)");
        }
        
        public override void Down()
        {
        }
    }
}
