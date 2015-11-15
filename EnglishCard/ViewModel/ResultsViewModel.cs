using System.ComponentModel;
using EnglishCard.Model;
using EnglishCard.Resources;
using System;
// Directive for the data model.
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishCard.ViewModel
{
    public class ResultsViewModel : INotifyPropertyChanged
    {
        private DictionaryModel dictionaryDB;
        private int _countSuccessTests;
        private int _countAllWords;
        private int _countKnownWords;
        private int _totalCountTest;

        public ResultsViewModel(string stringInDictionary)
        {
            dictionaryDB = new DictionaryModel(stringInDictionary);
        }

        public int CountSuccessTests
        {
            get { return _countSuccessTests; }
            set
            {
                _countSuccessTests = value;
                NotifyPropertyChanged("CountSuccessTests");
            }
        }
        public int TotalCountTest
        {
            get { return _totalCountTest; }
            set
            {
                _totalCountTest = value;
                NotifyPropertyChanged("TotalCountTest");
            }
        }

        public int CountKnownWords
        {
            get { return _countKnownWords; }
            set
            {
                _countKnownWords = value;
                NotifyPropertyChanged("CountKnownWords");
            }
        }
        public int CountAllWords
        {
            get { return _countAllWords; }
            set
            {
                _countAllWords = value;
                NotifyPropertyChanged("CountAllWords");
            }
        }
        public void LoadInfoFromDataBase()
        {
            CountSuccessTests = dictionaryDB.Words.Sum(word => word.SuccessfulEffortsNumber);
            TotalCountTest = dictionaryDB.Words.Sum(word => word.EffortsNumber);
            CountAllWords = dictionaryDB.Words.Count();
            var knownWordsInDB = dictionaryDB.Words.Where(word => word.SuccessfulEffortsNumber > 2 && word.SuccessfulEffortsNumber == word.EffortsNumber).OrderBy(word => word.OriginWord);
            CountKnownWords = knownWordsInDB.Count();
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
    }
}

