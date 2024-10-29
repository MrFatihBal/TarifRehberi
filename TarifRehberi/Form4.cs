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
			dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			TabloyuOlustur();
		}

		private void Form4_Load(object sender, EventArgs e)
		{
			comboBoxMalzemeBirim.Items.AddRange(new string[] {
				"Kilogram",
				"Litre",
				"Adet",
				"Konserve",
				"Demet",
				"Paket"
			});
			comboBoxMalzemeBirim.SelectedIndex = 0;

			MalzemeleriListele();
			MalzemelerimiListele();
		}

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
			}
			catch (Exception ex)
			{
				MessageBox.Show("Tablo oluşturulurken bir hata oluştu: " + ex.Message);
			}
		}

		private void MalzemeleriListele()
		{
			try
			{
				using (SqlConnection baglanti = new SqlConnection(connectionString))
				{
					baglanti.Open();
					SqlDataAdapter da = new SqlDataAdapter("SELECT MalzemeAdi FROM Malzemeler", baglanti);
					DataTable dt = new DataTable();
					da.Fill(dt);

					comboBoxMalzemeler.DataSource = dt;
					comboBoxMalzemeler.DisplayMember = "MalzemeAdi";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Malzemeleri listelerken hata oluştu: " + ex.Message);
			}
		}

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

		private void comboBoxMalzemeler_SelectedIndexChanged(object sender, EventArgs e)
		{
			string secilenMalzeme = comboBoxMalzemeler.Text;
			MalzemeDetaylariniDoldur(secilenMalzeme);
		}

		private void MalzemeDetaylariniDoldur(string malzemeAdi)
		{
			try
			{
				using (SqlConnection baglanti = new SqlConnection(connectionString))
				{
					baglanti.Open();
					SqlCommand komut = new SqlCommand("SELECT MalzemeAdi, ToplamMiktar, MalzemeBirim, BirimFiyat FROM Malzemeler WHERE MalzemeAdi = @MalzemeAdi", baglanti);
					komut.Parameters.AddWithValue("@MalzemeAdi", malzemeAdi);

					SqlDataReader dr = komut.ExecuteReader();
					if (dr.Read())
					{
						txtMalzemeAdi.Text = dr["MalzemeAdi"].ToString();
						txtMalzemeMiktar.Text = dr["ToplamMiktar"].ToString();
						comboBoxMalzemeBirim.SelectedItem = dr["MalzemeBirim"].ToString();
						txtBirimFiyat.Text = dr["BirimFiyat"].ToString();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Malzeme bilgilerini alırken hata oluştu: " + ex.Message);
			}
		}

		// Malzeme ekleme
		private void btnEkle_Click(object sender, EventArgs e)
		{
			string secilenMalzeme = txtMalzemeAdi.Text;

			if (MalzemeVarMi(secilenMalzeme))
			{
				MalzemeyiEkle(secilenMalzeme);
			}
			else
			{
				MalzemeyiHerIkiTabloyaEkle(secilenMalzeme);
			}

			MalzemelerimiListele();
		}

		private bool MalzemeVarMi(string malzemeAdi)
		{
			try
			{
				using (SqlConnection baglanti = new SqlConnection(connectionString))
				{
					baglanti.Open();
					SqlCommand komut = new SqlCommand("SELECT COUNT(*) FROM Malzemelerim WHERE MalzemelerimAdi = @MalzemeAdi", baglanti);
					komut.Parameters.AddWithValue("@MalzemeAdi", malzemeAdi);

					int count = (int)komut.ExecuteScalar();
					return count > 0;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Hata oluştu: " + ex.Message);
				return false;
			}
		}
		private void MalzemeyiEkle(string malzemeAdi)
		{
			try
			{
				using (SqlConnection baglanti = new SqlConnection(connectionString))
				{
					baglanti.Open();
					SqlCommand komutMalzemelerimiKontrol = new SqlCommand("SELECT COUNT(*) FROM Malzemelerim WHERE MalzemelerimAdi = @MalzemeAdi", baglanti);
					komutMalzemelerimiKontrol.Parameters.AddWithValue("@MalzemeAdi", malzemeAdi);
					int malzemeCount = (int)komutMalzemelerimiKontrol.ExecuteScalar();

					if (malzemeCount == 0)
					{
						
						SqlCommand komut = new SqlCommand("INSERT INTO Malzemelerim (MalzemelerimAdi, MalzemeMiktar, MalzemeBirim, BirimFiyat) VALUES (@MalzemelerimAdi, @MalzemeMiktar, @MalzemeBirim, @BirimFiyat)", baglanti);

						komut.Parameters.AddWithValue("@MalzemelerimAdi", malzemeAdi);
						komut.Parameters.AddWithValue("@MalzemeMiktar", double.Parse(txtMalzemeMiktar.Text));
						komut.Parameters.AddWithValue("@MalzemeBirim", comboBoxMalzemeBirim.SelectedItem.ToString());
						komut.Parameters.AddWithValue("@BirimFiyat", decimal.Parse(txtBirimFiyat.Text));

						komut.ExecuteNonQuery();
						MessageBox.Show("Malzeme başarıyla Malzemelerim tablosuna eklendi.");
					}
					else
					{
						MessageBox.Show("Bu malzeme zaten Malzemelerim tablosuna ekli.");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Hata oluştu: " + ex.Message);
			}
		}
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
					MalzemelerimiListele(); 
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
					MalzemelerimiListele(); 
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
		private void MalzemeyiHerIkiTabloyaEkle(string malzemeAdi)
		{
			try
			{
				using (SqlConnection baglanti = new SqlConnection(connectionString))
				{
					baglanti.Open();

					SqlCommand komutMalzemelerKontrol = new SqlCommand("SELECT COUNT(*) FROM Malzemeler WHERE MalzemeAdi = @MalzemeAdi", baglanti);
					komutMalzemelerKontrol.Parameters.AddWithValue("@MalzemeAdi", malzemeAdi);
					int malzemeCount = (int)komutMalzemelerKontrol.ExecuteScalar();

					if (malzemeCount == 0)
					{
						SqlCommand komutMalzemeler = new SqlCommand("INSERT INTO Malzemeler (MalzemeAdi, ToplamMiktar, MalzemeBirim, BirimFiyat) VALUES (@MalzemeAdi, @ToplamMiktar, @MalzemeBirim, @BirimFiyat)", baglanti);
						komutMalzemeler.Parameters.AddWithValue("@MalzemeAdi", malzemeAdi);
						komutMalzemeler.Parameters.AddWithValue("@ToplamMiktar", double.Parse(txtMalzemeMiktar.Text));
						komutMalzemeler.Parameters.AddWithValue("@MalzemeBirim", comboBoxMalzemeBirim.SelectedItem.ToString());
						komutMalzemeler.Parameters.AddWithValue("@BirimFiyat", decimal.Parse(txtBirimFiyat.Text));

						komutMalzemeler.ExecuteNonQuery();
						MessageBox.Show("Malzeme başarıyla her iki tabloya eklendi.");
					}

					MalzemeyiEkle(malzemeAdi);
				}
				
			}
			catch (Exception ex)
			{
				MessageBox.Show("Hata oluştu: " + ex.Message);
			}
		}

		private void dataGridView1_SelectionChanged(object sender, EventArgs e)
		{
			if (dataGridView1.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
				txtMalzemeAdi.Text = selectedRow.Cells["MalzemelerimAdi"].Value.ToString();
				txtMalzemeMiktar.Text = selectedRow.Cells["MalzemeMiktar"].Value.ToString();
				txtBirimFiyat.Text = selectedRow.Cells["BirimFiyat"].Value.ToString();
				comboBoxMalzemeBirim.SelectedItem = selectedRow.Cells["MalzemeBirim"].Value.ToString();
			}
		}
	}
}
