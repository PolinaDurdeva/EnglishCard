using System.Collections.ObjectModel;
using System.ComponentModel;
using EnglishCard.Models;
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

        private ObservableCollection<Words> _allWords;
        public ObservableCollection<Words> AllWords
        {
            get { return _allWords; }
            set
            {
                _allWords = value;
                NotifyPropertyChanged("AllWords");
            }
        }

        private ObservableCollection<Words> _knownWords;
        public ObservableCollection<Words> KnownWords
        {
            get { return _knownWords; }
            set
            {
                _knownWords = value;
                NotifyPropertyChanged("KnownWords");
            }
        }

        private ObservableCollection<Words> _unKnownWords;
        public ObservableCollection<Words> UnKnownWords
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
            var wordsInDB = from Words word in dictionaryDB.Dict
                                select word;

            // Query the database and load all words.
            AllWords = new ObservableCollection<Words>(wordsInDB);


            // Query the database and load all associated words to their respective collections.
            //!!!!Добавить инициализацию для коллекций неизвестных слов и известных
   /*          foreach (Words word in wordsInDB)
            {
                switch (word.FlagKnowledge)
                {
                    case true:
                        KnownWords = new ObservableCollection<Words>();
                        break;
                    case false:
                        UnKnownWords = new ObservableCollection<Words>();
                        break;
                    default:
                        break;
                }
            }
   */

        }
        // Add a to-do item to the database and collections.
        public void AddWord(Words newWords)
        {
            // Add a word to the data context.
            dictionaryDB.Dict.InsertOnSubmit(newWords);

            // Save changes to the database.
            dictionaryDB.SubmitChanges();

            // Add a word to the "all" observable collection.
            AllWords.Add(newWords);
            UnKnownWords.Add(newWords);
        }

        // Remove a word from the database and collections.
        public void DeleteWord(Words wordForDelete)
        {

            // Remove the to-do item from the "all" observable collection.
            AllWords.Remove(wordForDelete);

            // Remove the word from the data context.
            dictionaryDB.Dict.DeleteOnSubmit(wordForDelete);

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
