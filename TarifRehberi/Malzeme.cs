using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarifRehberi
{
	public class Malzeme
	{
		public int Id {  get; set; }

		public string Adi { get; set; }
		public double Miktar { get; set; }
	}

	public class Tarif
	{
		public int Id { get; set; }
		public string Adi { get; set; }
		public string Kategori { get; set; }
		public int Sure { get; set; }
		public string Talimat { get; set; }
		public List<Malzeme> Malzemeler { get; set; }
	}
	public class TarifMalzeme
	{
		public int Id { get; set; }

	}
}
