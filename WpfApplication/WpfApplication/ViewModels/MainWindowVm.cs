namespace WpfApplication.ViewModels
{
    #region Using

    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using Helper;
    using Models;

    #endregion

    public class MainWindowVm : ViewModelBase
    {
        private string _formNaziv = string.Empty;
        private float _formTezina = 0;
        private float _formKalorije = 0;
        private MyCommand _mojaKomanda;
        private MyCommand _mojaKomanda2;

        public ObservableCollection<PrehrambeniProizvod> PrehrambeniProizvodi { get; set; }

        public MainWindowVm(ObservableCollection<PrehrambeniProizvod> prehrambeniProizvodi)
        {
            PrehrambeniProizvodi = prehrambeniProizvodi;
        }

        public string FormNaziv
        {
            get { return _formNaziv; }
            set
            {
                _formNaziv = value;
                _mojaKomanda?.RaiseCanExecuteChanged();
                _mojaKomanda2?.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(FormNaziv));
            }
        }

        public int FormTezina
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

        public float FormKalorije
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
        //                   new PrehrambeniProizvod { Naziv = FormNaziv },
        //                   new PrehrambeniProizvod { Kalorije = FormKalorije },
        //                   new PrehrambeniProizvod { Težina = FormTezina }
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

        public void SaveDateFromForm()
        {
            PrehrambeniProizvodi.Add(new PrehrambeniProizvod
                                     {
                                         Naziv = FormNaziv,
                                         Kalorije = FormKalorije,
                                         Težina = FormTezina
                                     });
        }

        public bool CanShowWindow(object obj)
        {

            if (FormNaziv.Length > 0 && FormKalorije > 0 && FormTezina > 0)
            {
                //TODO: ako je unesen tekst za kalorije i tezinu button treba biti onemogućen
                //if (FormKalorije is typeof(float))
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