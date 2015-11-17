using System;
using EnglishCard.ViewModel;
using EnglishCard.Model;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace EnglishCard.View
{
    public partial class AddingNewWords : PhoneApplicationPage
    {
        private AddWordsViewModel viewModel;
        public AddWordsViewModel ViewModel
        {
            get { return viewModel; }
        }
        public AddingNewWords()
        {
            InitializeComponent();
            viewModel = new AddWordsViewModel();
            this.DataContext = ViewModel;
        }

        private void appBarOkButton_Click(object sender, EventArgs e)
        {
            // Confirm there is some text in the text box.
            if (tbNewOriginWord.Text.Length > 0 && tbNewTranslateWord.Text.Length > 0)
            {
                // Create a new word.
                Word newWord = new Word
                {
                    OriginWord = tbNewOriginWord.Text,
                    TranslateWord = tbNewTranslateWord.Text,
                };

                // Add the item to the ViewModel.
                ViewModel.AddWord(newWord);
                

                // Return to the main page.
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
            }
            else
            {
                string errorMessages = "jjjj";
                MessageBox.Show(errorMessages, "Warning: Invalid Values", MessageBoxButton.OK);
        }
    }

        private void appBarCancelButton_Click(object sender, EventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void originWord_Key_Down(object sender,KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                tbNewTranslateWord.Focus();
        }

        private void translateWord_Key_Down(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
                this.Focus();
        }
    }
}