using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace EnglishCard.Model
{
    [Table]
    public class Word : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public Word()
        {
            System.Diagnostics.Debug.WriteLine("Word constructor called");
        }

        public Word(Word previousWord)
        {
            System.Diagnostics.Debug.WriteLine("Word copy constructor called");
        }

        private int wordId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int WordId
        {
            get { return wordId; }
            set
            {
                if (wordId != value)
                {
                    NotifyPropertyChanging("WordId");
                    wordId = value;
                    NotifyPropertyChanged("WordId");
                }
            }
        }

        private string _originWord;
        [Column]
        public string OriginWord
        {
            get { return _originWord; }
            set
            {
                if (_originWord != value)
                {
                    NotifyPropertyChanging("OriginWord");
                    _originWord = value;
                    NotifyPropertyChanged("OriginWord");
                }
            }
        }

        public string _translateWord;
        [Column]
        public string TranslateWord
        {
            get { return _translateWord; }
            set
            {
                if (_translateWord != value)
                {
                    NotifyPropertyChanging("TranslateWord");
                    _translateWord = value;
                    NotifyPropertyChanged("TranslateWord");
                }
            }
        }

        private int _effortsNumber = 0;
        [Column]
        public int EffortsNumber
        {
            get
            {
                return _effortsNumber;
            }
            set
            {
                if (value != _effortsNumber)
                {
                    NotifyPropertyChanging("EffortsNumber");
                    _effortsNumber = value;
                    NotifyPropertyChanged("EffortsNumber");
                }

            }
        }

        private int _successfulEffortsNumber = 0;
        [Column]
        public int SuccessfulEffortsNumber
        {
            get
            {
                return _successfulEffortsNumber;
            }
            set
            {
                if (value != _successfulEffortsNumber)
                {
                    NotifyPropertyChanging("SuccessfulEffortsNumber");
                    _successfulEffortsNumber = value;
                    NotifyPropertyChanged("SuccessfulEffortsNumber");
                }
            }
        }
        
        // TODO: move away from model
        public bool isKnown()
        {
                return _successfulEffortsNumber > 0 && _successfulEffortsNumber / _effortsNumber > 0.5 && _effortsNumber > 3; 
        }


        #region INotifyPropertyChanged Members 

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }
        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion

    }
}
