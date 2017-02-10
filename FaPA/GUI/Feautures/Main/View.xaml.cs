using System;
using System.Windows;
using System.Windows.Controls.Ribbon;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Main
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : RibbonWindow
    {
        public View()
        {
            InitializeComponent();
        }

        private void RibbonApplicationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var ApplicationMenu = sender as RibbonApplicationMenuItem;
            MessageBox.Show(ApplicationMenu.Header + " is clicked");
        }

        private void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as RibbonButton;
            MessageBox.Show(button.Content + " is clicked");
        }

        private void ApriAnagrafiche(object sender, RoutedEventArgs e)
        {
            Presenters.Show("Anagrafica", new Action<GUI.Feautures.Anagrafica.Presenter>(p => p.CreateNewModel(0)));
        }

        //private void CercaAnagrafiche(object sender, RoutedEventArgs e)
        //{
        //    Presenters.Show("SearchAnagrafica");
        //}

        private void ApriFatture(object sender, RoutedEventArgs e)
        {
            Presenters.Show("Fattura", new Action<GUI.Feautures.Fattura.Presenter>(p => p.CreateNewModel(0)));
        }

        //private void CercaFatture(object sender, RoutedEventArgs e)
        //{
        //    Presenters.Show("SearchFattura");
        //}

        private void BackUpAndRestoreClick(object sender, RoutedEventArgs e)
        {

            Presenters.Show("BackUpRestore");
        }

        private void ChangePasswordClick(object sender, RoutedEventArgs e)
        {
            Presenters.Show("LogInManager");
        }

        private void UsersClick(object sender, RoutedEventArgs e)
        {
            Presenters.Show("User", new Action<GUI.Feautures.User.Presenter>(p => p.CreateNewModel(0)));
        }
    }
}
