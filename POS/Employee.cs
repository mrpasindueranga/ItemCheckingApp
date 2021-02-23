using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace POS
{
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            txtSearch.Focus();
            DataLoad();
            getNewID();
        }

        public void DataLoad()
        {
            DBConn Conn = new DBConn();
            Conn.cmd.CommandText = "SELECT ID as 'Register NO', Name, Address, ContactNo as 'Contact No', Status FROM Employee";
            Conn.ada.SelectCommand = Conn.cmd;
            DataTable dtEmployee = new DataTable();
            Conn.ada.Fill(dtEmployee);

            dgvEmployee.DataSource = dtEmployee;
        }

        private void dgvEmployee_SelectionChanged(object sender, EventArgs e)
        {
            getDataFromDGV();
        }

        public void getDataFromDGV() {
            if (btnAdd.Visible == true)
            {
                txtName.Focus();
                foreach (DataGridViewRow row in dgvEmployee.SelectedRows)
                {
                    txtRegID.Text = row.Cells[0].Value.ToString();
                    txtName.Text = row.Cells[1].Value.ToString();
                    txtAddress.Text = row.Cells[2].Value.ToString();
                    txtContact.Text = row.Cells[3].Value.ToString();

                    if (row.Cells[4].Value.ToString() == "Unemployeed")
                    {
                        btnUpdate.Visible = true;
                        btnDel.Visible = false;
                    }
                    else if (row.Cells[0].Value.ToString() == "EMP-001")
                    {
                        btnUpdate.Visible = false;
                        btnDel.Visible = false;
                    }
                    else
                    {
                        btnDel.Visible = true;
                        btnUpdate.Visible = true;
                    }
                }
            }
        }

        public void getNewID()
        {
            DBConn ConnMax = new DBConn();
            ConnMax.cmd.CommandText = "SELECT MAX(ID) AS MAXID FROM Employee";
            ConnMax.ada.SelectCommand = ConnMax.cmd;
            DataTable dtItem = new DataTable();
            ConnMax.ada.Fill(dtItem);
            foreach (DataRow row in dtItem.Rows)
            {
                object isNull = row["MAXID"];
                if (isNull == DBNull.Value)
                {
                    txtRegID.Text = "EMP-001";
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

        public void Clear()
        {
            txtName.Text = "";
            txtAddress.Text = "";
            txtContact.Text = "";
            txtName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
                Regex conNo = new Regex(@"0[0-9]{9,9}");
                if (!String.IsNullOrEmpty(txtName.Text.Trim()) && !String.IsNullOrEmpty(txtAddress.Text.Trim()) && conNo.IsMatch(txtContact.Text.Trim()))
                {
                    DialogResult saveConfirm = MessageBox.Show("Are you want to Save Employee " + txtRegID.Text + "?..", "Save Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveConfirm == DialogResult.Yes)
                    {
                        DBConn Conn = new DBConn();
                        Conn.cmd.CommandText = "INSERT INTO EMPLOYEE(ID, Name, Address, ContactNo) VALUES(@ID,@Name,@Address,@Contact)";
                        Conn.cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtRegID.Text.Trim();
                        Conn.cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = txtName.Text.Trim();
                        Conn.cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = txtAddress.Text.Trim();
                        Conn.cmd.Parameters.Add("@Contact", SqlDbType.VarChar).Value = txtContact.Text.Trim();
                        Conn.cmd.ExecuteNonQuery();
                        getNewID();
                        DataLoad();
                        MessageBox.Show("Successfully Saved Employee" + txtRegID.Text + "!!", "Successfully Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                        txtName.Focus();
                    }
                }
                else
                {
                    lblError.Text = "Please check validity of data you entered!..";
                    lblError.Visible = true;
                }
        }

        private void btnCl_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
                Regex conNo = new Regex(@"0[0-9]{9,9}");
                if (!String.IsNullOrEmpty(txtName.Text.Trim()) && !String.IsNullOrEmpty(txtAddress.Text.Trim()) && conNo.IsMatch(txtContact.Text.Trim()))
                {
                    DialogResult upConfirm = MessageBox.Show("Are you want to Update Employee No " + txtRegID.Text + "?..", "Update Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (upConfirm == DialogResult.Yes)
                    {
                        DBConn Conn = new DBConn();
                        Conn.cmd.CommandText = "UPDATE Employee SET Name=@Name, Address=@Address, ContactNo=@Contact WHERE ID=@ID";
                        Conn.cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtRegID.Text.Trim();
                        Conn.cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = txtName.Text.Trim();
                        Conn.cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = txtAddress.Text.Trim();
                        Conn.cmd.Parameters.Add("@Contact", SqlDbType.VarChar).Value = txtContact.Text.Trim();
                        Conn.cmd.ExecuteNonQuery();
                        DataLoad();
                        MessageBox.Show("Successfully Updated Employee No " + txtRegID.Text + "!!", "Successfully Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        getDataFromDGV();
                        txtName.Focus();
                    }
                }
                else
                {
                    lblError.Text = "Please check validity of data you entered!..";
                    lblError.Visible = true;
                }
        }

        private void txtName_OnValueChanged(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        private void txtAddress_OnValueChanged(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        private void txtContact_OnValueChanged(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        private void txtUsername_OnValueChanged(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult delConfirm = MessageBox.Show("Are you want to Delete Employee No " + txtRegID.Text + "?..", "Delete Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (delConfirm == DialogResult.Yes)
            {
                DBConn Conn = new DBConn();
                Conn.cmd.CommandText = "Update Employee SET Status=@Status WHERE ID=@ID";
                Conn.cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = "Unemployeed";
                Conn.cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtRegID.Text;
                Conn.cmd.ExecuteNonQuery();
                DataLoad();
            }
            else
            {
                MessageBox.Show("Successfully Delete Employee No " + txtRegID.Text + "!!", "Successfully Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                DBConn Conn = new DBConn();
                Conn.cmd.CommandText = "SELECT ID as 'Register NO', Name, Address, ContactNo as 'Contact No', Status FROM Employee WHERE Name LIKE CONCAT('%',@Search,'%')";
                Conn.cmd.Parameters.Add("@Search", SqlDbType.VarChar).Value = txtSearch.Text.Trim();
                Conn.ada.SelectCommand = Conn.cmd;
                DataTable dtEmployee = new DataTable();
                Conn.ada.Fill(dtEmployee);

                dgvEmployee.DataSource = dtEmployee;
            }
        }

        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (new Regex("[0-9]").IsMatch(e.KeyChar.ToString()) || e.KeyChar == 8) { e.Handled = false; }
            else { e.Handled = true; }
        }

        private void txtSearch_OnValueChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                DataLoad();
            }
        }

        private void txtRegID_TextChanged(object sender, EventArgs e)
        {
            if (txtRegID.Text == "EMP-001" && dgvEmployee.SelectedRows.Count == 0)
            {
                btnCh.Visible = false;
            }
            else if (txtRegID.Text != "EMP-001" && btnSave.Visible == true)
            {
                btnCh.Visible = true;
            }
        }
    }
}
