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
    using System.ComponentModel;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindowVm _viewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _viewModel = new MainWindowVm();
            var view = new MainWindow(_viewModel);

            view.Closing += ViewOnClosing;//prije zatvaranja spremi u bazu
            view.Show();
        }

        private void ViewOnClosing(object sender, CancelEventArgs e)
        {
            _viewModel.SaveToDatabase();
        }
    }
    
}
