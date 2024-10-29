using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TarifUygulamasi
{
	public partial class TarifGuncelleForm : Form
	{
		string connectionString;
		private SqlConnection baglanti;
		

		public TarifGuncelleForm()
		{
			connectionString = ConfigurationManager.ConnectionStrings["TarifRehberiContext"].ConnectionString;
			baglanti = new SqlConnection(connectionString);
			InitializeComponent();
			LoadMalzemeler();
			InitializeFormControls();
			InitializeMalzemeBirimComboBox();
			txtMalzemeMiktar.KeyPress += new KeyPressEventHandler(txtMalzemeMiktar_KeyPress);
			dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
		}
		private void txtMalzemeMiktar_KeyPress(object sender, KeyPressEventArgs e)
		{
			// Sadece rakam, backspace ve decimal separator'a izin ver
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
				(e.KeyChar != System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0]))
			{
				e.Handled = true;
			}

			// Birden fazla decimal separator'a izin verme
			if (e.KeyChar == System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0] &&
				(sender as TextBox).Text.IndexOf(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) > -1)
			{
				e.Handled = true;
			}
		}
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
		private void LoadMalzemeler()
		{
			try
			{
				if (cmbMalzeme == null)
				{
					MessageBox.Show("ComboBox kontrolü bulunamadı!");
					return;
				}

				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();
					string query = "SELECT MalzemeID, MalzemeAdi FROM Malzemeler";

					using (SqlCommand cmd = new SqlCommand(query, con))
					{
						SqlDataAdapter da = new SqlDataAdapter(cmd);
						DataTable dt = new DataTable();
						da.Fill(dt);

						if (dt.Rows.Count > 0)
						{
							cmbMalzeme.DataSource = null; 
							cmbMalzeme.Items.Clear();     
							cmbMalzeme.DataSource = dt;   
							cmbMalzeme.DisplayMember = "MalzemeAdi";
							cmbMalzeme.ValueMember = "MalzemeID";
						}
						else
						{
							MessageBox.Show("Malzemeler tablosunda veri bulunamadı!");
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Hata Detayı: {ex.Message}\nStack Trace: {ex.StackTrace}");
			}
		}
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

		private void LoadMalzemelerByTarif(int tarifID)
		{
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				string query = @"
                SELECT M.MalzemeID, M.MalzemeAdi, TM.MalzemeMiktar, M.BirimFiyat , M.MalzemeBirim
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

		private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
				txtTarifAdi.Text = row.Cells["TarifAdi"].Value.ToString();
				comboKategori.Text = row.Cells["Kategori"].Value.ToString();
				txtHazirlamaSuresi.Text = row.Cells["HazirlamaSuresi"].Value.ToString();
				txtTalimat.Text = row.Cells["Talimatlar"].Value.ToString();

				int selectedTarifID = Convert.ToInt32(row.Cells["TarifID"].Value);
				LoadMalzemelerByTarif(selectedTarifID);
			}
		}

		private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
				txtMalzemeAdi.Text = row.Cells["MalzemeAdi"].Value.ToString();
				txtMalzemeMiktar.Text = row.Cells["MalzemeMiktar"].Value.ToString();
				txtBirimFiyat.Text = row.Cells["BirimFiyat"].Value.ToString();
				dataGridView2.Columns["MalzemeBirim"].HeaderText = "Birim";
			}
		}

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

		private void btnSil_Click(object sender, EventArgs e)
		{
			if (dataGridView1.CurrentRow != null)
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();

					string deleteTarifMalzemeQuery = "DELETE FROM TarifMalzeme WHERE TarifID = @TarifID";
					using (SqlCommand cmd1 = new SqlCommand(deleteTarifMalzemeQuery, con))
					{
						cmd1.Parameters.AddWithValue("@TarifID", dataGridView1.CurrentRow.Cells["TarifID"].Value);
						cmd1.ExecuteNonQuery();
					}

					string deleteTariflerQuery = "DELETE FROM Tarifler WHERE TarifID = @TarifID";
					using (SqlCommand cmd2 = new SqlCommand(deleteTariflerQuery, con))
					{
						cmd2.Parameters.AddWithValue("@TarifID", dataGridView1.CurrentRow.Cells["TarifID"].Value);
						cmd2.ExecuteNonQuery();
					}
				}

				LoadTarifler();
				MessageBox.Show("Tarif başarıyla silindi.");
			}
		}
		private void InitializeMalzemeBirimComboBox()
		{
			cmbMalzemeBirim.Items.AddRange(new string[] {
			
			"Kilogram",
			"Litre",
			"Adet",
			"Paket",
			"Demet",
			"Konserve"
		     });

			cmbMalzemeBirim.SelectedIndex = 0;

			this.Controls.Add(cmbMalzemeBirim);
		}

		private void btnMalzemeEkle_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(txtMalzemeAdi.Text))
				{
					MessageBox.Show("Lütfen malzeme adını giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				if (string.IsNullOrWhiteSpace(txtBirimFiyat.Text) || !decimal.TryParse(txtBirimFiyat.Text, out decimal birimFiyat))
				{
					MessageBox.Show("Lütfen geçerli bir birim fiyat giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				if (!decimal.TryParse(txtMalzemeMiktar.Text, out decimal malzemeMiktar))
				{
					MessageBox.Show("Lütfen geçerli bir miktar giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return ;
				}

				if (malzemeMiktar <= 0)
				{
					MessageBox.Show("Miktar 0'dan büyük olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return ;
				}

				int malzemeId;
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();
					string query = @"INSERT INTO Malzemeler (MalzemeAdi, BirimFiyat, MalzemeBirim, ToplamMiktar) 
                             VALUES (@MalzemeAdi, @BirimFiyat, @MalzemeBirim, @ToplamMiktar);
                             SELECT SCOPE_IDENTITY();";

					using (SqlCommand cmd = new SqlCommand(query, con))
					{
						cmd.Parameters.AddWithValue("@MalzemeAdi", txtMalzemeAdi.Text.Trim());
						cmd.Parameters.AddWithValue("@BirimFiyat", birimFiyat);
						cmd.Parameters.AddWithValue("@MalzemeBirim", cmbMalzemeBirim.SelectedItem.ToString());
						cmd.Parameters.AddWithValue("@ToplamMiktar", 100); 
																		  
						malzemeId = Convert.ToInt32(cmd.ExecuteScalar());
					}

					// Şimdi TarifMalzeme tablosuna ekle
					query = @"INSERT INTO TarifMalzeme (TarifID, MalzemeID, MalzemeMiktar) 
                 VALUES (@TarifID, @MalzemeID, @MalzemeMiktar)";

					using (SqlCommand cmd = new SqlCommand(query, con))
					{
						cmd.Parameters.AddWithValue("@TarifID", Convert.ToInt32(dataGridView1.CurrentRow.Cells["TarifID"].Value));
						cmd.Parameters.AddWithValue("@MalzemeID", malzemeId);
						cmd.Parameters.AddWithValue("@MalzemeMiktar", malzemeMiktar);

						cmd.ExecuteNonQuery();
					}
				}

				LoadMalzemelerByTarif(Convert.ToInt32(dataGridView1.CurrentRow.Cells["TarifID"].Value));
				MessageBox.Show("Malzeme başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

				// Form elemanlarını temizle
				txtMalzemeAdi.Clear();
				txtBirimFiyat.Clear();
				txtMalzemeMiktar.Clear();
				cmbMalzemeBirim.SelectedIndex = 0;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Malzeme eklenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void btnMalzemeDegistir_Click(object sender, EventArgs e)
		{
			if (dataGridView2.CurrentRow != null)
			{
				try
				{
					if (string.IsNullOrWhiteSpace(txtMalzemeAdi.Text))
					{
						MessageBox.Show("Lütfen malzeme adını giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					
					if (string.IsNullOrWhiteSpace(txtMalzemeMiktar.Text) || !decimal.TryParse(txtMalzemeMiktar.Text, out decimal malzemeMiktar))
					{
						MessageBox.Show("Lütfen geçerli bir malzeme miktarı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}

					if (string.IsNullOrWhiteSpace(txtBirimFiyat.Text) || !decimal.TryParse(txtBirimFiyat.Text, out decimal birimFiyat))
					{
						MessageBox.Show("Lütfen geçerli bir birim fiyat giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}

					if (cmbMalzemeBirim.SelectedItem == null)
					{
						MessageBox.Show("Lütfen bir malzeme birimi seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					string malzemeBirim = cmbMalzemeBirim.SelectedItem.ToString();

					int malzemeID = Convert.ToInt32(dataGridView2.CurrentRow.Cells["MalzemeID"].Value);
					int tarifID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["TarifID"].Value);

					using (SqlConnection con = new SqlConnection(connectionString))
					{
						con.Open();
						using (SqlTransaction transaction = con.BeginTransaction())
						{
							try
							{
								
								string tarifMalzemeQuery = @"UPDATE TarifMalzeme 
                                                    SET MalzemeMiktar = @MalzemeMiktar 
                                                    WHERE MalzemeID = @MalzemeID AND TarifID = @TarifID";

								using (SqlCommand tarifMalzemeCmd = new SqlCommand(tarifMalzemeQuery, con, transaction))
								{
									tarifMalzemeCmd.Parameters.AddWithValue("@MalzemeID", malzemeID);
									tarifMalzemeCmd.Parameters.AddWithValue("@TarifID", tarifID);
									tarifMalzemeCmd.Parameters.AddWithValue("@MalzemeMiktar", malzemeMiktar);
									tarifMalzemeCmd.ExecuteNonQuery();
								}

								
								string malzemeQuery = @"UPDATE Malzemeler 
                                              SET MalzemeAdi = @MalzemeAdi,
                                                  BirimFiyat = @BirimFiyat,
                                                  MalzemeBirim = @MalzemeBirim 
                                              WHERE MalzemeID = @MalzemeID";

								using (SqlCommand malzemeCmd = new SqlCommand(malzemeQuery, con, transaction))
								{
									malzemeCmd.Parameters.AddWithValue("@MalzemeID", malzemeID);
									malzemeCmd.Parameters.AddWithValue("@MalzemeAdi", txtMalzemeAdi.Text.Trim());
									malzemeCmd.Parameters.AddWithValue("@BirimFiyat", birimFiyat);
									malzemeCmd.Parameters.AddWithValue("@MalzemeBirim", malzemeBirim);
									int affectedRows = malzemeCmd.ExecuteNonQuery();

									if (affectedRows == 0)
									{
										throw new Exception("Güncellenecek malzeme bulunamadı.");
									}
								}

								transaction.Commit();
							}
							catch (Exception ex)
							{
								transaction.Rollback();
								throw new Exception("Güncelleme işlemi sırasında bir hata oluştu: " + ex.Message);
							}
						}
					}
					LoadMalzemelerByTarif(tarifID);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Malzeme bilgileri güncellenirken bir hata oluştu: {ex.Message}",
								  "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				MessageBox.Show("Lütfen güncellenecek bir malzeme seçiniz.",
							   "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void cmbMalzeme_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbMalzeme.SelectedValue == null) return;

			int selectedValue;
			if (cmbMalzeme.SelectedValue is DataRowView dataRowView)
			{
				selectedValue = Convert.ToInt32(dataRowView["MalzemeID"]);
			}
			else
			{
				selectedValue = Convert.ToInt32(cmbMalzeme.SelectedValue);
			}

			if (selectedValue == -1)
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
						komut.Parameters.AddWithValue("@MalzemeID", selectedValue);

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

		private void InitializeFormControls()
		{
			DoldurMalzemeComboBox();
			txtMalzemeMiktar.KeyPress += new KeyPressEventHandler(txtMalzemeMiktar_KeyPress);
		}
		private void btnMalzemeSil_Click(object sender, EventArgs e)
		    {
			if (dataGridView2.CurrentRow != null && dataGridView1.CurrentRow != null)
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();
					string query = "DELETE FROM TarifMalzeme WHERE MalzemeID = @MalzemeID AND TarifID = @TarifID";
					using (SqlCommand cmd = new SqlCommand(query, con))
					{
						cmd.Parameters.AddWithValue("@MalzemeID", dataGridView2.CurrentRow.Cells["MalzemeID"].Value);
						cmd.Parameters.AddWithValue("@TarifID", dataGridView1.CurrentRow.Cells["TarifID"].Value);
						cmd.ExecuteNonQuery();
					}
				}

				LoadMalzemelerByTarif(Convert.ToInt32(dataGridView1.CurrentRow.Cells["TarifID"].Value));
				MessageBox.Show("Malzeme başarıyla silindi.");
			}
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
	}
}
