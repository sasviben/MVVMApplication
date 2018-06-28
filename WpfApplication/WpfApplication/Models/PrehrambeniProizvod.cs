using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication.Models
{
    public class PrehrambeniProizvod
    {
        public string Vrsta { get; set; }
        public string Naziv { get; set; }
        public float Tezina { get; set; }
        public float Kalorije { get; set; }
        public float SumaKalorija { get; set; }
    }
}
