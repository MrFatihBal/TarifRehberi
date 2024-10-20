using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TarifUygulamasi
{
	public partial class TarifGuncelleForm : Form
	{
		string connectionString;

		public TarifGuncelleForm()
		{
			connectionString = ConfigurationManager.ConnectionStrings["TarifRehberiContext"].ConnectionString;
			InitializeComponent();
		}

		// Form yüklendiğinde tarif ve malzemeleri DataGridView'lere doldur.
		private void TarifGuncelleForm_Load(object sender, EventArgs e)
		{
			LoadTarifler();
			LoadKategoriler();
		}

		private void LoadKategoriler()
		{
			comboKategori.Items.Clear();
			comboKategori.Items.Add("Çorba");
			comboKategori.Items.Add("Salata");
			comboKategori.Items.Add("Ana Yemek");
			comboKategori.Items.Add("İçecek");
			comboKategori.Items.Add("Tatlı");
		}

		// Tarifleri DataGridView'e yükleme
		private void LoadTarifler()
		{
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				string query = "SELECT TarifID, TarifAdi, Kategori, HazirlamaSuresi, Talimatlar FROM Tarifler";
				SqlDataAdapter da = new SqlDataAdapter(query, con);
				DataTable dt = new DataTable();
				da.Fill(dt);
				dataGridView1.DataSource = dt;
			}
		}

		// Seçilen tarife ait malzemeleri yükleme
		private void LoadMalzemelerByTarif(int tarifID)
		{
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				string query = @"
                SELECT M.MalzemeID, M.MalzemeAdi, TM.MalzemeMiktar, M.BirimFiyat 
                FROM TarifMalzeme TM 
                INNER JOIN Malzemeler M ON TM.MalzemeID = M.MalzemeID 
                WHERE TM.TarifID = @TarifID";

				SqlDataAdapter da = new SqlDataAdapter(query, con);
				da.SelectCommand.Parameters.AddWithValue("@TarifID", tarifID);
				DataTable dt = new DataTable();
				da.Fill(dt);
				dataGridView2.DataSource = dt;
			}
		}

		// Tarif seçildiğinde malzemeler ile ilgili bilgileri getir
		private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
				txtTarifAdi.Text = row.Cells["TarifAdi"].Value.ToString();
				comboKategori.Text = row.Cells["Kategori"].Value.ToString();
				txtHazirlamaSuresi.Text = row.Cells["HazirlamaSuresi"].Value.ToString();
				txtTalimat.Text = row.Cells["Talimatlar"].Value.ToString();

				// Seçilen tarife ait malzemeleri göster
				int selectedTarifID = Convert.ToInt32(row.Cells["TarifID"].Value);
				LoadMalzemelerByTarif(selectedTarifID);
			}
		}

		// DataGridView2'de bir malzeme seçildiğinde bilgilerini doldur
		private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
				txtMalzemeAdi.Text = row.Cells["MalzemeAdi"].Value.ToString();
				txtMalzemeMiktar.Text = row.Cells["MalzemeMiktar"].Value.ToString();
				txtBirimFiyat.Text = row.Cells["BirimFiyat"].Value.ToString();
			}
		}

		// Tarif güncelleme butonu
		private void btnGuncelle_Click(object sender, EventArgs e)
		{
			if (dataGridView1.CurrentRow != null)
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();
					string query = "UPDATE Tarifler SET TarifAdi = @TarifAdi, Kategori = @Kategori, HazirlamaSuresi = @HazirlamaSuresi, Talimatlar = @Talimatlar WHERE TarifID = @TarifID";
					using (SqlCommand cmd = new SqlCommand(query, con))
					{
						cmd.Parameters.AddWithValue("@TarifID", dataGridView1.CurrentRow.Cells["TarifID"].Value);
						cmd.Parameters.AddWithValue("@TarifAdi", txtTarifAdi.Text);
						cmd.Parameters.AddWithValue("@Kategori", comboKategori.Text);
						cmd.Parameters.AddWithValue("@HazirlamaSuresi", txtHazirlamaSuresi.Text);
						cmd.Parameters.AddWithValue("@Talimatlar", txtTalimat.Text);
						cmd.ExecuteNonQuery();
					}
				}

				LoadTarifler();
				MessageBox.Show("Tarif başarıyla güncellendi.");
			}
		}

		// Tarif silme butonu
		private void btnSil_Click(object sender, EventArgs e)
		{
			if (dataGridView1.CurrentRow != null)
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();
					string query = "DELETE FROM Tarifler WHERE TarifID = @TarifID";
					using (SqlCommand cmd = new SqlCommand(query, con))
					{
						cmd.Parameters.AddWithValue("@TarifID", dataGridView1.CurrentRow.Cells["TarifID"].Value);
						cmd.ExecuteNonQuery();
					}
				}

				LoadTarifler();
				MessageBox.Show("Tarif başarıyla silindi.");
			}
		}

		// Malzeme ekleme butonu
		private void btnMalzemeEkle_Click(object sender, EventArgs e)
		{
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				con.Open();
				string query = "INSERT INTO Malzemeler (MalzemeAdi, BirimFiyat) VALUES (@MalzemeAdi, @BirimFiyat)";
				using (SqlCommand cmd = new SqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@MalzemeAdi", txtMalzemeAdi.Text);
					cmd.Parameters.AddWithValue("@BirimFiyat", txtBirimFiyat.Text);
					cmd.ExecuteNonQuery();
				}
			}

			LoadMalzemelerByTarif(Convert.ToInt32(dataGridView1.CurrentRow.Cells["TarifID"].Value));
			MessageBox.Show("Malzeme başarıyla eklendi.");
		}

		// Malzeme güncelleme butonu
		private void btnMalzemeDegistir_Click(object sender, EventArgs e)
		{
			if (dataGridView2.CurrentRow != null)
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();
					string query = "UPDATE Malzemeler SET MalzemeAdi = @MalzemeAdi, BirimFiyat = @BirimFiyat WHERE MalzemeID = @MalzemeID";
					using (SqlCommand cmd = new SqlCommand(query, con))
					{
						cmd.Parameters.AddWithValue("@MalzemeID", dataGridView2.CurrentRow.Cells["MalzemeID"].Value);
						cmd.Parameters.AddWithValue("@MalzemeAdi", txtMalzemeAdi.Text);
						cmd.Parameters.AddWithValue("@BirimFiyat", txtBirimFiyat.Text);
						cmd.ExecuteNonQuery();
					}
				}

				LoadMalzemelerByTarif(Convert.ToInt32(dataGridView1.CurrentRow.Cells["TarifID"].Value));
				MessageBox.Show("Malzeme başarıyla güncellendi.");
			}
		}

		// Malzeme silme butonu
		private void btnMalzemeSil_Click(object sender, EventArgs e)
		{
			if (dataGridView2.CurrentRow != null)
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();
					string query = "DELETE FROM Malzemeler WHERE MalzemeID = @MalzemeID";
					using (SqlCommand cmd = new SqlCommand(query, con))
					{
						cmd.Parameters.AddWithValue("@MalzemeID", dataGridView2.CurrentRow.Cells["MalzemeID"].Value);
						cmd.ExecuteNonQuery();
					}
				}

				LoadMalzemelerByTarif(Convert.ToInt32(dataGridView1.CurrentRow.Cells["TarifID"].Value));
				MessageBox.Show("Malzeme başarıyla silindi.");
			}
		}

		
	}
}
