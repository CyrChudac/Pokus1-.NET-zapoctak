namespace Pokus1
{
	partial class Options
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
			this.Back = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Back
			// 
			this.Back.Location = new System.Drawing.Point(182, 140);
			this.Back.Name = "Back";
			this.Back.Size = new System.Drawing.Size(177, 91);
			this.Back.TabIndex = 1;
			this.Back.Text = "Back";
			this.Back.UseVisualStyleBackColor = true;
			// 
			// Options2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Back);
			this.Name = "Options2";
			this.Size = new System.Drawing.Size(540, 370);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button Back;
	}
}
