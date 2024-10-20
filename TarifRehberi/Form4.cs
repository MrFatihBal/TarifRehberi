using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TarifRehberi
{
	public partial class Form4 : Form
	{
		public string connectionString;

		public Form4()
		{
			InitializeComponent();
			connectionString = ConfigurationManager.ConnectionStrings["TarifRehberiContext"].ConnectionString;
			TabloyuOlustur();
		}

		// Form yüklendiğinde malzemeleri listele ve combobox doldur
		private void Form4_Load(object sender, EventArgs e)
		{
			// Malzeme Birimlerini ComboBox'a ekleyelim
			comboBoxMalzemeBirim.Items.AddRange(new string[] {
				"Kilogram",
				"Litre",
				"Adet",
				"Konserve",
				"Demet",
				"Paket"
			});

			// Varsayılan olarak ilk öğeyi seçelim
			comboBoxMalzemeBirim.SelectedIndex = 0;

			// Malzemeleri listele
			MalzemelerimiListele();
		}

		// Veritabanında tablo oluşturma
		private void TabloyuOlustur()
		{
			try
			{
				using (SqlConnection baglanti = new SqlConnection(connectionString))
				{
					baglanti.Open();
					string tabloSorgusu = @"
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Malzemelerim' AND xtype='U')
                        CREATE TABLE Malzemelerim (
                            MalzemelerimID INT PRIMARY KEY IDENTITY(1,1),
                            MalzemelerimAdi NVARCHAR(100),
                            MalzemeMiktar FLOAT,
                            MalzemeBirim NVARCHAR(50),
                            BirimFiyat DECIMAL(10, 2)
                        )";

					SqlCommand komut = new SqlCommand(tabloSorgusu, baglanti);
					komut.ExecuteNonQuery();
				}
				MessageBox.Show("Malzemelerim tablosu başarıyla oluşturuldu veya zaten mevcut.");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Tablo oluşturulurken bir hata oluştu: " + ex.Message);
			}
		}

		// Malzemeleri DataGridView'de listelemek için
		private void MalzemelerimiListele()
		{
			try
			{
				using (SqlConnection baglanti = new SqlConnection(connectionString))
				{
					baglanti.Open();
					SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Malzemelerim", baglanti);
					DataTable dt = new DataTable();
					da.Fill(dt);
					dataGridView1.DataSource = dt;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Hata oluştu: " + ex.Message);
			}
		}

		// Malzeme ekleme
		private void btnEkle_Click(object sender, EventArgs e)
		{
			try
			{
				using (SqlConnection baglanti = new SqlConnection(connectionString))
				{
					baglanti.Open();
					SqlCommand komut = new SqlCommand("INSERT INTO Malzemelerim (MalzemelerimAdi, MalzemeMiktar, MalzemeBirim, BirimFiyat) VALUES (@MalzemelerimAdi, @MalzemeMiktar, @MalzemeBirim, @BirimFiyat)", baglanti);

					komut.Parameters.AddWithValue("@MalzemelerimAdi", txtMalzemeAdi.Text);
					komut.Parameters.AddWithValue("@MalzemeMiktar", double.Parse(txtMalzemeMiktar.Text));
					komut.Parameters.AddWithValue("@MalzemeBirim", comboBoxMalzemeBirim.SelectedItem.ToString());
					komut.Parameters.AddWithValue("@BirimFiyat", decimal.Parse(txtBirimFiyat.Text));

					komut.ExecuteNonQuery();
				}
				MessageBox.Show("Malzeme başarıyla eklendi.");
				MalzemelerimiListele(); // Listeyi güncelle
			}
			catch (Exception ex)
			{
				MessageBox.Show("Hata oluştu: " + ex.Message);
			}
		}

		// Malzeme güncelleme
		private void btnGuncelle_Click(object sender, EventArgs e)
		{
			try
			{
				if (dataGridView1.SelectedRows.Count > 0)
				{
					int secilenId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

					using (SqlConnection baglanti = new SqlConnection(connectionString))
					{
						baglanti.Open();
						SqlCommand komut = new SqlCommand("UPDATE Malzemelerim SET MalzemelerimAdi = @MalzemelerimAdi, MalzemeMiktar = @MalzemeMiktar, MalzemeBirim = @MalzemeBirim, BirimFiyat = @BirimFiyat WHERE MalzemelerimID = @MalzemelerimID", baglanti);

						komut.Parameters.AddWithValue("@MalzemelerimAdi", txtMalzemeAdi.Text);
						komut.Parameters.AddWithValue("@MalzemeMiktar", double.Parse(txtMalzemeMiktar.Text));
						komut.Parameters.AddWithValue("@MalzemeBirim", comboBoxMalzemeBirim.SelectedItem.ToString());
						komut.Parameters.AddWithValue("@BirimFiyat", decimal.Parse(txtBirimFiyat.Text));
						komut.Parameters.AddWithValue("@MalzemelerimID", secilenId);

						komut.ExecuteNonQuery();
					}
					MessageBox.Show("Malzeme başarıyla güncellendi.");
					MalzemelerimiListele(); // Listeyi güncelle
				}
				else
				{
					MessageBox.Show("Lütfen güncellenecek bir malzeme seçin.");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Hata oluştu: " + ex.Message);
			}
		}

		// Malzeme silme
		private void btnSil_Click(object sender, EventArgs e)
		{
			try
			{
				if (dataGridView1.SelectedRows.Count > 0)
				{
					int secilenId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

					using (SqlConnection baglanti = new SqlConnection(connectionString))
					{
						baglanti.Open();
						SqlCommand komut = new SqlCommand("DELETE FROM Malzemelerim WHERE MalzemelerimID = @MalzemelerimID", baglanti);
						komut.Parameters.AddWithValue("@MalzemelerimID", secilenId);

						komut.ExecuteNonQuery();
					}
					MessageBox.Show("Malzeme başarıyla silindi.");
					MalzemelerimiListele(); // Listeyi güncelle
				}
				else
				{
					MessageBox.Show("Lütfen silinecek bir malzeme seçin.");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Hata oluştu: " + ex.Message);
			}
		}

		private void comboBoxMalzemeBirim_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
