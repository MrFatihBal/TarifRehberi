using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TarifRehberi
{
	public partial class TarifEkleForm : Form
	{
		public string connectionString;
		private SqlConnection baglanti;
		private List<string> tarifDetaylari = new List<string>();

		public TarifEkleForm()
		{
			InitializeComponent();
			connectionString = ConfigurationManager.ConnectionStrings["TarifRehberiContext"].ConnectionString;
			baglanti = new SqlConnection(connectionString);
		}

		private void TarifEkleForm_Load(object sender, EventArgs e)
		{
			// ComboBox'ları doldur
			cmbHazirlamaSuresi.Items.AddRange(new string[] { "Dakika", "Saat" });
			cmbHazirlamaSuresi.SelectedIndex = 0;

			cmbKategori.Items.AddRange(new string[] { "Çorba", "Ana Yemek", "Tatlı", "Salata", "Aperatif" });

			cmbMalzemeBirim.Items.AddRange(new string[] { "Gram", "Kilogram", "Litre", "Mililitre", "Adet", "Yemek Kaşığı", "Çay Kaşığı" });

			// Yeni buton ekle
			Button btnKaydet = new Button();
			btnKaydet.Text = "Veritabanına Kaydet";
			btnKaydet.Location = new Point(lstTarifDetaylari.Left, lstTarifDetaylari.Bottom + 10);
			btnKaydet.Click += new EventHandler(btnKaydet_Click);
			this.Controls.Add(btnKaydet);
		}

		private void btnIsimEkle_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(txtTarifAdi.Text))
			{
				tarifDetaylari.Add("Tarif Adı: " + txtTarifAdi.Text);
				GuncelleListBox();
				txtTarifAdi.Clear();
			}
			else
			{
				MessageBox.Show("Lütfen tarif adı girin.");
			}
		}

		private void btnHazirlamaSuresiEkle_Click(object sender, EventArgs e)
		{
			if (int.TryParse(txtHazirlamaSuresi.Text, out int sure))
			{
				string birim = cmbHazirlamaSuresi.SelectedItem.ToString();
				tarifDetaylari.Add($"Hazırlama Süresi: {sure} {birim}");
				GuncelleListBox();
				txtHazirlamaSuresi.Clear();
			}
			else
			{
				MessageBox.Show("Lütfen geçerli bir süre girin.");
			}
		}

		private void btnTalimatEkle_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(rtbTalimat.Text))
			{
				tarifDetaylari.Add("Talimat: " + rtbTalimat.Text);
				GuncelleListBox();
				rtbTalimat.Clear();
			}
			else
			{
				MessageBox.Show("Lütfen talimat girin.");
			}
		}

		private void btnKategoriEkle_Click(object sender, EventArgs e)
		{
			if (cmbKategori.SelectedIndex != -1)
			{
				tarifDetaylari.Add("Kategori: " + cmbKategori.SelectedItem.ToString());
				GuncelleListBox();
			}
			else
			{
				MessageBox.Show("Lütfen bir kategori seçin.");
			}
		}

		private void btnMalzemeEkle_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(txtMalzemeAdi.Text) && int.TryParse(txtMalzemeMiktar.Text, out int miktar) && cmbMalzemeBirim.SelectedIndex != -1)
			{
				string malzeme = $"Malzeme: {txtMalzemeAdi.Text}, {miktar} {cmbMalzemeBirim.SelectedItem}";
				tarifDetaylari.Add(malzeme);
				GuncelleListBox();
				txtMalzemeAdi.Clear();
				txtMalzemeMiktar.Clear();
				cmbMalzemeBirim.SelectedIndex = -1;
			}
			else
			{
				MessageBox.Show("Lütfen malzeme adı, miktarı ve birimini doğru şekilde girin.");
			}
		}

		private void GuncelleListBox()
		{
			lstTarifDetaylari.Items.Clear();
			foreach (string detay in tarifDetaylari)
			{
				lstTarifDetaylari.Items.Add(detay);
			}
		}

		private void btnKaydet_Click(object sender, EventArgs e)
		{
			try
			{
				baglanti.Open();

				// Önce Tarifler tablosuna ekleme yap
				string tarifSorgu = "INSERT INTO Tarifler (TarifAdi, Kategori, Hazirlamasuresi, Talimatlar) VALUES (@TarifAdi, @Kategori, @Hazirlamasuresi, @Talimatlar); SELECT SCOPE_IDENTITY();";
				SqlCommand tarifKomut = new SqlCommand(tarifSorgu, baglanti);

				tarifKomut.Parameters.AddWithValue("@TarifAdi", tarifDetaylari.FirstOrDefault(i => i.StartsWith("Tarif Adı:"))?.Replace("Tarif Adı: ", ""));
				tarifKomut.Parameters.AddWithValue("@Kategori", tarifDetaylari.FirstOrDefault(i => i.StartsWith("Kategori:"))?.Replace("Kategori: ", ""));

				// "Hazırlama Süresi" sadece sayı olmalı (birim ComboBox'tan seçiliyor, ama veritabanına eklenmiyor)
				string hazirlamaSuresiMetni = tarifDetaylari.FirstOrDefault(i => i.StartsWith("Hazırlama Süresi:"))?.Replace("Hazırlama Süresi: ", "");
				if (hazirlamaSuresiMetni != null)
				{
					// Hazırlama süresinin sadece sayı kısmını veritabanına ekliyoruz
					int hazirlamaSuresi = int.Parse(hazirlamaSuresiMetni.Split(' ')[0]);  // İlk kısmı sayı
					tarifKomut.Parameters.AddWithValue("@Hazirlamasuresi", hazirlamaSuresi);
				}
				else
				{
					tarifKomut.Parameters.AddWithValue("@Hazirlamasuresi", DBNull.Value);
				}

				tarifKomut.Parameters.AddWithValue("@Talimatlar", string.Join(Environment.NewLine, tarifDetaylari.Where(i => i.StartsWith("Talimat:")).Select(i => i.Replace("Talimat: ", ""))));

				int tarifId = Convert.ToInt32(tarifKomut.ExecuteScalar());

				// Sonra TarifMalzeme ve Malzemeler tablolarına ekleme yap
				foreach (string item in tarifDetaylari)
				{
					if (item.StartsWith("Malzeme:"))
					{
						string[] malzemeParcalari = item.Replace("Malzeme: ", "").Split(',');
						string malzemeAdi = malzemeParcalari[0].Trim();
						string[] miktarVeBirim = malzemeParcalari[1].Trim().Split(' ');
						int miktar = int.Parse(miktarVeBirim[0]);
						string birim = miktarVeBirim[1];

						// Malzeme varsa güncelle, yoksa ekle
						string malzemeSorgu = "IF EXISTS (SELECT 1 FROM Malzemeler WHERE MalzemeAdi = @MalzemeAdi) " +
											  "UPDATE Malzemeler SET ToplamMiktar = ToplamMiktar + @Miktar WHERE MalzemeAdi = @MalzemeAdi " +
											  "ELSE " +
											  "INSERT INTO Malzemeler (MalzemeAdi, ToplamMiktar, MalzemeBirim) VALUES (@MalzemeAdi, @Miktar, @Birim); " +
											  "SELECT SCOPE_IDENTITY();";

						SqlCommand malzemeKomut = new SqlCommand(malzemeSorgu, baglanti);
						malzemeKomut.Parameters.AddWithValue("@MalzemeAdi", malzemeAdi);
						malzemeKomut.Parameters.AddWithValue("@Miktar", miktar);
						malzemeKomut.Parameters.AddWithValue("@Birim", birim);

						int malzemeId = Convert.ToInt32(malzemeKomut.ExecuteScalar());

						// TarifMalzeme tablosuna ekleme yap
						string tarifMalzemeSorgu = "INSERT INTO TarifMalzeme (MalzemeID, TarifID, MalzemeMiktar) VALUES (@MalzemeID, @TarifID, @MalzemeMiktar)";
						SqlCommand tarifMalzemeKomut = new SqlCommand(tarifMalzemeSorgu, baglanti);
						tarifMalzemeKomut.Parameters.AddWithValue("@MalzemeID", malzemeId);
						tarifMalzemeKomut.Parameters.AddWithValue("@TarifID", tarifId);
						tarifMalzemeKomut.Parameters.AddWithValue("@MalzemeMiktar", miktar);
						tarifMalzemeKomut.ExecuteNonQuery();
					}
				}

				MessageBox.Show("Tarif başarıyla kaydedildi.");
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Hata oluştu: " + ex.Message);
			}
			finally
			{
				baglanti.Close();
			}
		}

		
	}
}