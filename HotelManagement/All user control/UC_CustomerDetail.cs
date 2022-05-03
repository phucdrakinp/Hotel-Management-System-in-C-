using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagement.All_user_control
{
    public partial class UC_CustomerDetail : UserControl
    {
        function fn = new function();
        String query;
        Rsa rsa = new Rsa();
        string privateKey = "<RSAKeyValue><Modulus>rfzlz5vARPlfFHSPfit9GoJ3uQ7u7pGZj9PIYQcJjM+600ZyOh1ofz+zCBx6RodcpYevk8Vb2PMk4Z1ePzghLpGep08yhASUspzyOtPwDdXqhX7Mf0ySdUF2ICpypkAu9/QYjvw1/fUm+aFGOAqqeFsJpXaDX6jl+SgPahwCrl0=</Modulus><Exponent>AQAB</Exponent><P>4LNv8IfQEaapvSk/6xW6BH9JZa0WqL3CoeVT9n4ySq8S2GYE9XmbFte28LK98eW+N8v7hhiCK8WWY4vb1cSPpw==</P><Q>xjkYPna3HuwWSav4/48Q2WHMcT5zBxAkGYxWTiZSUtHiXC735K627ELYtX4ZaWUVqX1w14s0SOBLRlY3FuMyWw==</Q><DP>dV8ldLXsiJvPBCEc4zZJIXo/o53DPUdJ+Hkq35HRwVMr+99mbbckvMzXIWmscEO6lbi2XLhGnoiqYrs2jLYM9w==</DP><DQ>XM6Gh1hVzGiE1uFpp114ag7cBXlTqc7o1/1YuyY+DQCvlrF25t7WTi/N/suXYj0tszlEB+bpB+Xb2IatLE4bWQ==</DQ><InverseQ>SGLSknLn0hzB9qCcCGLyk3UHRlut98wN2s5riNjmclUQODxgNr0x6ak0HbsRVnPiR+BzGgmyGG8hTB1EZIyolQ==</InverseQ><D>OYN/9EDoLeTBKWHejTaTBFBcgzAMi5BV0tWPR4OsBIAmofCHke5mvKmx5NyFDwtv9MgFojN7SRwW9P2wSfWkAdUTTHa4uLrcafR1YkxcNKcJd39nPcm0r+hdURvGKBg+rWnhdE0Nd+lrcR0u0+clFpmokTdHuActqJZtJoTg6YE=</D></RSAKeyValue>";

        public UC_CustomerDetail()
        {
            InitializeComponent();
        }

        private void txtSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (txtSearchBy.SelectedIndex == 0)
            {
                query = "select customer.cid,customer.cname,customer.mobile,customer.nationality,customer.gender,customer.dob,customer.idproof,customer.addres,customer.checkin,customer.checkout,rooms.roomNo,rooms.roomType,rooms.bed,rooms.price from customer inner join rooms on customer.roomid = rooms.roomid ";
                DataSet ds= fn.getData(query);

                
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    for (int i = 1; i < 9; i++)
                    {

                        ds.Tables[0].Rows[j][i] = rsa.Decryption(ds.Tables[0].Rows[j][i].ToString(), privateKey);

                    }
                }

                guna2DataGridView2.DataSource = ds.Tables[0];
            }
            else if(txtSearchBy.SelectedIndex == 1)
            {
                query = "select customer.cid,customer.cname,customer.mobile,customer.nationality,customer.gender,customer.dob,customer.idproof,customer.addres,customer.checkin,customer.checkout,rooms.roomNo,rooms.roomType,rooms.bed,rooms.price from customer inner join rooms on customer.roomid = rooms.roomid where checkout is null ";
                DataSet ds = fn.getData(query);

                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    for (int i = 1; i < 9; i++)
                    {

                        ds.Tables[0].Rows[j][i] = rsa.Decryption(ds.Tables[0].Rows[j][i].ToString(), privateKey);

                    }
                }
                guna2DataGridView2.DataSource = ds.Tables[0];
            }
            else
            {
                query = "select customer.cid,customer.cname,customer.mobile,customer.nationality,customer.gender,customer.dob,customer.idproof,customer.addres,customer.checkin,customer.checkout,rooms.roomNo,rooms.roomType,rooms.bed,rooms.price from customer inner join rooms on customer.roomid = rooms.roomid where checkout is not null ";
                DataSet ds = fn.getData(query);

                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    for (int i = 1; i < 9; i++)
                    {

                        ds.Tables[0].Rows[j][i] = rsa.Decryption(ds.Tables[0].Rows[j][i].ToString(), privateKey);

                    }
                }
                guna2DataGridView2.DataSource = ds.Tables[0];       
            }    
        }

        
    }
}
