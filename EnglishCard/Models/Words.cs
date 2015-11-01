using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;


namespace EnglishCard.Model
{
    [Table]
    public class Word : INotifyPropertyChanged, INotifyPropertyChanging
    {
        [Column(IsVersion = true)]
        private Binary _version;

        private int _Id;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    NotifyPropertyChanging("WordId");
                    _Id = value;
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

        private bool _flagKnowledge;
        [Column]
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
                    NotifyPropertyChanging("FlagKnowledge");
                    _flagKnowledge = value;
                    NotifyPropertyChanged("FlagKnowledge");
                }
            }
        }

        private int _countTest;
        [Column]
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
                    NotifyPropertyChanging("CountTest");
                    _countTest = value;
                    NotifyPropertyChanged("CountTest");
                }

            }
        }

        private int _countSuccessTest;
        [Column]
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
                    NotifyPropertyChanging("CountSuccessTest");
                    _countSuccessTest = value;
                    NotifyPropertyChanged("CountSuccessTest");
                }
            }
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
    public class Dictionary : DataContext
    {
        // Pass the connection string to the base class.
        public Dictionary(string connectionString) : base(connectionString)
        { }

        // Specify a table for the words.
        public Table<Word> Dict;
    }
}
