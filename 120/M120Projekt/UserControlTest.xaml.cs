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
    /// Interaktionslogik für UserControlTest.xaml
    /// </summary>
    public partial class UserControlTest : Window
    {
        public UserControlTest(){
            InitializeComponent();
        }

        public UserControlTest(Int64 id){
            InitializeComponent();
            this.Content = new WindowChange(id);
            this.Show();
        }
        
        
    }
}
