using System;
using System.ComponentModel;


namespace EnglishCard.Models
{
    public class Words : INotifyPropertyChanged
    {
        public string originWord { get; set; }
        public string translateWord { get; set; }
        private bool _flagKnowledge;
        public bool FlagKnowledge
        {
            get
            {
                return _flagKnowledge;
            }
            set
            {
                if (value != _flagKnowledge)
                {
                    _flagKnowledge = value;
                    NotifyPropertyChanged("FlagKnowledge");
                }
            }
        }
        private int _countTest;
        public int CountTest
        {
            get
            {
                return _countTest;
            }
            set
            {
                if (value != _countTest)
                {
                    _countTest = value;
                    NotifyPropertyChanged("CountTest");
                }

            }
        }
        private int _countSuccessTest;
        public int CountSuccessTest
        {
            get
            {
                return _countSuccessTest;
            }
            set
            {
                if (value != _countSuccessTest)
                {
                    _countSuccessTest = value;
                    NotifyPropertyChanged("CountSuccessTest");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }
        public Words GetCopy()
        {
            Words copy = (Words)this.MemberwiseClone();
            return copy;
        }
    }
}
