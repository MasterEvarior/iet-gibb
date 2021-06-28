using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace M120Projekt
{
    class dbManager
    {
        private Int64 id;
        private Data.Library book;
        public dbManager(Int64 id) {
            this.id = id;
            this.book = Data.Library.LesenID(id);
        }

        public String getAuthor() {
            return book.Autor;
        }

        public void updateAuthor(String author){
            book.Autor = author;
            book.Aktualisieren();
        }

        public String getTitle() {
            return book.Titel;
        }

        public void updateTitle(String title) {
            book.Titel = title;
            book.Aktualisieren();
        }

        public String getDescription(){
            return book.Description;
        }

        public void updateDescription(String description){
            book.Description = description;
            book.Aktualisieren();
        }

        public String getComment() {
            return book.Comment;
        }

        public void updateComment(String comment){
            book.Comment = comment;
            book.Aktualisieren();
        }

        public float getPrice(){
            return (float)book.Price;
        }

        public void updatePrice(float price) {
            book.Price = price;
            book.Aktualisieren();
        }

        public String getGenre() {
            return book.Genre;
        }

        public void updateGenre(String genre) {
            book.Genre = genre;
            book.Aktualisieren();
        }

        public String getLendTo() {
            return book.LendTo;
        }

        public void updateLendTo(String lendTo) {
            book.LendTo = lendTo;
            book.Aktualisieren();
        }

        public DateTime getPublishDate() {
            return book.PublishDate;
        }

        public void updatePublishDate(DateTime date) {
            book.PublishDate = date;
            book.Aktualisieren();
        }

        public Boolean getIsLendOut() {
            return book.LendOut;
        }

        public void updateIsLendOut(Boolean isLendOut) {
            book.LendOut = isLendOut;
            book.Aktualisieren();
        }

        public String getISBN() {
            return book.ISBN;
        }

        public void updateISBN(String ISBN) {
            book.ISBN = ISBN;
            book.Aktualisieren();
        }

        public void deleteBook(Int64 id) {
            if (MessageBox.Show("Are you sure you want to delete \"" + getTitle() + "\"? This action can not be reversed", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Data.Library.LesenID(id).Loeschen();
            }
        }

        public void deleteBookWithoutQuestion(Int64 id) {
            Data.Library.LesenID(id).Loeschen();
        }
    }
}
