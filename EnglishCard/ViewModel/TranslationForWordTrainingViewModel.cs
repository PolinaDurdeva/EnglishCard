using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using EnglishCard.Model;
using System.Collections;
using EnglishCard.ViewModelHelper;

namespace EnglishCard.ViewModel
{
    public class TranslationForWordTrainingViewModel : ITrainingViewModel
    {
        
        public TranslationForWordTrainingViewModel(int numberOfWordsInSession, int numberOfTranslationVariants)
        {
            this.dictionary = new DictionaryModel();
            this.numberOfWordsInSession = numberOfWordsInSession;
            this.numberOfTranslationSuggestions = numberOfTranslationVariants;
            this.Reset();
        }

        /// <summary>
        /// Returns the training task intended to be used as DataContext
        /// </summary>
        /// <returns>Training task</returns>
        public TrainingTask GetTask()
        {
            return this.task;
        }

        public bool UpdateTask()
        {
            if (allWordsForTrainSession.MoveNext())
            {
                var correctTranslation = allWordsForTrainSession.Current.TranslateWord;
                var translations = new string[numberOfTranslationSuggestions];
                allTranslationsForTrainSession.Where(translation => translation != correctTranslation)
                    .Take(numberOfTranslationSuggestions - 1).ToArray().CopyTo(translations, 0);
                translations[numberOfTranslationSuggestions - 1] = correctTranslation;
                translations = translations.OrderBy(_ => Guid.NewGuid()).ToArray();
                this.task.UpdateFields(allWordsForTrainSession.Current.OriginWord, translations);
                return true;
            }
            else
            {
                this.dictionary.SubmitChanges();
                return false;
            }
        }

        public bool SuggestAnswer(string answer)
        {
            this.allWordsForTrainSession.Current.EffortsNumber += 1;
            if (answer == allWordsForTrainSession.Current.TranslateWord)
            {
                this.allWordsForTrainSession.Current.SuccessfulEffortsNumber+= 1;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            var wordsForTrain = dictionary.Words.OrderBy(learningIndex).Take(numberOfWordsInSession);
            this.allWordsForTrainSession = wordsForTrain.GetEnumerator();
            this.allTranslationsForTrainSession = wordsForTrain.Select(word => word.TranslateWord);
            this.task = new TrainingTask();
            UpdateTask();
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

        private int numberOfWordsInSession;

        private int numberOfTranslationSuggestions;

        private IEnumerator<Word> allWordsForTrainSession;

        private IEnumerable<string> allTranslationsForTrainSession;

        private TrainingTask task;

        private DictionaryModel dictionary;
    }
}
