namespace Pokus1
{
	partial class CharacterStats
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
			this.CharName = new System.Windows.Forms.Label();
			this.Health = new System.Windows.Forms.Label();
			this.Picture = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
			this.SuspendLayout();
			// 
			// CharName
			// 
			this.CharName.Location = new System.Drawing.Point(13, 11);
			this.CharName.Name = "CharName";
			this.CharName.Size = new System.Drawing.Size(118, 23);
			this.CharName.TabIndex = 0;
			this.CharName.Text = "label1";
			// 
			// Health
			// 
			this.Health.Location = new System.Drawing.Point(13, 44);
			this.Health.Name = "Health";
			this.Health.Size = new System.Drawing.Size(121, 24);
			this.Health.TabIndex = 1;
			// 
			// Picture
			// 
			this.Picture.Location = new System.Drawing.Point(16, 71);
			this.Picture.Name = "Picture";
			this.Picture.Size = new System.Drawing.Size(71, 65);
			this.Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.Picture.TabIndex = 2;
			this.Picture.TabStop = false;
			// 
			// Character
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Picture);
			this.Controls.Add(this.Health);
			this.Controls.Add(this.CharName);
			this.Name = "Character";
			this.Size = new System.Drawing.Size(151, 157);
			this.Load += new System.EventHandler(this.Character_Load);
			((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label CharName;
		private System.Windows.Forms.Label Health;
		private System.Windows.Forms.PictureBox Picture;
	}
}
