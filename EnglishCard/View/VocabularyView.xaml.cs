using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using EnglishCard.ViewModel;
using EnglishCard.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace EnglishCard.View
{
    public partial class VocabularyView : PhoneApplicationPage
    {
        public VocabularyView()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
            tbCountWords.Text = App.ViewModel.AllWords.Count.ToString();
            tbCountKnownWords.Text = App.ViewModel.KnownWords.Count.ToString();
            tdCountTests.Text = App.ViewModel.AllWords.Sum(wrd => wrd.EffortsNumber).ToString();
            tbCountSuccessTests.Text = App.ViewModel.AllWords.Sum(wrd => wrd.SuccessfulEffortsNumber).ToString();
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