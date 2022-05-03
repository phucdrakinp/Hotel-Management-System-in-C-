using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagement.All_user_control
{
    public partial class UC_CustomerRegistration : UserControl
    {
        function fn = new function();
        String query;
        Rsa rsa = new Rsa();
        string publicKey = "<RSAKeyValue><Modulus>rfzlz5vARPlfFHSPfit9GoJ3uQ7u7pGZj9PIYQcJjM+600ZyOh1ofz+zCBx6RodcpYevk8Vb2PMk4Z1ePzghLpGep08yhASUspzyOtPwDdXqhX7Mf0ySdUF2ICpypkAu9/QYjvw1/fUm+aFGOAqqeFsJpXaDX6jl+SgPahwCrl0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        string privateKey = "<RSAKeyValue><Modulus>rfzlz5vARPlfFHSPfit9GoJ3uQ7u7pGZj9PIYQcJjM+600ZyOh1ofz+zCBx6RodcpYevk8Vb2PMk4Z1ePzghLpGep08yhASUspzyOtPwDdXqhX7Mf0ySdUF2ICpypkAu9/QYjvw1/fUm+aFGOAqqeFsJpXaDX6jl+SgPahwCrl0=</Modulus><Exponent>AQAB</Exponent><P>4LNv8IfQEaapvSk/6xW6BH9JZa0WqL3CoeVT9n4ySq8S2GYE9XmbFte28LK98eW+N8v7hhiCK8WWY4vb1cSPpw==</P><Q>xjkYPna3HuwWSav4/48Q2WHMcT5zBxAkGYxWTiZSUtHiXC735K627ELYtX4ZaWUVqX1w14s0SOBLRlY3FuMyWw==</Q><DP>dV8ldLXsiJvPBCEc4zZJIXo/o53DPUdJ+Hkq35HRwVMr+99mbbckvMzXIWmscEO6lbi2XLhGnoiqYrs2jLYM9w==</DP><DQ>XM6Gh1hVzGiE1uFpp114ag7cBXlTqc7o1/1YuyY+DQCvlrF25t7WTi/N/suXYj0tszlEB+bpB+Xb2IatLE4bWQ==</DQ><InverseQ>SGLSknLn0hzB9qCcCGLyk3UHRlut98wN2s5riNjmclUQODxgNr0x6ak0HbsRVnPiR+BzGgmyGG8hTB1EZIyolQ==</InverseQ><D>OYN/9EDoLeTBKWHejTaTBFBcgzAMi5BV0tWPR4OsBIAmofCHke5mvKmx5NyFDwtv9MgFojN7SRwW9P2wSfWkAdUTTHa4uLrcafR1YkxcNKcJd39nPcm0r+hdURvGKBg+rWnhdE0Nd+lrcR0u0+clFpmokTdHuActqJZtJoTg6YE=</D></RSAKeyValue>";

        public UC_CustomerRegistration()
        {
            InitializeComponent();
        }

        public void setComboBox(String query,ComboBox combo)
        {
            SqlDataReader sdr = fn.getForCombo(query);
            while (sdr.Read())
            {
                for(int i = 0; i < sdr.FieldCount; i++)
                {
                    combo.Items.Add(sdr.GetString(i));
                }
            }
            sdr.Close();
        }

       

        private void txtRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRoomNo.Items.Clear();
            txtPrice.Clear();
            query = "select roomNo from rooms where bed = '"+txtBed.Text+"' and roomType='"+txtRoom.Text+"' and booked= 'NO' ";
            setComboBox(query, txtRoomNo);

        }

        private void txtBed_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRoom.SelectedIndex = -1;
            txtRoomNo.Items.Clear ();
            txtPrice.Clear ();
        }
        int rid;
        private void txtRoomNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            query = "select price, roomid from rooms where roomNo = '"+txtRoomNo.Text+"' ";
            DataSet ds = fn.getData(query);
            txtPrice.Text = ds.Tables[0].Rows[0][0].ToString();
            rid=int.Parse(ds.Tables[0].Rows[0][1].ToString());  
        }

        private void btnAlloteRoom_Click(object sender, EventArgs e)
        {
            if (txtName.Text !="" && txtContact.Text !="" && txtNationality.Text !="" && txtDob.Text !="" && txtGender.Text !="" && txtIdProof.Text!=""&& txtAddress.Text!=""&& txtCheckin.Text!=""&& txtPrice.Text!="")
            {
                String name = rsa.Encryption(txtName.Text,publicKey);
                String mobile = rsa.Encryption(txtContact.Text,publicKey);
                String nationality = rsa.Encryption(txtNationality.Text,publicKey);
                String gender = rsa.Encryption(txtGender.Text,publicKey);
                String dob = rsa.Encryption(txtDob.Text,publicKey);
                String idprof = rsa.Encryption(txtIdProof.Text,publicKey);
                String address= rsa.Encryption(txtAddress.Text,publicKey);   
                String checkin= rsa.Encryption(txtCheckin.Text,publicKey);

                query = "insert into customer(cname,mobile,nationality,gender,dob,idproof,addres,checkin,roomid) values ('"+name+"','"+mobile+"','"+nationality+"','"+gender+"','"+dob+"','"+idprof+"','"+address+"','"+checkin+"',"+rid+") update rooms set booked ='YES' where roomNo = '"+txtRoomNo.Text+"'";
                fn.setData(query, " Room No " + txtRoomNo.Text + " Allocation Successful.");
                clearAll();
            }
            else
            {
                MessageBox.Show("All field are madetory.","Information!!",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        public void clearAll()
        {
            txtName.Clear();
            txtContact.Clear();
            txtContact.Clear();
            txtDob.ResetText();
            txtNationality.Clear();
            txtGender.SelectedIndex = -1;
            txtIdProof.Clear();
            txtAddress.Clear();
            txtCheckin.ResetText();
            txtBed.SelectedIndex = -1;
            txtRoom.SelectedIndex = -1; 
            txtRoomNo.SelectedIndex = -1;  
            txtPrice.Clear();
        }

        private void UC_CustomerRegistration_Leave(object sender, EventArgs e)
        {
            clearAll();
        }
    }
    }

