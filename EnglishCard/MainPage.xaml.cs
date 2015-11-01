using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using EnglishCard.Resources;
using EnglishCard.ViewModels;
using EnglishCard.Model;

namespace EnglishCard
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the LongListSelector control to the sample data
            DataContext = App.ViewModel;

        }

        private void bTraining_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/Teaching.xaml", UriKind.Relative));
        }

        private void bDictionary_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/Dictionary.xaml", UriKind.Relative));
        }

        private void bSettings_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}