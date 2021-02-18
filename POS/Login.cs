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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        public String Uname;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            String username = txtUsername.Text.Trim();
            String password = txtPassword.Text.Trim();
            DBConn Conn = new DBConn();
            Conn.cmd.CommandText = "SELECT * FROM Employee WHERE Uname = @Uname AND Passwd = @Passwd";
            Conn.cmd.Parameters.Add("@Uname",SqlDbType.VarChar).Value = username;
            Conn.cmd.Parameters.Add("@Passwd",SqlDbType.VarChar).Value = password;
            Conn.ada.SelectCommand = Conn.cmd;
            DataTable CredTable = new DataTable();
            Conn.ada.Fill(CredTable);

            if (CredTable.Rows.Count > 0)
            {
                Dashboard DashFrm = new Dashboard();
                DashFrm.getUsername(username);
                DashFrm.Show();
                this.Hide();
            }
            else
            {
                lblError.Visible = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult confirmExit = MessageBox.Show("Do you want to Exit?","Confirm Exit",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(confirmExit == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void txtUsername_OnValueChanged(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        private void txtPassword_OnValueChanged(object sender, EventArgs e)
        {
            txtPassword.isPassword = true;
            lblError.Visible = false;
        }
    }
}
