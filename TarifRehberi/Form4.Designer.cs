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
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(281, 12);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(507, 426);
			this.dataGridView1.TabIndex = 0;
			// 
			// txtMalzemeAdi
			// 
			this.txtMalzemeAdi.Location = new System.Drawing.Point(12, 38);
			this.txtMalzemeAdi.Name = "txtMalzemeAdi";
			this.txtMalzemeAdi.Size = new System.Drawing.Size(100, 20);
			this.txtMalzemeAdi.TabIndex = 1;
			// 
			// txtMalzemeMiktar
			// 
			this.txtMalzemeMiktar.Location = new System.Drawing.Point(12, 123);
			this.txtMalzemeMiktar.Name = "txtMalzemeMiktar";
			this.txtMalzemeMiktar.Size = new System.Drawing.Size(100, 20);
			this.txtMalzemeMiktar.TabIndex = 2;
			// 
			// txtBirimFiyat
			// 
			this.txtBirimFiyat.Location = new System.Drawing.Point(12, 304);
			this.txtBirimFiyat.Name = "txtBirimFiyat";
			this.txtBirimFiyat.Size = new System.Drawing.Size(100, 20);
			this.txtBirimFiyat.TabIndex = 4;
			// 
			// btnEkle
			// 
			this.btnEkle.Location = new System.Drawing.Point(158, 73);
			this.btnEkle.Name = "btnEkle";
			this.btnEkle.Size = new System.Drawing.Size(75, 23);
			this.btnEkle.TabIndex = 5;
			this.btnEkle.Text = "Ekle";
			this.btnEkle.UseVisualStyleBackColor = true;
			this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
			// 
			// btnGuncelle
			// 
			this.btnGuncelle.Location = new System.Drawing.Point(158, 161);
			this.btnGuncelle.Name = "btnGuncelle";
			this.btnGuncelle.Size = new System.Drawing.Size(75, 23);
			this.btnGuncelle.TabIndex = 6;
			this.btnGuncelle.Text = "Güncelle";
			this.btnGuncelle.UseVisualStyleBackColor = true;
			this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
			// 
			// btnSil
			// 
			this.btnSil.Location = new System.Drawing.Point(158, 251);
			this.btnSil.Name = "btnSil";
			this.btnSil.Size = new System.Drawing.Size(75, 23);
			this.btnSil.TabIndex = 7;
			this.btnSil.Text = "Sil";
			this.btnSil.UseVisualStyleBackColor = true;
			this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(67, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Malzeme Adı";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 97);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Malzeme Miktarı";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 188);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(79, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Malzeme  Birimi";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 279);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(54, 13);
			this.label4.TabIndex = 11;
			this.label4.Text = "Birim Fiyat";
			// 
			// comboBoxMalzemeBirim
			// 
			this.comboBoxMalzemeBirim.FormattingEnabled = true;
			this.comboBoxMalzemeBirim.Location = new System.Drawing.Point(15, 217);
			this.comboBoxMalzemeBirim.Name = "comboBoxMalzemeBirim";
			this.comboBoxMalzemeBirim.Size = new System.Drawing.Size(97, 21);
			this.comboBoxMalzemeBirim.TabIndex = 12;
			this.comboBoxMalzemeBirim.SelectedIndexChanged += new System.EventHandler(this.comboBoxMalzemeBirim_SelectedIndexChanged);
			// 
			// Form4
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
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
	}
}