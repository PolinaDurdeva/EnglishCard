using System.Collections.ObjectModel;
using System.ComponentModel;
using EnglishCard.Models;
using EnglishCard.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishCard.ViewModels
{
    public class Dictionary : INotifyPropertyChanged
    {
        public Dictionary()
        {
            this.Dict = new ObservableCollection<Words>();
        }
        public ObservableCollection<Words> Dict { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
