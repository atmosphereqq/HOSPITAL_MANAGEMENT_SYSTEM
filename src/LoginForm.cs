using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;



namespace 图书管理系统
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // 获取账号
            string userName = txtId.Text.Trim();
            // 获取密码
            string Pwd = txtPwd.Text.Trim();

            if (userName == "" || Pwd == "")
            {
                MessageBox.Show("请输入账号或密码");
            }
            else
            {
                MySqlConnection myconn = null;
                MySqlCommand mycom = null;
                MySqlDataAdapter myrec = null;
                myconn = new MySqlConnection("Host =localhost;Database=library;Username=root;Password=java@163");
                myconn.Open();
                mycom = myconn.CreateCommand();
                mycom.CommandText = "SELECT * FROM user where ename='" + userName + "' and password='" + Pwd + "'LIMIT 1";
                MySqlDataAdapter adap = new MySqlDataAdapter(mycom);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                MySqlDataReader sdr = mycom.ExecuteReader();
                Boolean isAssess = sdr.HasRows;
                if (!isAssess)
                {
                    MessageBox.Show("该用户不存在！", "登录失败");
                }
                else
                {
                    // 登录窗体隐藏
                    this.Hide();
                    // 创建主窗体
                    MainForm mainForm = new MainForm();
                    // 将账号传给主窗体MainForm
                    mainForm.Id = userName;
                    // 用主窗体MainForm下的_Tag标记登陆的是用户还是管理员
                    if (rBtn1.Checked == true)
                    {
                        mainForm._Tag = "user";
                        //mainForm.Name1 = name;

                        //sql = "select uBan from users where uId='" + userName + "' and uPwd='" + Pwd + "'";
                        //cmd = new SqlCommand(sql, conn);
                        //string b = cmd.ExecuteScalar().ToString();
                        //mainForm.B = b;
                    }
                    else
                    {
                        mainForm._Tag = "admin";
                        //mainForm.Name1 = name;
                    }
                    // 显示主窗体
                    mainForm.ShowDialog();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
