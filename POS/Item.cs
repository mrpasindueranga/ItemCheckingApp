using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace POS
{
    public partial class Item : Form
    {
        public Item()
        {
            InitializeComponent();
        }

        private void Item_Load(object sender, EventArgs e)
        {
            DataLoad();
            loadBarcodeNumber();
            getNewID();
        }

        public void DataLoad()
        {
            DBConn Conn = new DBConn();
            Conn.cmd.CommandText = "SELECT * FROM Item";
            Conn.ada.SelectCommand = Conn.cmd;
            DataTable dtItem = new DataTable();
            Conn.ada.Fill(dtItem);

            dgvItem.DataSource = dtItem;
        }

        public void Clear()
        {
            txtName.Text = "";
            txtName.Focus();
        }

        public void getDataFromDGV()
        {
            if (btnAdd.Visible == true)
            {
                txtName.Focus();
                foreach (DataGridViewRow row in dgvItem.SelectedRows)
                {
                    txtRegID.Text = row.Cells[0].Value.ToString();
                    txtName.Text = row.Cells[1].Value.ToString();

                    if (row.Cells[2].Value.ToString() == "Unavailable")
                    {
                        btnDel.Visible = false;
                    }
                    else
                    {
                        btnDel.Visible = true;
                    }
                }
            }
        }

        private void btnCh_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnCl.Visible = false;
            btnCh.Visible = false;
            btnUpdate.Visible = true;
            btnDel.Visible = true;
            btnAdd.Visible = true;
            Clear();
            getDataFromDGV();
        }

        public void getNewID()
        {
            DBConn ConnMax = new DBConn();
            ConnMax.cmd.CommandText = "SELECT MAX(ID) AS MAXID FROM Item";
            ConnMax.ada.SelectCommand = ConnMax.cmd;
            DataTable dtItem = new DataTable();
            ConnMax.ada.Fill(dtItem);
            foreach (DataRow row in dtItem.Rows)
            {
                object isNull = row["MAXID"];
                if (isNull == DBNull.Value)
                {
                    txtRegID.Text = "ITM-001";
                }
                else
                {
                    String maxID = row["MAXID"].ToString();
                    txtRegID.Text = maxID.Substring(0, 4) + String.Format("{0:000}", (int.Parse(maxID.Substring(4)) + 1));
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Clear();
            btnUpdate.Visible = false;
            btnDel.Visible = false;
            btnSave.Visible = true;
            btnCl.Visible = true;
            btnAdd.Visible = false;
            btnCh.Visible = true;
            getNewID();
        }

        private void dgvItem_SelectionChanged(object sender, EventArgs e)
        {
            getDataFromDGV();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtName.Text.Trim()))
            {
                DialogResult saveConfirm = MessageBox.Show("Are you want to Save Item No " + txtRegID.Text + "?..", "Save Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveConfirm == DialogResult.Yes)
                {
                    DBConn Conn = new DBConn();
                    Conn.cmd.CommandText = "INSERT INTO Item(ID, Name) VALUES(@ID,@Name)";
                    Conn.cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtRegID.Text.Trim();
                    Conn.cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = txtName.Text.Trim();
                    Conn.cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Saved Item" + txtRegID.Text + "!!", "Successfully Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getNewID();
                    DataLoad();
                    Clear();
                    loadBarcodeNumber();
                    txtName.Focus();
                }
            }
            else
            {
                lblError.Visible = true;
            }
        }

        private void btnCl_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult delConfirm = MessageBox.Show("Are you want to Delete Item No " + txtRegID.Text + "?..", "Delete Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (delConfirm == DialogResult.Yes)
            {
                DBConn Conn = new DBConn();
                Conn.cmd.CommandText = "Update Item SET Status=@Status WHERE ID=@ID";
                Conn.cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = "Unavailable";
                Conn.cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtRegID.Text;
                Conn.cmd.ExecuteNonQuery();
                DataLoad();
                loadBarcodeNumber();
            }
            else
            {
                MessageBox.Show("Successfully Delete Item No " + txtRegID.Text + "!!", "Successfully Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtName.Text.Trim()))
            {
                DialogResult upConfirm = MessageBox.Show("Are you want to Update Item No " + txtRegID.Text + "?..", "Update Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (upConfirm == DialogResult.Yes)
                {
                    DBConn Conn = new DBConn();
                    Conn.cmd.CommandText = "UPDATE Item SET Name=@Name WHERE ID=@ID";
                    Conn.cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtRegID.Text.Trim();
                    Conn.cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = txtName.Text.Trim();
                    Conn.cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Updated Item No" + txtRegID.Text + "!!", "Successfully Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataLoad();
                    getDataFromDGV();
                    txtName.Focus();
                }
            }
            else
            {
                lblError.Visible = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                DBConn Conn = new DBConn();
                Conn.cmd.CommandText = "SELECT * FROM Item WHERE Name LIKE CONCAT('%',@Search,'%')";
                Conn.cmd.Parameters.Add("@Search", SqlDbType.VarChar).Value = txtSearch.Text.Trim();
                Conn.ada.SelectCommand = Conn.cmd;
                DataTable dtItem = new DataTable();
                Conn.ada.Fill(dtItem);

                dgvItem.DataSource = dtItem;
            }
        }

        private void txtName_OnValueChanged(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        private void txtSearch_OnValueChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                DataLoad();
            }
        }

        Image img;

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if(cmbBarcode.SelectedIndex != 0)
            {
                lblBarcodeError.Visible = false;
                dsBarcode1.Clear();
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, ImageFormat.Png);
                    for (int i = 0; i < 5; i++)
                    {
                        dsBarcode1.Barcode.AddBarcodeRow(cmbBarcode.Text, ms.ToArray());
                    }
                }

                using (Barcode barcode = new Barcode(dsBarcode1.Barcode))
                {
                    barcode.ShowDialog();
                }
            }
            else
            {
                lblBarcodeError.Visible = true;
            }
        }

        private void previewBarcode()
        {
            BarcodeLib.Barcode barcode = new BarcodeLib.Barcode();
            img = barcode.Encode(BarcodeLib.TYPE.CODE128, cmbBarcode.Text, Color.Black, Color.White, pictureBoxBarcode.Size.Width, pictureBoxBarcode.Size.Height);
            pictureBoxBarcode.Image = img;
        }

        private void loadBarcodeNumber()
        {

            cmbBarcode.Items.Clear();

            DBConn Conn = new DBConn();
            Conn.cmd.CommandText = "SELECT ID FROM Item";
            Conn.ada.SelectCommand = Conn.cmd;
            DataTable dtID = new DataTable();
            Conn.ada.Fill(dtID);
            cmbBarcode.Items.Add("--SELECT BARCODE--");
            cmbBarcode.SelectedIndex = 0;
            
            foreach(DataRow row in dtID.Rows)
            {
                cmbBarcode.Items.Add(row["ID"]);
            }
        }

        private void cmbBarcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblBarcodeError.Visible = false;

            if(cmbBarcode.SelectedIndex != 0)
            {
                previewBarcode();
            }
        }

        private void txtRegID_OnValueChanged(object sender, EventArgs e)
        {
            if (txtRegID.Text == "ITM-001" && dgvItem.SelectedRows.Count == 0)
            {
                btnCh.Visible = false;
            }
            else if (txtRegID.Text != "ITM-001" && btnSave.Visible == true)
            {
                btnCh.Visible = true;
            }
        }
    }
}
