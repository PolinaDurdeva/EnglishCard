using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using EnglishCard.Resources;
using EnglishCard.ViewModel;
using EnglishCard.Model;
using EnglishCard.ViewModelHelper;


namespace EnglishCard.View
{
    public partial class TrainingView : PhoneApplicationPage
    {
        public TrainingView()
        {
            InitializeComponent();
            vmlocator = new TrainingViewModelLocator();
            stopTraining();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            startButton = sender as Button;
            this.DataContext = viewModel.GetTask();
        }

        private void bTranslateButton_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = sender as Button;
            bgRestore.Add(clickedButton, clickedButton.Background);
            bool matched = viewModel.SuggestAnswer((clickedButton.Content as TextBlock).Text);
            if(matched)
            {
                clickedButton.Background = new SolidColorBrush(Colors.Green);
            } else
            {
                clickedButton.Background = new SolidColorBrush(Colors.Red);
            }
        }

        private void bNextWord_Click(object sender, RoutedEventArgs e)
        {
            if(!viewModel.UpdateTask())
            {
                stopTraining();
            }
            foreach(Button b in bgRestore.Keys)
            {
                b.Background = bgRestore[b];
            }
            bgRestore.Clear();
        }

        private void stopTraining()
        {
            this.viewModel = vmlocator.GetViewModel("StubTrainingViewModel");
            this.DataContext = viewModel.GetTask();
        }

        private TrainingViewModelLocator vmlocator;

        private ITrainingViewModel viewModel;

        private Button startButton;

        private Dictionary<Button, Brush> bgRestore = new Dictionary<Button, Brush>();

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pivot = sender as Pivot;
            var pivotItem = pivot.SelectedItem as PivotItem;
            System.Diagnostics.Debug.WriteLine(pivotItem.Name);
            viewModel = vmlocator.GetViewModel(pivotItem.Name);
        }
    }
}