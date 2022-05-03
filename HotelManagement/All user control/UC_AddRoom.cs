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
    public partial class UC_AddRoom : UserControl
    {
        function fn = new function();
        String query;
        public UC_AddRoom()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if(txtRoomNo.Text!=""&& txtType.Text!="" && txtBed.Text!="" && txtPrice.Text != "")
            {
                String roomno = txtRoomNo.Text;
                String type= txtType.Text;
                String bed = txtBed.Text;
                Int64 price = Int64.Parse(txtPrice.Text);

                query = "Insert into rooms(roomNo,roomType,bed,price) values('"+roomno+"','"+type+"','"+bed+"',"+price+")";
                fn.setData(query, "Room Added.");

                UC_AddRoom_Load(this, null);
                clearAll();

            }
            else
            {
                MessageBox.Show("Fill All Fields.", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }    
        }

        private void UC_AddRoom_Load(object sender, EventArgs e)
        {
            query = "select roomNo,roomType,bed,price,booked from rooms";
            DataSet ds =  fn.getData(query);
            DataGridView2.DataSource = ds.Tables[0];   
        }


        public void clearAll()
        {
            txtRoomNo.Clear();
            txtType.SelectedIndex = -1;
            txtBed.SelectedIndex = -1;
            txtPrice.Clear();
        }

        private void UC_AddRoom_Leave(object sender, EventArgs e)
        {
            clearAll();
        }

        private void UC_AddRoom_Enter(object sender, EventArgs e)
        {
            UC_AddRoom_Load(this, null);
        }

       
    }
}
