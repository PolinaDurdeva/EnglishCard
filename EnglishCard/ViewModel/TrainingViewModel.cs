using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using EnglishCard.Model;
using System.Collections;

namespace EnglishCard.ViewModel
{
    public class TrainingViewModel : INotifyPropertyChanged
    {
        
        public TrainingViewModel()
        {
            var wordsForTrain = dictionary.Words.OrderBy(learningIndex).Take(NumberOfWordsInPack);
            this.words = wordsForTrain.GetEnumerator();
            this.allTranslations = wordsForTrain.Select(word => word.TranslateWord);
            this.SetNextWord();
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

        public Word WordObject
        {
            get
            {
                return words.Current;
            }
        }

        public string Translation0
        {
            get
            {
                return translations[0];
            }
        }

        public string Translation1
        {
            get
            {
                return translations[1];
            }
        }

        public string Translation2
        {
            get
            {
                return translations[2];
            }
        }

        public void SetNextWord()
        {
            words.MoveNext();
            var translations = allTranslations.Where(translation => translation != words.Current.TranslateWord).OrderBy(x => Guid.NewGuid()).Take(2).ToList();
            translations.Add(words.Current.TranslateWord);
            this.translations = translations.ToArray();
            NotifyPropertyChanged("Translation0");
            NotifyPropertyChanged("Translation1");
            NotifyPropertyChanged("Translation2");
            NotifyPropertyChanged("WordObject");
        }

        /// <summary>
        /// Determines the order over the words in the dictionary.
        /// </summary>
        /// <returns>The positive value increasing with the familiarity of user with a word.</returns>
        private double learningIndex(Word w)
        {
            // TODO: Add randomness
            return Math.Log(1 + w.EffortsNumber) * w.SuccessfulEffortsNumber;
        }

        private const int NumberOfWordsInPack = 10;

        private const int TranslationsCount = 3;

        private DictionaryModel dictionary = new DictionaryModel();

        private IEnumerator<Word> words;

        private string[] translations;

        // TODO: rename me!
        private IEnumerable<string> allTranslations;
    }
}
