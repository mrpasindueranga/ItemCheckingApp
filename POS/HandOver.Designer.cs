namespace POS
{
    partial class HandOver
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblError = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.btnCl = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.bunifuCustomLabel2 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.dgvItem = new System.Windows.Forms.DataGridView();
            this.bunifuCustomLabel4 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.cmbSupID = new System.Windows.Forms.ComboBox();
            this.lblNotify = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.panelNotify = new System.Windows.Forms.Panel();
            this.btnCompare = new System.Windows.Forms.Button();
            this.txtBarcode = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.cmbItemID = new System.Windows.Forms.ComboBox();
            this.bunifuCustomLabel5 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.btnAddNote = new System.Windows.Forms.Button();
            this.btnComplete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItem)).BeginInit();
            this.SuspendLayout();
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.BackColor = System.Drawing.Color.Transparent;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(18, 507);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(307, 20);
            this.lblError.TabIndex = 68;
            this.lblError.Text = "Please check validity of data you entered!..";
            this.lblError.Visible = false;
            // 
            // btnCl
            // 
            this.btnCl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(105)))), ((int)(((byte)(179)))));
            this.btnCl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnCl.ForeColor = System.Drawing.Color.White;
            this.btnCl.Location = new System.Drawing.Point(677, 223);
            this.btnCl.Name = "btnCl";
            this.btnCl.Size = new System.Drawing.Size(109, 43);
            this.btnCl.TabIndex = 67;
            this.btnCl.Text = "Clear";
            this.btnCl.UseVisualStyleBackColor = false;
            this.btnCl.Click += new System.EventHandler(this.btnCl_Click);
            // 
            // btnDel
            // 
            this.btnDel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(31)))), ((int)(((byte)(42)))));
            this.btnDel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnDel.ForeColor = System.Drawing.Color.White;
            this.btnDel.Location = new System.Drawing.Point(677, 162);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(109, 43);
            this.btnDel.TabIndex = 66;
            this.btnDel.Text = "Delete";
            this.btnDel.UseVisualStyleBackColor = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(544, 94);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(109, 43);
            this.btnAdd.TabIndex = 65;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // bunifuCustomLabel2
            // 
            this.bunifuCustomLabel2.AutoSize = true;
            this.bunifuCustomLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bunifuCustomLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel2.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel2.Location = new System.Drawing.Point(18, 41);
            this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
            this.bunifuCustomLabel2.Size = new System.Drawing.Size(90, 24);
            this.bunifuCustomLabel2.TabIndex = 62;
            this.bunifuCustomLabel2.Text = "Supply ID";
            // 
            // nudQuantity
            // 
            this.nudQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.nudQuantity.Location = new System.Drawing.Point(460, 101);
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(58, 29);
            this.nudQuantity.TabIndex = 61;
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.AutoSize = true;
            this.bunifuCustomLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(376, 103);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(78, 24);
            this.bunifuCustomLabel1.TabIndex = 60;
            this.bunifuCustomLabel1.Text = "Quantity";
            // 
            // dgvItem
            // 
            this.dgvItem.AllowUserToAddRows = false;
            this.dgvItem.AllowUserToDeleteRows = false;
            this.dgvItem.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
            this.dgvItem.Location = new System.Drawing.Point(22, 162);
            this.dgvItem.Name = "dgvItem";
            this.dgvItem.ReadOnly = true;
            this.dgvItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItem.Size = new System.Drawing.Size(631, 318);
            this.dgvItem.TabIndex = 58;
            // 
            // bunifuCustomLabel4
            // 
            this.bunifuCustomLabel4.AutoSize = true;
            this.bunifuCustomLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bunifuCustomLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel4.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel4.Location = new System.Drawing.Point(18, 103);
            this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
            this.bunifuCustomLabel4.Size = new System.Drawing.Size(155, 24);
            this.bunifuCustomLabel4.TabIndex = 56;
            this.bunifuCustomLabel4.Text = "Barcode Number";
            // 
            // cmbSupID
            // 
            this.cmbSupID.BackColor = System.Drawing.Color.White;
            this.cmbSupID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupID.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSupID.FormattingEnabled = true;
            this.cmbSupID.Location = new System.Drawing.Point(196, 38);
            this.cmbSupID.Name = "cmbSupID";
            this.cmbSupID.Size = new System.Drawing.Size(457, 32);
            this.cmbSupID.TabIndex = 69;
            this.cmbSupID.SelectedIndexChanged += new System.EventHandler(this.cmbSupID_SelectedIndexChanged);
            // 
            // lblNotify
            // 
            this.lblNotify.AutoSize = true;
            this.lblNotify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblNotify.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotify.ForeColor = System.Drawing.Color.White;
            this.lblNotify.Location = new System.Drawing.Point(843, 9);
            this.lblNotify.Name = "lblNotify";
            this.lblNotify.Size = new System.Drawing.Size(189, 29);
            this.lblNotify.TabIndex = 70;
            this.lblNotify.Text = "Notification Area";
            // 
            // panelNotify
            // 
            this.panelNotify.AutoScroll = true;
            this.panelNotify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelNotify.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelNotify.Location = new System.Drawing.Point(806, 45);
            this.panelNotify.Name = "panelNotify";
            this.panelNotify.Size = new System.Drawing.Size(417, 728);
            this.panelNotify.TabIndex = 71;
            // 
            // btnCompare
            // 
            this.btnCompare.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(105)))), ((int)(((byte)(179)))));
            this.btnCompare.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompare.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompare.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnCompare.ForeColor = System.Drawing.Color.White;
            this.btnCompare.Location = new System.Drawing.Point(544, 495);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(109, 43);
            this.btnCompare.TabIndex = 72;
            this.btnCompare.Text = "Compare";
            this.btnCompare.UseVisualStyleBackColor = false;
            this.btnCompare.Visible = false;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // txtBarcode
            // 
            this.txtBarcode.BorderColorFocused = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txtBarcode.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtBarcode.BorderColorMouseHover = System.Drawing.SystemColors.ControlDarkDark;
            this.txtBarcode.BorderThickness = 3;
            this.txtBarcode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarcode.ForeColor = System.Drawing.Color.White;
            this.txtBarcode.isPassword = false;
            this.txtBarcode.Location = new System.Drawing.Point(196, 93);
            this.txtBarcode.Margin = new System.Windows.Forms.Padding(6);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(171, 45);
            this.txtBarcode.TabIndex = 73;
            this.txtBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBarcode.OnValueChanged += new System.EventHandler(this.txtBarcode_OnValueChanged);
            // 
            // cmbItemID
            // 
            this.cmbItemID.BackColor = System.Drawing.Color.White;
            this.cmbItemID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItemID.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbItemID.FormattingEnabled = true;
            this.cmbItemID.Location = new System.Drawing.Point(104, 585);
            this.cmbItemID.Name = "cmbItemID";
            this.cmbItemID.Size = new System.Drawing.Size(350, 32);
            this.cmbItemID.TabIndex = 74;
            // 
            // bunifuCustomLabel5
            // 
            this.bunifuCustomLabel5.AutoSize = true;
            this.bunifuCustomLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bunifuCustomLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel5.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel5.Location = new System.Drawing.Point(12, 588);
            this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
            this.bunifuCustomLabel5.Size = new System.Drawing.Size(67, 24);
            this.bunifuCustomLabel5.TabIndex = 75;
            this.bunifuCustomLabel5.Text = "Item ID";
            // 
            // txtNote
            // 
            this.txtNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtNote.Location = new System.Drawing.Point(104, 632);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(350, 141);
            this.txtNote.TabIndex = 76;
            // 
            // btnAddNote
            // 
            this.btnAddNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAddNote.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddNote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnAddNote.ForeColor = System.Drawing.Color.White;
            this.btnAddNote.Location = new System.Drawing.Point(480, 730);
            this.btnAddNote.Name = "btnAddNote";
            this.btnAddNote.Size = new System.Drawing.Size(109, 43);
            this.btnAddNote.TabIndex = 77;
            this.btnAddNote.Text = "Add Note";
            this.btnAddNote.UseVisualStyleBackColor = false;
            this.btnAddNote.Click += new System.EventHandler(this.btnAddNote_Click);
            // 
            // btnComplete
            // 
            this.btnComplete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnComplete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnComplete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnComplete.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.btnComplete.ForeColor = System.Drawing.Color.White;
            this.btnComplete.Location = new System.Drawing.Point(544, 495);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(109, 43);
            this.btnComplete.TabIndex = 78;
            this.btnComplete.Text = "Complete";
            this.btnComplete.UseVisualStyleBackColor = false;
            this.btnComplete.Visible = false;
            this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
            // 
            // HandOver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(1235, 920);
            this.Controls.Add(this.btnAddNote);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.bunifuCustomLabel5);
            this.Controls.Add(this.cmbItemID);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.panelNotify);
            this.Controls.Add(this.lblNotify);
            this.Controls.Add(this.cmbSupID);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnCl);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.bunifuCustomLabel2);
            this.Controls.Add(this.nudQuantity);
            this.Controls.Add(this.bunifuCustomLabel1);
            this.Controls.Add(this.dgvItem);
            this.Controls.Add(this.bunifuCustomLabel4);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.btnComplete);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HandOver";
            this.Text = "HandOver";
            this.Load += new System.EventHandler(this.HandOver_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuCustomLabel lblError;
        private System.Windows.Forms.Button btnCl;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel2;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private System.Windows.Forms.DataGridView dgvItem;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel4;
        private System.Windows.Forms.ComboBox cmbSupID;
        private Bunifu.Framework.UI.BunifuCustomLabel lblNotify;
        private System.Windows.Forms.Panel panelNotify;
        private System.Windows.Forms.Button btnCompare;
        private Bunifu.Framework.UI.BunifuMetroTextbox txtBarcode;
        private System.Windows.Forms.ComboBox cmbItemID;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel5;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Button btnAddNote;
        private System.Windows.Forms.Button btnComplete;
    }
}