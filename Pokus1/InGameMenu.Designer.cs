﻿namespace Pokus1
{
	partial class InGameMenu
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
			this.Continue = new System.Windows.Forms.Button();
			this.End = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Continue
			// 
			this.Continue.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.Continue.Location = new System.Drawing.Point(436, 57);
			this.Continue.Name = "Continue";
			this.Continue.Size = new System.Drawing.Size(99, 56);
			this.Continue.TabIndex = 0;
			this.Continue.TabStop = false;
			this.Continue.Text = "Continue";
			this.Continue.UseVisualStyleBackColor = true;
			this.Continue.Click += new System.EventHandler(this.Continue_Click);
			// 
			// End
			// 
			this.End.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.End.Location = new System.Drawing.Point(436, 231);
			this.End.Name = "End";
			this.End.Size = new System.Drawing.Size(99, 56);
			this.End.TabIndex = 1;
			this.End.TabStop = false;
			this.End.Text = "End";
			this.End.UseVisualStyleBackColor = true;
			this.End.Click += new System.EventHandler(this.End_Click);
			// 
			// InGameMenu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.End);
			this.Controls.Add(this.Continue);
			this.Name = "InGameMenu";
			this.Size = new System.Drawing.Size(605, 346);
			this.Load += new System.EventHandler(this.InGameMenu_Load);
			this.ResumeLayout(false);

		}

		#endregion
		
		private System.Windows.Forms.Button End;
		private System.Windows.Forms.Button Continue;
	}
}