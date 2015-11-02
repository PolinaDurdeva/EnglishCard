using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using EnglishCard.ViewModels;
using EnglishCard.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace EnglishCard.View
{
    public partial class DictionaryPage : PhoneApplicationPage
    {
        public DictionaryPage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }

        private void newWordButtom_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/AddingNewWords.xaml", UriKind.Relative));
        }

        private void deleteWordsButton_Click(object sender, RoutedEventArgs e)
        {
            // Cast the parameter as a button.
            var button = sender as Button;

            if (button != null)
            {
                // Get a handle for the to-do item bound to the button.
                Word wordForDelete = button.DataContext as Word;

                App.ViewModel.DeleteWord(wordForDelete);
            }

            // Put the focus back to the main page.
            this.Focus();
        }
    }
}