﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using EnglishCard.Resources;
using EnglishCard.Model;

namespace EnglishCard.View
{
    public partial class Teaching : PhoneApplicationPage
    {
        public Teaching()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
            //Dictionary dict = new Dictionary(Dictionary.DBConnectionString);

            Random rnd = new Random();
            int nextWordNumber = rnd.Next(0, App.ViewModel.UnKnownWords.Count);
            
            tWord.DataContext = App.ViewModel.UnKnownWords[nextWordNumber];
            tWord.Text = App.ViewModel.UnKnownWords[nextWordNumber].OriginWord;

            bTranslate1.DataContext = App.ViewModel.UnKnownWords[nextWordNumber];
            bTranslate1.Content = App.ViewModel.UnKnownWords[nextWordNumber].TranslateWord;

            bTranslate2.DataContext = App.ViewModel.UnKnownWords[nextWordNumber].TranslateWord;
            bTranslate2.Content = App.ViewModel.UnKnownWords[nextWordNumber].TranslateWord;

            bTranslate3.DataContext = App.ViewModel.UnKnownWords[nextWordNumber].TranslateWord;
            bTranslate3.Content = App.ViewModel.UnKnownWords[nextWordNumber].TranslateWord;
        }

        private void bTranslate1Button_Clic(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button != null)
            {
                // Get a handle for the to-do item bound to the button.
                Word wordForCompare = button.DataContext as Word;
                Word wordForCompare2 = this.tWord.DataContext as Word;
                MessageBox.Show((wordForCompare.TranslateWord == wordForCompare2.TranslateWord).ToString());
            }

        }

        private void bTranslate2Button_Clic(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button != null)
            {
                // Get a handle for the to-do item bound to the button.
                Word wordForCompare = button.DataContext as Word;
                Word wordForCompare2 = this.tWord.DataContext as Word;
                MessageBox.Show((wordForCompare.TranslateWord == wordForCompare2.TranslateWord).ToString());
            }

        }

        private void bTranslate3Button_Clic(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button != null)
            {
                // Get a handle for the to-do item bound to the button.
                Word wordForCompare = button.DataContext as Word;
                Word wordForCompare2 = this.tWord.DataContext as Word;
                MessageBox.Show((wordForCompare.TranslateWord == wordForCompare2.TranslateWord).ToString());
            }

        }
    }
}