using System;
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

            Dictionary dict = new Dictionary(Dictionary.DBConnectionString);
            this.tWord.Text = dict.Words.First<Word>().OriginWord;
        }
    }
}