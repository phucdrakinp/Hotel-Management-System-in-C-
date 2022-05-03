
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagement
{
    public partial class Form1 : Form
    {
        function fn = new function();
        String query;
        Hash hash = new Hash();
        Rsa rsa = new Rsa();
        string publicKey = "<RSAKeyValue><Modulus>rfzlz5vARPlfFHSPfit9GoJ3uQ7u7pGZj9PIYQcJjM+600ZyOh1ofz+zCBx6RodcpYevk8Vb2PMk4Z1ePzghLpGep08yhASUspzyOtPwDdXqhX7Mf0ySdUF2ICpypkAu9/QYjvw1/fUm+aFGOAqqeFsJpXaDX6jl+SgPahwCrl0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        string privateKey = "<RSAKeyValue><Modulus>rfzlz5vARPlfFHSPfit9GoJ3uQ7u7pGZj9PIYQcJjM+600ZyOh1ofz+zCBx6RodcpYevk8Vb2PMk4Z1ePzghLpGep08yhASUspzyOtPwDdXqhX7Mf0ySdUF2ICpypkAu9/QYjvw1/fUm+aFGOAqqeFsJpXaDX6jl+SgPahwCrl0=</Modulus><Exponent>AQAB</Exponent><P>4LNv8IfQEaapvSk/6xW6BH9JZa0WqL3CoeVT9n4ySq8S2GYE9XmbFte28LK98eW+N8v7hhiCK8WWY4vb1cSPpw==</P><Q>xjkYPna3HuwWSav4/48Q2WHMcT5zBxAkGYxWTiZSUtHiXC735K627ELYtX4ZaWUVqX1w14s0SOBLRlY3FuMyWw==</Q><DP>dV8ldLXsiJvPBCEc4zZJIXo/o53DPUdJ+Hkq35HRwVMr+99mbbckvMzXIWmscEO6lbi2XLhGnoiqYrs2jLYM9w==</DP><DQ>XM6Gh1hVzGiE1uFpp114ag7cBXlTqc7o1/1YuyY+DQCvlrF25t7WTi/N/suXYj0tszlEB+bpB+Xb2IatLE4bWQ==</DQ><InverseQ>SGLSknLn0hzB9qCcCGLyk3UHRlut98wN2s5riNjmclUQODxgNr0x6ak0HbsRVnPiR+BzGgmyGG8hTB1EZIyolQ==</InverseQ><D>OYN/9EDoLeTBKWHejTaTBFBcgzAMi5BV0tWPR4OsBIAmofCHke5mvKmx5NyFDwtv9MgFojN7SRwW9P2wSfWkAdUTTHa4uLrcafR1YkxcNKcJd39nPcm0r+hdURvGKBg+rWnhdE0Nd+lrcR0u0+clFpmokTdHuActqJZtJoTg6YE=</D></RSAKeyValue>";
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
            
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            String hashPass= hash.Hash_SHA1(txtPassword.Text);
            //String userName = rsa.Encryption(txtUsername.Text,publicKey);
            //query = "select username,pass from employee where username='"+userName+"'and pass='"+hashPass+"'";
            query = "select username,pass,role from employee1";
            DataSet ds = fn.getData(query);   
            DataTable dt = ds.Tables[0];
            if (ds.Tables[0].Rows.Count!=0)
            {
                DataSet loginDs = new DataSet();
                DataTable loginTbl = new DataTable("Login1");
                loginTbl.Columns.Add(new DataColumn("userName", typeof(string)));
                loginTbl.Columns.Add(new DataColumn("password", typeof(string)));
                loginTbl.Columns.Add(new DataColumn("role", typeof(string)));
                foreach (DataRow data in dt.Rows)
                {
                    DataRow row = loginTbl.NewRow();
                    var a = rsa.Decryption(data["username"].ToString(), privateKey);
                    row["userName"] = rsa.Decryption(data["username"].ToString(), privateKey);
                    row["role"] = rsa.Decryption(data["role"].ToString(), privateKey);
                    row["password"] = data["pass"].ToString();
                    loginTbl.Rows.Add(row);
                }
                loginDs.Tables.Add(loginTbl);

                DataRow[] result = loginDs.Tables[0].Select("userName = '" + txtUsername.Text + "' AND password = '" + hashPass + "'");
                
                if (result.Count()==0)
                {
                    labelError.Visible = true;
                    txtPassword.Clear();
                }
              
                else
                {
                    labelError.Visible = false;
                    Dashboard dash = new Dashboard();
                    IdentityModel.Role = result[0][2].ToString();
                    this.Hide();
                    dash.Show();
                }
            }   
            else
            {
                MessageBox.Show("Dont have any data in database!!!","Information",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }    
        }

       
    }
}
