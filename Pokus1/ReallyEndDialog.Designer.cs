namespace Pokus1
{
	partial class ReallyEndDialog
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
			this.label1 = new System.Windows.Forms.Label();
			this.yes = new System.Windows.Forms.Button();
			this.no = new System.Windows.Forms.Button();
			this.ramecek = new System.Windows.Forms.GroupBox();
			this.ramecek.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label1.Location = new System.Drawing.Point(69, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(253, 45);
			this.label1.TabIndex = 0;
			this.label1.Text = "REALLY EXIT?";
			// 
			// yes
			// 
			this.yes.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.yes.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.yes.Location = new System.Drawing.Point(44, 107);
			this.yes.Name = "yes";
			this.yes.Size = new System.Drawing.Size(125, 59);
			this.yes.TabIndex = 1;
			this.yes.Text = "YES";
			this.yes.UseVisualStyleBackColor = true;
			this.yes.Click += new System.EventHandler(this.yes_Click);
			// 
			// no
			// 
			this.no.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.no.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.no.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.no.Location = new System.Drawing.Point(207, 107);
			this.no.Name = "no";
			this.no.Size = new System.Drawing.Size(125, 59);
			this.no.TabIndex = 2;
			this.no.Text = "NO";
			this.no.UseVisualStyleBackColor = true;
			this.no.Click += new System.EventHandler(this.no_Click);
			// 
			// ramecek
			// 
			this.ramecek.Controls.Add(this.no);
			this.ramecek.Controls.Add(this.yes);
			this.ramecek.Controls.Add(this.label1);
			this.ramecek.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ramecek.Location = new System.Drawing.Point(0, 0);
			this.ramecek.Name = "ramecek";
			this.ramecek.Size = new System.Drawing.Size(375, 199);
			this.ramecek.TabIndex = 3;
			this.ramecek.TabStop = false;
			this.ramecek.Enter += new System.EventHandler(this.ramecek_Enter);
			// 
			// ReallyEndDialog
			// 
			this.AcceptButton = this.yes;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.no;
			this.ClientSize = new System.Drawing.Size(375, 199);
			this.Controls.Add(this.ramecek);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "ReallyEndDialog";
			this.Text = "ReallyEndDialog";
			this.Load += new System.EventHandler(this.ReallyEndDialog_Load);
			this.ramecek.ResumeLayout(false);
			this.ramecek.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button yes;
		private System.Windows.Forms.Button no;
		private System.Windows.Forms.GroupBox ramecek;
	}
}