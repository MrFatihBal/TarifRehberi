﻿using System;
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
	public partial class Form1 : Form
	{
		public string connectionString;
		
		
		Db db = new Db();
		public Form1()
		{
			InitializeComponent();
			connectionString = ConfigurationManager.ConnectionStrings["TarifRehberiContext"].ConnectionString;
			db.CreateDatabaseAndTables();
			db.InsertSampleData();
		}

		
		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void btnTarifleriListele_Click(object sender, EventArgs e)
		{
			Form2 tarifleriListeleForm = new Form2();
			tarifleriListeleForm.Show();
		}

		private void btnTarifYonetimi_Click(object sender, EventArgs e)
		{
			Form3 tarifYonetimiForm= new Form3();
			tarifYonetimiForm.Show();
		}

		private void btnMalzemeKayit_Click(object sender, EventArgs e)
		{
			Form4 malzemeKayitForm = new Form4();
			malzemeKayitForm.Show();

		}
	}
}
