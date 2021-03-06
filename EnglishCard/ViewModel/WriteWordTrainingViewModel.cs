﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnglishCard.ViewModelHelper;
using EnglishCard.Model;

namespace EnglishCard.ViewModel
{
    public class WriteWordTrainingViewModel : ITrainingViewModel
    {
        public WriteWordTrainingViewModel(int numberOfWordsInSession)
        {
            this.numberOfWordsInSession = numberOfWordsInSession;
        }

        public TrainingTask GetTask()
        {
            return this.task;
        }

        public void Reset()
        {
            this.dictionary = new DictionaryModel();
            var wordsForTrain = dictionary.Words.OrderBy(learningIndex).Take(numberOfWordsInSession);
            this.allWordsForTrainSession = wordsForTrain.GetEnumerator();
            this.task = new TrainingTask();
            UpdateTask();
        }

        public bool SuggestAnswer(string answer)
        {
            var correctOrigin = allWordsForTrainSession.Current.OriginWord;
            // Three elements because of bindings
            this.task.UpdateFields(allWordsForTrainSession.Current.TranslateWord, new string[] { correctOrigin, "", "" });
            // copypasted. The compicated logic ought to be here
            if (answer == allWordsForTrainSession.Current.OriginWord)
            {
                this.allWordsForTrainSession.Current.SuccessfulEffortsNumber += 1;
                this.dictionary.SubmitChanges();
                return true;
            }
            return false;
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
                // Three elements because of bindings
                this.task.UpdateFields(allWordsForTrainSession.Current.TranslateWord, new string[] {"", "", ""});
                return true;
            }
            else
            {
                this.dictionary.SubmitChanges();
                this.task.Initialized = false;
                return false;
            }
        }

        private double learningIndex(Word w)
        {
            // TODO: Add randomness
            return w.EffortsNumber + 2 * w.SuccessfulEffortsNumber + (new Random()).Next(-2, 2);
        }

        private int numberOfWordsInSession;
        private TrainingTask task;
        private IEnumerator<Word> allWordsForTrainSession;
        private DictionaryModel dictionary;
    }
}
