using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TarifRehberi
{
	public class Db
	{
		private string connectionString;
		public Db() 
		{
			connectionString = ConfigurationManager.ConnectionStrings["TarifRehberiContext"].ConnectionString;
		}	
		public void CreateDatabaseAndTables()
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string createDatabase = "IF DB_ID('TarifRehberi') IS NULL CREATE DATABASE TarifRehberi;";
					using (SqlCommand command = new SqlCommand(createDatabase, connection))
					{
						command.ExecuteNonQuery();
						
					}

					string createTariflerTable = @"
                        IF OBJECT_ID('Tarifler', 'U') IS NULL
                        CREATE TABLE Tarifler (
                            TarifID INT PRIMARY KEY IDENTITY,
                            TarifAdi VARCHAR(255) NOT NULL,
                            Kategori VARCHAR(100) NOT NULL,
                            HazirlamaSuresi INT NOT NULL,
                            Talimatlar TEXT NOT NULL
                        );";

					string createMalzemelerTable = @"
                        IF OBJECT_ID('Malzemeler', 'U') IS NULL
                        CREATE TABLE Malzemeler (
                            MalzemeID INT PRIMARY KEY IDENTITY,
                            MalzemeAdi VARCHAR(255) NOT NULL,
                            ToplamMiktar VARCHAR(50),

                            MalzemeBirim VARCHAR(50),
                            BirimFiyat FLOAT
                        );";

					string createTarifMalzemeTable = @"
                        IF OBJECT_ID('TarifMalzeme', 'U') IS NULL
                        CREATE TABLE TarifMalzeme (
                            TarifID INT,
                            MalzemeID INT,
                            MalzemeMiktar FLOAT,
                            FOREIGN KEY (TarifID) REFERENCES Tarifler(TarifID),
                            FOREIGN KEY (MalzemeID) REFERENCES Malzemeler(MalzemeID)
                        );";

					using (SqlCommand command = new SqlCommand(createTariflerTable, connection))
					{
						command.ExecuteNonQuery();
						
					}

					using (SqlCommand command = new SqlCommand(createMalzemelerTable, connection))
					{
						command.ExecuteNonQuery();
						
					}

					using (SqlCommand command = new SqlCommand(createTarifMalzemeTable, connection))
					{
						command.ExecuteNonQuery();
						
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Hata: " + ex.Message);
			}
		}
		public void InsertSampleData()
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					var malzemeler = new[]
					{
				new { Adi = "Yoğurt", Birim = "Kilogram", Fiyat = 20.0 },
				new { Adi = "Tereyağı", Birim = "Kilogram", Fiyat = 80.0 },
				new { Adi = "Un", Birim = "Kilogram", Fiyat = 8.0 },
				new { Adi = "Mercimek", Birim = "Kilogram", Fiyat = 15.0 },
				new { Adi = "Domates", Birim = "Kilogram", Fiyat = 10.0 },
				new { Adi = "Salatalık", Birim = "Kilogram", Fiyat = 7.0 },
				new { Adi = "Soğan", Birim = "Kilogram", Fiyat = 5.0 },
				new { Adi = "Marul", Birim = "Adet", Fiyat = 3.0 },
				new { Adi = "Tavuk", Birim = "Kilogram", Fiyat = 30.0 },
				new { Adi = "Patlıcan", Birim = "Kilogram", Fiyat = 12.0 },
				new { Adi = "Kıyma", Birim = "Kilogram", Fiyat = 90.0 },
				new { Adi = "Pirinç", Birim = "Kilogram", Fiyat = 12.0 },
				new { Adi = "Hamur", Birim = "Kilogram", Fiyat = 25.0 },
				new { Adi = "Fıstık", Birim = "Kilogram", Fiyat = 100.0 },
				new { Adi = "Süt", Birim = "Litre", Fiyat = 7.0 },
				new { Adi = "Limon", Birim = "Adet", Fiyat = 1.5 },
				new { Adi = "Şeker", Birim = "Kilogram", Fiyat = 6.0 },
				new { Adi = "Su", Birim = "Litre", Fiyat = 1.0 },
				new { Adi = "Bulgur", Birim = "Kilogram", Fiyat = 15.0 },
	            new { Adi = "Şehriye", Birim = "Kilogram", Fiyat = 12.0 },
				new { Adi = "Tarhana", Birim = "Kilogram", Fiyat = 25.0 },
				new { Adi = "Havuç", Birim = "Kilogram", Fiyat = 8.0 },
				new { Adi = "Kabak", Birim = "Kilogram", Fiyat = 10.0 },
				new { Adi = "Patates", Birim = "Kilogram", Fiyat = 6.0 },
				new { Adi = "Balık", Birim = "Kilogram", Fiyat = 45.0 },
				new { Adi = "Kuzu Eti", Birim = "Kilogram", Fiyat = 80.0 },
				new { Adi = "Biber", Birim = "Kilogram", Fiyat = 10.0 },
				new { Adi = "Roka", Birim = "Demet", Fiyat = 5.0 },
				new { Adi = "Bezelye", Birim = "Kilogram", Fiyat = 14.0 },
				new { Adi = "İnce Bulgur", Birim = "Kilogram", Fiyat = 18.0 },
				new { Adi = "Ton Balığı", Birim = "Konserve", Fiyat = 12.0 },
				new { Adi = "Mısır", Birim = "Kilogram", Fiyat = 7.0 },
				new { Adi = "Avokado", Birim = "Adet", Fiyat = 15.0 },
				new { Adi = "Mayonez", Birim = "Kilogram", Fiyat = 18.0 },
				new { Adi = "Tavuk Göğsü", Birim = "Kilogram", Fiyat = 25.0 },
				new { Adi = "Makarna", Birim = "Paket", Fiyat = 6.0 },
				new { Adi = "Enginar", Birim = "Adet", Fiyat = 10.0 },
				new { Adi = "Zeytinyağı", Birim = "Litre", Fiyat = 40.0 },
				new { Adi = "Limon Suyu", Birim = "Litre", Fiyat = 20.0 },
				new { Adi = "Kuskus", Birim = "Kilogram", Fiyat = 10.0 },
				new { Adi = "Spagetti", Birim = "Paket", Fiyat = 7.0 },
				new { Adi = "Kaşar Peyniri", Birim = "Kilogram", Fiyat = 35.0 },
				new { Adi = "Ekmek İçi", Birim = "Adet", Fiyat = 2.0 },
				new { Adi = "Yufka", Birim = "Adet", Fiyat = 5.0 },
				new { Adi = "Ceviz", Birim = "Kilogram", Fiyat = 80.0 },
				new { Adi = "Çikolata", Birim = "Adet", Fiyat = 10.0 },
				new { Adi = "Kuru Fasulye", Birim = "Kilogram", Fiyat = 12.0 },
				new { Adi = "Vanilya", Birim = "Paket", Fiyat = 3.0 },
				new { Adi = "Kedidili", Birim = "Paket", Fiyat = 12.0 },
				new { Adi = "Kahve", Birim = "Kilogram", Fiyat = 50.0 },
				new { Adi = "Mascarpone", Birim = "Kilogram", Fiyat = 60.0 },
				new { Adi = "Meyve", Birim = "Kilogram", Fiyat = 20.0 },
				new { Adi = "Puding", Birim = "Paket", Fiyat = 8.0 },
				new { Adi = "Karamel", Birim = "Paket", Fiyat = 15.0 },
				new { Adi = "Sakız", Birim = "Paket", Fiyat = 5.0 },
				new { Adi = "Yumurta", Birim = "Adet", Fiyat = 2.5 },
				new { Adi = "Bal", Birim = "Kilogram", Fiyat = 50.0 },
				new { Adi = "Çay", Birim = "Kilogram", Fiyat = 30.0 },
				new { Adi = "Nane", Birim = "Demet", Fiyat = 5.0 },
				new { Adi = "Buz", Birim = "Kilogram", Fiyat = 3.0 }
				};

					foreach (var malzeme in malzemeler)
					{
						string insertMalzeme = @"
                    IF NOT EXISTS (SELECT 1 FROM Malzemeler WHERE MalzemeAdi = @MalzemeAdi)
                    INSERT INTO Malzemeler (MalzemeAdi, ToplamMiktar, MalzemeBirim, BirimFiyat)
                    VALUES (@MalzemeAdi, '100', @MalzemeBirim, @BirimFiyat)";

						using (SqlCommand command = new SqlCommand(insertMalzeme, connection))
						{
							command.Parameters.AddWithValue("@MalzemeAdi", malzeme.Adi);
							command.Parameters.AddWithValue("@MalzemeBirim", malzeme.Birim);
							command.Parameters.AddWithValue("@BirimFiyat", malzeme.Fiyat);
							command.ExecuteNonQuery();
						}
					}

					var tarifler = new[]
					{
                    // Çorbalar
                    new Tarif
				    {
					Adi = "Yayla Çorbası",
					Kategori = "Çorba",
					Sure = 20,
					Talimat = "Yoğurt, un ve su bir tencereye alınarak karıştırılır. Orta ateşte karıştırmaya devam edilir. Ayrı bir tavada tereyağı eritilir ve üzerine nane eklenir. Karışımın üzerine tuz ilave edilip karıştırmaya devam edilir. Kaynamaya başladığında tereyağlı karışım çorbanın üzerine dökülür. Çorba sıcak olarak servis edilir ve yanında limon dilimleri ile sunulabilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Yoğurt", Miktar = 0.2 },
						new Malzeme { Adi = "Tereyağı", Miktar = 0.05 },
						new Malzeme { Adi = "Un", Miktar = 0.03 }
					}
				    },
				    new Tarif
					{
					Adi = "Mercimek Çorbası",
					Kategori = "Çorba",
					Sure = 25,
					Talimat = "Mercimekler yıkanır ve süzülür. Tencereye sıvı yağ konulur, ardından doğranmış soğan ve havuç eklenir. Hafifçe kavrulduktan sonra mercimek ve su eklenir. Malzemeler iyice yumuşayana kadar pişirilir. Blender ile pürüzsüz hale getirilir ve üzerine tereyağında kavrulmuş kırmızı biber eklenerek sıcak servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Mercimek", Miktar = 0.2 },
						new Malzeme { Adi = "Soğan", Miktar = 0.1 }
					}
				},
				new Tarif
				{
					Adi = "Domates Çorbası",
					Kategori = "Çorba",
					Sure = 30,
					Talimat = "Taze domatesler rendelenir ve tencerede tereyağı ile kavrulur. Un eklenerek birkaç dakika daha kavrulmaya devam edilir. Yavaşça su eklenir ve karışım kaynamaya bırakılır. Blender ile karıştırılarak pürüzsüz bir kıvam elde edilir. Üzerine rendelenmiş kaşar peyniri serpilerek sıcak servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Domates", Miktar = 0.4 },
						new Malzeme { Adi = "Un", Miktar = 0.02 },
						new Malzeme { Adi = "Tereyağı", Miktar = 0.05 }
					}
				},
				new Tarif
				{
					Adi = "Ezogelin Çorbası",
					Kategori = "Çorba",
					Sure = 35,
					Talimat = "Mercimek, bulgur ve pirinç yıkanır, tencereye alınır. Üzerine su eklenerek kaynamaya bırakılır. Ayrı bir tavada soğan, sarımsak, un ve biber salçası kavrulur. Kavrulan karışım kaynamakta olan çorbaya eklenir. İyice piştikten sonra blenderdan geçirilir ve üzerine tereyağında kavrulmuş nane dökülerek sıcak servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Mercimek", Miktar = 0.2 },
						new Malzeme { Adi = "Bulgur", Miktar = 0.1 },
						new Malzeme { Adi = "Pirinç", Miktar = 0.05 }
					}
				},
				new Tarif
				{
					Adi = "Şehriye Çorbası",
					Kategori = "Çorba",
					Sure = 20,
					Talimat = "Tencereye sıvı yağ alınır ve ince doğranmış soğan hafifçe kavrulur. Üzerine salça eklenir ve kokusu çıkana kadar kavurmaya devam edilir. Ardından şehriye eklenir ve su ile karıştırılır. Şehriyeler yumuşayana kadar kaynatılır. Son olarak limon suyu ve baharatlar eklenerek sıcak servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Şehriye", Miktar = 0.1 },
						new Malzeme { Adi = "Soğan", Miktar = 0.05 }
					}
				},
				new Tarif
				{
					Adi = "Tarhana Çorbası",
					Kategori = "Çorba",
					Sure = 15,
					Talimat = "Tarhana, soğuk su ile karıştırılarak eritilir. Tencereye tereyağı alınır ve eritilir, ardından tarhana karışımı eklenir. Orta ateşte sürekli karıştırarak pişirilir. Kıvamı koyulaşmaya başladığında baharatlar eklenir ve birkaç dakika daha kaynatıldıktan sonra sıcak servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Tarhana", Miktar = 0.1 },
						new Malzeme { Adi = "Tereyağı", Miktar = 0.05 }
					}
				},
				new Tarif
				{
					Adi = "Sebze Çorbası",
					Kategori = "Çorba",
					Sure = 30,
					Talimat = "Havuç, kabak, patates ve soğan küçük küçük doğranır ve tencereye alınır. Üzerine su eklenir ve sebzeler yumuşayana kadar pişirilir. Blender ile çorba pürüzsüz hale getirilir ve üzerine zeytinyağı ile kavrulmuş nane eklenerek sıcak servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Havuç", Miktar = 0.1 },
						new Malzeme { Adi = "Kabak", Miktar = 0.1 },
						new Malzeme { Adi = "Patates", Miktar = 0.1 },
						new Malzeme { Adi = "Soğan", Miktar = 0.05 }
					}
				},
				new Tarif
				{
					Adi = "Tavuk Suyu Çorbası",
					Kategori = "Çorba",
					Sure = 40,
					Talimat = "Tavuk suyu kaynatılır ve üzerine ince ince doğranmış havuç eklenir. Ayrı bir tencerede tereyağı eritilir ve un kavrulur. Tavuk suyu yavaşça eklenerek karıştırılır. Haşlanmış tavuk didiklenir ve çorbaya ilave edilir. Kıvamı koyulaşana kadar kaynatılır ve sıcak servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Tavuk", Miktar = 0.3 },
						new Malzeme { Adi = "Un", Miktar = 0.02 },
						new Malzeme { Adi = "Tereyağı", Miktar = 0.05 }
					}
				},
				new Tarif
				{
					Adi = "Balık Çorbası",
					Kategori = "Çorba",
					Sure = 50,
					Talimat = "Balık suyu kaynatılır ve içerisine defne yaprağı, karabiber eklenir. Küçük doğranmış havuç, patates ve soğan ilave edilerek sebzeler yumuşayana kadar pişirilir. Haşlanmış balık eti didiklenir ve çorbaya eklenir. Limon suyu ve zeytinyağı eklenerek sıcak servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Balık", Miktar = 0.4 },
						new Malzeme { Adi = "Havuç", Miktar = 0.1 },
						new Malzeme { Adi = "Patates", Miktar = 0.1 }
					}
				},
				new Tarif
				{
					Adi = "Düğün Çorbası",
					Kategori = "Çorba",
					Sure = 45,
					Talimat = "Kuzu eti tencereye alınır ve üzerini geçecek kadar su eklenerek haşlanır. Ayrı bir tavada tereyağı ve un kavrulur. Kavrulan un, haşlama suyuna eklenir ve kıvam alınana kadar pişirilir. Üzerine limon suyu ve tuz eklenerek sıcak servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Kuzu Eti", Miktar = 0.3 },
						new Malzeme { Adi = "Tereyağı", Miktar = 0.05 },
						new Malzeme { Adi = "Un", Miktar = 0.03 }
					}
				},

  			  // Salatalar
   			 new Tarif
				{
					Adi = "Çoban Salata",
					Kategori = "Salata",
					Sure = 15,
					Talimat = "Domates, salatalık, biber ve soğan doğranır. Üzerine zeytinyağı, limon suyu ve tuz eklenerek karıştırılır. Servis edilmeden önce taze nane eklenebilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Domates", Miktar = 0.3 },
						new Malzeme { Adi = "Salatalık", Miktar = 0.3 },
						new Malzeme { Adi = "Biber", Miktar = 0.1 }
					}
				},
				new Tarif
				{
					Adi = "Mevsim Salata",
					Kategori = "Salata",
					Sure = 10,
					Talimat = "Mevsim yeşillikleri doğranır. Üzerine limon suyu, zeytinyağı, tuz ve isteğe bağlı baharat eklenir. Karıştırılarak servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Marul", Miktar = 0.2 },
						new Malzeme { Adi = "Roka", Miktar = 0.1 }
					}
				},
				new Tarif
				{
					Adi = "Pirinç Salatası",
					Kategori = "Salata",
					Sure = 30,
					Talimat = "Haşlanmış pirinç, doğranmış sebzeler (biber, havuç, bezelye) ile karıştırılır. Üzerine yoğurt, mayonez ve limon suyu eklenerek karıştırılır ve soğutulmuş şekilde servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Pirinç", Miktar = 0.2 },
						new Malzeme { Adi = "Havuç", Miktar = 0.1 },
						new Malzeme { Adi = "Bezelye", Miktar = 0.1 }
					}
				},
				new Tarif
				{
					Adi = "Kısır",
					Kategori = "Salata",
					Sure = 20,
					Talimat = "İnce bulgur, sıcak su ile ıslatılır. Üzerine domates, salatalık, yeşil soğan doğranır. Nar ekşisi, zeytinyağı ve baharatlar eklenerek yoğrulur ve servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "İnce Bulgur", Miktar = 0.2 },
						new Malzeme { Adi = "Domates", Miktar = 0.1 },
						new Malzeme { Adi = "Salatalık", Miktar = 0.1 }
					}
				},
				new Tarif
				{
					Adi = "Bulgur Salatası",
					Kategori = "Salata",
					Sure = 25,
					Talimat = "Bulgur haşlanır, ardından doğranmış sebzeler (domates, biber, soğan) ile karıştırılır. Zeytinyağı, limon suyu ve tuz eklenerek harmanlanır ve soğutulmuş şekilde servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Bulgur", Miktar = 0.2 },
						new Malzeme { Adi = "Domates", Miktar = 0.1 },
						new Malzeme { Adi = "Biber", Miktar = 0.1 }
					}
				},
				new Tarif
				{
					Adi = "Ton Balıklı Salata",
					Kategori = "Salata",
					Sure = 15,
					Talimat = "Konserve ton balığı, doğranmış salatalık, mısır ve zeytin ile karıştırılır. Üzerine zeytinyağı ve limon suyu eklenir. Karıştırılarak servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Ton Balığı", Miktar = 0.2 },
						new Malzeme { Adi = "Salatalık", Miktar = 0.1 },
						new Malzeme { Adi = "Mısır", Miktar = 0.1 }
					}
				},
				new Tarif
				{
					Adi = "Avokado Salatası",
					Kategori = "Salata",
					Sure = 20,
					Talimat = "Avokadolar doğranır, üzerine domates, soğan ve maydanoz eklenir. Zeytinyağı ve limon suyu eklenerek karıştırılır. İsteğe bağlı olarak üzerine ceviz serpilebilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Avokado", Miktar = 0.2 },
						new Malzeme { Adi = "Domates", Miktar = 0.1 },
						new Malzeme { Adi = "Soğan", Miktar = 0.05 }
					}
				},
				new Tarif
				{
					Adi = "Fattoush Salatası",
					Kategori = "Salata",
					Sure = 25,
					Talimat = "Taze sebzeler (domates, salatalık, marul) doğranır. Kızarmış pita ekmekleri eklenir. Zeytinyağı ve nar ekşisi ile tatlandırılır. Karıştırılarak servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Marul", Miktar = 0.2 },
						new Malzeme { Adi = "Domates", Miktar = 0.1 },
						new Malzeme { Adi = "Salatalık", Miktar = 0.1 }
					}
				},
				new Tarif
				{
					Adi = "Patates Salatası",
					Kategori = "Salata",
					Sure = 30,
					Talimat = "Patatesler haşlanır ve küp şeklinde doğranır. Soğan, mayonez, limon suyu ve tuz eklenerek karıştırılır. İsteğe bağlı olarak haşlanmış yumurta da eklenebilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Patates", Miktar = 0.3 },
						new Malzeme { Adi = "Soğan", Miktar = 0.1 },
						new Malzeme { Adi = "Mayonez", Miktar = 0.1 }
					}
				},
				new Tarif
				{
					Adi = "Sezar Salata",
					Kategori = "Salata",
					Sure = 20,
					Talimat = "Marul yapraklarını doğrayın. Üzerine tavuk göğsü, kruton ekmek ve parmesan peyniri ekleyin. Zeytinyağı, limon suyu, tuz ve karabiber ile hazırlanan sosu dökün. Karıştırarak servis yapın.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Marul", Miktar = 0.5 },
						new Malzeme { Adi = "Tavuk Göğsü", Miktar = 0.2 },
						new Malzeme { Adi = "Ekmek İçi", Miktar = 0.1 },
						new Malzeme { Adi = "Kaşar Peyniri", Miktar = 0.05 },
						new Malzeme { Adi = "Zeytinyağı", Miktar = 0.05 },
						new Malzeme { Adi = "Limon Suyu", Miktar = 0.05 }
					}
				},
				//İçecekler
				new Tarif
				{
					Adi = "Limonata",
					Kategori = "İçecek",
					Sure = 15,
					Talimat = "Limonlar sıkılır ve su ile karıştırılır. Şeker eklenir ve karıştırılır. Buzdolabında soğutulur.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Limon", Miktar = 0.5 },
						new Malzeme { Adi = "Su", Miktar = 1.0 },
						new Malzeme { Adi = "Şeker", Miktar = 0.1 }
					}
				},
				new Tarif
				{
					Adi = "Portakal Suyu",
					Kategori = "İçecek",
					Sure = 10,
					Talimat = "Portakallar sıkılır ve sürahiye alınır. Soğuk servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Meyve", Miktar = 0.5 }
					}
				},
				new Tarif
				{
					Adi = "Çilekli Smoothie",
					Kategori = "İçecek",
					Sure = 10,
					Talimat = "Çilek, yoğurt ve balı blenderda karıştırın. Soğuk servis edin.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Meyve", Miktar = 0.3 },
						new Malzeme { Adi = "Yoğurt", Miktar = 0.2 },
						new Malzeme { Adi = "Bal", Miktar = 0.05 }
					}
				},
				new Tarif
				{
					Adi = "Meyve Kokteyli",
					Kategori = "İçecek",
					Sure = 10,
					Talimat = "Karışık meyveleri blenderda karıştırın. Buz ekleyerek soğuk servis edin.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Meyve", Miktar = 0.5 },
						new Malzeme { Adi = "Su", Miktar = 0.3 }
					}
				},
				new Tarif
				{
					Adi = "Buzlu Çay",
					Kategori = "İçecek",
					Sure = 20,
					Talimat = "Çayı demleyin ve soğumaya bırakın. Limon dilimleri ve buzla servis edin.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Çay", Miktar = 0.2 },
						new Malzeme { Adi = "Limon", Miktar = 0.1 },
						new Malzeme { Adi = "Şeker", Miktar = 0.05 }
					}
				},
				new Tarif
				{
					Adi = "Mango Lassi",
					Kategori = "İçecek",
					Sure = 10,
					Talimat = "Mango, yoğurt ve şeker blenderda karıştırılır. Soğuk servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Meyve", Miktar = 0.4 },
						new Malzeme { Adi = "Yoğurt", Miktar = 0.3 },
						new Malzeme { Adi = "Şeker", Miktar = 0.05 }
					}
				},
				new Tarif
				{
					Adi = "Naneli Ayran",
					Kategori = "İçecek",
					Sure = 5,
					Talimat = "Yoğurt, su ve nane karıştırılır. Soğuk servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Yoğurt", Miktar = 0.3 },
						new Malzeme { Adi = "Su", Miktar = 0.2 },
						new Malzeme { Adi = "Nane", Miktar = 0.01 } 
    			    }
				},
				new Tarif
				{
					Adi = "Karpuzlu Serinletici",
					Kategori = "İçecek",
					Sure = 10,
					Talimat = "Karpuz dilimlerini blenderda karıştırın. Buz ekleyin ve servis yapın.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Meyve", Miktar = 0.5 },
						new Malzeme { Adi = "Su", Miktar = 0.2 }
					}
				},
				new Tarif
				{
					Adi = "Ballı Süt",
					Kategori = "İçecek",
					Sure = 5,
					Talimat = "Süt ve balı karıştırın. Ilık veya soğuk servis edin.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Süt", Miktar = 0.3 },
						new Malzeme { Adi = "Bal", Miktar = 0.05 }
					}
				},
				new Tarif
				{
					Adi = "Kahveli Frappe",
					Kategori = "İçecek",
					Sure = 10,
					Talimat = "Kahve, süt ve buz blenderda karıştırılır. Soğuk servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Kahve", Miktar = 0.05 },
						new Malzeme { Adi = "Süt", Miktar = 0.2 },
						new Malzeme { Adi = "Buz", Miktar = 0.1 } 
     			   }
				},
    			new Tarif
				{
					Adi = "Tavuk Sote",
					Kategori = "Ana Yemek",
					Sure = 40,
					Talimat = "Tavuk göğsü küp şeklinde doğranır. Tavada sıvı yağ ile birlikte kavrulur. Üzerine doğranmış sebzeler (biber, soğan, havuç) eklenir. Baharatlar ve tuz ilave edilerek pişirilir. Sıcak olarak servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Tavuk Göğsü", Miktar = 0.4 },
						new Malzeme { Adi = "Biber", Miktar = 0.1 },
						new Malzeme { Adi = "Soğan", Miktar = 0.05 }
					}
				},
				new Tarif
				{
					Adi = "Kıymalı Makarna",
					Kategori = "Ana Yemek",
					Sure = 30,
					Talimat = "Kıymalar tavada kavrulur. Üzerine doğranmış soğan ve biber eklenir. Piştikten sonra haşlanmış makarna ile karıştırılır. Sıcak servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Kıyma", Miktar = 0.3 },
						new Malzeme { Adi = "Makarna", Miktar = 0.2 },
						new Malzeme { Adi = "Soğan", Miktar = 0.05 }
					}
				},
				new Tarif
				{
					Adi = "Zeytinyağlı Enginar",
					Kategori = "Ana Yemek",
					Sure = 50,
					Talimat = "Enginarlar temizlenir. Zeytinyağı, limon suyu ve baharatlarla birlikte pişirilir. Sıcak veya soğuk servis edilebilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Enginar", Miktar = 0.4 },
						new Malzeme { Adi = "Zeytinyağı", Miktar = 0.1 },
						new Malzeme { Adi = "Limon Suyu", Miktar = 0.02 }
					}
				},
				new Tarif
				{
					Adi = "Fırında Tavuk",
					Kategori = "Ana Yemek",
					Sure = 60,
					Talimat = "Tavuk, baharatlar ve zeytinyağı ile marine edilir. Fırında pişirilir. Yanında sebzeler ile servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Tavuk", Miktar = 1.0 },
						new Malzeme { Adi = "Zeytinyağı", Miktar = 0.1 }
					}
				},
				new Tarif
				{
					Adi = "Sebzeli Kuskus",
					Kategori = "Ana Yemek",
					Sure = 30,
					Talimat = "Kuskus, sıcak su ile demlenir. Üzerine haşlanmış sebzeler (biber, havuç, bezelye) eklenir. Zeytinyağı ile karıştırılarak servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Kuskus", Miktar = 0.2 },
						new Malzeme { Adi = "Biber", Miktar = 0.1 },
						new Malzeme { Adi = "Havuç", Miktar = 0.1 }
					}
				},
				new Tarif
				{
					Adi = "Balık Tava",
					Kategori = "Ana Yemek",
					Sure = 40,
					Talimat = "Balık, unlanarak tavada kızartılır. Yanında limon ve yeşilliklerle servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Balık", Miktar = 0.4 },
						new Malzeme { Adi = "Un", Miktar = 0.05 },
						new Malzeme { Adi = "Limon", Miktar = 0.1 }
					}
				},
				new Tarif
				{
					Adi = "Soslu Spagetti",
					Kategori = "Ana Yemek",
					Sure = 30,
					Talimat = "Spagetti haşlanır. Tavada hazırlanan sos (domates, soğan, biber) ile karıştırılır. Sıcak servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Spagetti", Miktar = 0.2 },
						new Malzeme { Adi = "Domates", Miktar = 0.1 },
						new Malzeme { Adi = "Soğan", Miktar = 0.05 }
					}
				},
				new Tarif
				{
					Adi = "Kumpir",
					Kategori = "Ana Yemek",
					Sure = 45,
					Talimat = "Patatesler haşlanır ve ezilir. Üzerine tereyağı, kaşar peyniri, zeytin ve diğer isteğe bağlı malzemeler eklenir. Karıştırılarak servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Patates", Miktar = 0.3 },
						new Malzeme { Adi = "Tereyağı", Miktar = 0.05 },
						new Malzeme { Adi = "Kaşar Peyniri", Miktar = 0.1 }
					}
				},
				new Tarif
				{
					Adi = "Mantı",
					Kategori = "Ana Yemek",
					Sure = 90,
					Talimat = "Hamur açılır ve içine kıyma konularak kapatılır. Kaynar suda haşlanır. Yoğurt ve sos ile servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Hamur", Miktar = 0.4 },
						new Malzeme { Adi = "Kıyma", Miktar = 0.2 },
						new Malzeme { Adi = "Yoğurt", Miktar = 0.1 }
					}
				},
				new Tarif
				{
					Adi = "Köfte",
					Kategori = "Ana Yemek",
					Sure = 40,
					Talimat = "Kıyma, soğan, baharatlar ve ekmek içi ile yoğrulur. Şekil verilerek tavada pişirilir. Sıcak olarak servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Kıyma", Miktar = 0.3 },
						new Malzeme { Adi = "Soğan", Miktar = 0.1 },
						new Malzeme { Adi = "Ekmek İçi", Miktar = 0.05 }
					}
				},

    			// Tatlılar
   			    new Tarif
				{
					Adi = "Baklava",
					Kategori = "Tatlı",
					Sure = 60,
					Talimat = "Yufkalar açılır, aralarına ceviz konulur ve yağ ile kaplanır. Fırında pişirilir ve üzerine şerbet dökülerek servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Yufka", Miktar = 0.5 },
						new Malzeme { Adi = "Ceviz", Miktar = 0.3 },
						new Malzeme { Adi = "Şeker", Miktar = 0.1 }
					}
				},
			    new Tarif
				{
					Adi = "Browni",
					Kategori = "Tatlı",
					Sure = 40,
					Talimat = "Çikolata ve tereyağını benmari usulü eritin. Şeker ve yumurtayı çırpın. Çikolatalı karışımı ekleyin, un ekleyip karıştırın. Karışımı kalıba dökün ve 180 derecede 25-30 dakika pişirin.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Çikolata", Miktar = 0.2 },
						new Malzeme { Adi = "Tereyağı", Miktar = 0.1 },
						new Malzeme { Adi = "Şeker", Miktar = 0.2 },
						new Malzeme { Adi = "Un", Miktar = 0.1 },
						new Malzeme { Adi = "Yumurta", Miktar = 3.0 } 
    				}
				},
				new Tarif
				{
					Adi = "Sütlaç",
					Kategori = "Tatlı",
					Sure = 45,
					Talimat = "Pirinç haşlanır, süt ve şeker eklenir. Koyulaşana kadar pişirilir. Soğutularak servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Pirinç", Miktar = 0.1 },
						new Malzeme { Adi = "Süt", Miktar = 0.5 },
						new Malzeme { Adi = "Şeker", Miktar = 0.1 }
					}
				},
				new Tarif
				{
					Adi = "Profiterol",
					Kategori = "Tatlı",
					Sure = 90,
					Talimat = "Hamur hazırlanır, fırında pişirilir. Üzerine çikolata sosu dökülerek servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Hamur", Miktar = 0.3 },
						new Malzeme { Adi = "Çikolata", Miktar = 0.2 }
					}
				},
				new Tarif
				{
					Adi = "Çikolatalı Kek",
					Kategori = "Tatlı",
					Sure = 50,
					Talimat = "Malzemeler çırpılır, hamur hazırlanır ve fırında pişirilir. Soğuyunca dilimlenerek servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Un", Miktar = 0.3 },
						new Malzeme { Adi = "Şeker", Miktar = 0.2 },
						new Malzeme { Adi = "Çikolata", Miktar = 0.1 }
					}
				},
				new Tarif
				{
					Adi = "Kuru Fasulye Tatlısı",
					Kategori = "Tatlı",
					Sure = 60,
					Talimat = "Kuru fasulyeler haşlanır. Şeker, vanilya ve diğer malzemeler eklenerek pişirilir. Soğutularak servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Kuru Fasulye", Miktar = 0.3 },
						new Malzeme { Adi = "Şeker", Miktar = 0.1 },
						new Malzeme { Adi = "Vanilya", Miktar = 0.02 }
					}
				},
				new Tarif
				{
					Adi = "Tiramisu",
					Kategori = "Tatlı",
					Sure = 60,
					Talimat = "Kedidilinin üzerine kahve ve mascarpone karışımı eklenir. Buzdolabında dinlendirilerek servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Kedidili", Miktar = 0.3 },
						new Malzeme { Adi = "Kahve", Miktar = 0.1 },
						new Malzeme { Adi = "Mascarpone", Miktar = 0.2 }
					}
				},
				new Tarif
				{
					Adi = "Meyveli Yoğurt",
					Kategori = "Tatlı",
					Sure = 20,
					Talimat = "Yoğurt ve meyveler karıştırılır. Soğuk servis edilir.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Yoğurt", Miktar = 0.5 },
						new Malzeme { Adi = "Meyve", Miktar = 0.2 }
					}
				},
				new Tarif
				{
					Adi = "Karamelli Puding",
					Kategori = "Tatlı",
					Sure = 50,
					Talimat = "Pudingi karıştırarak pişirin, üzerine karamel sos dökerek servis edin.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Puding", Miktar = 0.3 },
						new Malzeme { Adi = "Karamel", Miktar = 0.1 }
					}
				},
				new Tarif
				{
					Adi = "Sakızlı Muhallebi",
					Kategori = "Tatlı",
					Sure = 40,
					Talimat = "Muhallebi hazırlanır ve üzerine sakız serpilerek soğutulur.",
					Malzemeler = new List<Malzeme>
					{
						new Malzeme { Adi = "Süt", Miktar = 0.5 },
						new Malzeme { Adi = "Şeker", Miktar = 0.2 },
						new Malzeme { Adi = "Sakız", Miktar = 0.02 }
					}
				}
			};



					foreach (var tarif in tarifler)
					{
						string insertTarif = @"
                    IF NOT EXISTS (SELECT 1 FROM Tarifler WHERE TarifAdi = @TarifAdi)
                    BEGIN
                        INSERT INTO Tarifler (TarifAdi, Kategori, HazirlamaSuresi, Talimatlar)
                        VALUES (@TarifAdi, @Kategori, @HazirlamaSuresi, @Talimatlar);
                        SELECT SCOPE_IDENTITY();
                    END";

						using (SqlCommand command = new SqlCommand(insertTarif, connection))
						{
							command.Parameters.AddWithValue("@TarifAdi", tarif.Adi);
							command.Parameters.AddWithValue("@Kategori", tarif.Kategori);
							command.Parameters.AddWithValue("@HazirlamaSuresi", tarif.Sure);
							command.Parameters.AddWithValue("@Talimatlar", tarif.Talimat);

							object result = command.ExecuteScalar();
							tarif.Id = result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
						}
						if (tarif.Id > 0)
						{
							foreach (var malzeme in tarif.Malzemeler)
							{
								string selectMalzemeID = @"
                            SELECT MalzemeID FROM Malzemeler 
                            WHERE MalzemeAdi = @MalzemeAdi";

								using (SqlCommand selectCommand = new SqlCommand(selectMalzemeID, connection))
								{
									selectCommand.Parameters.AddWithValue("@MalzemeAdi", malzeme.Adi);

									using (SqlDataReader reader = selectCommand.ExecuteReader())
									{
										if (reader.Read())
										{
											int malzemeId = reader.GetInt32(0);
											InsertTarifMalzeme(malzemeId, tarif.Id, malzeme.Miktar);
										}
										reader.Close();
									}
								}
							}
						}
						
					}
						void InsertTarifMalzeme(int malzemeId, int tarifId, double malzemeMiktar)
					   {
						string insertTarifMalzeme = @"
        				INSERT INTO TarifMalzeme (TarifID, MalzemeID, MalzemeMiktar)
        				VALUES (@TarifID, @MalzemeID, @MalzemeMiktar)";

						using (SqlCommand insertCommand = new SqlCommand(insertTarifMalzeme, connection))
						{
							insertCommand.Parameters.AddWithValue("@TarifID", tarifId);
							insertCommand.Parameters.AddWithValue("@MalzemeID", malzemeId);
							insertCommand.Parameters.AddWithValue("@MalzemeMiktar", malzemeMiktar);

							insertCommand.ExecuteNonQuery(); 
						}
					   }
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Hata: " + ex.Message);
			}
		}


	}
}
