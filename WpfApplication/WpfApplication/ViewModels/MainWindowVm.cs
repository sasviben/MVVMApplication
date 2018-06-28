namespace WpfApplication.ViewModels
{
    using System;

    #region Using

    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Helper;
    using Models;

    #endregion

    public class MainWindowVm : ViewModelBase
    {
        private string _formVrstaObroka = string.Empty;
        private string _formNazivProizvoda = string.Empty;
        private string _formTezina = string.Empty;
        private string _formKalorije = string.Empty;
        private MyCommand _mojaKomanda;
        private MyCommand _mojaKomanda2;

        public ObservableCollection<PrehrambeniProizvod> PrehrambeniProizvodi { get; set; }

        public MainWindowVm(ObservableCollection<PrehrambeniProizvod> prehrambeniProizvodi)
        {
            PrehrambeniProizvodi = prehrambeniProizvodi;
        }

        public string FormNazivProizvoda
        {
            get { return _formNazivProizvoda; }
            set
            {
                _formNazivProizvoda = value;
                _mojaKomanda?.RaiseCanExecuteChanged();
                _mojaKomanda2?.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(FormNazivProizvoda));
            }
        }

        public string FormVrstaObroka
        {
            get { return _formVrstaObroka; }
            set
            {
                _formVrstaObroka = value;
                _mojaKomanda?.RaiseCanExecuteChanged();
                _mojaKomanda2?.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(FormVrstaObroka));
            }
        }

        public string FormTezina
        {
            get { return _formTezina; }
            set
            {
                _formTezina = value;
                _mojaKomanda?.RaiseCanExecuteChanged();
                _mojaKomanda2?.RaiseCanExecuteChanged();
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
                _mojaKomanda2?.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(FormKalorije));
            }
        }

        /*public string TxtNaziv
        {
            get { return _txtNaziv; }
            set
            {
                _txtNaziv = value;
                _mojaKomanda2?.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(TxtNaziv));
            }
        }*/

        //public List<PrehrambeniProizvod> PrehrambeniProizvods
        //{
        //    get
        //    {
        //        return new List<PrehrambeniProizvod>
        //               {
        //                   new PrehrambeniProizvod { Naziv = FormNazivProizvoda },
        //                   new PrehrambeniProizvod { Kalorije = FormKalorije },
        //                   new PrehrambeniProizvod { Tezina = FormTezina }
        //               };
        //    }
        //    set
        //    {
        //        //NazivProizvoda = value;
        //        _mojaKomanda2?.RaiseCanExecuteChanged();
        //        OnPropertyChanged(nameof(PrehrambeniProizvods));
        //    }
        //}


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
                                         Vrsta = FormVrstaObroka,
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
            if (FormNazivProizvoda.Length > 0 && FormKalorije.Length > 0 && FormTezina.Length > 0)
            {
                //TODO: ako je unesen tekst za kalorije i tezinu button treba biti onemogućen
                float xResult;
                if (float.TryParse(FormKalorije, out xResult) && float.TryParse(FormTezina, out xResult))
                    return true;
            }
            return false;
        }


        //ovo je samo za button koji prikazuje podatke iz kolekcije na View
        public ICommand MojaKomanda2
        {
            get { return _mojaKomanda2 != null ? _mojaKomanda2 : (_mojaKomanda2 = new MyCommand(() => PrikaziText(), CanPrikazatiText)); }
        }

        public void PrikaziText()
        {
            //dohvati sve iz nazive Prehrambenih proizvoda iz liste

            //var nazivProizvoda = PrehrambeniProizvodi.Select(proizvod => proizvod.Naziv).ToString();
        }

        public bool CanPrikazatiText(object obj)
        {
            //TODO
            return true;
        }

        #endregion
    }
}