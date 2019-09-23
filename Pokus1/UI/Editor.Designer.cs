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
			this.components = new System.ComponentModel.Container();
			this.panel1 = new System.Windows.Forms.Panel();
			this.load = new System.Windows.Forms.Button();
			this.panel3 = new System.Windows.Forms.Panel();
			this.passiveEnemy = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.knifeThrower = new System.Windows.Forms.Button();
			this.jumper = new System.Windows.Forms.Button();
			this.save = new System.Windows.Forms.Button();
			this.Options = new System.Windows.Forms.Panel();
			this.NoTile = new System.Windows.Forms.Button();
			this.Wall = new System.Windows.Forms.Button();
			this.MapSize = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.MapWidth = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.MapHeight = new System.Windows.Forms.TextBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.panel1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.Options.SuspendLayout();
			this.MapSize.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.load);
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.save);
			this.panel1.Controls.Add(this.Options);
			this.panel1.Controls.Add(this.MapSize);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 42);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(875, 64);
			this.panel1.TabIndex = 0;
			// 
			// load
			// 
			this.load.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.load.BackColor = System.Drawing.Color.WhiteSmoke;
			this.load.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.load.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.load.FlatAppearance.BorderSize = 0;
			this.load.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.load.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.load.Location = new System.Drawing.Point(748, 10);
			this.load.Name = "load";
			this.load.Size = new System.Drawing.Size(48, 41);
			this.load.TabIndex = 4;
			this.load.Text = "&Load";
			this.load.UseVisualStyleBackColor = false;
			this.load.Click += new System.EventHandler(this.load_Click);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.passiveEnemy);
			this.panel3.Location = new System.Drawing.Point(619, 5);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(93, 58);
			this.panel3.TabIndex = 3;
			// 
			// passiveEnemy
			// 
			this.passiveEnemy.BackColor = System.Drawing.Color.WhiteSmoke;
			this.passiveEnemy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.passiveEnemy.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.passiveEnemy.FlatAppearance.BorderSize = 0;
			this.passiveEnemy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.passiveEnemy.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.passiveEnemy.Location = new System.Drawing.Point(11, 4);
			this.passiveEnemy.Name = "passiveEnemy";
			this.passiveEnemy.Size = new System.Drawing.Size(48, 41);
			this.passiveEnemy.TabIndex = 0;
			this.passiveEnemy.Text = "P";
			this.passiveEnemy.UseVisualStyleBackColor = false;
			this.passiveEnemy.Click += new System.EventHandler(this.passiveEnemy_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.knifeThrower);
			this.panel2.Controls.Add(this.jumper);
			this.panel2.Location = new System.Drawing.Point(452, 5);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(161, 58);
			this.panel2.TabIndex = 2;
			// 
			// knifeThrower
			// 
			this.knifeThrower.BackColor = System.Drawing.Color.WhiteSmoke;
			this.knifeThrower.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.knifeThrower.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.knifeThrower.FlatAppearance.BorderSize = 0;
			this.knifeThrower.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.knifeThrower.Location = new System.Drawing.Point(78, 4);
			this.knifeThrower.Name = "knifeThrower";
			this.knifeThrower.Size = new System.Drawing.Size(48, 41);
			this.knifeThrower.TabIndex = 1;
			this.knifeThrower.Text = "K";
			this.knifeThrower.UseVisualStyleBackColor = false;
			this.knifeThrower.Click += new System.EventHandler(this.knifeThrower_Click);
			// 
			// jumper
			// 
			this.jumper.BackColor = System.Drawing.Color.WhiteSmoke;
			this.jumper.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.jumper.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.jumper.FlatAppearance.BorderSize = 0;
			this.jumper.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.jumper.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.jumper.Location = new System.Drawing.Point(11, 4);
			this.jumper.Name = "jumper";
			this.jumper.Size = new System.Drawing.Size(48, 41);
			this.jumper.TabIndex = 0;
			this.jumper.Text = "J";
			this.jumper.UseVisualStyleBackColor = false;
			this.jumper.Click += new System.EventHandler(this.jumper_Click);
			// 
			// save
			// 
			this.save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.save.BackColor = System.Drawing.Color.WhiteSmoke;
			this.save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.save.Enabled = false;
			this.save.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.save.FlatAppearance.BorderSize = 0;
			this.save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.save.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.save.Location = new System.Drawing.Point(812, 10);
			this.save.Name = "save";
			this.save.Size = new System.Drawing.Size(48, 41);
			this.save.TabIndex = 2;
			this.save.Text = "&Save";
			this.save.UseVisualStyleBackColor = false;
			this.save.Click += new System.EventHandler(this.save_Click);
			// 
			// Options
			// 
			this.Options.Controls.Add(this.NoTile);
			this.Options.Controls.Add(this.Wall);
			this.Options.Location = new System.Drawing.Point(186, 5);
			this.Options.Name = "Options";
			this.Options.Size = new System.Drawing.Size(251, 58);
			this.Options.TabIndex = 1;
			// 
			// NoTile
			// 
			this.NoTile.BackColor = System.Drawing.Color.WhiteSmoke;
			this.NoTile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.NoTile.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.NoTile.FlatAppearance.BorderSize = 0;
			this.NoTile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.NoTile.Location = new System.Drawing.Point(78, 4);
			this.NoTile.Name = "NoTile";
			this.NoTile.Size = new System.Drawing.Size(48, 41);
			this.NoTile.TabIndex = 1;
			this.NoTile.Text = "N";
			this.NoTile.UseVisualStyleBackColor = false;
			this.NoTile.Click += new System.EventHandler(this.NoTile_Click);
			// 
			// Wall
			// 
			this.Wall.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Wall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.Wall.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.Wall.FlatAppearance.BorderSize = 0;
			this.Wall.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.Wall.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.Wall.Location = new System.Drawing.Point(11, 4);
			this.Wall.Name = "Wall";
			this.Wall.Size = new System.Drawing.Size(48, 41);
			this.Wall.TabIndex = 0;
			this.Wall.Text = "W";
			this.Wall.UseVisualStyleBackColor = false;
			this.Wall.Click += new System.EventHandler(this.Wall_Click);
			// 
			// MapSize
			// 
			this.MapSize.Controls.Add(this.label2);
			this.MapSize.Controls.Add(this.MapWidth);
			this.MapSize.Controls.Add(this.label1);
			this.MapSize.Controls.Add(this.MapHeight);
			this.MapSize.Location = new System.Drawing.Point(12, 1);
			this.MapSize.Name = "MapSize";
			this.MapSize.Size = new System.Drawing.Size(155, 59);
			this.MapSize.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label2.Location = new System.Drawing.Point(6, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Width:";
			// 
			// MapWidth
			// 
			this.MapWidth.Location = new System.Drawing.Point(62, 4);
			this.MapWidth.Name = "MapWidth";
			this.MapWidth.Size = new System.Drawing.Size(88, 20);
			this.MapWidth.TabIndex = 0;
			this.MapWidth.Text = "15";
			this.MapWidth.TextChanged += new System.EventHandler(this.Width_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label1.Location = new System.Drawing.Point(6, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(50, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "Height:";
			// 
			// MapHeight
			// 
			this.MapHeight.Location = new System.Drawing.Point(62, 30);
			this.MapHeight.Name = "MapHeight";
			this.MapHeight.Size = new System.Drawing.Size(88, 20);
			this.MapHeight.TabIndex = 1;
			this.MapHeight.Text = "15";
			this.MapHeight.TextChanged += new System.EventHandler(this.MapHeight_TextChanged);
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// Editor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Name = "Editor";
			this.Size = new System.Drawing.Size(875, 106);
			this.Load += new System.EventHandler(this.Editor_Load);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Editor_MouseClick);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Editor_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Editor_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Editor_MouseUp);
			this.panel1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.Options.ResumeLayout(false);
			this.MapSize.ResumeLayout(false);
			this.MapSize.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel Options;
		private System.Windows.Forms.Button Wall;
		private System.Windows.Forms.Panel MapSize;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox MapWidth;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox MapHeight;
		private System.Windows.Forms.Button NoTile;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Button save;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button passiveEnemy;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button knifeThrower;
		private System.Windows.Forms.Button jumper;
		private System.Windows.Forms.Button load;
	}
}