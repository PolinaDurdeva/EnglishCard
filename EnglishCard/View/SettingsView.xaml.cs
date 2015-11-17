using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using EnglishCard.ViewModelHelper;

namespace EnglishCard.View
{
    public partial class Settings : PhoneApplicationPage
    {
        public Settings()
        {
            InitializeComponent();
            foreach (Language lang in LanguageLocator.GetLanguages())
            {
                var tb = new TextBlock();
                tb.FontSize = 35;
                tb.Text = lang.Name.ToUpper();
                Langs.Items.Add(tb);
            }
            Langs.SelectedItem = Langs.Items.Where(item => (item as TextBlock).Text ==  App.Lang.Name.ToUpper()).First();
        }

        private void Langs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lbi = (sender as ListBox).SelectedItem as TextBlock;
            if (lbi != null)
            {
                App.Lang = LanguageLocator.GetLanguage(lbi.Text.ToLower());
            } else
            {
                System.Diagnostics.Debug.WriteLine("lbi is null");
            }
        }
    }
}