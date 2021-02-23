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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult confirmExit = MessageBox.Show("Do you want to Logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmExit == DialogResult.Yes)
            {
                btnExit.Enabled = false;
                Application.Exit();
            }
        }

        public void getUsername(String Uname)
        {
            lblDname.Text = Uname.Trim();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            panelLoad.Controls.Clear();
            Employee empFrm = new Employee();
            empFrm.TopLevel = false;
            panelLoad.Controls.Add(empFrm);
            empFrm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            empFrm.Dock = DockStyle.Fill;
            empFrm.Show();
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            panelLoad.Controls.Clear();
            Item itemFrm = new Item();
            itemFrm.TopLevel = false;
            panelLoad.Controls.Add(itemFrm);
            itemFrm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            itemFrm.Dock = DockStyle.Fill;
            itemFrm.Show();
        }

        private void panelLoad_Paint(object sender, PaintEventArgs e)
        {
            Supply supplyFrm = new Supply();
            supplyFrm.TopLevel = false;
            panelLoad.Controls.Add(supplyFrm);
            supplyFrm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            supplyFrm.Dock = DockStyle.Fill;
            supplyFrm.Show();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            panelLoad.Controls.Clear();
            Supply supplyFrm = new Supply();
            supplyFrm.TopLevel = false;
            panelLoad.Controls.Add(supplyFrm);
            supplyFrm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            supplyFrm.Dock = DockStyle.Fill;
            supplyFrm.Show();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            panelLoad.Controls.Clear();
            HandOver handOverFrm = new HandOver();
            handOverFrm.TopLevel = false;
            panelLoad.Controls.Add(handOverFrm);
            handOverFrm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            handOverFrm.Dock = DockStyle.Fill;
            handOverFrm.Show();
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            panelLoad.Controls.Clear();
            Log logFrm = new Log();
            logFrm.TopLevel = false;
            panelLoad.Controls.Add(logFrm);
            logFrm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            logFrm.Dock = DockStyle.Fill;
            logFrm.Show();
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            panelLoad.Controls.Clear();
            Stock stockFrm = new Stock();
            stockFrm.TopLevel = false;
            panelLoad.Controls.Add(stockFrm);
            stockFrm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            stockFrm.Dock = DockStyle.Fill;
            stockFrm.Show();
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            panelLoad.Controls.Clear();
            Account accFrm = new Account();
            accFrm.TopLevel = false;
            panelLoad.Controls.Add(accFrm);
            accFrm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            accFrm.Dock = DockStyle.Fill;
            accFrm.Show();
        }
    }
}
