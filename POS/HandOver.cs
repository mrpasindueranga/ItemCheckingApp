using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class HandOver : Form
    {
        public HandOver()
        {
            InitializeComponent();
        }

        private void getPendingItemID()
        {
            DBConn ConnPID = new DBConn();
            ConnPID.cmd.CommandText = "SELECT Assign.ItemID FROM Assign LEFT JOIN Rent ON Assign.RenID=Rent.ID WHERE Rent.Status=@Status AND Assign.RenID=@SupID ";
            ConnPID.cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = "Pending";
            ConnPID.cmd.Parameters.Add("@SupID", SqlDbType.VarChar).Value = cmbSupID.Text;
            ConnPID.ada.SelectCommand = ConnPID.cmd;
            DataTable dtItmID = new DataTable();
            ConnPID.ada.Fill(dtItmID);
            cmbItemID.Items.Clear();
            cmbItemID.Items.Add("--SELECT Item ID--");
            cmbItemID.SelectedIndex = 0;

            foreach (DataRow row in dtItmID.Rows)
            {
                    cmbItemID.Items.Add(row["ItemID"]);
            }
        }

        private void getSupID()
        {
            DBConn Conn = new DBConn();
            Conn.cmd.CommandText = "SELECT Rent.ID FROM Rent WHERE Status=@Status";
            Conn.cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = "Pending";
            Conn.ada.SelectCommand = Conn.cmd;
            DataTable dtSupID = new DataTable();
            Conn.ada.Fill(dtSupID);
            cmbSupID.Items.Clear();
            cmbSupID.Items.Add("--SELECT Supply ID--");
            cmbSupID.SelectedIndex = 0;

            foreach (DataRow row in dtSupID.Rows)
            {
                cmbSupID.Items.Add(row["ID"]);
            }
        }

        DataTable dtLoad = new DataTable();

        private void HandOver_Load(object sender, EventArgs e)
        {
            getPendingItemID();
            getSupID();
            dtLoad.Columns.Add("Item ID");
            dtLoad.Columns.Add("Name");
            txtBarcode.Focus();
        }

        bool isValid = true;

        private void addItem()
        {
            if (!String.IsNullOrEmpty(txtBarcode.Text) && cmbSupID.SelectedIndex != 0)
            {
                foreach (DataGridViewRow row in dgvItem.Rows)
                {
                    if (txtBarcode.Text == row.Cells["Item ID"].Value.ToString())
                    {
                        isValid = false;
                        break;
                    }
                    else
                    {
                        isValid = true;
                    }
                }

                if (isValid)
                {
                    DBConn Conn = new DBConn();
                    Conn.cmd.CommandText = "SELECT ID, Name FROM Item WHERE ID=@ID";
                    Conn.cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtBarcode.Text;
                    Conn.sdr = Conn.cmd.ExecuteReader();

                    while (Conn.sdr.Read())
                    {
                        var dr = dtLoad.NewRow();
                        dr["Item ID"] = Conn.sdr.GetValue(0).ToString();
                        dr["Name"] = Conn.sdr.GetValue(1).ToString();
                        dtLoad.Rows.Add(dr);
                        dgvItem.DataSource = dtLoad;
                    }

                    txtBarcode.Text = "";
                    txtBarcode.Focus();

                    if (cmbSupID.SelectedIndex != 0)
                    {
                        btnCompare.Visible = true;
                    }
                }
                else
                {
                    lblError.Text = "This Item Already Define....!!";
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text = "Please enter valid item details..";
                lblError.Visible = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addItem();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {

            if(dgvItem.SelectedRows.Count > 0)
            {
                dgvItem.Rows.RemoveAt(dgvItem.SelectedRows[0].Index);
                panelNotify.Controls.Clear();
            }

            if (dgvItem.Rows.Count == 0)
            {
                btnCompare.Visible = false;
            }
        }

        Label Description(String ItemID, String txtDes)
        {
            Label des = new Label();
            des.Name = "des" + ItemID.ToString();
            des.Text = txtDes;
            des.Location = new Point(12, 60);
            des.AutoSize = true;
            return des;
        }

        GroupBox addGroupBox(String ItemID, int coord)
        {
            GroupBox group = new GroupBox();
            group.Name = "groupBox" + ItemID;
            group.Text = "Item ID : " + ItemID;
            group.Font = new System.Drawing.Font("Segoe UI", 14.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            group.Width = 369;
            group.ForeColor = Color.WhiteSmoke;
            group.BackColor = Color.DimGray;
            group.TabStop = false;
            group.Height = 160;
            group.Location = new Point(3, coord);
            group.Margin = new Padding(7);
            return group;
        }

        int coord = 3;

        private void getCard(String ItemID, String txtDes)
        {
            GroupBox group = addGroupBox(ItemID, coord);
            Label des = Description(ItemID, txtDes);
            group.Controls.Add(des);
            panelNotify.Controls.Add(group);
            coord += 160;
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            if (cmbSupID.SelectedIndex != 0 && dgvItem.Rows.Count != 0)
            {
                coord = 3;
                panelNotify.Controls.Clear();
                foreach (DataGridViewRow row in dgvItem.Rows)
                {
                    DBConn Conn = new DBConn();
                    Conn.cmd.CommandText = "SELECT * FROM Assign WHERE RenID=@SupID AND ItemID=@ItemID AND [Status]=@Pending";
                    Conn.cmd.Parameters.Add("@SupID", SqlDbType.VarChar).Value = cmbSupID.Text;
                    Conn.cmd.Parameters.Add("@Pending", SqlDbType.VarChar).Value = "Stock Out";
                    Conn.cmd.Parameters.Add("@ItemID", SqlDbType.VarChar).Value = row.Cells["Item ID"].Value.ToString();
                    Conn.ada.SelectCommand = Conn.cmd;

                    DataTable dtCheck = new DataTable();
                    Conn.ada.Fill(dtCheck);

                    if (dtCheck.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dtCheck.Rows)
                        {
                            DBConn ConnIn = new DBConn();
                            ConnIn.cmd.CommandText = "UPDATE Assign SET [Status]=@Status, RetDate=@Date WHERE RenID=@SupID AND ItemID=@ItemID AND [Status]=@Pending";
                            ConnIn.cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = "Stock In";
                            ConnIn.cmd.Parameters.Add("@Pending", SqlDbType.VarChar).Value = "Stock Out";
                            ConnIn.cmd.Parameters.Add("@SupID", SqlDbType.VarChar).Value = cmbSupID.Text;
                            ConnIn.cmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.Now.ToString("yyyy-MM-dd");
                            ConnIn.cmd.Parameters.Add("@ItemID", SqlDbType.VarChar).Value = row.Cells["Item ID"].Value.ToString();
                            ConnIn.cmd.ExecuteNonQuery();

                            MessageBox.Show("Successfully Saved some records !!", "Successfully Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        getCard(row.Cells["Item ID"].Value.ToString(), "This Item is not in the supply...!!");
                    }
                }


                getPendingItemID();
                if (cmbSupID.SelectedIndex != 0 && panelNotify.Controls.Count == 0 && cmbItemID.Items.Count == 1)
                {
                    btnComplete.Visible = true;
                    btnCompare.Visible = false;
                }
                else
                {
                    btnCompare.Visible = true;
                    btnComplete.Visible = false;
                }
            }else
            {
                lblError.Text = "Please Enter Valid Data";
                lblError.Visible = true;
            }
        }

        private void cmbSupID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbSupID.SelectedIndex != 0)
            {
                getPendingItemID();
                lblError.Visible = false;
                
                if(cmbItemID.Items.Count == 1)
                {
                    btnCompare.Visible = false;
                    btnComplete.Visible = true;
                }
            }

        }

        private void btnAddNote_Click(object sender, EventArgs e)
        {
            if (cmbItemID.SelectedIndex != 0)
            {
                DBConn Conn = new DBConn();
                Conn.cmd.CommandText = "UPDATE Assign SET Note=@Note, Status=@Status WHERE ItemID=@ItemID AND RenID=@SupID AND [Status]=@Pending";
                Conn.cmd.Parameters.Add("@Note",SqlDbType.VarChar).Value = txtNote.Text;
                Conn.cmd.Parameters.Add("@ItemID",SqlDbType.VarChar).Value = cmbItemID.Text;
                Conn.cmd.Parameters.Add("@SupID",SqlDbType.VarChar).Value = cmbSupID.Text;
                Conn.cmd.Parameters.Add("@Pending", SqlDbType.VarChar).Value = "Stock Out";
                Conn.cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = "Stock In";
                Conn.cmd.ExecuteNonQuery();
                getPendingItemID();
                txtNote.Text = "";
                MessageBox.Show("Successfully Saved note !!", "Successfully Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCompare.Visible = false;
                btnComplete.Visible = true;
            }
        }

        private void btnCl_Click(object sender, EventArgs e)
        {
            cmbItemID.SelectedIndex = 0;
            cmbSupID.SelectedIndex = 0;
            txtBarcode.Text = "";
            btnCompare.Visible = false;
            btnComplete.Visible = false;
            dgvItem.DataSource = null;
            dtLoad.Rows.Clear();
            lblError.Visible = false;
            panelNotify.Controls.Clear();
        }

        private void txtBarcode_OnValueChanged(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            DBConn Conn = new DBConn();
            Conn.cmd.CommandText = "UPDATE Rent SET Status=@Status, RetDate=@Date WHERE SupID=@SupID AND [Status]=@Pending";
            Conn.cmd.Parameters.Add("@Status",SqlDbType.VarChar).Value = "Completed";
            Conn.cmd.Parameters.Add("@SupID", SqlDbType.VarChar).Value = cmbSupID.Text;
            Conn.cmd.Parameters.Add("@Pending", SqlDbType.VarChar).Value = "Pending";
            Conn.cmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.Now.ToString("yyyy-MM-dd");
            Conn.cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Saved some records !!", "Successfully Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnCompare.Visible = false;
            btnComplete.Visible = false;
            dgvItem.DataSource = null;
            getSupID();
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 13)
            {
                addItem();
            }
        }
    }
}
