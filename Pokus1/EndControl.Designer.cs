namespace Pokus1
{
	partial class EndControl
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
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label1.Location = new System.Drawing.Point(44, 143);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(1017, 61);
			this.label1.TabIndex = 0;
			this.label1.Text = "";
			// 
			// WellDoneControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label1);
			this.Name = "WellDoneControl";
			this.Size = new System.Drawing.Size(491, 300);
			this.Load += new System.EventHandler(this.WellDoneControl_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WellDoneControl_KeyDown);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.WellDoneControl_MouseClick);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
	}
}
