namespace LdapReader
{
    partial class Form1
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
            this.richTextBoxMessage = new System.Windows.Forms.RichTextBox();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.labelPath = new System.Windows.Forms.Label();
            this.labelAccount = new System.Windows.Forms.Label();
            this.textBoxAccount = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.dataGridViewAttribute = new System.Windows.Forms.DataGridView();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxDnToDelete = new System.Windows.Forms.TextBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttribute)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBoxMessage
            // 
            this.richTextBoxMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBoxMessage.Location = new System.Drawing.Point(0, 506);
            this.richTextBoxMessage.Name = "richTextBoxMessage";
            this.richTextBoxMessage.Size = new System.Drawing.Size(1008, 155);
            this.richTextBoxMessage.TabIndex = 1;
            this.richTextBoxMessage.Text = "";
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(46, 6);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(438, 22);
            this.textBoxPath.TabIndex = 2;
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(12, 9);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(28, 12);
            this.labelPath.TabIndex = 3;
            this.labelPath.Text = "Path:";
            // 
            // labelAccount
            // 
            this.labelAccount.AutoSize = true;
            this.labelAccount.Location = new System.Drawing.Point(490, 12);
            this.labelAccount.Name = "labelAccount";
            this.labelAccount.Size = new System.Drawing.Size(47, 12);
            this.labelAccount.TabIndex = 4;
            this.labelAccount.Text = "Account:";
            // 
            // textBoxAccount
            // 
            this.textBoxAccount.Location = new System.Drawing.Point(543, 9);
            this.textBoxAccount.Name = "textBoxAccount";
            this.textBoxAccount.Size = new System.Drawing.Size(120, 22);
            this.textBoxAccount.TabIndex = 5;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(669, 12);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(51, 12);
            this.labelPassword.TabIndex = 6;
            this.labelPassword.Text = "Password:";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(726, 9);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(120, 22);
            this.textBoxPassword.TabIndex = 7;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // dataGridViewAttribute
            // 
            this.dataGridViewAttribute.AllowUserToAddRows = false;
            this.dataGridViewAttribute.AllowUserToDeleteRows = false;
            this.dataGridViewAttribute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewAttribute.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridViewAttribute.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAttribute.Location = new System.Drawing.Point(557, 157);
            this.dataGridViewAttribute.Name = "dataGridViewAttribute";
            this.dataGridViewAttribute.ReadOnly = true;
            this.dataGridViewAttribute.RowTemplate.Height = 24;
            this.dataGridViewAttribute.Size = new System.Drawing.Size(451, 343);
            this.dataGridViewAttribute.TabIndex = 9;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(0, 157);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(432, 343);
            this.treeView1.TabIndex = 10;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(852, 9);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(75, 23);
            this.buttonLogin.TabIndex = 2;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "DN:";
            // 
            // textBoxDnToDelete
            // 
            this.textBoxDnToDelete.Location = new System.Drawing.Point(43, 40);
            this.textBoxDnToDelete.Name = "textBoxDnToDelete";
            this.textBoxDnToDelete.Size = new System.Drawing.Size(803, 22);
            this.textBoxDnToDelete.TabIndex = 18;
            this.textBoxDnToDelete.Text = "OU=Soetek,DC=iac,DC=com";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(930, 40);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 19;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(852, 40);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 1;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.buttonSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 661);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.textBoxDnToDelete);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.dataGridViewAttribute);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.textBoxAccount);
            this.Controls.Add(this.labelAccount);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.richTextBoxMessage);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttribute)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBoxMessage;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Label labelAccount;
        private System.Windows.Forms.TextBox textBoxAccount;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.DataGridView dataGridViewAttribute;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxDnToDelete;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonSearch;
    }
}

