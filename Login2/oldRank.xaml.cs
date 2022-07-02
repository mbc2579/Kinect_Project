using MySql.Data.MySqlClient;
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
    /// oldRank.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class oldRank : Window
    {
        public oldRank()
        {
            InitializeComponent();
            MySqlDataReader ranklist = reSQL("select name, record from base where record > 0 order by record desc");
            string Totext = "성함\t\t\t기록\n\n";
            while (ranklist.Read())
            {
                Totext += ranklist.GetString(0);
                Totext += "\t\t\t";
                Totext += ranklist.GetInt32(1) + "\n";
            }
            rankbox.Text = Totext;
        }
        public MySqlDataReader reSQL(string sql)
        {
            string mysqldb = "Server = 14.37.24.166; Port = 3306; Database = kinect; Uid = root; Pwd = rootpw";
            MySqlConnection conn = new MySqlConnection(mysqldb);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader table = cmd.ExecuteReader();



            return table;

            conn.Close();
            conn.Dispose();
        }
    }
}
