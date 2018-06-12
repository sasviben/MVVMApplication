using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using WpfApplication.Models;
using WpfApplication.ViewModels;
using WpfApplication.Views;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            var vm = new MainWindowVm(new List<PrehrambeniProizvod>());
            var view = new MainWindow(vm);

            view.Show();
        }
    }
    
}
