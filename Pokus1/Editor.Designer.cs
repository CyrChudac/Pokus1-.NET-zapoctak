namespace Pokus1
{
	partial class Editor
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.Size = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.Height = new System.Windows.Forms.TextBox();
			this.Width = new System.Windows.Forms.TextBox();
			this.Wall = new System.Windows.Forms.Button();
			this.Options = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.Size.SuspendLayout();
			this.Options.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.Options);
			this.panel1.Controls.Add(this.Size);
			this.panel1.Location = new System.Drawing.Point(0, 390);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(799, 64);
			this.panel1.TabIndex = 0;
			// 
			// Size
			// 
			this.Size.Controls.Add(this.label2);
			this.Size.Controls.Add(this.Width);
			this.Size.Controls.Add(this.label1);
			this.Size.Controls.Add(this.Height);
			this.Size.Location = new System.Drawing.Point(12, 1);
			this.Size.Name = "Size";
			this.Size.Size = new System.Drawing.Size(155, 59);
			this.Size.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label2.Location = new System.Drawing.Point(6, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "Width:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label1.Location = new System.Drawing.Point(6, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(50, 16);
			this.label1.TabIndex = 7;
			this.label1.Text = "Height:";
			// 
			// Height
			// 
			this.Height.Location = new System.Drawing.Point(62, 30);
			this.Height.Name = "Height";
			this.Height.Size = new System.Drawing.Size(88, 20);
			this.Height.TabIndex = 6;
			// 
			// Width
			// 
			this.Width.Location = new System.Drawing.Point(62, 4);
			this.Width.Name = "Width";
			this.Width.Size = new System.Drawing.Size(88, 20);
			this.Width.TabIndex = 5;
			// 
			// Wall
			// 
			this.Wall.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Wall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.Wall.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.Wall.FlatAppearance.BorderSize = 0;
			this.Wall.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.Wall.Location = new System.Drawing.Point(11, 4);
			this.Wall.Name = "Wall";
			this.Wall.Size = new System.Drawing.Size(48, 41);
			this.Wall.TabIndex = 5;
			this.Wall.UseVisualStyleBackColor = false;
			// 
			// Options
			// 
			this.Options.Controls.Add(this.Wall);
			this.Options.Location = new System.Drawing.Point(186, 5);
			this.Options.Name = "Options";
			this.Options.Size = new System.Drawing.Size(375, 58);
			this.Options.TabIndex = 6;
			// 
			// Editor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.panel1);
			this.Name = "Editor";
			this.Text = "Editor";
			this.Load += new System.EventHandler(this.Editor_Load);
			this.panel1.ResumeLayout(false);
			this.Size.ResumeLayout(false);
			this.Size.PerformLayout();
			this.Options.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel Options;
		private System.Windows.Forms.Button Wall;
		private new System.Windows.Forms.Panel Size;
		private System.Windows.Forms.Label label2;
		private new System.Windows.Forms.TextBox Width;
		private System.Windows.Forms.Label label1;
		private new System.Windows.Forms.TextBox Height;
	}
}