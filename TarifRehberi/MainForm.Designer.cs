namespace TarifRehberi
{
	partial class MainForm
	{
		/// <summary>
		///Gerekli tasarımcı değişkeni.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///Kullanılan tüm kaynakları temizleyin.
		/// </summary>
		///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer üretilen kod

		/// <summary>
		/// Tasarımcı desteği için gerekli metot - bu metodun 
		///içeriğini kod düzenleyici ile değiştirmeyin.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.btnMalzemeKayit = new System.Windows.Forms.Button();
			this.btnTarifYonetimi = new System.Windows.Forms.Button();
			this.btnTarifleriListele = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// btnMalzemeKayit
			// 
			this.btnMalzemeKayit.BackColor = System.Drawing.Color.BurlyWood;
			this.btnMalzemeKayit.Location = new System.Drawing.Point(659, 418);
			this.btnMalzemeKayit.Name = "btnMalzemeKayit";
			this.btnMalzemeKayit.Size = new System.Drawing.Size(214, 59);
			this.btnMalzemeKayit.TabIndex = 2;
			this.btnMalzemeKayit.Text = "Malzemelerim";
			this.btnMalzemeKayit.UseVisualStyleBackColor = false;
			this.btnMalzemeKayit.Click += new System.EventHandler(this.btnMalzemeKayit_Click);
			// 
			// btnTarifYonetimi
			// 
			this.btnTarifYonetimi.BackColor = System.Drawing.Color.BurlyWood;
			this.btnTarifYonetimi.Location = new System.Drawing.Point(659, 310);
			this.btnTarifYonetimi.Name = "btnTarifYonetimi";
			this.btnTarifYonetimi.Size = new System.Drawing.Size(214, 59);
			this.btnTarifYonetimi.TabIndex = 3;
			this.btnTarifYonetimi.Text = "Tarif Ekle/Güncelle/Sil";
			this.btnTarifYonetimi.UseVisualStyleBackColor = false;
			this.btnTarifYonetimi.Click += new System.EventHandler(this.btnTarifYonetimi_Click);
			// 
			// btnTarifleriListele
			// 
			this.btnTarifleriListele.BackColor = System.Drawing.Color.BurlyWood;
			this.btnTarifleriListele.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnTarifleriListele.Location = new System.Drawing.Point(659, 199);
			this.btnTarifleriListele.Name = "btnTarifleriListele";
			this.btnTarifleriListele.Size = new System.Drawing.Size(214, 59);
			this.btnTarifleriListele.TabIndex = 4;
			this.btnTarifleriListele.Text = "Tarifleri Görüntüle";
			this.btnTarifleriListele.UseVisualStyleBackColor = false;
			this.btnTarifleriListele.Click += new System.EventHandler(this.btnTarifleriListele_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(994, 780);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 5;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(994, 780);
			this.Controls.Add(this.btnTarifleriListele);
			this.Controls.Add(this.btnTarifYonetimi);
			this.Controls.Add(this.btnMalzemeKayit);
			this.Controls.Add(this.pictureBox1);
			this.Name = "MainForm";
			this.Text = "MainForm";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnMalzemeKayit;
		private System.Windows.Forms.Button btnTarifYonetimi;
		private System.Windows.Forms.Button btnTarifleriListele;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}

