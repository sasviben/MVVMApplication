namespace WpfApplication.Models
{
    public class PrehrambeniProizvod
    {
        public PrehrambeniProizvod()
        {
        }

        public PrehrambeniProizvod(Hrana hrana) : this()
        {
            Id = hrana.id;
            Vrsta = hrana.vrsta_obroka;
            Naziv = hrana.naziv_proizvoda;
            Tezina = (float) hrana.tezina;
            Kalorije = (float) hrana.kalorije;
            SumaKalorija = (float) hrana.suma_kalorija;
        }

        public int Id { get; set; }
        public string Vrsta { get; set; }
        public string Naziv { get; set; }
        public float Tezina { get; set; }
        public float Kalorije { get; set; }
        public float SumaKalorija { get; set; }

    }
}