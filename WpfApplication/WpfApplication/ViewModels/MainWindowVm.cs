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
        private string _text;
        private MyCommand _mojaKomanda;
        private MyCommand _mojaKomanda2;
        private ObservableCollection<PrehrambeniProizvod> _prehrambeniProizvodi;

        public MainWindowVm()
        {
            Text = "";
        }
        
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                _mojaKomanda?.RaiseCanExecuteChanged();
                _mojaKomanda2?.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(Text));
            }
        }

        #region Commands

        //ovo je samo za button
        public ICommand MojaKomanda
        {
            get
            {
                return _mojaKomanda != null ? _mojaKomanda : (_mojaKomanda = new MyCommand(()=>ShowWindow(), CanShowWindow));
            }
        }
        
        public void ShowWindow()
        {
           // MessageBox.Show("Bla", "ksjdsdjsd");
            var prehrambeniProizvod = new PrehrambeniProizvod();
            prehrambeniProizvod.Naziv = Text;
            prehrambeniProizvod.Kalorije = 10;
            prehrambeniProizvod.Težina = 2;
            _prehrambeniProizvodi.ToList().Add(prehrambeniProizvod);
        }

        public bool CanShowWindow(object obj)
        {
            return Text.Length > 0;
        }


        //ovo je samo za button
        public ICommand MojaKomanda2
        {
            get
            {
                return _mojaKomanda2 != null ? _mojaKomanda2 : (_mojaKomanda2 = new MyCommand(() => ResetirajText(), CanResetirajText));
            }
        }

        public void ResetirajText()
        {
            Text = "";
        }

        public bool CanResetirajText(object obj)
        {
            return Text.Length > 0;
        }

        #endregion

    }

}
