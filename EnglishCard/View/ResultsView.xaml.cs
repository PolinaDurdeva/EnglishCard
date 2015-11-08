using System;
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
    public partial class ResultsView : PhoneApplicationPage
    {
        public ResultsView()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.DataContext = App.ViewModel;

            int countAllWords = App.ViewModel.totalCountWord();
            int countKnownWords = App.ViewModel.KnownWords.Count;
            //for progress page
            pbProgress.Value = countKnownWords / countAllWords * 100;
            tbCountWords.Text = countAllWords.ToString();
            tbCountKnownWords.Text = countKnownWords.ToString();
            tdCountTests.Text = App.ViewModel.AllWords.Sum(wrd => wrd.EffortsNumber).ToString();
            tbCountSuccessTests.Text = App.ViewModel.AllWords.Sum(wrd => wrd.SuccessfulEffortsNumber).ToString();
        }
    }
}