using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TarifRehberi
{
	public partial class FormDetail : Form
	{
		private string connectionString;
		private DataRow recipeData;

		public FormDetail(DataRow dataRow)
		{
			connectionString = ConfigurationManager.ConnectionStrings["TarifRehberiContext"].ConnectionString;
			InitializeComponent();
			this.Load += FormDetail_Load;
			recipeData = dataRow;
			SetupForm();
		}

		private void FormDetail_Load(object sender, EventArgs e)
		{
			// Yükleme işlemleri
		}

		private void SetupForm()
		{
			this.Text = "Tarif Detayları";
			this.Size = new Size(800, 600);
			this.BackColor = Color.White;

			// Sol panel
			Panel leftPanel = new Panel
			{
				Location = new Point(0, 0),
				Size = new Size(400, 600),
				BackColor = Color.Beige
			};
			this.Controls.Add(leftPanel);

			// Sağ panel
			Panel rightPanel = new Panel
			{
				Location = new Point(400, 0),
				Size = new Size(400, 600),
				BackColor = Color.LightGray
			};
			this.Controls.Add(rightPanel);

			// Font büyütme
			Font largeFont = new Font("Arial", 12, FontStyle.Bold);
			Font normalFont = new Font("Arial", 10);

			// Sol paneldeki kontroller
			Label labelTarifAdi = new Label { Location = new Point(20, 20), AutoSize = true, Font = largeFont };
			Label labelKategori = new Label { Location = new Point(20, 60), AutoSize = true, Font = normalFont };
			Label labelSure = new Label { Location = new Point(20, 100), AutoSize = true, Font = normalFont };
			Label labelMalzemeler = new Label { Location = new Point(20, 140), AutoSize = true, Font = normalFont };
			leftPanel.Controls.AddRange(new Control[] { labelTarifAdi, labelKategori, labelSure, labelMalzemeler });

			// Sağ paneldeki kontroller
			Label labelTalimatlar = new Label
			{
				Location = new Point(20, 20),
				AutoSize = true,
				MaximumSize = new Size(360, 0),
				Font = normalFont
			};
			rightPanel.Controls.Add(labelTalimatlar);

			// Veri ataması
			labelTarifAdi.Text = recipeData["TarifAdi"].ToString();
			labelKategori.Text = "Kategori: " + recipeData["Kategori"].ToString();
			labelSure.Text = "Hazırlama Süresi: " + recipeData["HazirlamaSuresi"] + " dakika";
			labelMalzemeler.Text = "Malzemeler:\n" + GetMalzemeler(recipeData);
			labelTalimatlar.Text = "Talimatlar:\n" + GetTalimatlar((int)recipeData["TarifID"]);
		}

		private string GetMalzemeler(DataRow recipeRow)
		{
			int tarifID = (int)recipeRow["TarifID"];
			string malzemeListesi = string.Empty;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string query = @"SELECT m.MalzemeAdi, tm.MalzemeMiktar, m.MalzemeBirim 
                                 FROM Malzemeler m 
                                 JOIN TarifMalzeme tm ON m.MalzemeID = tm.MalzemeID 
                                 WHERE tm.TarifID = @TarifID";

				using (SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@TarifID", tarifID);
					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							malzemeListesi += $"• {reader["MalzemeAdi"]} ({reader["MalzemeMiktar"]} {reader["MalzemeBirim"]})\n";
						}
					}
				}
			}

			return malzemeListesi.TrimEnd('\n');
		}

		private string GetTalimatlar(int tarifID)
		{
			string talimatlar = string.Empty;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string query = "SELECT Talimatlar FROM Tarifler WHERE TarifID = @TarifID";

				using (SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@TarifID", tarifID);
					connection.Open();
					object result = command.ExecuteScalar();
					if (result != null && result != DBNull.Value)
					{
						talimatlar = result.ToString();
					}
				}
			}

			return string.IsNullOrEmpty(talimatlar) ? "Talimat bulunamadı." : talimatlar;
		}
	}
}