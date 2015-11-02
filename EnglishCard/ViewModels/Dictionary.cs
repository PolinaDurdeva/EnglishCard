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

namespace EnglishCard.ViewModels
{
    public class DictionaryViewModel : INotifyPropertyChanged
    {
        private Dictionary dictionaryDB;

        public DictionaryViewModel (string stringInDictionary)
        {
            dictionaryDB = new Dictionary(stringInDictionary);
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
            var wordsInDB = dictionaryDB.Words.Where(_ => true);

            /* Not implemented yet

            // Query the database and load all words.
            AllWords = new ObservableCollection<Word>(wordsInDB);
            // Query the database and load unknown words.
            var unknownWordsInDB = dictionaryDB.Words.Where(word => !word.FlagKnowledge);
            
            UnKnownWords = new ObservableCollection<Word>(unknownWordsInDB);

            // Query the database and load known words.
            var knownWordsInDB = dictionaryDB.Words.Where(word => word.FlagKnowledge);
            KnownWords = new ObservableCollection<Word>(knownWordsInDB);
            */
        }

        // Add a word to the database and collections.
        public void AddWord(Word newWords)
        {
            // Add a word to the data context.
            dictionaryDB.Words.InsertOnSubmit(newWords);

            // Save changes to the database.
            dictionaryDB.SubmitChanges();

            // Add a word to the "all" observable collection.
            AllWords.Add(newWords);
            // Add a word to the "unknown" observable collection.
            UnKnownWords.Add(newWords);
        }

        // Remove a word from the database and collections.
        public void DeleteWord(Word wordForDelete)
        {

            // Remove the word from the "all" observable collection.
            AllWords.Remove(wordForDelete);

            // Remove the word from the data context.
            dictionaryDB.Words.DeleteOnSubmit(wordForDelete);

            // Remove the word from the appropriate category.   
            switch (wordForDelete.FlagKnowledge)
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
