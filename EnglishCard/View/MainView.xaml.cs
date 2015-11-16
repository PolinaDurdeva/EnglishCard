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
using EnglishCard.ViewModel;
using EnglishCard.Model;

namespace EnglishCard
{
    public partial class MainView : PhoneApplicationPage
    {
        // Constructor
        public MainView()
        {
            InitializeComponent();

            // Set the data context of the LongListSelector control to the sample data
            DataContext = App.ViewModel;

        }

        private void bTraining_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/TrainingView.xaml", UriKind.Relative));
        }

        private void bDictionary_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/VocabularyView.xaml", UriKind.Relative));
        }

        private void bResults_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/ResultsView.xaml", UriKind.Relative));
        }
    }
}