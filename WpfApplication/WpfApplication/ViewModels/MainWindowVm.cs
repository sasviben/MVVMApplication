using System.Collections.Generic;
using System.Windows.Documents;

namespace WpfApplication.ViewModels
{
    #region Using

    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Windows.Input;
    using System.Xml.Linq;
    using Helper;
    using Models;

    #endregion


    public class MainWindowVm : ViewModelBase
    {
        private string _comboBoxVrstaObroka;
        private string _formNazivProizvoda = string.Empty;
        private string _formTezina = string.Empty;
        private string _formKalorije = string.Empty;
        private float _sumaKalorijaUDanu;
        private MyCommand _mojaKomanda;
        private readonly BazaEntities _bazaEntities;
        private MyCommand _mojaKomanda2;
        private readonly string _pathOfXml = @"C:\Users\ssviben\Source\Repos\MVVMApplication\WpfApplication\WpfApplication\Models\nutrition.xml";
        private string _comboBoxGrupaHrane = string.Empty;
        private string _comboBoxHrana = string.Empty;


        public MainWindowVm()
        {
            _bazaEntities = new BazaEntities();

            PrehrambeniProizvodi = new ObservableCollection<PrehrambeniProizvod>();

            //PrehrambeniProizvodiINutritivneVrijednosti = new ObservableCollection<PrehrambeniProizvod>();

            VrsteObroka = new ObservableCollection<string>(Enum.GetNames(typeof(VrstaObrokaEnum)));

            LoadDataFromDatabase(); //ucitaj podatke iz baze u kolekciju PrehrambeniProizvodi

            PrehrambeniProizvodiXmlParser(); //ucitaj podatke iz xml-a

            GrupeHrane = new ObservableCollection<string>();

            Hrana = new ObservableCollection<string>();

            PopulateGrupeHrane();

            //GetFood();
        }

        public ObservableCollection<PrehrambeniProizvod> PrehrambeniProizvodi { get; set; }
        //public ObservableCollection<PrehrambeniProizvod> PrehrambeniProizvodiINutritivneVrijednosti { get; set; }
        public ObservableCollection<string> VrsteObroka { get; set; }
        public ObservableCollection<string> GrupeHrane { get; set; }
        public ObservableCollection<string> Hrana { get; set; }
        public NutritionModel NutritionModel { get; set; }


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

        public string ComboBoxGrupaHrane
        {
            get { return _comboBoxGrupaHrane; }
            set
            {
                _comboBoxGrupaHrane = value;

                if(!string.IsNullOrEmpty(value))
                    GetFood();
                _mojaKomanda?.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(ComboBoxGrupaHrane));
            }
        }

        public string ComboBoxHrana
        {
            get { return _comboBoxHrana; }
            set
            {
                _comboBoxHrana = value;
                _mojaKomanda?.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(ComboBoxHrana));
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

        public float SumaKalorijaUDanu
        {
            get { return _sumaKalorijaUDanu; }
            set
            {
                _sumaKalorijaUDanu = value;
                _mojaKomanda2?.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(SumaKalorijaUDanu));
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

            var foodGroup = NutritionModel.Foodgroup.First(foodgroup => foodgroup.Name.Equals(ComboBoxGrupaHrane));
            var hrana = foodGroup.Food.Single(x => x.Name == ComboBoxHrana);

            PrehrambeniProizvodi.Add(new PrehrambeniProizvod
                                     {
                                         Vrsta = ComboBoxVrstaObroka,
                                         Naziv = ComboBoxHrana,
                                         Kalorije = hrana.Kalorije,
                                         Masti = hrana.Masti,
                                         Bjelancevine = hrana.Bjelancevine,
                                         Ugljikohidrati = hrana.Ugljikohidrati,
                                         Tezina = float.Parse(FormTezina),
                                         SumaKalorija = CalculateSum(float.Parse(FormTezina), hrana.Kalorije), 
                                         
                                     });
        }

        private bool CanSaveDateFromForm(object obj)
        {
            //ako su polja prazna button treba biti onemogućen
            if (FormTezina.Length > 0 && float.TryParse(FormTezina, out _))
            {
                return true;
            }

            return false;
        }


        public ICommand MojaKomanda2
        {
            get { return _mojaKomanda2 != null ? _mojaKomanda2 : (_mojaKomanda2 = new MyCommand(() => CalculateDaySum(), CanCalculateSum)); }
        }

        private void CalculateDaySum()
        {
            //todo: doraditi računanje;
            float sum = 0;
            foreach (var prehrambeniProizvod in PrehrambeniProizvodi)
            {
                sum += prehrambeniProizvod.SumaKalorija;
            }

            SumaKalorijaUDanu = sum;
        }

        private bool CanCalculateSum(object obj)
        {
            return true;
        }

        #endregion

        private float CalculateSum(float tezina, float kalorije)
        {
            return (tezina / 100) * kalorije;
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
                                tezina = prehrambeniProizvod.Tezina,
                                kalorije = prehrambeniProizvod.Kalorije,
                                bjelancevine = prehrambeniProizvod.Bjelancevine,
                                ugljikohidrati = prehrambeniProizvod.Ugljikohidrati,
                                masti = prehrambeniProizvod.Masti,
                                suma_kalorija = prehrambeniProizvod.SumaKalorija
                            };

                _bazaEntities.Hrana.Add(hrana);
            }

            _bazaEntities.SaveChanges();
        }

        public void PrehrambeniProizvodiXmlParser()
        { 

            // XmlSerializer.SaveData<NutritionModel>(model, path);

            NutritionModel = XmlSerializer.LoadData<NutritionModel>(_pathOfXml);

        }

        private void PopulateGrupeHrane()
        {
            foreach (var nutritionFoodgroup in NutritionModel.Foodgroup)
            {
                GrupeHrane.Add(nutritionFoodgroup.Name);
            }
        }

        private void GetFood()
        {
            //pronađi foodGroup.Name sa traženim imenom
            //uzmi listu Food od toga foodGroup.Name - a
            var listFood = new List<NutritionFoodgroup>();
            var hrana = NutritionModel.Foodgroup.First(foodgroup => foodgroup.Name.Equals(ComboBoxGrupaHrane));

            Hrana.Clear();

            foreach (var item in hrana.Food)
            {
                Hrana.Add(item.Name);   
            }

            ComboBoxHrana = Hrana.FirstOrDefault();

            //kako sad ovo prebaciti u kolekciju Hrana na koju se bindam??

        }

        private void PopulateFood()
        {
            throw new NotImplementedException();
        }
    }
}