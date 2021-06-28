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
    public partial class WindowChange : UserControl
    {

        private Int64 id;

        public WindowChange()
        {
            InitializeComponent();
        }
        

        public WindowChange(Int64 id) {
            InitializeComponent();
            loadDBObject(id);
            this.id = id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (checkInput()) {
                Boolean lendOutBool;
                if (lentOut.SelectedIndex == 0) {
                    lendOutBool = true;
                }
                else {
                    lendOutBool = false;
                }

                saveDatabase(author.Text, comment.Text, description.Text, Genre.Text, ISBN.Text, lendOutBool , lentOutTo.Text, float.Parse(Price.Text), datePicker.SelectedDate.Value.Date, title.Text);
                try{
                    Data.Library.LesenID(this.id).Loeschen();
                }catch (Exception) {
                   //
                }
                (Application.Current.MainWindow as WindowMain).fillList();
                (Window.GetWindow(this)).Close();
            }
        }


        private void loadDBObject(Int64 id) {
            dbManager manager = new dbManager(id);

            title.Text = manager.getTitle();
            ISBN.Text = manager.getISBN();
            author.Text = manager.getAuthor();
            datePicker.SelectedDate = manager.getPublishDate();
            Price.Text = manager.getPrice().ToString();
            Genre.Text = manager.getGenre();
            if (manager.getIsLendOut() == true)
            {
                lentOut.SelectedIndex = 0;
            }else{
                lentOut.SelectedIndex = 1;
            }
            lentOutTo.Text = manager.getLendTo();
            description.Text = manager.getDescription();
            comment.Text = manager.getComment();


        }

        private void saveDatabase(String author, String comment, String description, String genre, String ISBN, Boolean lendOut, String lendTo, float price, DateTime publishDate, String title) {
            Data.Library book = new Data.Library();

            book.Autor = author;
            book.Comment = comment;
            book.Description = description;
            book.Genre = genre;
            book.ISBN = ISBN;
            book.LendOut = lendOut;
            if (lendOut == true)
            {
                book.LendTo = lendTo;
            }else {
                book.LendTo = "-";
            }
            book.Price = price;
            book.PublishDate = publishDate;
            book.Titel = title;

            Int64 bookID = book.Erstellen();
        }



       
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private Boolean checkInput() {
            System.Text.RegularExpressions.Regex regTitle = new System.Text.RegularExpressions.Regex(@"[a-zA-Z0-9_\s]{5,30}");
            System.Text.RegularExpressions.Regex regISBN = new System.Text.RegularExpressions.Regex("^(?:ISBN(?:-10)?:? )?(?=[0-9X]{10}$|(?=(?:[0-9]+[- ]){3})[- 0-9X]{13}$)[0-9]{1,5}[- ]?[0-9]+[- ]?[0-9]+[- ]?[0-9X]$");
            System.Text.RegularExpressions.Regex regAuthor = new System.Text.RegularExpressions.Regex(@"[a-zA-Z_\s]{5,50}");
            System.Text.RegularExpressions.Regex regPrice = new System.Text.RegularExpressions.Regex(@"^(\d+\.\d{1,2})$"); //Example: 19123123,12
            System.Text.RegularExpressions.Regex regGenre = new System.Text.RegularExpressions.Regex(@"[a-zA-Z_\s]{2,20}");
            System.Text.RegularExpressions.Regex regLentOutTo = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z_\s]{2,30}$");
            System.Text.RegularExpressions.Regex regDescription = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9.\s]{0,200}$");
            System.Text.RegularExpressions.Regex regComment = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9.\s]{0,400}$");

            Boolean check = true;

            check = checkRegex(title, regTitle, "Only letters and numbers. \n 5-30 characters.");
            check = checkRegex(ISBN,regISBN, "The ISBN of your book \n Example: 0-596-52068-9");
            check = checkRegex(author, regAuthor, "Only letters and numbers. \n 5-50 characters.");
            check = checkRegex(Genre, regGenre, "Only letters and numbers. \n 2-20 characters.");
            check = checkRegex(description,regDescription , "Only letters and numbers. \n Max. 200 characters.");
            if (lentOut.SelectedIndex == 0) {
                check = checkRegex(lentOutTo, regLentOutTo, "Only letters and numbers. \n 2-30 characters.");
            }
            if (datePicker.SelectedDate == null) {
                showDatepickerMessage("Please select a date");
                check = false;
            }
            check = checkRegex(comment,regComment , "Only letters and numbers. \n Max. 400 characters.");
            check = checkRegex(Price, regPrice, "Only numbers and a point. \n Example: 399.99");

            return check;
        }

        private Boolean checkRegex(TextBox t, System.Text.RegularExpressions.Regex r, String errorMessage) {
            if (!r.Match(t.Text).Success)
            {
                ToolTip tooltip = new ToolTip { Content = errorMessage };
                t.Background = Brushes.Red;
                t.Foreground = Brushes.White;
                tooltip.PlacementTarget = t;
                tooltip.Placement = System.Windows.Controls.Primitives.PlacementMode.Right;
                tooltip.IsOpen = true;
                if(t.ToolTip == null) {
                    t.ToolTip = tooltip;
                }
                return false;
            }
            else {
                t.Background = Brushes.White;
                t.Foreground = Brushes.Black;
                t.ToolTip = null;
                return true;
            }
        }

        private void showDatepickerMessage(String errorMessage) {
            ToolTip toolTip = new ToolTip { Content = errorMessage };
            toolTip.PlacementTarget = datePicker;
            toolTip.Placement = System.Windows.Controls.Primitives.PlacementMode.Right;
            toolTip.IsOpen = true;
            datePicker.ToolTip = toolTip;
        }
        
        //This is what happens when you click cancel
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel? None of your input will be saved.", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
                (Window.GetWindow(this)).Close();
            }
           
            
        }

    }
}
