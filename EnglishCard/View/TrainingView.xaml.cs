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


namespace EnglishCard.View
{
    public partial class TrainingView : PhoneApplicationPage
    {
        public TrainingView()
        {
            InitializeComponent();
            // TODO: Define params as consts
            this.viewModel = new TranslationForWordTrainingViewModel(10, 3);
            // Causes double initialization in model
            this.trainingStarted = false;
        }

        private void bTranslateButton_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = sender as Button;
            bool matched = viewModel.SuggestAnswer((clickedButton.Content as TextBlock).Text);
            var btns = new Button[] { bTranslate1, bTranslate2, bTranslate3 };
            //System.Diagnostics.Debug.WriteLine();
            /*
            foreach(Button btn in btns)
            {
                //btn.IsEnabled = false;
                if ((btn.Content as TextBlock).Text == vm.WordObject.TranslateWord)
                {
                    btn.Background = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    btn.Background = new SolidColorBrush(Colors.Red);
                }
            }*/
        }

        private void bNextWord_Click(object sender, RoutedEventArgs e)
        {
            if (trainingStarted)
            {
                var btns = new Button[] { bTranslate1, bTranslate2, bTranslate3 };
                foreach (Button btn in btns)
                {
                    btn.IsEnabled = true;
                    btn.Background = new SolidColorBrush(Colors.Transparent);
                }
                viewModel.UpdateTask();
            }
            else
                initializeTraining();
        }

        private void initializeTraining()
        {
            trainingStarted = true;
            this.DataContext = viewModel.GetTask();
            this.bNextWord.Content = "Next";
        }

        private bool trainingStarted;

        private ITrainingViewModel viewModel;
    }
}