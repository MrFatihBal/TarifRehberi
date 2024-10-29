namespace TarifRehberi
{
	partial class Form3
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
			this.btnEkle = new System.Windows.Forms.Button();
			this.btnGuncelle = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// btnEkle
			// 
			this.btnEkle.BackColor = System.Drawing.Color.PapayaWhip;
			this.btnEkle.Location = new System.Drawing.Point(40, 243);
			this.btnEkle.Name = "btnEkle";
			this.btnEkle.Size = new System.Drawing.Size(220, 66);
			this.btnEkle.TabIndex = 0;
			this.btnEkle.Text = "                             Tarif                               Ekle";
			this.btnEkle.UseVisualStyleBackColor = false;
			this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
			// 
			// btnGuncelle
			// 
			this.btnGuncelle.BackColor = System.Drawing.Color.PapayaWhip;
			this.btnGuncelle.Location = new System.Drawing.Point(838, 243);
			this.btnGuncelle.Name = "btnGuncelle";
			this.btnGuncelle.Size = new System.Drawing.Size(220, 62);
			this.btnGuncelle.TabIndex = 1;
			this.btnGuncelle.Text = "                          Tarif                          Güncelle/Sil";
			this.btnGuncelle.UseVisualStyleBackColor = false;
			this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(1094, 682);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
			// 
			// Form3
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1094, 682);
			this.Controls.Add(this.btnGuncelle);
			this.Controls.Add(this.btnEkle);
			this.Controls.Add(this.pictureBox1);
			this.Name = "Form3";
			this.Text = "Form3";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnEkle;
		private System.Windows.Forms.Button btnGuncelle;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}