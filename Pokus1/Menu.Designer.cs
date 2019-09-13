namespace Pokus1
{
	partial class Menu
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
			this.Loader = new System.Windows.Forms.Button();
			this.Editor = new System.Windows.Forms.Button();
			this.NewGame = new System.Windows.Forms.Button();
			this.Options = new System.Windows.Forms.Button();
			this.Exit = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Loader
			// 
			this.Loader.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.Loader.Enabled = false;
			this.Loader.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.Loader.Location = new System.Drawing.Point(229, 104);
			this.Loader.Name = "Loader";
			this.Loader.Size = new System.Drawing.Size(178, 55);
			this.Loader.TabIndex = 1;
			this.Loader.Text = "Load";
			this.Loader.UseVisualStyleBackColor = true;
			this.Loader.Click += new System.EventHandler(this.Loader_Click);
			// 
			// Editor
			// 
			this.Editor.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.Editor.Enabled = false;
			this.Editor.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.Editor.Location = new System.Drawing.Point(229, 165);
			this.Editor.Name = "Editor";
			this.Editor.Size = new System.Drawing.Size(178, 55);
			this.Editor.TabIndex = 2;
			this.Editor.Text = "Editor";
			this.Editor.UseVisualStyleBackColor = true;
			// 
			// NewGame
			// 
			this.NewGame.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.NewGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.NewGame.Location = new System.Drawing.Point(229, 43);
			this.NewGame.Name = "NewGame";
			this.NewGame.Size = new System.Drawing.Size(178, 55);
			this.NewGame.TabIndex = 0;
			this.NewGame.Text = "New Game";
			this.NewGame.UseVisualStyleBackColor = true;
			this.NewGame.Click += new System.EventHandler(this.NewGame_Click);
			// 
			// Options
			// 
			this.Options.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.Options.Enabled = false;
			this.Options.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.Options.Location = new System.Drawing.Point(229, 226);
			this.Options.Name = "Options";
			this.Options.Size = new System.Drawing.Size(178, 55);
			this.Options.TabIndex = 3;
			this.Options.Text = "Options";
			this.Options.UseVisualStyleBackColor = true;
			// 
			// Exit
			// 
			this.Exit.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.Exit.Location = new System.Drawing.Point(253, 287);
			this.Exit.Name = "Exit";
			this.Exit.Size = new System.Drawing.Size(135, 45);
			this.Exit.TabIndex = 4;
			this.Exit.Text = "Exit";
			this.Exit.UseVisualStyleBackColor = true;
			this.Exit.Click += new System.EventHandler(this.Exit_Click_1);
			// 
			// Menu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Exit);
			this.Controls.Add(this.Options);
			this.Controls.Add(this.Loader);
			this.Controls.Add(this.Editor);
			this.Controls.Add(this.NewGame);
			this.Name = "Menu";
			this.Size = new System.Drawing.Size(622, 376);
			this.Load += new System.EventHandler(this.Menu_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button Loader;
		private System.Windows.Forms.Button Editor;
		private System.Windows.Forms.Button NewGame;
		private System.Windows.Forms.Button Options;
		private System.Windows.Forms.Button Exit;
	}
}
