using System.Collections.ObjectModel;
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
    public class VocabularyViewModel : INotifyPropertyChanged
    {
        private DictionaryModel dictionaryDB;
        Random rnd = new Random();
        private Word _nextCard;

        public Word NextCard
        {
            get
            {
                return _nextCard;
            }
            set
            {
                _nextCard = value;
                NotifyPropertyChanged("NextCard");
            }

        }
 
        public void generateNextWord()
        {
            
            int wordRandom = rnd.Next(0, this.UnKnownWords.Count());
            Word nextCard = this.UnKnownWords[wordRandom];
            NextCard = nextCard;
        }

        public VocabularyViewModel (string stringInDictionary)
        {
            dictionaryDB = new DictionaryModel(stringInDictionary);
        }

        private ObservableCollection<Word> _needableWord;
        public ObservableCollection<Word> NeedableWord
        {
            get { return _needableWord; }
            set
            {
                _needableWord = value;
                NotifyPropertyChanged("NeedableWord");
            }
        }

        public void searchWords(string word)
        {
            var needableWord = dictionaryDB.Words.Where(wrd => wrd.OriginWord.ToLower().StartsWith(word.ToLower())).OrderBy(wrd => wrd.OriginWord);
            NeedableWord = new ObservableCollection<Word>(needableWord);
        }

        private ObservableCollection<Word> _allWords;

        public ObservableCollection<Word> AllWords
        {
            get { return _allWords; }
            set
            {
                _allWords = value;
                NotifyPropertyChanged("AllWords");
            }
        }


        private ObservableCollection<Word> _knownWords;

        public ObservableCollection<Word> KnownWords
        {
            get { return _knownWords; }
            set
            {
                _knownWords = value;
                NotifyPropertyChanged("KnownWords");
            }
        }

        private ObservableCollection<Word> _unKnownWords;

        public ObservableCollection<Word> UnKnownWords
        {
            get { return _unKnownWords; }
            set
            {
                _unKnownWords = value;
                NotifyPropertyChanged("UnKnownWords");
            }
        } 
        // Write changes in the data context to the database.
        public void SaveChangesToDB()
        {
            dictionaryDB.SubmitChanges();
        }
        // Query database and load the collections and list used by the pivot pages.
        public void LoadCollectionsFromDatabase()
        {
            // Specify the query for all words in the database.
            var wordsInDB = dictionaryDB.Words.Where(_ => true).OrderBy(word => word.OriginWord);
            //var wordsInDB = from Word word in dictionaryDB.Words select word;
            //Not implemented yet

            // Query the database and load all words.
            AllWords = new ObservableCollection<Word>(wordsInDB);
            // Query the database and load unknown words.
            var unknownWordsInDB = dictionaryDB.Words.Where(word => word.SuccessfulEffortsNumber == 0 || word.SuccessfulEffortsNumber != word.EffortsNumber).OrderBy(word => word.OriginWord);
            //var unknownWordsInDB = from Word word in dictionaryDB.Words where !word.FlagKnowledge select word;
            UnKnownWords = new ObservableCollection<Word>(unknownWordsInDB);
            //var knownWordsInDB = from Word word in dictionaryDB.Words where word.FlagKnowledge select word;
            // Query the database and load known words.
            var knownWordsInDB = dictionaryDB.Words.Where(word => word.SuccessfulEffortsNumber > 2 && word.SuccessfulEffortsNumber == word.EffortsNumber).OrderBy(word => word.OriginWord);
            KnownWords = new ObservableCollection<Word>(knownWordsInDB);

        }


        // Remove a word from the database and collections.
        public void DeleteWord(Word wordForDelete)
        {

            // Remove the word from the "all" observable collection.
            AllWords.Remove(wordForDelete);
            NeedableWord.Remove(wordForDelete);
            // Remove the word from the data context.
            

            // Remove the word from the appropriate category.   
            switch (wordForDelete.isKnown())
            {
                case true:
                    KnownWords.Remove(wordForDelete);
                    break;
                case false:
                    UnKnownWords.Remove(wordForDelete);
                    break;
                default:
                    break;
            }
            dictionaryDB.Words.DeleteOnSubmit(wordForDelete);
            // Save changes to the database.
            dictionaryDB.SubmitChanges();
            
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
