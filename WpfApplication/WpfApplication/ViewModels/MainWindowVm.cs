namespace WpfApplication.ViewModels
{
    #region Using

    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Helper;
    using Models;

    #endregion


    public class MainWindowVm : ViewModelBase
    {
        private string _comboBoxVrstaObroka;
        private string _formNazivProizvoda = string.Empty;
        private string _formTezina = string.Empty;
        private string _formKalorije = string.Empty;
        private MyCommand _mojaKomanda;
        private readonly BazaEntities _bazaEntities;

        public MainWindowVm()
        {
            _bazaEntities = new BazaEntities();

            PrehrambeniProizvodi = new ObservableCollection<PrehrambeniProizvod>();

            VrsteObroka = new ObservableCollection<string>(Enum.GetNames(typeof(VrstaObrokaEnum)));

            LoadDataFromDatabase();//ucitaj podatke iz baze u kolekciju PrehrambeniProizvodi
        }

        public ObservableCollection<PrehrambeniProizvod> PrehrambeniProizvodi { get; set; }
        public ObservableCollection<string> VrsteObroka { get; set; }


        public string FormNazivProizvoda
        {
            get { return _formNazivProizvoda; }
            set
            {
                _formNazivProizvoda = value;
                _mojaKomanda?.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(FormNazivProizvoda));
            }
        }

        public string ComboBoxVrstaObroka
        {
            get { return _comboBoxVrstaObroka; }
            set
            {
                _comboBoxVrstaObroka = value;
                _mojaKomanda?.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(ComboBoxVrstaObroka));
            }
        }

        public string FormTezina
        {
            get { return _formTezina; }
            set
            {
                _formTezina = value;
                _mojaKomanda?.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(FormTezina));
            }
        }

        public string FormKalorije
        {
            get { return _formKalorije; }
            set
            {
                _formKalorije = value;
                _mojaKomanda?.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(FormKalorije));
            }
        }

        #region Commands

        //ovo je samo za button koji sprema podatke iz forme u kolekciju
        public ICommand MojaKomanda
        {
            get { return _mojaKomanda != null ? _mojaKomanda : (_mojaKomanda = new MyCommand(() => SaveDateFromForm(), CanSaveDateFromForm)); }
        }
   
        private void SaveDateFromForm()
        {
            PrehrambeniProizvodi.Add(new PrehrambeniProizvod
                                     {
                                         Vrsta = ComboBoxVrstaObroka,
                                         Naziv = FormNazivProizvoda,
                                         Kalorije = float.Parse(FormKalorije),
                                         Tezina = float.Parse(FormTezina),
                                         SumaKalorija = IzracunajSumu(float.Parse(FormTezina), float.Parse(FormKalorije))
                                     });
        }

        private float IzracunajSumu(float tezina, float kalorije)
        {
            return (tezina / 100) * kalorije;
        }

        private bool CanSaveDateFromForm(object obj)
        {
            //ako su polja prazna button treba biti onemogućen
            if (FormNazivProizvoda.Length > 0 && FormKalorije.Length > 0 && FormTezina.Length > 0)
            {
                //ako nije unesen broj za kalorije i tezinu button treba biti onemogućen
                if (float.TryParse(FormKalorije, out _) && float.TryParse(FormTezina, out _))
                    return true;
            }

            return false;
        }
        private void LoadDataFromDatabase()
        {
            var hranaDb = _bazaEntities.Hrana.ToList();
            foreach (var hrana in hranaDb)
            {
                PrehrambeniProizvodi.Add(new PrehrambeniProizvod(hrana));
            }
        }

        public void SaveToDatabase()
        {
            //na Close window spremi iz liste PrehrambeniProizvodi u bazu
            foreach (var hrana in _bazaEntities.Hrana.ToList())
            {
                //ako postoji u listi PrehrambeniProizvodi i u bazi nemoj brisati iz baze
                if (PrehrambeniProizvodi.Any(x => x.Id == hrana.id))
                    continue;
                //ako ne postoji u listi PrehrambeniProizvodi izbriši iz baze
                _bazaEntities.Hrana.Remove(hrana);
            }

            foreach (var prehrambeniProizvod in PrehrambeniProizvodi)
            {
                //ako postoji u bazi i u listi PrehrambeniProizvodi nemoj dodati u bazu
                if (_bazaEntities.Hrana.Any(x => x.id == prehrambeniProizvod.Id))
                    continue;

                var hrana = new Hrana
                            {
                                vrsta_obroka = prehrambeniProizvod.Vrsta,
                                naziv_proizvoda = prehrambeniProizvod.Naziv,
                                kalorije = prehrambeniProizvod.Kalorije,
                                tezina = prehrambeniProizvod.Tezina,
                                suma_kalorija = prehrambeniProizvod.SumaKalorija
                            };

                _bazaEntities.Hrana.Add(hrana);
            }

            _bazaEntities.SaveChanges();
        }

        #endregion
    }
}