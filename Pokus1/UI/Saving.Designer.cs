namespace Pokus1
{
	partial class Saving
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
			this.fileName = new System.Windows.Forms.TextBox();
			this.cancel = new System.Windows.Forms.Button();
			this.save = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.note = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// fileName
			// 
			this.fileName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fileName.Location = new System.Drawing.Point(25, 43);
			this.fileName.Name = "fileName";
			this.fileName.Size = new System.Drawing.Size(222, 20);
			this.fileName.TabIndex = 0;
			this.fileName.TextChanged += new System.EventHandler(this.fileName_TextChanged);
			// 
			// cancel
			// 
			this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cancel.Location = new System.Drawing.Point(25, 110);
			this.cancel.Name = "cancel";
			this.cancel.Size = new System.Drawing.Size(87, 60);
			this.cancel.TabIndex = 2;
			this.cancel.Text = "&Cancel";
			this.cancel.UseVisualStyleBackColor = true;
			this.cancel.Click += new System.EventHandler(this.cancel_Click);
			// 
			// save
			// 
			this.save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.save.Enabled = false;
			this.save.Location = new System.Drawing.Point(160, 110);
			this.save.Name = "save";
			this.save.Size = new System.Drawing.Size(87, 60);
			this.save.TabIndex = 1;
			this.save.Text = "&Save";
			this.save.UseVisualStyleBackColor = true;
			this.save.Click += new System.EventHandler(this.save_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(22, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Save name:";
			// 
			// note
			// 
			this.note.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.note.AutoSize = true;
			this.note.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.note.Location = new System.Drawing.Point(22, 75);
			this.note.Name = "note";
			this.note.Size = new System.Drawing.Size(112, 13);
			this.note.TabIndex = 3;
			this.note.Text = "Please, type a name...";
			// 
			// Saving
			// 
			this.AcceptButton = this.save;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancel;
			this.ClientSize = new System.Drawing.Size(275, 187);
			this.Controls.Add(this.note);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.save);
			this.Controls.Add(this.cancel);
			this.Controls.Add(this.fileName);
			this.MinimumSize = new System.Drawing.Size(291, 225);
			this.Name = "Saving";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Saving";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.Button save;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label note;
		public System.Windows.Forms.TextBox fileName;
	}
}