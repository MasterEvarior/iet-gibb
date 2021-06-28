using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace M120Projekt
{
    /// <summary>
    /// Interaktionslogik für Window2.xaml
    /// </summary>
    public partial class WindowLib : Window
    {
        Int64 id;
        public WindowLib(Int64 id)
        {
            InitializeComponent();
            this.id = id;
            loadDB(this.id);
        }

        private void loadDB(Int64 id) {
            dbManager manager = new dbManager(id);
            Title.Content = manager.getTitle();
            ISBN.Content = manager.getISBN();
            Author.Content = manager.getAuthor();
            PublishDate.Content = manager.getPublishDate().ToShortDateString().ToString();
            Price.Content = manager.getPrice().ToString();
            Genre.Content = manager.getGenre();
            LendOut.Content = showLendOut(manager.getIsLendOut());
            LendOutTo.Content = manager.getLendTo();
            Description.Text = manager.getDescription();
            Comment.Text = manager.getComment();
        }

        private String showLendOut(Boolean lendOut) {
            if (lendOut){
                return "Yes";
            }
            else{
                return "No";
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e){
            dbManager manager = new dbManager(id);
            manager.deleteBook(id);
            (Application.Current.MainWindow as WindowMain).fillList();
            this.Close();
        }

        private void Change_Click(object sender, RoutedEventArgs e){

            UserControlTest test = new UserControlTest(id);
            this.Close();
        }
    }
}
