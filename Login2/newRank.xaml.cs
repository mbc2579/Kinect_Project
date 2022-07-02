using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// newRank.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class newRank : Window
    {
        public newRank(int count, string id)
        {

            InitializeComponent();
            Int32 rec = reSQL("select record from base where id = '" + id + "'");
            if (count > rec)
            {
                usingSQL("update base set record = " + count + " where id = '" + id + "'");
                System.Windows.MessageBox.Show("신기록 달성");
            }
            oldRank rankpage1 = new oldRank();
            rankpage1.Show();
            Thread.Sleep(100);
            this.Close();
        }
        public void usingSQL(string sql)
        {
            string mysqldb = "Server = 14.37.24.166; Port = 3306; Database = kinect; Uid = root; Pwd = rootpw";
            MySqlConnection conn = new MySqlConnection(mysqldb);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();

            cmd.ExecuteNonQuery();


            conn.Close();
            conn.Dispose();
        }
        public Int32 reSQL(string sql)
        {
            string mysqldb = "Server = 14.37.24.166; Port = 3306; Database = kinect; Uid = root; Pwd = rootpw";
            MySqlConnection conn = new MySqlConnection(mysqldb);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader table = cmd.ExecuteReader();



            if (table.Read())
            {
                return table.GetInt32(0);

            }
            else return 0;

            conn.Close();
            conn.Dispose();
        }
    }
}
