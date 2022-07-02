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

namespace Login2
{
    /// <summary>
    /// menu.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class menu : Window
    {
        String id;
        public menu(String id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Game game = new Game(id);
            game.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            oldRank rankpage = new oldRank();
            rankpage.Show();
            this.Close();
        }
    }
}
