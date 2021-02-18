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
    public partial class Supply : Form
    {
        public Supply()
        {
            InitializeComponent();
        }

        DataTable dtLoad = new DataTable();

        public void getNewID()
        {
            DBConn ConnMax = new DBConn();
            ConnMax.cmd.CommandText = "SELECT MAX(ID) AS MAXID FROM Supply";
            ConnMax.ada.SelectCommand = ConnMax.cmd;
            DataTable dtItem = new DataTable();
            ConnMax.ada.Fill(dtItem);
            foreach (DataRow row in dtItem.Rows)
            {
                object isNull = row["MAXID"];
                if (isNull == DBNull.Value)
                {
                    txtSupID.Text = "SUP-001";
                }
                else
                {
                    String maxID = row["MAXID"].ToString();
                    txtSupID.Text = maxID.Substring(0, 4) + String.Format("{0:000}", (int.Parse(maxID.Substring(4)) + 1));
                }
            }
        }

        private void Supply_Load(object sender, EventArgs e)
        {
            getNewID();
            txtCusName.Focus();
            dtLoad.Columns.Add("Item ID");
            dtLoad.Columns.Add("Name");
            dtLoad.Columns.Add("Quantity");
            this.reportViewer1.RefreshReport();
        }

        bool isValid = true;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(txtBarcode.Text) && nudQuantity.Value != 0)
            {
              
                foreach (DataGridViewRow row in dgvItem.Rows)
                {
                    if(txtBarcode.Text == row.Cells["Item ID"].Value.ToString())
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
                    Conn.cmd.CommandText = "SELECT * FROM Item WHERE ID=@ID";
                    Conn.cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtBarcode.Text;
                    Conn.sdr = Conn.cmd.ExecuteReader();

                    while (Conn.sdr.Read())
                    {
                        var dr = dtLoad.NewRow();
                        dr["Item ID"] = Conn.sdr.GetValue(0).ToString();
                        dr["Name"] = Conn.sdr.GetValue(1).ToString();
                        dr["Quantity"] = nudQuantity.Value.ToString();
                        dtLoad.Rows.Add(dr);
                        dgvItem.DataSource = dtLoad;
                    }

                    txtBarcode.Text = "";
                    txtBarcode.Focus();
                    nudQuantity.Value = 0;

                    if (!String.IsNullOrEmpty(txtCusName.Text))
                    {
                        btnSave.Visible = true;
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

        private void Clear()
        {
            txtCusName.Text = "";
            nudQuantity.Value = 0;
            txtBarcode.Text = "";
            dtLoad.Clear();
            dgvItem.DataSource = null;
            btnSave.Visible = false;
            txtCusName.Focus();
        }

        private void btnCl_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgvItem.SelectedRows.Count != 0)
            {
                dgvItem.Rows.RemoveAt(dgvItem.SelectedRows[0].Index);
            }

            if(dgvItem.Rows.Count == 0)
            {
                btnSave.Visible = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult saveConfirm = MessageBox.Show("Are you want to Save Supply NO " + txtSupID.Text + "?..", "Save Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (saveConfirm == DialogResult.Yes)
            {
                DBConn Conn = new DBConn();
                Conn.cmd.CommandText = "INSERT INTO Supply VALUES(@ID, @Name, @Date)";
                Conn.cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtSupID.Text;
                Conn.cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = txtCusName.Text;
                Conn.cmd.Parameters.Add("@Date",SqlDbType.Date).Value = DateTime.Now.ToString("yyyy-MM-dd");
                Conn.cmd.ExecuteNonQuery();

                foreach(DataGridViewRow row in dgvItem.Rows)
                {
                    DBConn ConnAss = new DBConn();
                    ConnAss.cmd.CommandText = "INSERT INTO Assign(SupID, ItemID, Status, Quantity) VALUES(@SupID, @ItemID, @Status, @Quantity)";
                    ConnAss.cmd.Parameters.Add("@SupID", SqlDbType.VarChar).Value = txtSupID.Text;
                    ConnAss.cmd.Parameters.Add("@ItemID", SqlDbType.VarChar).Value = row.Cells["Item ID"].Value;
                    ConnAss.cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = row.Cells["Quantity"].Value;
                    ConnAss.cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = "Pending";
                    ConnAss.cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Successfully Saved Supply No " + txtSupID.Text + "!!", "Successfully Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DBConn ConnRep = new DBConn();
                ConnRep.cmd.CommandText = "SELECT Supply.ID AS SupID, Supply.Customer, Supply.Date, Assign.ItemID, Item.Name, Assign.Quantity FROM Supply LEFT JOIN Assign ON Supply.ID = Assign.SupID LEFT JOIN Item ON Assign.ItemID = Item.ID WHERE Supply.ID=@ID";
                ConnRep.cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtSupID.Text;
                ConnRep.ada.SelectCommand = ConnRep.cmd;
                DataTable dt = new DataTable();
                ConnRep.ada.Fill(dt);

                ReportDataSource dataSource = new ReportDataSource("dsSupply",dt);
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(dataSource);
                reportViewer1.RefreshReport();
                Clear();
                txtCusName.Focus();

            }
            else
            {
                lblError.Text = "All field must be fill....!!";
                lblError.Visible = true;
            }
        }

        private void txtCusName_OnValueChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCusName.Text))
            {
                lblError.Visible = false;
            }
        }

        private void txtBarcode_OnValueChanged(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }
    }
}
