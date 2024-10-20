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

		

		public Form2()
		{
			connectionString = ConfigurationManager.ConnectionStrings["TarifRehberiContext"].ConnectionString;
			InitializeComponent();
			this.Load += Form2_Load;
		}

		private void Form2_Load(object sender, EventArgs e)
		{
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
                    t.TarifID, 
                    t.TarifAdi, 
                    t.Kategori, 
                    t.HazirlamaSuresi,
                    COUNT(DISTINCT tm.MalzemeID) AS MalzemeSayisi,
                    STRING_AGG(m.MalzemeAdi, ', ') AS MalzemeAdlari,
                    SUM(tm.MalzemeMiktar * m.BirimFiyat) AS Maliyet
                FROM 
                    Tarifler t
                LEFT JOIN 
                    TarifMalzeme tm ON t.TarifID = tm.TarifID
                LEFT JOIN 
                    Malzemeler m ON tm.MalzemeID = m.MalzemeID
                GROUP BY 
                    t.TarifID, t.TarifAdi, t.Kategori, t.HazirlamaSuresi";

					SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
					recipesTable = new DataTable();
					adapter.Fill(recipesTable);

					if (recipesTable.Rows.Count == 0)
					{
						MessageBox.Show("Tarif veritabanı boş.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}

					filteredTable = recipesTable.Copy();
					DisplayRecipes(filteredTable);

					// PopulateFilterComboBoxes burada çağrılıyor
					PopulateFilterComboBoxes();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Tarifler yüklenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}



		private void DisplayRecipes(DataTable table)
		{
			if (dataGridViewRecipes == null) return;

			dataGridViewRecipes.DataSource = table;

			if (dataGridViewRecipes.Columns["TarifID"] != null) dataGridViewRecipes.Columns["TarifID"].Visible = false;
			if (dataGridViewRecipes.Columns["TarifAdi"] != null) dataGridViewRecipes.Columns["TarifAdi"].HeaderText = "Tarif Adı";
			if (dataGridViewRecipes.Columns["HazirlamaSuresi"] != null) dataGridViewRecipes.Columns["HazirlamaSuresi"].HeaderText = "Hazırlama Süresi";
			if (dataGridViewRecipes.Columns["MalzemeSayisi"] != null) dataGridViewRecipes.Columns["MalzemeSayisi"].HeaderText = "Malzeme Sayısı";
			if (dataGridViewRecipes.Columns["MalzemeAdlari"] != null) dataGridViewRecipes.Columns["MalzemeAdlari"].HeaderText = "Malzeme Adları";
			dataGridViewRecipes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
					 (costRangeFilter == "0-10" && row.Field<double>("Maliyet") <= 10) ||
					 (costRangeFilter == "10-20" && row.Field<double>("Maliyet") > 10 && row.Field<double>("Maliyet") <= 20) ||
					 (costRangeFilter == "20 ve üstü" && row.Field<double>("Maliyet") > 20))
				);

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

		
	}
}