using System;
using System.Collections.Generic;
using System.Data;
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
using MySql.Data.MySqlClient;


namespace Login2
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        
        //Game game;
        public MainWindow()
        {
            InitializeComponent();
            

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkId(this.dataId.Text) && checkPW(this.dataPw.Password))
            {
                //form2.myPicture.Image = Image.FromFile(form2.getPicture());
                //form2.textId.Text = this.dataId.Text;
                string username = checkName(this.dataId.Text);
                MessageBox.Show(username + "님 환영합니다");
                changeToGame();
            }
            else
            {
                MessageBox.Show("올바른 아이디 또는 패스워드가 아닙니다.");
            }
        }
        public bool checkId(string id) //id check
        {
            string mysqldb = "Server = 14.37.24.166; Port = 3306; Database = kinect; Uid = root; Pwd = rootpw";
            MySqlConnection conn = new MySqlConnection(mysqldb);
            conn.Open();

            string sql = "select id from base where id=\'" + this.dataId.Text + "\'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;

            MySqlDataReader dr = cmd.ExecuteReader();
            bool result = false;
            if (dr.HasRows) result = true;
            else result = false;

            conn.Close();
            conn.Dispose();

            return result;
        }
        public bool checkPW(string pw) //pwd check
        {
            //string mysqldb = "Server = 14.37.24.166; Port = 3306; Database = kinect; Uid = root; Pwd = rootpw";
            string mysqldb = "Server = 14.37.24.166; Port = 3306; Database = kinect; Uid = root; Pwd = rootpw";
            MySqlConnection conn = new MySqlConnection(mysqldb);
            conn.Open();

            string sql = "select pwd from base where id=\'" + this.dataId.Text + "\'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;

            MySqlDataReader dr = cmd.ExecuteReader();

            bool result = false;
            dr.Read();
            if (pw == dr.GetString(0)) result = true;
            else result = false;


            conn.Close();
            conn.Dispose();

            return result;
        }

        public void changeToGame()
        {

            menu menupage = new menu(this.dataId.Text);
            menupage.Show();
            this.Close();
        }

        public void usingSQL(string sql)
        {
            string mysqldb = "Server = 14.37.24.166; Port = 3306; Database = kinect; Uid = root; Pwd = rootpw";
            MySqlConnection conn = new MySqlConnection(mysqldb);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;

            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Console.Write(dr[0].ToString() + "\t");
                Console.Write(dr.GetString(1).ToString() + "\n");
            }
            conn.Close();
            conn.Dispose();
        }

        private void RegisButton_Click(object sender, RoutedEventArgs e)
        {
            RegisForm regisform;
            regisform = new RegisForm();
            regisform.Show();
            this.Close();
        }

        public string checkName(string id)
        {
            string mysqldb = "Server = 14.37.24.166; Port = 3306; Database = kinect; Uid = root; Pwd = rootpw";
            MySqlConnection conn = new MySqlConnection(mysqldb);
            conn.Open();

            string sql = "select name from base where id='" + id + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;

            MySqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            string result = dr.GetString(0);
            conn.Close();
            conn.Dispose();

            return result;
        }
    }
}
