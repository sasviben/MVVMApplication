using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApplication.Helper;
using WpfApplication.Models;

namespace WpfApplication.ViewModels
{
    public class MainWindowVm : ViewModelBase
    {
        private string _txtNaziv;
        private int _txtTezina;
        private float _txtKalorije;
        private MyCommand _mojaKomanda;
        private MyCommand _mojaKomanda2;
        public ObservableCollection<PrehrambeniProizvod> PrehrambeniProizvodi { get; set; }

        public MainWindowVm(List<PrehrambeniProizvod> prehrambeniProizvods)
        {
            PrehrambeniProizvods = prehrambeniProizvods;
        }
        
        public string TxtNaziv
        {
            get { return _txtNaziv; }
            set
            {
                _txtNaziv = value;
                _mojaKomanda?.RaiseCanExecuteChanged();
                _mojaKomanda2?.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(TxtNaziv));
            }
        }

        public int TxtTezina
        {
            get { return _txtTezina; }
            set
            {
                _txtTezina = value;
                _mojaKomanda?.RaiseCanExecuteChanged();
                _mojaKomanda2?.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(TxtTezina));
            }
        }

        public float TxtKalorije
        {
            get { return _txtKalorije; }
            set
            {
                _txtKalorije = value;
                _mojaKomanda?.RaiseCanExecuteChanged();
                _mojaKomanda2?.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(TxtKalorije));
            }
        }

        #region Commands

        //ovo je samo za button
        public ICommand MojaKomanda
        {
            get
            {
                return _mojaKomanda != null ? _mojaKomanda : (_mojaKomanda = new MyCommand(()=>SaveDateFromForm(), CanShowWindow));
            }
        }
        
        public void SaveDateFromForm()
        {
            var prehrambeniProizvod = new PrehrambeniProizvod
            {
                Naziv = TxtNaziv,
                Kalorije = TxtKalorije,
                Težina = TxtTezina
            };
            PrehrambeniProizvods.Add(prehrambeniProizvod);
            PrehrambeniProizvodi = new ObservableCollection<PrehrambeniProizvod>(PrehrambeniProizvods);

        }

        public bool CanShowWindow(object obj)
        {
           // return TxtNaziv.Length > 0;
           //TODO
            return true;
        }


        //ovo je samo za button
        public ICommand MojaKomanda2
        {
            get
            {
                return _mojaKomanda2 != null ? _mojaKomanda2 : (_mojaKomanda2 = new MyCommand(() => ResetirajText(), CanResetirajText));
            }
        }

        public List<PrehrambeniProizvod> PrehrambeniProizvods { get; }

        public void ResetirajText()
        {
            TxtNaziv = "";
        }

        public bool CanResetirajText(object obj)
        {
            return TxtNaziv.Length > 0;
        }

        #endregion

    }

}
