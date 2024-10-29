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
		private List<Malzeme> secilenMalzemeler = new List<Malzeme>();
		private string tarifAdi = "";
		private string kategori = "";
		private int hazirlamaSuresi = 0;
		private string talimatlar = "";

		public TarifEkleForm()
		{
			InitializeComponent();
			connectionString = ConfigurationManager.ConnectionStrings["TarifRehberiContext"].ConnectionString;
			baglanti = new SqlConnection(connectionString);
			InitializeFormControls();
		}

		private void InitializeFormControls()
		{
			cmbKategori.Items.Clear();
			cmbKategori.Items.AddRange(new string[] { "Çorba", "Ana Yemek", "Tatlı", "Salata", "İçecek" });
			cmbKategori.SelectedIndex = 0;

			cmbMalzemeBirim.Items.Clear();
			cmbMalzemeBirim.Items.AddRange(new string[] {"Kilogram","Litre", "Demet", "Adet", "Konserve", "Paket" });
			cmbMalzemeBirim.SelectedIndex = 0;

			DoldurMalzemeComboBox();
			txtMalzemeMiktar.KeyPress += new KeyPressEventHandler(txtMalzemeMiktar_KeyPress);
		}
		private void txtMalzemeMiktar_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
				(e.KeyChar != System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0]))
			{
				e.Handled = true;
			}

			if (e.KeyChar == System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0] &&
				(sender as TextBox).Text.IndexOf(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) > -1)
			{
				e.Handled = true;
			}
		}
		private void TarifEkleForm_Load(object sender, EventArgs e)
		{
			ClearAllFields();
			RefreshListBox();
		}
		private void DoldurMalzemeComboBox()
		{
			try
			{
				baglanti.Open();
				string sorgu = "SELECT MalzemeID, MalzemeAdi FROM Malzemeler";
				SqlDataAdapter adapter = new SqlDataAdapter(sorgu, baglanti);
				DataTable dt = new DataTable();
				adapter.Fill(dt);

				DataRow yeniSatir = dt.NewRow();
				yeniSatir["MalzemeID"] = -1;
				yeniSatir["MalzemeAdi"] = "Yeni Malzeme";
				dt.Rows.InsertAt(yeniSatir, 0);

				cmbMalzeme.DisplayMember = "MalzemeAdi";
				cmbMalzeme.ValueMember = "MalzemeID";
				cmbMalzeme.DataSource = dt;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Malzemeler yüklenirken hata oluştu: " + ex.Message);
			}
			finally
			{
				if (baglanti.State == ConnectionState.Open)
					baglanti.Close();
			}
		}

		private void btnIsimEkle_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(txtTarifAdi.Text))
			{
				tarifAdi = txtTarifAdi.Text;
				tarifDetaylari.RemoveAll(x => x.StartsWith("Tarif Adı:"));
				tarifDetaylari.Add($"Tarif Adı: {tarifAdi}");
				RefreshListBox();
			}
			else
			{
				MessageBox.Show("Lütfen tarif adı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnKategoriEkle_Click(object sender, EventArgs e)
		{
			if (cmbKategori.SelectedIndex != -1)
			{
				kategori = cmbKategori.SelectedItem.ToString();
				tarifDetaylari.RemoveAll(x => x.StartsWith("Kategori:"));
				tarifDetaylari.Add($"Kategori: {kategori}");
				RefreshListBox();
			}
			else
			{
				MessageBox.Show("Lütfen bir kategori seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnHazirlamaSuresiEkle_Click(object sender, EventArgs e)
		{
			if (int.TryParse(txtHazirlamaSuresi.Text, out int sure))
			{
				hazirlamaSuresi = sure; 
				tarifDetaylari.RemoveAll(x => x.StartsWith("Hazırlama Süresi:"));
				tarifDetaylari.Add($"Hazırlama Süresi: {sure} Dakika");
				RefreshListBox();
			}
			else
			{
				MessageBox.Show("Lütfen geçerli bir süre giriniz (dakika cinsinden).", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}


		private void btnTalimatEkle_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(rtbTalimat.Text))
			{
				talimatlar = rtbTalimat.Text;
				tarifDetaylari.RemoveAll(x => x.StartsWith("Talimat:"));
				tarifDetaylari.Add($"Talimat: {talimatlar}");
				RefreshListBox();
			}
			else
			{
				MessageBox.Show("Lütfen tarif talimatlarını giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnMalzemeEkle_Click(object sender, EventArgs e)
		{
			if (ValidateMalzemeInputs())
			{
				int malzemeId = Convert.ToInt32(cmbMalzeme.SelectedValue);
				string malzemeAdi = malzemeId == -1 ? txtMalzemeAdi.Text : cmbMalzeme.Text;
				decimal miktar = Convert.ToDecimal(txtMalzemeMiktar.Text);
				string birim = cmbMalzemeBirim.SelectedItem.ToString();
				decimal birimFiyat = Convert.ToDecimal(txtBirimFiyat.Text);
				if (malzemeId == -1)
				{
					try
					{
						using (SqlConnection connection = new SqlConnection(connectionString))
						{
							connection.Open();
							string insertSorgu = @"INSERT INTO Malzemeler (MalzemeAdi, BirimFiyat, MalzemeBirim, ToplamMiktar) 
                                         VALUES (@MalzemeAdi, @BirimFiyat, @MalzemeBirim, @ToplamMiktar);
                                         SELECT SCOPE_IDENTITY();";

							using (SqlCommand komut = new SqlCommand(insertSorgu, connection))
							{
								komut.Parameters.AddWithValue("@MalzemeAdi", malzemeAdi);
								komut.Parameters.AddWithValue("@BirimFiyat", birimFiyat);
								komut.Parameters.AddWithValue("@MalzemeBirim", birim);
								komut.Parameters.AddWithValue("@ToplamMiktar", 100); 

								malzemeId = Convert.ToInt32(komut.ExecuteScalar());
							}
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show("Yeni malzeme eklenirken hata oluştu: " + ex.Message,
									  "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
				}

				var yeniMalzeme = new Malzeme
				{
					MalzemeID = malzemeId,
					MalzemeAdi = malzemeAdi,
					Miktar = miktar,
					Birim = birim,
					BirimFiyat = birimFiyat
				};

				secilenMalzemeler.Add(yeniMalzeme);
				tarifDetaylari.Add($"Malzeme: {malzemeAdi}, {miktar} {birim}, Birim Fiyat: {birimFiyat:C}");
				RefreshListBox();
				ClearMalzemeFields();

				DoldurMalzemeComboBox();
			}
		}

		private bool ValidateMalzemeInputs()
		{
			if (cmbMalzeme.SelectedValue == null)
			{
				MessageBox.Show("Lütfen bir malzeme seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (Convert.ToInt32(cmbMalzeme.SelectedValue) == -1 && string.IsNullOrWhiteSpace(txtMalzemeAdi.Text))
			{
				MessageBox.Show("Lütfen malzeme adı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (!decimal.TryParse(txtMalzemeMiktar.Text, out decimal miktar))
			{
				MessageBox.Show("Lütfen geçerli bir miktar giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			// Miktar 0'dan büyük olmalı
			if (miktar <= 0)
			{
				MessageBox.Show("Miktar 0'dan büyük olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (!decimal.TryParse(txtBirimFiyat.Text, out decimal birimFiyat))
			{
				MessageBox.Show("Lütfen geçerli bir birim fiyat giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			return true;
		}

		private void btnKaydet_Click(object sender, EventArgs e)
		{
			if (ValidateRecipe())
			{
				SaveRecipe();
			}
		}

		private bool ValidateRecipe()
		{
			if (string.IsNullOrWhiteSpace(tarifAdi))
			{
				MessageBox.Show("Lütfen tarif adı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (TarifZatenVarMi(tarifAdi, kategori))
			{
				MessageBox.Show($"'{tarifAdi}' adlı tarif '{kategori}' kategorisinde zaten mevcut. Lütfen farklı bir tarif adı veya kategori seçiniz.",
							  "Mükerrer Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (string.IsNullOrWhiteSpace(kategori))
			{
				MessageBox.Show("Lütfen kategori seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (string.IsNullOrWhiteSpace(hazirlamaSuresi.ToString()))
			{
				MessageBox.Show("Lütfen hazırlama süresini giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (string.IsNullOrWhiteSpace(talimatlar))
			{
				MessageBox.Show("Lütfen tarif talimatlarını giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (secilenMalzemeler.Count == 0)
			{
				MessageBox.Show("Lütfen en az bir malzeme ekleyiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			return true;
		}

		private bool TarifZatenVarMi(string tarifAdi, string kategori)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					string sorgu = @"SELECT COUNT(*) 
                               FROM Tarifler 
                               WHERE LOWER(TarifAdi) = LOWER(@TarifAdi) 
                               AND LOWER(Kategori) = LOWER(@Kategori)";

					using (SqlCommand komut = new SqlCommand(sorgu, connection))
					{
						komut.Parameters.AddWithValue("@TarifAdi", tarifAdi.ToLower());
						komut.Parameters.AddWithValue("@Kategori", kategori.ToLower());

						int existingCount = (int)komut.ExecuteScalar();
						return existingCount > 0;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Tarif kontrolü sırasında bir hata oluştu: " + ex.Message,
							  "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}

		private void SaveRecipe()
		{
			try
			{
				baglanti.Open();
				using (SqlTransaction transaction = baglanti.BeginTransaction())
				{
					try
					{
						string tarifSorgu = @"INSERT INTO Tarifler (TarifAdi, Kategori, HazirlamaSuresi, Talimatlar) 
                                            VALUES (@TarifAdi, @Kategori, @HazirlamaSuresi, @Talimatlar);
                                            SELECT SCOPE_IDENTITY();";

						SqlCommand tarifKomut = new SqlCommand(tarifSorgu, baglanti, transaction);
						tarifKomut.Parameters.AddWithValue("@TarifAdi", tarifAdi);
						tarifKomut.Parameters.AddWithValue("@Kategori", kategori);
						tarifKomut.Parameters.AddWithValue("@HazirlamaSuresi", hazirlamaSuresi);
						tarifKomut.Parameters.AddWithValue("@Talimatlar", talimatlar);

						int tarifId = Convert.ToInt32(tarifKomut.ExecuteScalar());

						foreach (var malzeme in secilenMalzemeler)
						{
							string malzemeSorgu = @"INSERT INTO TarifMalzeme (MalzemeID, TarifID, MalzemeMiktar) 
                                                   VALUES (@MalzemeID, @TarifID, @MalzemeMiktar)";

							SqlCommand malzemeKomut = new SqlCommand(malzemeSorgu, baglanti, transaction);
							malzemeKomut.Parameters.AddWithValue("@MalzemeID", malzeme.MalzemeID);
							malzemeKomut.Parameters.AddWithValue("@TarifID", tarifId);
							malzemeKomut.Parameters.AddWithValue("@MalzemeMiktar", malzeme.Miktar);
							malzemeKomut.ExecuteNonQuery();
						}

						transaction.Commit();
						MessageBox.Show("Tarif başarıyla kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
						ClearAllFields();
					}
					catch (Exception ex)
					{
						transaction.Rollback();
						throw new Exception("Tarif kaydedilirken bir hata oluştu: " + ex.Message);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				if (baglanti.State == ConnectionState.Open)
					baglanti.Close();
			}
		}


		private void RefreshListBox()
		{
			lstTarifDetaylari.Items.Clear();
			foreach (string detay in tarifDetaylari)
			{
				lstTarifDetaylari.Items.Add(detay);
			}
		}

		private void ClearMalzemeFields()
		{
			cmbMalzeme.SelectedIndex = 0;
			txtMalzemeMiktar.Clear();
			txtBirimFiyat.Clear();
			cmbMalzemeBirim.SelectedIndex = 0;
		}

		private void ClearAllFields()
		{
			txtTarifAdi.Clear();
			cmbKategori.SelectedIndex = 0;
			txtHazirlamaSuresi.Clear();
			rtbTalimat.Clear();
			ClearMalzemeFields();
			tarifDetaylari.Clear();
			secilenMalzemeler.Clear();
			RefreshListBox();

			tarifAdi = "";
			kategori = "";
			hazirlamaSuresi = 0;
			talimatlar = "";
		}

		private class Malzeme
		{
			public int MalzemeID { get; set; }
			public string MalzemeAdi { get; set; }
			public decimal Miktar { get; set; }
			public string Birim { get; set; }
			public decimal BirimFiyat { get; set; }
		}
		private void cmbMalzeme_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbMalzeme.SelectedValue == null) return;

			if (Convert.ToInt32(cmbMalzeme.SelectedValue) == -1)
			{
				txtMalzemeAdi.Text = "";
				txtBirimFiyat.Text = "";
				txtMalzemeMiktar.Text = "1";
				cmbMalzemeBirim.SelectedIndex = 0;

				txtMalzemeAdi.ReadOnly = false;
				txtMalzemeAdi.Enabled = true;
				return;
			}

			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string sorgu = @"SELECT MalzemeAdi, BirimFiyat, MalzemeBirim 
                           FROM Malzemeler 
                           WHERE MalzemeID = @MalzemeID";

					using (SqlCommand komut = new SqlCommand(sorgu, connection))
					{
						komut.Parameters.AddWithValue("@MalzemeID", cmbMalzeme.SelectedValue);

						using (SqlDataReader reader = komut.ExecuteReader())
						{
							if (reader.Read())
							{
								txtMalzemeAdi.Text = reader["MalzemeAdi"].ToString();
								txtMalzemeAdi.ReadOnly = true;
								txtMalzemeAdi.Enabled = false;

								decimal birimFiyat;
								if (decimal.TryParse(reader["BirimFiyat"].ToString(), out birimFiyat))
								{
									txtBirimFiyat.Text = birimFiyat.ToString();
								}

								string birim = reader["MalzemeBirim"].ToString();
								int birimIndex = cmbMalzemeBirim.Items.IndexOf(birim);
								if (birimIndex != -1)
								{
									cmbMalzemeBirim.SelectedIndex = birimIndex;
								}

								txtMalzemeMiktar.Text = "1";
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Malzeme bilgileri alınırken hata oluştu: " + ex.Message,
							   "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void TarifEkleForm_Load_1(object sender, EventArgs e)
		{

		}
	}
}