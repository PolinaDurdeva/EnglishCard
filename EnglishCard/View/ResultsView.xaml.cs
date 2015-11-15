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
            App.ViewModel.LoadCollectionsFromDatabase();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.DataContext = App.ViewModel;
            pbProgress.Value = App.ViewModel.CountKnownWords / App.ViewModel.CountAllWords * 100;           
        }
    }
}