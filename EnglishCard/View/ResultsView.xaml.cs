using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using EnglishCard.ViewModel;
using EnglishCard.Model;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace EnglishCard.View
{
    public partial class ResultsView : PhoneApplicationPage
    {
        private ResultsViewModel viewModel;
        public ResultsViewModel ViewModel
        {
            get { return viewModel; }
        }
        public ResultsView()
        {
            InitializeComponent();
            viewModel = new ResultsViewModel(DictionaryModel.DBConnectionString);
            ViewModel.LoadInfoFromDataBase();
            this.DataContext = ViewModel;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            pbProgress.Value = ViewModel.CountKnownWords /(float) ViewModel.CountAllWords * 100;           
        }
    }
}