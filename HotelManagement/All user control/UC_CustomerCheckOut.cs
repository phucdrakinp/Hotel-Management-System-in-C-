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
    public partial class UC_CustomerCheckOut : UserControl
    {

        function fn = new function();
        String query;
        Rsa rsa = new Rsa();
        string privateKey = "<RSAKeyValue><Modulus>rfzlz5vARPlfFHSPfit9GoJ3uQ7u7pGZj9PIYQcJjM+600ZyOh1ofz+zCBx6RodcpYevk8Vb2PMk4Z1ePzghLpGep08yhASUspzyOtPwDdXqhX7Mf0ySdUF2ICpypkAu9/QYjvw1/fUm+aFGOAqqeFsJpXaDX6jl+SgPahwCrl0=</Modulus><Exponent>AQAB</Exponent><P>4LNv8IfQEaapvSk/6xW6BH9JZa0WqL3CoeVT9n4ySq8S2GYE9XmbFte28LK98eW+N8v7hhiCK8WWY4vb1cSPpw==</P><Q>xjkYPna3HuwWSav4/48Q2WHMcT5zBxAkGYxWTiZSUtHiXC735K627ELYtX4ZaWUVqX1w14s0SOBLRlY3FuMyWw==</Q><DP>dV8ldLXsiJvPBCEc4zZJIXo/o53DPUdJ+Hkq35HRwVMr+99mbbckvMzXIWmscEO6lbi2XLhGnoiqYrs2jLYM9w==</DP><DQ>XM6Gh1hVzGiE1uFpp114ag7cBXlTqc7o1/1YuyY+DQCvlrF25t7WTi/N/suXYj0tszlEB+bpB+Xb2IatLE4bWQ==</DQ><InverseQ>SGLSknLn0hzB9qCcCGLyk3UHRlut98wN2s5riNjmclUQODxgNr0x6ak0HbsRVnPiR+BzGgmyGG8hTB1EZIyolQ==</InverseQ><D>OYN/9EDoLeTBKWHejTaTBFBcgzAMi5BV0tWPR4OsBIAmofCHke5mvKmx5NyFDwtv9MgFojN7SRwW9P2wSfWkAdUTTHa4uLrcafR1YkxcNKcJd39nPcm0r+hdURvGKBg+rWnhdE0Nd+lrcR0u0+clFpmokTdHuActqJZtJoTg6YE=</D></RSAKeyValue>";
        public UC_CustomerCheckOut()
        {
            InitializeComponent();
        }

        private void UC_CustomerCheckOut_Load(object sender, EventArgs e)
        {
            /*query = "select customer.cid,customer.cname,customer.mobile,customer.nationality,customer.gender,customer.dob,customer.idproof,customer.addres,customer.checkin,rooms.roomNo,rooms.roomType,rooms.bed,rooms.price from customer inner join rooms on customer.roomid = rooms.roomid where chekout = 'NO'";
            DataSet ds = fn.getData(query);

            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                for (int i = 1; i < 9; i++)
                {

                    ds.Tables[0].Rows[j][i] = rsa.Decryption(ds.Tables[0].Rows[j][i].ToString(), privateKey);

                }
            }*/
         
             guna2DataGridView1.DataSource = LoadDataCustomer().Tables[0];
        }

        private DataSet LoadDataCustomer()
        {
            query = "select customer.cid,customer.cname,customer.mobile,customer.nationality,customer.gender,customer.dob,customer.idproof,customer.addres,customer.checkin,rooms.roomNo,rooms.roomType,rooms.bed,rooms.price from customer inner join rooms on customer.roomid = rooms.roomid where chekout = 'NO'";
            DataSet ds = fn.getData(query);

            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                for (int i = 1; i < 9; i++)
                {

                    ds.Tables[0].Rows[j][i] = rsa.Decryption(ds.Tables[0].Rows[j][i].ToString(), privateKey);

                }
            }

            return ds;
        }

        /*  private void txtName_TextChanged(object sender, EventArgs e)
           {
               query = "select customer.cid,customer.cname,customer.mobile,customer.nationality,customer.gender,customer.dob,customer.idproof,customer.addres,customer.checkin,rooms.roomNo,rooms.roomType,rooms.bed,rooms.price from customer inner join rooms on customer.roomid = rooms.roomid where cname like '"+txtName.Text+"%' and chekout = 'NO'";
               DataSet ds = fn.getData(query);
               DataTable dt= ds.Tables[0];
              for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
               {
                   for (int i = 1; i < 9; i++)
                   {

                       ds.Tables[0].Rows[j][i] = rsa.Decryption(ds.Tables[0].Rows[j][i].ToString(), privateKey);

                   }
               }
               DataRow[] dataRows = ds.Tables[0].Select("Cname= '"+txtName.Text+"'");




               //
               /*
               DataSet findByNameDs= new DataSet();
               DataTable findByNameDt = new DataTable("ListCustomers");
               findByNameDt.Columns.Add(new DataColumn("name", typeof(String)));
               findByNameDt.Columns.Add(new DataColumn("mobile",typeof(String)));
               findByNameDt.Columns.Add(new DataColumn("nationality",typeof (String)));
               findByNameDt.Columns.Add(new DataColumn("gender", typeof(String)));
               findByNameDt.Columns.Add(new DataColumn("dob", typeof(String)));
               findByNameDt.Columns.Add(new DataColumn("idproof", typeof(String)));
               findByNameDt.Columns.Add(new DataColumn("address", typeof(String)));
               findByNameDt.Columns.Add(new DataColumn("chekin", typeof(String)));
               findByNameDt.Columns.Add(new DataColumn("roomNo", typeof(String)));
               findByNameDt.Columns.Add(new DataColumn("roomType", typeof(String)));
               findByNameDt.Columns.Add(new DataColumn("bed", typeof(String)));
               findByNameDt.Columns.Add(new DataColumn("price", typeof(String)));

               foreach(DataRow dr in dt.Rows )
               {
                   DataRow row = findByNameDt.NewRow();
                   row["name"]=rsa.Decryption(dr["cname"].ToString(),privateKey);
                   row["mobile"] = rsa.Decryption(dr["mobile"].ToString(), privateKey);
                   row["nationality"] = rsa.Decryption(dr["nationality"].ToString(), privateKey);
                   row["gender"] = rsa.Decryption(dr["gender"].ToString(), privateKey);
                   row["dob"] = rsa.Decryption(dr["dob"].ToString(), privateKey);
                   row["idproof"] = rsa.Decryption(dr["idproof"].ToString(), privateKey);
                   row["address"] = rsa.Decryption(dr["addres"].ToString(), privateKey);
                   row["chekin"] = rsa.Decryption(dr["checkin"].ToString(), privateKey);
                   row["roomNo"] = dr["roomNo"];
                   row["roomType"] = dr["roomType"];
                   row["bed"] = dr["bed"];
                   row["price"] = dr["bed"];
                   findByNameDt.Rows.Add(row);

               }    
               findByNameDs.Tables.Add(findByNameDt);
            //   DataRow[] result = findByNameDs.Tables[0].Select("name = '" + txtName.Text + "' ");
               //DataTable dt1 = result.CopyToDataTable();
               DataSet ds1 = new DataSet();
              // ds1.Tables.Add(dt1);

               for(int i=0; i<result.Length;i++)
               {
                   Console.WriteLine(result[i].ToString());
               }    

               */
        //


        //       guna2DataGridView1.DataSource = ds.Tables[0];    

        //    }



        int id;
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                id = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtCName.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtRoom.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            }

        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if(txtCName.Text != "")
            {
                if(MessageBox.Show("Are You Sure ?","Comfirmation",MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)==DialogResult.OK)
                {
                    String cdate = txtCheckOutDate.Text;
                    query = "update customer set chekout = 'YES', checkout='" + cdate + "' where cid = "+id+" update rooms set booked = 'NO' where roomNo ='"+txtRoom.Text+"' ";
                    fn.setData(query, "Check Out Succesfully.");
                    UC_CustomerCheckOut_Load(this, null);
                    clearAll();
                }

            }
            else
            {
                MessageBox.Show("No Customer Selected.", "Message", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        public void clearAll()
        {
            txtCName.Clear();
            txtRoom.Clear();
            txtName.Clear();
            txtCheckOutDate.ResetText();
        }

        private void UC_CustomerCheckOut_Leave(object sender, EventArgs e)
        {
            clearAll();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtName.Text))
            {
                (guna2DataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Cname = '{0}'", txtName.Text);

            }
            else
            {
                guna2DataGridView1.DataSource = LoadDataCustomer().Tables[0];
            }


            /* string searchValue = txtName.Text;

             guna2DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
             try
             {
                 foreach (Table  in guna2DataGridView1.DataSource)
                 {
                     if (row.Cells[2].Value.ToString().Equals(searchValue))
                     {
                         row.Selected = true;
                         break;
                     }
                 }
             }
             catch (Exception exc)
             {
                 MessageBox.Show(exc.Message);
             }*/

            /*query = "select customer.cid,customer.cname,customer.mobile,customer.nationality,customer.gender,customer.dob,customer.idproof,customer.addres,customer.checkin,rooms.roomNo,rooms.roomType,rooms.bed,rooms.price from customer inner join rooms on customer.roomid = rooms.roomid where cname like '" + txtName.Text + "%' and chekout = 'NO'";
            DataSet ds = fn.getData(query);
            DataTable dt = ds.Tables[0];
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                for (int i = 1; i < 9; i++)
                {

                    ds.Tables[0].Rows[j][i] = rsa.Decryption(ds.Tables[0].Rows[j][i].ToString(), privateKey);

                }
            }
            DataRow[] dataRows = ds.Tables[0].Select("Cname= '" + txtName.Text + "'");*/

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtName.Text))
            {
                (guna2DataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Cname LIKE '{0}%' or mobile like '{0}%'", txtName.Text);

            }
            else
            {
                guna2DataGridView1.DataSource = LoadDataCustomer().Tables[0];
            }

        }
    }
}
