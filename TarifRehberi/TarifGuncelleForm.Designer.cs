namespace TarifUygulamasi
{
	partial class TarifGuncelleForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TarifGuncelleForm));
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.txtTarifAdi = new System.Windows.Forms.TextBox();
			this.txtMalzemeAdi = new System.Windows.Forms.TextBox();
			this.txtHazirlamaSuresi = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.comboKategori = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.btnMalzemeEkle = new System.Windows.Forms.Button();
			this.btnMalzemeDegistir = new System.Windows.Forms.Button();
			this.btnMalzemeSil = new System.Windows.Forms.Button();
			this.txtTalimat = new System.Windows.Forms.RichTextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.btnGuncelle = new System.Windows.Forms.Button();
			this.btnSil = new System.Windows.Forms.Button();
			this.dataGridView2 = new System.Windows.Forms.DataGridView();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.txtBirimFiyat = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.txtMalzemeMiktar = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.cmbMalzemeBirim = new System.Windows.Forms.ComboBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.cmbMalzeme = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ScrollBar;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaption;
			this.dataGridView1.Location = new System.Drawing.Point(12, 12);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(651, 771);
			this.dataGridView1.TabIndex = 0;
			this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
			this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
			// 
			// txtTarifAdi
			// 
			this.txtTarifAdi.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.txtTarifAdi.Location = new System.Drawing.Point(672, 53);
			this.txtTarifAdi.Name = "txtTarifAdi";
			this.txtTarifAdi.Size = new System.Drawing.Size(128, 20);
			this.txtTarifAdi.TabIndex = 1;
			// 
			// txtMalzemeAdi
			// 
			this.txtMalzemeAdi.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.txtMalzemeAdi.Location = new System.Drawing.Point(1102, 128);
			this.txtMalzemeAdi.Name = "txtMalzemeAdi";
			this.txtMalzemeAdi.Size = new System.Drawing.Size(256, 20);
			this.txtMalzemeAdi.TabIndex = 2;
			// 
			// txtHazirlamaSuresi
			// 
			this.txtHazirlamaSuresi.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.txtHazirlamaSuresi.Location = new System.Drawing.Point(672, 268);
			this.txtHazirlamaSuresi.Name = "txtHazirlamaSuresi";
			this.txtHazirlamaSuresi.Size = new System.Drawing.Size(128, 20);
			this.txtHazirlamaSuresi.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(669, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Tarif ismi";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(669, 132);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "kategori";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(669, 234);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(83, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Hazırlama suresi";
			// 
			// comboKategori
			// 
			this.comboKategori.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.comboKategori.FormattingEnabled = true;
			this.comboKategori.Location = new System.Drawing.Point(672, 161);
			this.comboKategori.Name = "comboKategori";
			this.comboKategori.Size = new System.Drawing.Size(121, 21);
			this.comboKategori.TabIndex = 7;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(1099, 96);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(69, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Malzeme ismi";
			// 
			// btnMalzemeEkle
			// 
			this.btnMalzemeEkle.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.btnMalzemeEkle.Location = new System.Drawing.Point(1400, 38);
			this.btnMalzemeEkle.Name = "btnMalzemeEkle";
			this.btnMalzemeEkle.Size = new System.Drawing.Size(116, 183);
			this.btnMalzemeEkle.TabIndex = 9;
			this.btnMalzemeEkle.Text = "ekle";
			this.btnMalzemeEkle.UseVisualStyleBackColor = false;
			this.btnMalzemeEkle.Click += new System.EventHandler(this.btnMalzemeEkle_Click);
			// 
			// btnMalzemeDegistir
			// 
			this.btnMalzemeDegistir.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.btnMalzemeDegistir.Location = new System.Drawing.Point(1400, 234);
			this.btnMalzemeDegistir.Name = "btnMalzemeDegistir";
			this.btnMalzemeDegistir.Size = new System.Drawing.Size(116, 175);
			this.btnMalzemeDegistir.TabIndex = 10;
			this.btnMalzemeDegistir.Text = "değiştir";
			this.btnMalzemeDegistir.UseVisualStyleBackColor = false;
			this.btnMalzemeDegistir.Click += new System.EventHandler(this.btnMalzemeDegistir_Click);
			// 
			// btnMalzemeSil
			// 
			this.btnMalzemeSil.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.btnMalzemeSil.Location = new System.Drawing.Point(1102, 419);
			this.btnMalzemeSil.Name = "btnMalzemeSil";
			this.btnMalzemeSil.Size = new System.Drawing.Size(414, 70);
			this.btnMalzemeSil.TabIndex = 11;
			this.btnMalzemeSil.Text = "sil";
			this.btnMalzemeSil.UseVisualStyleBackColor = false;
			this.btnMalzemeSil.Click += new System.EventHandler(this.btnMalzemeSil_Click);
			// 
			// txtTalimat
			// 
			this.txtTalimat.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.txtTalimat.Location = new System.Drawing.Point(672, 382);
			this.txtTalimat.Name = "txtTalimat";
			this.txtTalimat.Size = new System.Drawing.Size(266, 147);
			this.txtTalimat.TabIndex = 12;
			this.txtTalimat.Text = "";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(669, 342);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 13);
			this.label5.TabIndex = 13;
			this.label5.Text = "Talimat";
			// 
			// btnGuncelle
			// 
			this.btnGuncelle.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.btnGuncelle.Location = new System.Drawing.Point(672, 587);
			this.btnGuncelle.Name = "btnGuncelle";
			this.btnGuncelle.Size = new System.Drawing.Size(266, 65);
			this.btnGuncelle.TabIndex = 14;
			this.btnGuncelle.Text = "güncelle";
			this.btnGuncelle.UseVisualStyleBackColor = false;
			this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
			// 
			// btnSil
			// 
			this.btnSil.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.btnSil.Location = new System.Drawing.Point(672, 680);
			this.btnSil.Name = "btnSil";
			this.btnSil.Size = new System.Drawing.Size(266, 61);
			this.btnSil.TabIndex = 15;
			this.btnSil.Text = "sil";
			this.btnSil.UseVisualStyleBackColor = false;
			this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
			// 
			// dataGridView2
			// 
			this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.ScrollBar;
			this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView2.Location = new System.Drawing.Point(1102, 527);
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.Size = new System.Drawing.Size(435, 256);
			this.dataGridView2.TabIndex = 16;
			this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(985, -2);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(21, 796);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 17;
			this.pictureBox1.TabStop = false;
			// 
			// txtBirimFiyat
			// 
			this.txtBirimFiyat.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.txtBirimFiyat.Location = new System.Drawing.Point(1102, 278);
			this.txtBirimFiyat.Name = "txtBirimFiyat";
			this.txtBirimFiyat.Size = new System.Drawing.Size(256, 20);
			this.txtBirimFiyat.TabIndex = 21;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(1099, 249);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(54, 13);
			this.label6.TabIndex = 22;
			this.label6.Text = "Birim Fiyat";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(1099, 172);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(81, 13);
			this.label7.TabIndex = 24;
			this.label7.Text = "Malzeme Miktar";
			// 
			// txtMalzemeMiktar
			// 
			this.txtMalzemeMiktar.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.txtMalzemeMiktar.Location = new System.Drawing.Point(1102, 201);
			this.txtMalzemeMiktar.Name = "txtMalzemeMiktar";
			this.txtMalzemeMiktar.Size = new System.Drawing.Size(256, 20);
			this.txtMalzemeMiktar.TabIndex = 23;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(1099, 329);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(74, 13);
			this.label8.TabIndex = 25;
			this.label8.Text = "Malzeme Birim";
			// 
			// cmbMalzemeBirim
			// 
			this.cmbMalzemeBirim.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.cmbMalzemeBirim.ForeColor = System.Drawing.Color.Black;
			this.cmbMalzemeBirim.FormattingEnabled = true;
			this.cmbMalzemeBirim.Location = new System.Drawing.Point(1102, 359);
			this.cmbMalzemeBirim.Name = "cmbMalzemeBirim";
			this.cmbMalzemeBirim.Size = new System.Drawing.Size(256, 21);
			this.cmbMalzemeBirim.TabIndex = 26;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(1057, -2);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(22, 796);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox2.TabIndex = 27;
			this.pictureBox2.TabStop = false;
			// 
			// pictureBox3
			// 
			this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
			this.pictureBox3.Location = new System.Drawing.Point(1021, -2);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(21, 796);
			this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox3.TabIndex = 28;
			this.pictureBox3.TabStop = false;
			// 
			// cmbMalzeme
			// 
			this.cmbMalzeme.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.cmbMalzeme.FormattingEnabled = true;
			this.cmbMalzeme.Location = new System.Drawing.Point(1102, 54);
			this.cmbMalzeme.Name = "cmbMalzeme";
			this.cmbMalzeme.Size = new System.Drawing.Size(256, 21);
			this.cmbMalzeme.TabIndex = 29;
			this.cmbMalzeme.SelectedIndexChanged += new System.EventHandler(this.cmbMalzeme_SelectedIndexChanged);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.label9.Location = new System.Drawing.Point(1099, 38);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(69, 13);
			this.label9.TabIndex = 30;
			this.label9.Text = "Malzeme seç";
			// 
			// TarifGuncelleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.ClientSize = new System.Drawing.Size(1549, 795);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.cmbMalzeme);
			this.Controls.Add(this.pictureBox3);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.cmbMalzemeBirim);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.txtMalzemeMiktar);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.txtBirimFiyat);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.dataGridView2);
			this.Controls.Add(this.btnSil);
			this.Controls.Add(this.btnGuncelle);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtTalimat);
			this.Controls.Add(this.btnMalzemeSil);
			this.Controls.Add(this.btnMalzemeDegistir);
			this.Controls.Add(this.btnMalzemeEkle);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.comboKategori);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtHazirlamaSuresi);
			this.Controls.Add(this.txtMalzemeAdi);
			this.Controls.Add(this.txtTarifAdi);
			this.Controls.Add(this.dataGridView1);
			this.Name = "TarifGuncelleForm";
			this.Text = "TarifGuncelleForm";
			this.Load += new System.EventHandler(this.TarifGuncelleForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.TextBox txtTarifAdi;
		private System.Windows.Forms.TextBox txtMalzemeAdi;
		private System.Windows.Forms.TextBox txtHazirlamaSuresi;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboKategori;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnMalzemeEkle;
		private System.Windows.Forms.Button btnMalzemeDegistir;
		private System.Windows.Forms.Button btnMalzemeSil;
		private System.Windows.Forms.RichTextBox txtTalimat;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnGuncelle;
		private System.Windows.Forms.Button btnSil;
		private System.Windows.Forms.DataGridView dataGridView2;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TextBox txtBirimFiyat;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtMalzemeMiktar;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox cmbMalzemeBirim;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.ComboBox cmbMalzeme;
		private System.Windows.Forms.Label label9;
	}
}