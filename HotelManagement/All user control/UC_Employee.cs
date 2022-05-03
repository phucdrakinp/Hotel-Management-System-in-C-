using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagement.All_user_control
{
    public partial class UC_Employee : UserControl
    {
        function fn= new function();
        String query;
        //RsaEncryption rsa = new RsaEncryption();
        Hash hash = new Hash();
        Rsa rsa = new Rsa();
        string publicKey = "<RSAKeyValue><Modulus>rfzlz5vARPlfFHSPfit9GoJ3uQ7u7pGZj9PIYQcJjM+600ZyOh1ofz+zCBx6RodcpYevk8Vb2PMk4Z1ePzghLpGep08yhASUspzyOtPwDdXqhX7Mf0ySdUF2ICpypkAu9/QYjvw1/fUm+aFGOAqqeFsJpXaDX6jl+SgPahwCrl0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        string privateKey = "<RSAKeyValue><Modulus>rfzlz5vARPlfFHSPfit9GoJ3uQ7u7pGZj9PIYQcJjM+600ZyOh1ofz+zCBx6RodcpYevk8Vb2PMk4Z1ePzghLpGep08yhASUspzyOtPwDdXqhX7Mf0ySdUF2ICpypkAu9/QYjvw1/fUm+aFGOAqqeFsJpXaDX6jl+SgPahwCrl0=</Modulus><Exponent>AQAB</Exponent><P>4LNv8IfQEaapvSk/6xW6BH9JZa0WqL3CoeVT9n4ySq8S2GYE9XmbFte28LK98eW+N8v7hhiCK8WWY4vb1cSPpw==</P><Q>xjkYPna3HuwWSav4/48Q2WHMcT5zBxAkGYxWTiZSUtHiXC735K627ELYtX4ZaWUVqX1w14s0SOBLRlY3FuMyWw==</Q><DP>dV8ldLXsiJvPBCEc4zZJIXo/o53DPUdJ+Hkq35HRwVMr+99mbbckvMzXIWmscEO6lbi2XLhGnoiqYrs2jLYM9w==</DP><DQ>XM6Gh1hVzGiE1uFpp114ag7cBXlTqc7o1/1YuyY+DQCvlrF25t7WTi/N/suXYj0tszlEB+bpB+Xb2IatLE4bWQ==</DQ><InverseQ>SGLSknLn0hzB9qCcCGLyk3UHRlut98wN2s5riNjmclUQODxgNr0x6ak0HbsRVnPiR+BzGgmyGG8hTB1EZIyolQ==</InverseQ><D>OYN/9EDoLeTBKWHejTaTBFBcgzAMi5BV0tWPR4OsBIAmofCHke5mvKmx5NyFDwtv9MgFojN7SRwW9P2wSfWkAdUTTHa4uLrcafR1YkxcNKcJd39nPcm0r+hdURvGKBg+rWnhdE0Nd+lrcR0u0+clFpmokTdHuActqJZtJoTg6YE=</D></RSAKeyValue>";
        public UC_Employee()
        {
            InitializeComponent();
        }

       
        private bool checkRole()
        {
            var role = IdentityModel.Role;
            var listRole = this.Tag.ToString().Split(',').ToList();
            if (listRole.Any(x => x == role))
            {
                return true;
            }
            return false;
        }
     

        private void btnRegister_Click(object sender, EventArgs e)
        {
            
            if (txtName.Text!="" && txtMobile.Text!="" && txtGender.Text!="" && txtEmail.Text !="" && txtUsername.Text!= "" && txtPassword.Text!= "")
            {

                if (txtPassword.Text.Length < 8)
                {
                    MessageBox.Show("Password too short!!!","Warning...!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                else
                {
                    
                String name     = rsa.Encryption(txtName.Text,publicKey);
                String  mobile   = rsa.Encryption(txtMobile.Text,publicKey);
                String gender   = rsa.Encryption(txtGender.Text,publicKey);
                String email    = rsa.Encryption(txtEmail.Text,publicKey);
                String username = rsa.Encryption(txtUsername.Text,publicKey);
                String pass     = hash.Hash_SHA1(txtPassword.Text);
                String role     = rsa.Encryption(txtRole.Text,publicKey);

                query = "insert into employee1 (ename,mobile,gender,email,username,pass,role) values('"+name+"','"+mobile+"','"+gender+ "','"+email+"','"+username+"','"+pass+"','"+role+"') ";
                fn.setData(query, "Employee Registered.");

                clearAll();
                
                }
                

            }
            else
            {
                MessageBox.Show("Fill All Fields.", "Warning...!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }

        private void tabEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabEmployee.SelectedIndex ==1 )
            {
                setEmployee(guna2DataGridView1);
                }
                else if (tabEmployee.SelectedIndex ==2 )
                {
                    setEmployee(guna2DataGridView2);
            }    
        }

        public void setEmployee(DataGridView dgv)
        {
            if (checkRole())
            {
                query = "select * from employee1";
                DataSet ds = fn.getData(query);
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    for (int i = 1; i < 6; i++)
                    {
                        ds.Tables[0].Rows[j][i] = rsa.Decryption(ds.Tables[0].Rows[j][i].ToString(), privateKey);
                    }
                    ds.Tables[0].Rows[j][6] = hash.Hash_SHA1(ds.Tables[0].Rows[j][6].ToString());
                    ds.Tables[0].Rows[j][7] = rsa.Decryption(ds.Tables[0].Rows[j][7].ToString(), privateKey);
                }
                

                dgv.DataSource = ds.Tables[0];
            }
            else
            {
                query = "select * from employee1";
                DataSet ds = fn.getData(query);
                dgv.DataSource = ds.Tables[0];
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (checkRole())
            {
                if (txtID.Text != "")
                {
                    if (MessageBox.Show("Are you sure?", "Comfirmation...!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        query = "delete from employee1 where eid=" + txtID.Text + "";
                        fn.setData(query, "Record Deleted.");
                        tabEmployee_SelectedIndexChanged(this, null);
                    }

                }

            }
            else
            {
                MessageBox.Show("You are not allowed to deleted employee", "Imformation", MessageBoxButtons.OK);
                btnDelete.Enabled = false;
            }
            
        }

        private void UC_Employee_Leave(object sender, EventArgs e)
        {
            clearAll();
        }

       

        public void clearAll()
        {
            txtEmail.Clear();
            txtMobile.Clear();
            txtName.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtGender.SelectedIndex = -1;
            txtRole.SelectedIndex = -1;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (!reg.IsMatch(txtEmail.Text))
            {
                txtEmail.ForeColor = Color.Red;
            }
            else
                txtEmail.ForeColor = Color.Black;
            
        }
    }
}
