﻿using System;
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
    public class WordForTransaltionTrainingViewModel : ITrainingViewModel
    {

        public WordForTransaltionTrainingViewModel(int numberOfWordsInSession, int numberOfTranslationVariants)
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

        public bool UpdateTask()
        {
            // check if iterator is initialized
            if (this.allWordsForTrainSession.Current != null)
            {
                this.allWordsForTrainSession.Current.EffortsNumber += 1;
                this.dictionary.SubmitChanges();
            }
            // Here origin means word in Russian
            if (allWordsForTrainSession.MoveNext())
            {
                this.task.Initialized = true;
                var correctOrigin = allWordsForTrainSession.Current.OriginWord;
                var origins = new string[numberOfTranslationSuggestions];
                allOriginsForTrainSession.Where(origin => origin != correctOrigin)
                    .Take(numberOfTranslationSuggestions - 1).ToArray().CopyTo(origins, 0);
                origins[numberOfTranslationSuggestions - 1] = correctOrigin;
                origins = origins.OrderBy(_ => Guid.NewGuid()).ToArray();
                this.task.UpdateFields(allWordsForTrainSession.Current.TranslateWord, origins);
                return true;
            }
            else
            {
                this.dictionary.SubmitChanges();
                this.task.Initialized = false;
                return false;
            }
        }

        public bool SuggestAnswer(string answer)
        {
            if (answer == allWordsForTrainSession.Current.OriginWord)
            {
                this.allWordsForTrainSession.Current.SuccessfulEffortsNumber += 1;
                this.dictionary.SubmitChanges();
                return true;
            }
            return false;
        }

        public void Reset()
        {
            this.dictionary = new DictionaryModel();
            var wordsForTrain = dictionary.Words.OrderBy(learningIndex).Take(numberOfWordsInSession);
            this.allWordsForTrainSession = wordsForTrain.GetEnumerator();
            this.allOriginsForTrainSession = wordsForTrain.Select(word => word.OriginWord);
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

        private IEnumerable<string> allOriginsForTrainSession;

        private TrainingTask task;

        private DictionaryModel dictionary;
    }
}
