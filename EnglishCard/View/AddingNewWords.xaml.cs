using System;
using EnglishCard.ViewModels;
using EnglishCard.Model;
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
        public AddingNewWords()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
        }

        private void appBarOkButton_Click(object sender, EventArgs e)
        {
            // Confirm there is some text in the text box.
            if (newOriginWordTextBox.Text.Length > 0)
            {
                // Create a new word.
                Word newWord = new Word
                {
                    OriginWord = newOriginWordTextBox.Text,
                    TranslateWord = newTranslateWordTextBox.Text,                   
                };

                // Add the item to the ViewModel.
                App.ViewModel.AddWord(newWord);

                // Return to the main page.
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
            }
        }

        private void appBarCancelButton_Click(object sender, EventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}