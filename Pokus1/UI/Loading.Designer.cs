namespace Pokus1
{
	partial class Loading
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
			this.list = new System.Windows.Forms.ListBox();
			this.cancel = new System.Windows.Forms.Button();
			this.load = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// list
			// 
			this.list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.list.FormattingEnabled = true;
			this.list.Location = new System.Drawing.Point(3, 3);
			this.list.Name = "list";
			this.list.Size = new System.Drawing.Size(229, 173);
			this.list.TabIndex = 0;
			// 
			// cancel
			// 
			this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancel.Location = new System.Drawing.Point(3, 182);
			this.cancel.Name = "cancel";
			this.cancel.Size = new System.Drawing.Size(109, 66);
			this.cancel.TabIndex = 2;
			this.cancel.Text = "&Cancel";
			this.cancel.UseVisualStyleBackColor = true;
			this.cancel.Click += new System.EventHandler(this.cancel_Click);
			// 
			// load
			// 
			this.load.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.load.Location = new System.Drawing.Point(129, 182);
			this.load.Name = "load";
			this.load.Size = new System.Drawing.Size(103, 66);
			this.load.TabIndex = 1;
			this.load.Text = "&Load";
			this.load.UseVisualStyleBackColor = true;
			this.load.Click += new System.EventHandler(this.load_Click);
			// 
			// Loading
			// 
			this.AcceptButton = this.load;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancel;
			this.ClientSize = new System.Drawing.Size(236, 258);
			this.Controls.Add(this.load);
			this.Controls.Add(this.cancel);
			this.Controls.Add(this.list);
			this.MinimumSize = new System.Drawing.Size(252, 296);
			this.Name = "Loading";
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Loading";
			this.Load += new System.EventHandler(this.Loading_Load);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.Button load;
		public System.Windows.Forms.ListBox list;
	}
}
