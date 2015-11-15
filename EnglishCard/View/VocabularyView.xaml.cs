using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Data;
using System.Net;
using EnglishCard.ViewModel;
using EnglishCard.Model;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace EnglishCard.View
{
    public partial class VocabularyView : PhoneApplicationPage
    {
        private Random rnd = new Random();
        
        public VocabularyView()
        {
            InitializeComponent();
            //App.ViewModel.LoadCollectionsFromDatabase();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.DataContext = App.ViewModel;
            App.ViewModel.LoadCollectionsFromDatabase();
            App.ViewModel.generateNextWord();
            App.ViewModel.searchWords("");
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
                
                button.Visibility = Visibility.Collapsed;
            }
            
            
            // Put the focus back to the main page.
            this.Focus();
        }

        private void nextWord_Button_Click(object sender, RoutedEventArgs e)
        {
           
            App.ViewModel.generateNextWord();
        }

        private void Searchbox_TextChanged(object sender, TextChangedEventArgs e)
        {

            App.ViewModel.searchWords(Searchbox.Text.ToLower());
        }

        private void searchbox_Key_Down(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                this.Focus();
        }

        private void resultsBotton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/ResultsView.xaml", UriKind.Relative));
        }
    }
}