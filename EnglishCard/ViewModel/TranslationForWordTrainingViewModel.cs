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
            this.numberOfWordsInSession = numberOfWordsInSession;
            this.numberOfTranslationSuggestions = numberOfTranslationVariants;
        }

        /// <summary>
        /// Returns the training task intended to be used as DataContext
        /// </summary>
        /// <returns>Training task</returns>
        public TrainingTask GetTask()
        {
            return this.task;
        }

        /// <summary>
        /// Updates the task provided by <see cref="GetTask"/> method. If update is impossibe, you should call Reset method.
        /// </summary>
        /// <returns></returns>
        public bool UpdateTask()
        {
            // check if iterator is initialized
            if(current != null)
            {
                current.EffortsNumber += 1;
            }
            // Origin is english
            if (allWordsForTrainSession.MoveNext())
            {
                current = allWordsForTrainSession.Current;
                this.task.Initialized = true;
                var correctTranslation = current.TranslateWord;
                var translations = new string[numberOfTranslationSuggestions];
                allTranslationsForTrainSession.Where(translation => translation != correctTranslation)
                    .Take(numberOfTranslationSuggestions - 1).ToArray().CopyTo(translations, 0);
                translations[numberOfTranslationSuggestions - 1] = correctTranslation;
                translations = translations.OrderBy(_ => Guid.NewGuid()).ToArray();
                this.task.UpdateFields(current.OriginWord, translations);             
                return true;
            }
            else
            {
                // Works fine, I promise
                this.dictionary.SubmitChanges();
                this.task.Initialized = false;
                return false;
            }
            
        }

        public bool SuggestAnswer(string answer)
        {
            if (answer == current.TranslateWord)
            {
                current.SuccessfulEffortsNumber+= 1;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            this.dictionary = new DictionaryModel();
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
            return w.EffortsNumber + 2 * w.SuccessfulEffortsNumber + (new Random()).Next(-2, 2);
        }

        private int numberOfWordsInSession;

        private int numberOfTranslationSuggestions;

        private IEnumerator<Word> allWordsForTrainSession;

        private IEnumerable<string> allTranslationsForTrainSession;

        private TrainingTask task;

        private DictionaryModel dictionary;

        private Word current;
    }
}
