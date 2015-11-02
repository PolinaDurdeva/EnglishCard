﻿using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;


namespace EnglishCard.Model
{
    [Table]
    public class Word : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private int _Id;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int WordId
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
                    NotifyPropertyChanging("CountTest");
                    _effortsNumber = value;
                    NotifyPropertyChanged("CountTest");
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
                    NotifyPropertyChanging("CountSuccessTest");
                    _successfulEffortsNumber = value;
                    NotifyPropertyChanged("CountSuccessTest");
                }
            }
        }
        
        public bool FlagKnowledge
        {
            get
            {
                return _successfulEffortsNumber > 0 && _successfulEffortsNumber / _effortsNumber > 0.5 && _effortsNumber > 3; 
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
        //public static string DBConnectionString = "Data Source=appdata:/Dictionary.sdf";
        public static string DBConnectionString = @"isostore:/Dictionary.sdf";
        // Pass the connection string to the base class.
        public Dictionary(string connectionString) : base(connectionString)
        {
            
            //this.Words = this.GetTable<Word>();
        }

        // Specify a table for the words.
        public Table<Word> Words
        {
           get
               {
               return this.GetTable<Word>();
               }
        }
    }
}
