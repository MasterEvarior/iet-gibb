using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace M120Projekt
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class WindowMain : Window
    {
        public WindowMain()
        {
            InitializeComponent();
            
            
            
            // Wichtig!
            Data.Global.context = new Data.Context();
            // Aufruf diverse APIDemo Methoden
            /*
            APIDemo.LibraryCreate();
            APIDemo.LibraryCreateKurz();
            APIDemo.LibraryRead();
            APIDemo.LibaryUpdate();
            APIDemo.LibraryRead();
            */
            //APIDemo.LibraryDelete();
            
            
            
            
            datagrid.CanUserAddRows = false;
            datagrid.CanUserAddRows = false;
            datagrid.CanUserDeleteRows = false;
            datagrid.CanUserSortColumns = true;
            datagrid.IsReadOnly = true;
            datagrid.AutoGenerateColumns = false;

            //setDictionary();

            fillList();

           
        }

        private void addNewBook(object sender, RoutedEventArgs e){

            UserControlTest wintest = new UserControlTest();
            wintest.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        public void fillList() {
            var entities = Data.Global.context.Library.ToList();
            //var entities = Data.Global.context.Library.SqlQuery("SELECT BookID, Titel, Picture FROM dbo.Library").ToList();
            entities = entities.Where(x => $"{x.Titel}".ToLower().Contains(searchBar.Text.ToLower())).ToList();
            datagrid.ItemsSource = entities;
        }

        public void look(object sender, RoutedEventArgs e){
            WindowLib libwindows = new WindowLib(getID(sender));
            libwindows.Show();
        }

        public void delete(object sender, RoutedEventArgs e){

            Int64 id = getID(sender);

            dbManager manager = new dbManager(id);

            if (MessageBox.Show("Are you sure you want to delete \"" + manager.getTitle() + "\"? This action can not be reversed", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Data.Library.LesenID(id).Loeschen();
                fillList();
            }
        }

        public void change(object sender, RoutedEventArgs e){
            UserControlTest change = new UserControlTest(getID(sender));
        }

        
        private Int64 getID(object sender) {
            return Int64.Parse(((Button)sender).Tag.ToString());
        }

        private void WatermarkTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            fillList();
        }

        private void setDictionary() {
            ResourceDictionary dict = new ResourceDictionary();
            dict.Source = new Uri("pack://application:,,,/ReferencedAssembly;component/Resources/Languages/DictionaryEnglish.xaml", UriKind.RelativeOrAbsolute);
            this.Resources.MergedDictionaries.Add(dict);
        }
    }
}
