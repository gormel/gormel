namespace RoomsClient
{
	partial class LoginPanel
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.login = new System.Windows.Forms.Button();
			this.nameText = new System.Windows.Forms.TextBox();
			this.reg = new System.Windows.Forms.Button();
			this.passText = new System.Windows.Forms.TextBox();
			this.status = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.login.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.login.Location = new System.Drawing.Point(0, 265);
			this.login.Name = "button1";
			this.login.Size = new System.Drawing.Size(318, 23);
			this.login.TabIndex = 0;
			this.login.Text = "Login";
			this.login.UseVisualStyleBackColor = true;
			this.login.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox1
			// 
			this.nameText.Dock = System.Windows.Forms.DockStyle.Top;
			this.nameText.Location = new System.Drawing.Point(0, 13);
			this.nameText.Name = "textBox1";
			this.nameText.Size = new System.Drawing.Size(318, 20);
			this.nameText.TabIndex = 1;
			this.nameText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// button2
			// 
			this.reg.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.reg.Location = new System.Drawing.Point(0, 242);
			this.reg.Name = "button2";
			this.reg.Size = new System.Drawing.Size(318, 23);
			this.reg.TabIndex = 2;
			this.reg.Text = "Register";
			this.reg.UseVisualStyleBackColor = true;
			this.reg.Click += new System.EventHandler(this.button2_Click);
			// 
			// textBox2
			// 
			this.passText.Dock = System.Windows.Forms.DockStyle.Top;
			this.passText.Location = new System.Drawing.Point(0, 33);
			this.passText.Name = "textBox2";
			this.passText.PasswordChar = '*';
			this.passText.Size = new System.Drawing.Size(318, 20);
			this.passText.TabIndex = 3;
			this.passText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label1
			// 
			this.status.Dock = System.Windows.Forms.DockStyle.Top;
			this.status.Location = new System.Drawing.Point(0, 0);
			this.status.Name = "label1";
			this.status.Size = new System.Drawing.Size(318, 13);
			this.status.TabIndex = 4;
			// 
			// LoginPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.passText);
			this.Controls.Add(this.reg);
			this.Controls.Add(this.nameText);
			this.Controls.Add(this.login);
			this.Controls.Add(this.status);
			this.Name = "LoginPanel";
			this.Size = new System.Drawing.Size(318, 288);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button login;
		private System.Windows.Forms.TextBox nameText;
        private System.Windows.Forms.Button reg ;
        private System.Windows.Forms.TextBox passText;
		private System.Windows.Forms.Label status;
	}
}
