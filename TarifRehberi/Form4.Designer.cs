namespace TarifRehberi
{
	partial class Form4
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
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.txtMalzemeAdi = new System.Windows.Forms.TextBox();
			this.txtMalzemeMiktar = new System.Windows.Forms.TextBox();
			this.txtBirimFiyat = new System.Windows.Forms.TextBox();
			this.btnEkle = new System.Windows.Forms.Button();
			this.btnGuncelle = new System.Windows.Forms.Button();
			this.btnSil = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBoxMalzemeBirim = new System.Windows.Forms.ComboBox();
			this.comboBoxMalzemeler = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ScrollBar;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.dataGridView1.Location = new System.Drawing.Point(413, 12);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(490, 651);
			this.dataGridView1.TabIndex = 0;
			this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
			// 
			// txtMalzemeAdi
			// 
			this.txtMalzemeAdi.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.txtMalzemeAdi.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.txtMalzemeAdi.Location = new System.Drawing.Point(39, 315);
			this.txtMalzemeAdi.Name = "txtMalzemeAdi";
			this.txtMalzemeAdi.Size = new System.Drawing.Size(167, 20);
			this.txtMalzemeAdi.TabIndex = 1;
			// 
			// txtMalzemeMiktar
			// 
			this.txtMalzemeMiktar.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.txtMalzemeMiktar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.txtMalzemeMiktar.Location = new System.Drawing.Point(39, 400);
			this.txtMalzemeMiktar.Name = "txtMalzemeMiktar";
			this.txtMalzemeMiktar.Size = new System.Drawing.Size(167, 20);
			this.txtMalzemeMiktar.TabIndex = 2;
			// 
			// txtBirimFiyat
			// 
			this.txtBirimFiyat.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.txtBirimFiyat.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.txtBirimFiyat.Location = new System.Drawing.Point(39, 581);
			this.txtBirimFiyat.Name = "txtBirimFiyat";
			this.txtBirimFiyat.Size = new System.Drawing.Size(167, 20);
			this.txtBirimFiyat.TabIndex = 4;
			// 
			// btnEkle
			// 
			this.btnEkle.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.btnEkle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.btnEkle.Location = new System.Drawing.Point(279, 306);
			this.btnEkle.Name = "btnEkle";
			this.btnEkle.Size = new System.Drawing.Size(75, 54);
			this.btnEkle.TabIndex = 5;
			this.btnEkle.Text = "Ekle";
			this.btnEkle.UseVisualStyleBackColor = false;
			this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
			// 
			// btnGuncelle
			// 
			this.btnGuncelle.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.btnGuncelle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.btnGuncelle.Location = new System.Drawing.Point(279, 549);
			this.btnGuncelle.Name = "btnGuncelle";
			this.btnGuncelle.Size = new System.Drawing.Size(75, 52);
			this.btnGuncelle.TabIndex = 6;
			this.btnGuncelle.Text = "Güncelle";
			this.btnGuncelle.UseVisualStyleBackColor = false;
			this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
			// 
			// btnSil
			// 
			this.btnSil.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.btnSil.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.btnSil.Location = new System.Drawing.Point(279, 425);
			this.btnSil.Name = "btnSil";
			this.btnSil.Size = new System.Drawing.Size(75, 53);
			this.btnSil.TabIndex = 7;
			this.btnSil.Text = "Sil";
			this.btnSil.UseVisualStyleBackColor = false;
			this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label1.Location = new System.Drawing.Point(39, 289);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(67, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Malzeme Adı";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label2.Location = new System.Drawing.Point(39, 374);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Malzeme Miktarı";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label3.Location = new System.Drawing.Point(39, 465);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(79, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Malzeme  Birimi";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label4.Location = new System.Drawing.Point(39, 556);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(54, 13);
			this.label4.TabIndex = 11;
			this.label4.Text = "Birim Fiyat";
			// 
			// comboBoxMalzemeBirim
			// 
			this.comboBoxMalzemeBirim.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.comboBoxMalzemeBirim.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.comboBoxMalzemeBirim.FormattingEnabled = true;
			this.comboBoxMalzemeBirim.Location = new System.Drawing.Point(42, 494);
			this.comboBoxMalzemeBirim.Name = "comboBoxMalzemeBirim";
			this.comboBoxMalzemeBirim.Size = new System.Drawing.Size(164, 21);
			this.comboBoxMalzemeBirim.TabIndex = 12;
			// 
			// comboBoxMalzemeler
			// 
			this.comboBoxMalzemeler.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.comboBoxMalzemeler.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.comboBoxMalzemeler.FormattingEnabled = true;
			this.comboBoxMalzemeler.Location = new System.Drawing.Point(39, 44);
			this.comboBoxMalzemeler.Name = "comboBoxMalzemeler";
			this.comboBoxMalzemeler.Size = new System.Drawing.Size(121, 21);
			this.comboBoxMalzemeler.TabIndex = 13;
			this.comboBoxMalzemeler.SelectedIndexChanged += new System.EventHandler(this.comboBoxMalzemeler_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label5.Location = new System.Drawing.Point(36, 9);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(59, 13);
			this.label5.TabIndex = 14;
			this.label5.Text = "malzemeler";
			// 
			// Form4
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.ClientSize = new System.Drawing.Size(910, 675);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.comboBoxMalzemeler);
			this.Controls.Add(this.comboBoxMalzemeBirim);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnSil);
			this.Controls.Add(this.btnGuncelle);
			this.Controls.Add(this.btnEkle);
			this.Controls.Add(this.txtBirimFiyat);
			this.Controls.Add(this.txtMalzemeMiktar);
			this.Controls.Add(this.txtMalzemeAdi);
			this.Controls.Add(this.dataGridView1);
			this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.Name = "Form4";
			this.Text = "Form4";
			this.Load += new System.EventHandler(this.Form4_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.TextBox txtMalzemeAdi;
		private System.Windows.Forms.TextBox txtMalzemeMiktar;
		private System.Windows.Forms.TextBox txtBirimFiyat;
		private System.Windows.Forms.Button btnEkle;
		private System.Windows.Forms.Button btnGuncelle;
		private System.Windows.Forms.Button btnSil;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBoxMalzemeBirim;
		private System.Windows.Forms.ComboBox comboBoxMalzemeler;
		private System.Windows.Forms.Label label5;
	}
}