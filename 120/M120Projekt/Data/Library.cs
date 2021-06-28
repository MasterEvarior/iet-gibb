using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace M120Projekt.Data
{
    public class Library
    {
        #region Datenbankschicht
        [Key][Required]
        public Int64 BookID { get; set; }
        [Required]
        public String ISBN { get; set; }
        [Required]
        public String Autor { get; set; }
        [Required]
        public String Titel { get; set; }
        [Required]
        public String Description { get; set; }
        public String Comment { get; set; }
        public double Price { get; set; }
        public String Genre { get; set; }
        public String LendTo { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
        [Required]
        public Boolean LendOut { get; set; }
        public Byte[] Picture { get; set; }
        #endregion
        #region Applikationsschicht
        public Library() { }
        [NotMapped]
        public String BerechnetesAttribut
        {
            get
            {
                return "Im Getter kann Code eingefügt werden für berechnete Attribute";
            }
        }
        public static IEnumerable<Data.Library> LesenAlle()
        {
            return (from record in Data.Global.context.Library select record);
        }
        public static Data.Library LesenID(Int64 klasseAId)
        {
            return (from record in Data.Global.context.Library where record.BookID == klasseAId select record).FirstOrDefault();
        }
        public static IEnumerable<Data.Library> LesenAttributGleich(String suchbegriff)
        {
            return (from record in Data.Global.context.Library where record.Autor == suchbegriff select record);
        }
        public static IEnumerable<Data.Library> LesenAttributWie(String suchbegriff)
        {
            return (from record in Data.Global.context.Library where record.Autor.Contains(suchbegriff) select record);
        }
        public Int64 Erstellen()
        {
            if (this.Autor == null || this.Autor == "") this.Autor = "leer";
            if (this.PublishDate == null) this.PublishDate = DateTime.MinValue;
            Data.Global.context.Library.Add(this);
            Data.Global.context.SaveChanges();
            return this.BookID;
        }
        public Int64 Aktualisieren()
        {
            Data.Global.context.Entry(this).State = System.Data.Entity.EntityState.Modified;
            Data.Global.context.SaveChanges();
            return this.BookID;
        }
        public void Loeschen()
        {
            Data.Global.context.Entry(this).State = System.Data.Entity.EntityState.Deleted;
            Data.Global.context.SaveChanges();
        }
        public override string ToString()
        {
            return BookID.ToString(); // Für bessere Coded UI Test Erkennung
        }
        #endregion
    }
}
