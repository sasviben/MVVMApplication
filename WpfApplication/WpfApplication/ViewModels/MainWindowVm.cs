﻿namespace WpfApplication.ViewModels
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
        private MyCommand _mojaKomanda2;
        private BazaEntities _bazaEntities;

        public MainWindowVm()
        {
            _bazaEntities = new BazaEntities();

            PrehrambeniProizvodi = new ObservableCollection<PrehrambeniProizvod>();
            
            VrsteObroka = new ObservableCollection<string>(Enum.GetNames(typeof(VrstaObrokaEnum)));

            LoadDataFromDatabase();
        }

        private void LoadDataFromDatabase()
        {
            var hranaLocal = _bazaEntities.Hrana.ToList();
            foreach (var hrana in hranaLocal)
            {
                PrehrambeniProizvodi.Add(new PrehrambeniProizvod(hrana));
            }
        }


        public ObservableCollection<PrehrambeniProizvod> PrehrambeniProizvodi { get; set; }

        //  public VrstaObrokaEnum VrsteObroka { get; set; }
        public ObservableCollection<string> VrsteObroka { get; set; }


        public string FormNazivProizvoda
        {
            get { return _formNazivProizvoda; }
            set
            {
                _formNazivProizvoda = value;
                _mojaKomanda?.RaiseCanExecuteChanged();
                //_mojaKomanda2?.RaiseCanExecuteChanged();
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
               // _mojaKomanda2?.RaiseCanExecuteChanged();
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
               // _mojaKomanda2?.RaiseCanExecuteChanged();
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
               // _mojaKomanda2?.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(FormKalorije));
            }
        }

        #region Commands

        //ovo je samo za button koji sprema podatke iz forme u kolekciju
        public ICommand MojaKomanda
        {
            get { return _mojaKomanda != null ? _mojaKomanda : (_mojaKomanda = new MyCommand(() => SaveDateFromForm(), CanShowWindow)); }
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

        private bool CanShowWindow(object obj)
        {
            //ako su polja prazna button treba biti onemogućen
            if (FormNazivProizvoda.Length > 0 && FormKalorije.Length > 0 && FormTezina.Length > 0)
            {
                //ako nije unesen broj za kalorije i tezinu button treba biti onemogućen
                float xResult;
                if (float.TryParse(FormKalorije, out xResult) && float.TryParse(FormTezina, out xResult))
                    return true;
            }

            return false;
        }




        public void SaveToDatabase()
        {
            //spremi iz liste PrehrambeniProizvodi u bazu


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
                if (_bazaEntities.Hrana.Any(x=>x.id == prehrambeniProizvod.Id))
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
            

            //var nazivProizvoda = PrehrambeniProizvodi.Select(proizvod => proizvod.Naziv).ToString();
        }

        #endregion
    }
}