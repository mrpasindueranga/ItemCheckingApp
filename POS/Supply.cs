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
            ConnMax.cmd.CommandText = "SELECT MAX(ID) AS MAXID FROM Rent";
            ConnMax.ada.SelectCommand = ConnMax.cmd;
            DataTable dtItem = new DataTable();
            ConnMax.ada.Fill(dtItem);
            foreach (DataRow row in dtItem.Rows)
            {
                object isNull = row["MAXID"];
                if (isNull == DBNull.Value)
                {
                    txtSupID.Text = "Ren-001";
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

            cmbEmpID.Items.Clear();

            DBConn Conn = new DBConn();
            Conn.cmd.CommandText = "SELECT ID FROM Employee WHERE ID != 'EMP-001'";
            Conn.ada.SelectCommand = Conn.cmd;
            DataTable dtID = new DataTable();
            Conn.ada.Fill(dtID);
            cmbEmpID.Items.Add("--SELECT Agent ID--");
            cmbEmpID.SelectedIndex = 0;

            foreach (DataRow row in dtID.Rows)
            {
                cmbEmpID.Items.Add(row["ID"]);
            }

            getNewID();
            txtCusName.Focus();
            dtLoad.Columns.Add("Item ID");
            dtLoad.Columns.Add("Name");
            this.reportViewer1.RefreshReport();
        }

        bool isValid = true;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            itemAdd();
        }

        private void Clear()
        {
            txtCusName.Text = "";
            txtBarcode.Text = "";
            dtLoad.Clear();
            dgvItem.DataSource = null;
            dtLoad.Rows.Clear();
            lblError.Visible = false;
            btnSave.Visible = false;
            txtCusName.Focus();
            cmbEmpID.SelectedIndex = 0;
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
            if (!String.IsNullOrEmpty(txtCusName.Text) && cmbEmpID.SelectedIndex != 0 && dgvItem.Rows.Count != 0)
            {
                DialogResult saveConfirm = MessageBox.Show("Are you want to Save Receipt NO " + txtSupID.Text + "?..", "Save Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveConfirm == DialogResult.Yes)
                {
                    DBConn Conn = new DBConn();
                    Conn.cmd.CommandText = "INSERT INTO Rent(ID, Customer, RenDate, EmpID, Status) VALUES(@ID, @Customer, @RenDate, @EmpID, @Status)";
                    Conn.cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtSupID.Text;
                    Conn.cmd.Parameters.Add("@Customer", SqlDbType.VarChar).Value = txtCusName.Text.Trim();
                    Conn.cmd.Parameters.Add("@RenDate", SqlDbType.Date).Value = DateTime.Now.ToString("yyyy-MM-dd");
                    Conn.cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = cmbEmpID.Text;
                    Conn.cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = "Pending";
                    Conn.cmd.ExecuteNonQuery();

                    DBConn ConnEmp = new DBConn();
                    ConnEmp.cmd.CommandText = "UPDATE Employee SET Status=@Status WHERE ID=@ID";
                    ConnEmp.cmd.Parameters.Add("@Status",SqlDbType.VarChar).Value = "Unavailable";
                    ConnEmp.cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = cmbEmpID.Text;
                    ConnEmp.cmd.ExecuteNonQuery();

                    foreach (DataGridViewRow row in dgvItem.Rows)
                    {
                        DBConn ConnAss = new DBConn();
                        ConnAss.cmd.CommandText = "INSERT INTO Assign(RenID, ItemID) VALUES(@RenID, @ItemID)";
                        ConnAss.cmd.Parameters.Add("@RenID", SqlDbType.VarChar).Value = txtSupID.Text;
                        ConnAss.cmd.Parameters.Add("@ItemID", SqlDbType.VarChar).Value = row.Cells["Item ID"].Value;
                        ConnAss.cmd.ExecuteNonQuery();

                        DBConn ConnItm = new DBConn();
                        ConnItm.cmd.CommandText = "UPDATE Item SET Status=@Status WHERE ID=@ID";
                        ConnItm.cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = row.Cells["Item ID"].Value;
                        ConnItm.cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = "Stock Out";
                        ConnItm.cmd.ExecuteNonQuery(); 
                    }

                    MessageBox.Show("Successfully Saved Receipt No " + txtSupID.Text + "!!", "Successfully Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DBConn ConnRep = new DBConn();
                    ConnRep.cmd.CommandText = "SELECT Rent.ID AS SupID, Rent.Customer, Employee.Name As EmpName, Rent.RenDate AS Date, Assign.ItemID, Item.Name FROM Rent LEFT JOIN Assign ON Rent.ID = Assign.RenID LEFT JOIN Employee ON Rent.EmpID = Employee.ID LEFT JOIN Item ON Assign.ItemID = Item.ID WHERE Rent.ID=@ID";
                    ConnRep.cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtSupID.Text;
                    ConnRep.ada.SelectCommand = ConnRep.cmd;
                    DataTable dt = new DataTable();
                    ConnRep.ada.Fill(dt);

                    ReportDataSource dataSource = new ReportDataSource("dsSupply", dt);
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(dataSource);
                    reportViewer1.RefreshReport();
                    Clear();
                    txtCusName.Focus();

                    getNewID();
                }
            }
            else
            {
                lblError.Text = "Please Enter Valid Details";
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

        private void itemAdd()
        {
            if (!String.IsNullOrEmpty(txtBarcode.Text))
            {

                DBConn ConnStock = new DBConn();
                ConnStock.cmd.CommandText = "SELECT ID FROM Item WHERE ID=@ID AND Status=@Status";
                ConnStock.cmd.Parameters.Add("@ID",SqlDbType.VarChar).Value = txtBarcode.Text;
                ConnStock.cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = "Stock Out";
                ConnStock.ada.SelectCommand = ConnStock.cmd;

                DataTable dtStock = new DataTable();
                ConnStock.ada.Fill(dtStock);

                if (dtStock.Rows.Count == 0)
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
                }else
                {
                    lblError.Text = "This Item Out of Stock";
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text = "Please enter valid item details..";
                lblError.Visible = true;
            }

            txtBarcode.Text = "";
            txtBarcode.Focus();
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                itemAdd();
            }
        }
    }
}
