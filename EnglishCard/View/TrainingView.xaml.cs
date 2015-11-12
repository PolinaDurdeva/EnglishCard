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
            viewModel = vmlocator.GetViewModel(localPivotName);
            // startButton = sender as Button;
            viewModel.Reset();
            this.DataContext = viewModel.GetTask();
        }

        private void bOkButton_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = sender as Button;
            bool matched = viewModel.SuggestAnswer(tbWriting.Text);
            bgRestore.Add(clickedButton, clickedButton.Background);
            if (matched)
            {
                clickedButton.Background = new SolidColorBrush(Colors.Green);
            } else
            {
                clickedButton.Background = new SolidColorBrush(Colors.Red);
            }
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
            // If we skip word, it is assumed failed
            viewModel.SuggestAnswer("");
            if(!viewModel.UpdateTask())
            {
                stopTraining();
            }
            foreach(Button b in bgRestore.Keys)
            {
                b.Background = bgRestore[b];
            }
            bgRestore.Clear();
            tbWriting.Text = "";
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

        string localPivotName;

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pivot = sender as Pivot;
            var pivotItem = pivot.SelectedItem as PivotItem;
            localPivotName = pivotItem.Name;
            System.Diagnostics.Debug.WriteLine(localPivotName);
            stopTraining();
        }
    }
}