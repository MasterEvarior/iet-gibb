using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M120Projekt
{
    static class APIDemo
    {
        #region Library
        // Create
        public static void LibraryCreate()
        {
            Debug.Print("--- DemoACreate ---");
            // KlasseA
            Data.Library Library = new Data.Library();
            Library.PublishDate = DateTime.Today;
            Library.Picture = null;
            Library.Titel = "The Holy bibel";
            Library.Price = 399.99;
            Library.Comment = "Like Game of Thrones just with more violence";
            Library.Description = "Let me show you the wonderful world of imagination";
            Library.Autor = "God himself";
            Library.ISBN = "9781623370770";
            Library.LendOut = false;
            Library.Genre = "Fiction";
            Int64 klasseA1Id = Library.Erstellen();
            Debug.Print("Artikel erstellt mit Id:" + klasseA1Id);
        }
        public static void LibraryCreateKurz()
        {
            Data.Library Library = new Data.Library { Autor = "Nick Horbny", LendOut = true, PublishDate = DateTime.Parse("07/05/2005"), ISBN = "9788580869903", Description = "This is a very good book" , LendTo = "Thierry", Comment = "This comment is very interessting and original", Titel = "A Long Way Down", Genre = "Novel", Price = 19.99};
            Int64 klasseA2Id = Library.Erstellen();
            Debug.Print("Artikel erstellt mit Id:" + klasseA2Id);
        }

        // Read
        public static void LibraryRead()
        {
            Debug.Print("--- DemoARead ---");
            // Demo liest alle
            foreach (Data.Library Library in Data.Library.LesenAlle())
            {
                Debug.Print("Artikel Id:" + Library.BookID + " Name:" + Library.Autor);
            }
        }
        // Update
        public static void LibaryUpdate()
        {
            Debug.Print("--- DemoAUpdate ---");
            // KlasseA ändert Attribute
            Data.Library Library = Data.Library.LesenID(1);
            Library.Autor = "Artikel 1 nach Update";
            Library.Aktualisieren();
        }
        // Delete
        public static void LibraryDelete()
        {
            Debug.Print("--- DemoADelete ---");
            Data.Library.LesenID(1).Loeschen();
            Debug.Print("Artikel mit Id 1 gelöscht");
        }
        #endregion
    }
}
