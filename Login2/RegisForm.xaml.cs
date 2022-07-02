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
    /// RegisForm.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RegisForm : Window
    {
        MainWindow mainwin;
        public RegisForm()
        {
            InitializeComponent();
        }

        private void RegisButton_Click(object sender, RoutedEventArgs e)
        {
            string strcpy = "INSERT INTO base(id, pwd, name, birth, email)";
            strcpy += "VALUES ('" + this.dataId.Text + "', '" + this.dataPw.Password + "', '" + this.name.Text + "', '" + this.birth.Text + "', '" + this.email.Text + "')";
            this.mainwin.usingSQL(strcpy);
            mainwin.Show();
            this.Close();
        }
    }
}
