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
            DialogResult confirmExit = MessageBox.Show("Do you want to Exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

        private void Dashboard_Load(object sender, EventArgs e)
        {
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

        private void timerDate_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString("yyyy/mm/dd HH:MM:ss");
        }
    }
}
