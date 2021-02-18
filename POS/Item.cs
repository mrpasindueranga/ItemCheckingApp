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
            txtName.Enabled = true;
            txtName.Text = "";
            txtName.Focus();
        }

        public void getDataFromDGV()
        {
            if (btnUpdate.Visible == true)
            {
                txtName.Enabled = true;
                btnUpdate.Enabled = true;
                btnDel.Enabled = true;
                txtName.Focus();
                foreach (DataGridViewRow row in dgvItem.SelectedRows)
                {
                    txtRegID.Text = row.Cells[0].Value.ToString();
                    txtName.Text = row.Cells[1].Value.ToString();
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
                    Conn.cmd.CommandText = "INSERT INTO Item VALUES(@ID,@Name)";
                    Conn.cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtRegID.Text.Trim();
                    Conn.cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = txtName.Text.Trim();
                    Conn.cmd.ExecuteNonQuery();
                    getNewID();
                    DataLoad();
                    MessageBox.Show("Successfully Saved Item" + txtRegID.Text + "!!", "Successfully Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                Conn.cmd.CommandText = "DELETE Item WHERE ID=@ID";
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
                    DataLoad();
                    MessageBox.Show("Successfully Updated Item No" + txtRegID.Text + "!!", "Successfully Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if(cmbBarcode.SelectedIndex != 0)
            {
                PrintDialog printDlg = new PrintDialog();
                PrintDocument printDoc = new PrintDocument();
                printDlg.Document = printDoc;
                printDlg.AllowSelection = true;
                printDlg.AllowSomePages = true;
                if (printDlg.ShowDialog() == DialogResult.OK)
                {
                    printDoc.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                    printDoc.Print();
                }
            }
        }

        private void previewBarcode()
        {
            BarcodeLib.Barcode barcode = new BarcodeLib.Barcode();
            Image img = barcode.Encode(BarcodeLib.TYPE.CODE128, cmbBarcode.Text, Color.Black, Color.White, pictureBoxBarcode.Size.Width, pictureBoxBarcode.Size.Height);
            pictureBoxBarcode.Image = img;
        }

        private void loadBarcodeNumber()
        {
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
            if(cmbBarcode.SelectedIndex != 0)
            {
                lblBarcodeError.Visible = false;
                previewBarcode();
            }
            else
            {
                lblBarcodeError.Visible = true;
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(pictureBoxBarcode.Width, pictureBoxBarcode.Height);
            pictureBoxBarcode.DrawToBitmap(bm, new Rectangle(0, 0, pictureBoxBarcode.Width, pictureBoxBarcode.Height));
            e.Graphics.DrawImage(bm, 0, 0);
            bm.Dispose();
        }
    }
}
