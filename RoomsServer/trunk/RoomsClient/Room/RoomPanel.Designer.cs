namespace RoomsClient
{
	partial class RoomPanel
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
			this.messageTextBox = new System.Windows.Forms.TextBox();
			this.messagesTextBox = new System.Windows.Forms.TextBox();
			this.sendButton = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.filedPanel = new System.Windows.Forms.Panel();
			this.playingPlayerName = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.messageTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.messageTextBox.Location = new System.Drawing.Point(0, 75);
			this.messageTextBox.Name = "textBox1";
			this.messageTextBox.Size = new System.Drawing.Size(448, 20);
			this.messageTextBox.TabIndex = 0;
			// 
			// textBox2
			// 
			this.messagesTextBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.messagesTextBox.Location = new System.Drawing.Point(0, 0);
			this.messagesTextBox.Multiline = true;
			this.messagesTextBox.Name = "textBox2";
			this.messagesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.messagesTextBox.Size = new System.Drawing.Size(448, 46);
			this.messagesTextBox.TabIndex = 1;
			// 
			// button1
			// 
			this.sendButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.sendButton.Location = new System.Drawing.Point(0, 52);
			this.sendButton.Name = "button1";
			this.sendButton.Size = new System.Drawing.Size(448, 23);
			this.sendButton.TabIndex = 2;
			this.sendButton.Text = "send";
			this.sendButton.UseVisualStyleBackColor = true;
			this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.sendButton);
			this.panel1.Controls.Add(this.messagesTextBox);
			this.panel1.Controls.Add(this.messageTextBox);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 284);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(448, 95);
			this.panel1.TabIndex = 3;
			// 
			// filedPanel
			// 
			this.filedPanel.Location = new System.Drawing.Point(0, 24);
			this.filedPanel.Name = "filedPanel";
			this.filedPanel.Size = new System.Drawing.Size(448, 260);
			this.filedPanel.TabIndex = 4;
			// 
			// label1
			// 
			this.playingPlayerName.Dock = System.Windows.Forms.DockStyle.Top;
			this.playingPlayerName.Location = new System.Drawing.Point(0, 0);
			this.playingPlayerName.Name = "label1";
			this.playingPlayerName.Size = new System.Drawing.Size(448, 21);
			this.playingPlayerName.TabIndex = 5;
			this.playingPlayerName.Text = "label1";
			this.playingPlayerName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// RoomPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.playingPlayerName);
			this.Controls.Add(this.filedPanel);
			this.Controls.Add(this.panel1);
			this.Name = "RoomPanel";
			this.Size = new System.Drawing.Size(448, 379);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox messageTextBox;
		private System.Windows.Forms.TextBox messagesTextBox;
		private System.Windows.Forms.Button sendButton;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel filedPanel;
		private System.Windows.Forms.Label playingPlayerName;
	}
}
