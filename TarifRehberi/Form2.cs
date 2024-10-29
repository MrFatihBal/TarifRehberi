using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TarifRehberi
{
	public partial class Form2 : Form
	{
		private string connectionString;
		private DataTable recipesTable;
		private DataTable filteredTable;
		private DataTable availableIngredientsTable;


		public Form2()
		{
			connectionString = ConfigurationManager.ConnectionStrings["TarifRehberiContext"].ConnectionString;
			InitializeComponent();
			this.Load += Form2_Load;
		}

		private void Form2_Load(object sender, EventArgs e)
		{
			LoadAvailableIngredients();
			LoadRecipes();
			SetPlaceholderText(searchBox, "Tarif adına göre ara...");
			SetPlaceholderText(ingredientSearchBox, "Malzemeye göre ara...");
		}
		private void PopulateFilterComboBoxes()
		{
			if (comboBoxIngredientCount != null)
			{
				comboBoxIngredientCount.Items.Clear();
				comboBoxIngredientCount.Items.AddRange(new string[] { "Tüm Malzeme Sayıları", "0-3", "4 ve üstü" });
				comboBoxIngredientCount.SelectedIndex = 0;
			}

			if (comboBoxCategory != null)
			{
				comboBoxCategory.Items.Clear();
				comboBoxCategory.Items.Add("Tüm Kategoriler");
				if (recipesTable != null)
				{
					var categories = recipesTable.AsEnumerable().Select(row => row.Field<string>("Kategori")).Distinct().ToList();
					comboBoxCategory.Items.AddRange(categories.ToArray());
				}
				comboBoxCategory.SelectedIndex = 0;
			}

			if (comboBoxCostRange != null)
			{
				comboBoxCostRange.Items.Clear();
				comboBoxCostRange.Items.AddRange(new string[] { "Tüm Maliyet Aralıkları", "0-10", "10-20", "20 ve üstü" });
				comboBoxCostRange.SelectedIndex = 0;
			}

			if (comboBoxSort != null)
			{
				comboBoxSort.Items.Clear();
				comboBoxSort.Items.AddRange(new string[] { "Sıralama Seçin", "Hazırlama Süresi: Artan", "Hazırlama Süresi: Azalan", "Maliyet: Artan", "Maliyet: Azalan" });
				comboBoxSort.SelectedIndex = 0;
			}
		}
		private void LoadAvailableIngredients()
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					string query = @"
            SELECT 
                m.malzemelerimID,
                m.malzemelerimAdi,
                m.MalzemeMiktar AS ToplamMiktar,
                m.MalzemeBirim,
                m.BirimFiyat,
                mal.MalzemeID  -- Malzemeler tablosundaki ID'yi de alalım
            FROM 
                Malzemelerim m
            INNER JOIN 
                Malzemeler mal ON m.malzemelerimAdi = mal.MalzemeAdi";

					SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
					availableIngredientsTable = new DataTable();
					adapter.Fill(availableIngredientsTable);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Malzemelerim yüklenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void CalculateRecipeStatus()
		{
			try
			{
				if (!recipesTable.Columns.Contains("EslesmeYuzdesi"))
				{
					recipesTable.Columns.Add("EslesmeYuzdesi", typeof(decimal));
				}

				foreach (DataRow recipe in recipesTable.Rows)
				{
					decimal eksikMalzemeMaliyeti = 0;
					bool isRecipePossible = true;
					string malzemeDetaylari = recipe["MalzemeDetaylari"]?.ToString();
					var eksikMalzemeListesi = new System.Text.StringBuilder();
					int toplamMalzemeSayisi = 0;
					int bulunanMalzemeSayisi = 0;

					if (!string.IsNullOrEmpty(malzemeDetaylari))
					{
						string[] malzemeler = malzemeDetaylari.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
						toplamMalzemeSayisi = malzemeler.Length;

						foreach (string malzeme in malzemeler)
						{
							string[] parts = malzeme.Split(':');
							if (parts.Length >= 3)
							{
								if (!int.TryParse(parts[0], out int malzemeId))
								{
									continue;
								}

								if (!decimal.TryParse(parts[1], out decimal gerekleMiktar))
								{
									continue;
								}

								string birim = parts[2];

								var stokMalzeme = availableIngredientsTable.AsEnumerable()
									.FirstOrDefault(r => Convert.ToInt32(r["MalzemeID"]) == malzemeId);

								if (stokMalzeme != null)
								{
									decimal stokMiktar = Convert.ToDecimal(stokMalzeme["ToplamMiktar"]);
									bulunanMalzemeSayisi++; 

									if (stokMiktar < gerekleMiktar)
									{
										isRecipePossible = false;
										decimal eksikMiktar = gerekleMiktar - stokMiktar;
										decimal birimFiyat = Convert.ToDecimal(stokMalzeme["BirimFiyat"]);
										eksikMalzemeMaliyeti += eksikMiktar * birimFiyat;

										eksikMalzemeListesi.AppendLine(
											$"{stokMalzeme["malzemelerimAdi"]?.ToString()}: " +
											$"{eksikMiktar:F2} {stokMalzeme["MalzemeBirim"]?.ToString()}");
									}
								}
								else
								{
									using (SqlConnection conn = new SqlConnection(connectionString))
									{
										conn.Open();
										string query = "SELECT MalzemeAdi, MalzemeBirim, BirimFiyat FROM Malzemeler WHERE MalzemeID = @malzemeId";
										using (SqlCommand cmd = new SqlCommand(query, conn))
										{
											cmd.Parameters.AddWithValue("@malzemeId", malzemeId);
											using (SqlDataReader reader = cmd.ExecuteReader())
											{
												if (reader.Read())
												{
													isRecipePossible = false;
													decimal birimFiyat = Convert.ToDecimal(reader["BirimFiyat"]);
													eksikMalzemeMaliyeti += gerekleMiktar * birimFiyat;

													eksikMalzemeListesi.AppendLine(
														$"{reader["MalzemeAdi"]?.ToString()}: " +
														$"{gerekleMiktar:F2} {reader["MalzemeBirim"]?.ToString()} (Stokta Yok)");
												}
											}
										}
									}
								}
							}
						}
					}
					decimal eslesmeYuzdesi = toplamMalzemeSayisi > 0
						? Math.Round((decimal)bulunanMalzemeSayisi / toplamMalzemeSayisi * 100, 2)
						: 0;

					recipe["EksikMalzemeMaliyeti"] = eksikMalzemeMaliyeti;
					recipe["EksikMalzemeler"] = eksikMalzemeListesi.ToString();
					recipe["EslesmeYuzdesi"] = eslesmeYuzdesi;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"CalculateRecipeStatus'te hata: {ex.Message}\nStack: {ex.StackTrace}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private void SetPlaceholderText(TextBox textBox, string placeholder)
		{
			if (textBox == null) return;

			textBox.Text = placeholder;
			textBox.ForeColor = Color.Gray;

			textBox.Enter += (sender, args) =>
			{
				if (textBox.Text == placeholder)
				{
					textBox.Text = "";
					textBox.ForeColor = Color.Black;
				}
			};

			textBox.Leave += (sender, args) =>
			{
				if (string.IsNullOrWhiteSpace(textBox.Text))
				{
					textBox.Text = placeholder;
					textBox.ForeColor = Color.Gray;
				}
			};
		}


		private void LoadRecipes()
		{
			try
			{
				

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					

					string query = @"
                SELECT 
                    CAST(t.TarifID AS int) AS TarifID, 
                    t.TarifAdi, 
                    t.Kategori, 
                    CAST(t.HazirlamaSuresi AS int) AS HazirlamaSuresi,
                    t.Talimatlar,
                    CAST(COUNT(DISTINCT tm.MalzemeID) AS int) AS MalzemeSayisi,
                    ISNULL(STRING_AGG(m.MalzemeAdi, ', '), '') AS MalzemeAdlari,
                    ISNULL(STRING_AGG(
                        CONCAT(
                            CAST(tm.MalzemeID AS varchar), ':', 
                            CAST(ISNULL(tm.MalzemeMiktar, 0) AS varchar), ':', 
                            ISNULL(m.MalzemeBirim, '')
                        ), ';'
                    ), '') AS MalzemeDetaylari,
                    CAST(ISNULL(SUM(ISNULL(tm.MalzemeMiktar * m.BirimFiyat, 0)), 0) AS decimal(18,2)) AS Maliyet
                FROM 
                    Tarifler t
                LEFT JOIN 
                    TarifMalzeme tm ON t.TarifID = tm.TarifID
                LEFT JOIN 
                    Malzemeler m ON tm.MalzemeID = m.MalzemeID
                GROUP BY 
                    t.TarifID, t.TarifAdi, t.Kategori, t.HazirlamaSuresi, t.Talimatlar";

					

					recipesTable = new DataTable();
					

					using (SqlCommand cmd = new SqlCommand(query, connection))
					{
						connection.Open();

						using (SqlDataReader reader = cmd.ExecuteReader())
						{

							for (int i = 0; i < reader.FieldCount; i++)
							{
								string columnName = reader.GetName(i);
								Type columnType = reader.GetFieldType(i);
								recipesTable.Columns.Add(columnName, columnType);
							}

							while (reader.Read())
							{
								DataRow row = recipesTable.NewRow();
								for (int i = 0; i < reader.FieldCount; i++)
								{
									row[i] = reader.IsDBNull(i) ? DBNull.Value : reader.GetValue(i);
								}
								recipesTable.Rows.Add(row);
							}

						}
					}
					if (recipesTable.Rows.Count == 0)
					{
						MessageBox.Show("Tarif veritabanı boş.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}

					// Eksik malzeme maliyeti için yeni kolon ekle
					if (!recipesTable.Columns.Contains("EksikMalzemeMaliyeti"))
					{
						recipesTable.Columns.Add("EksikMalzemeMaliyeti", typeof(decimal));
						recipesTable.Columns.Add("EksikMalzemeler", typeof(string));
					}


					filteredTable = recipesTable.Copy();

					CalculateRecipeStatus();

					DisplayRecipes(filteredTable);

					PopulateFilterComboBoxes();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Hata Detayı:\nMessage: {ex.Message}\nStackTrace: {ex.StackTrace}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private void DisplayRecipes(DataTable table)
		{
			if (dataGridViewRecipes == null) return;

			dataGridViewRecipes.CellFormatting -= DataGridViewRecipes_CellFormatting;

			dataGridViewRecipes.DataSource = table;

			if (dataGridViewRecipes.Columns["TarifID"] != null) dataGridViewRecipes.Columns["TarifID"].Visible = false;
			if (dataGridViewRecipes.Columns["TarifAdi"] != null) dataGridViewRecipes.Columns["TarifAdi"].HeaderText = "Tarif Adı";
			if (dataGridViewRecipes.Columns["HazirlamaSuresi"] != null) dataGridViewRecipes.Columns["HazirlamaSuresi"].HeaderText = "Hazırlama Süresi";
			if (dataGridViewRecipes.Columns["MalzemeSayisi"] != null) dataGridViewRecipes.Columns["MalzemeSayisi"].HeaderText = "Malzeme Sayısı";
			if (dataGridViewRecipes.Columns["MalzemeAdlari"] != null) dataGridViewRecipes.Columns["MalzemeAdlari"].HeaderText = "Malzeme Adları";
			if (dataGridViewRecipes.Columns["Maliyet"] != null)
			{
				dataGridViewRecipes.Columns["Maliyet"].HeaderText = "Toplam Maliyet";
				dataGridViewRecipes.Columns["Maliyet"].DefaultCellStyle.Format = "C2";
			}
			if (dataGridViewRecipes.Columns["EksikMalzemeMaliyeti"] != null)
			{
				dataGridViewRecipes.Columns["EksikMalzemeMaliyeti"].HeaderText = "Eksik Malzeme Maliyeti";
				dataGridViewRecipes.Columns["EksikMalzemeMaliyeti"].DefaultCellStyle.Format = "C2";
			}
			if (dataGridViewRecipes.Columns["EksikMalzemeler"] != null)
			{
				dataGridViewRecipes.Columns["EksikMalzemeler"].HeaderText = "Eksik Malzemeler";
			}
			if (dataGridViewRecipes.Columns["EslesmeYuzdesi"] != null)
			{
				dataGridViewRecipes.Columns["EslesmeYuzdesi"].HeaderText = "Eşleşme Yüzdesi";
				dataGridViewRecipes.Columns["EslesmeYuzdesi"].DefaultCellStyle.Format = "N2";
				dataGridViewRecipes.Columns["EslesmeYuzdesi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			}
			if (dataGridViewRecipes.Columns["MalzemeDetaylari"] != null) dataGridViewRecipes.Columns["MalzemeDetaylari"].Visible = false;
			if (dataGridViewRecipes.Columns["Talimatlar"] != null) dataGridViewRecipes.Columns["Talimatlar"].Visible = false;

			dataGridViewRecipes.CellFormatting += DataGridViewRecipes_CellFormatting;

			dataGridViewRecipes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		}

		private void DataGridViewRecipes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (dataGridViewRecipes == null || e.RowIndex < 0) return;

			try
			{
				
				if (dataGridViewRecipes.Columns.Contains("EslesmeYuzdesi") &&
					e.ColumnIndex == dataGridViewRecipes.Columns["EslesmeYuzdesi"].Index &&
					e.Value != null)
				{
					if (decimal.TryParse(e.Value.ToString(), out decimal value))
					{
						e.Value = $"%{value:N2}";
						e.FormattingApplied = true;
					}
				}

				
				DataGridViewRow row = dataGridViewRecipes.Rows[e.RowIndex];
				if (row != null && dataGridViewRecipes.Columns.Contains("EksikMalzemeMaliyeti"))
				{
					var eksikMaliyetCell = row.Cells["EksikMalzemeMaliyeti"];
					if (eksikMaliyetCell != null && eksikMaliyetCell.Value != null && eksikMaliyetCell.Value != DBNull.Value)
					{
						double eksikMaliyet = Convert.ToDouble(eksikMaliyetCell.Value);
						row.DefaultCellStyle.BackColor = eksikMaliyet > 0 ? Color.LightPink : Color.LightGreen;
					}
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"CellFormatting error: {ex.Message}");
			}
		}
		private void SearchButton_Click(object sender, EventArgs e)
		{
			ApplyFilters();
		}

		private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ApplyFilters();
		}

		private void ApplyFilters()
		{
			if (recipesTable == null) return;

			string recipeSearchText = searchBox?.Text.ToLower() ?? "";
			string ingredientSearchText = ingredientSearchBox?.Text.ToLower() ?? "";
			string ingredientCountFilter = comboBoxIngredientCount?.SelectedItem?.ToString() ?? "Tüm Malzeme Sayıları";
			string categoryFilter = comboBoxCategory?.SelectedItem?.ToString() ?? "Tüm Kategoriler";
			string costRangeFilter = comboBoxCostRange?.SelectedItem?.ToString() ?? "Tüm Maliyet Aralıkları";

			var filteredRecipes = recipesTable.AsEnumerable()
				.Where(row =>
					(recipeSearchText == "tarif adına göre ara..." || row.Field<string>("TarifAdi").ToLower().Contains(recipeSearchText)) &&
					(ingredientSearchText == "malzemeye göre ara..." || row.Field<string>("MalzemeAdlari").ToLower().Contains(ingredientSearchText)) &&
					(ingredientCountFilter == "Tüm Malzeme Sayıları" ||
					 (ingredientCountFilter == "0-3" && row.Field<int>("MalzemeSayisi") <= 3) ||
					 (ingredientCountFilter == "4 ve üstü" && row.Field<int>("MalzemeSayisi") > 3)) &&
					(categoryFilter == "Tüm Kategoriler" || row.Field<string>("Kategori") == categoryFilter) &&
					(costRangeFilter == "Tüm Maliyet Aralıkları" ||
					 (costRangeFilter == "0-10" && row.Field<decimal>("Maliyet") <= 10) ||
					 (costRangeFilter == "10-20" && row.Field<decimal>("Maliyet") > 10 && row.Field<decimal>("Maliyet") <= 20) ||
					 (costRangeFilter == "20 ve üstü" && row.Field<decimal>("Maliyet") > 20)));
			
			if (filteredRecipes.Any())
			{
				filteredTable = filteredRecipes.CopyToDataTable();
				ApplySorting();
			}
			else
			{
				if (dataGridViewRecipes != null) dataGridViewRecipes.DataSource = null;
				MessageBox.Show("Seçilen filtrelere uygun tarif bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void ApplySorting()
		{
			if (filteredTable == null || filteredTable.Rows.Count == 0 || comboBoxSort == null) return;

			try
			{
				DataView view = filteredTable.DefaultView;

				switch (comboBoxSort.SelectedItem?.ToString())
				{
					case "Hazırlama Süresi: Artan":
						view.Sort = "HazirlamaSuresi ASC";
						break;
					case "Hazırlama Süresi: Azalan":
						view.Sort = "HazirlamaSuresi DESC";
						break;
					case "Maliyet: Artan":
						view.Sort = "Maliyet ASC";
						break;
					case "Maliyet: Azalan":
						view.Sort = "Maliyet DESC";
						break;
					default:
						view.Sort = "";
						break;
				}

				filteredTable = view.ToTable();
				DisplayRecipes(filteredTable);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Sıralama sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private void DataGridViewRecipes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0 && dataGridViewRecipes != null)
			{
				DataGridViewRow row = dataGridViewRecipes.Rows[e.RowIndex];
				if (row.DataBoundItem is DataRowView drv)
				{
					DataRow selectedRow = drv.Row;

					FormDetail detailForm = new FormDetail(selectedRow);
					detailForm.ShowDialog();
				}
			}
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{

		}
	}
}