using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TarifUygulamasi;

namespace TarifRehberi
{
	public partial class Form3 : Form
	{
		public Form3()
		{
			InitializeComponent();
		}

		private void btnEkle_Click(object sender, EventArgs e)
		{
			TarifEkleForm tarifEkleForm = new TarifEkleForm();
			tarifEkleForm.ShowDialog();
		}

		private void btnGuncelle_Click(object sender, EventArgs e)
		{
			TarifGuncelleForm tarifGuncelleForm = new TarifGuncelleForm();
			tarifGuncelleForm.ShowDialog();	
		}
	}
}
