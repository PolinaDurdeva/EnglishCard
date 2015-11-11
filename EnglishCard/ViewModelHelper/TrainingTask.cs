using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using EnglishCard.Model;

namespace EnglishCard.ViewModelHelper
{
    public class TrainingTask: INotifyPropertyChanged
    {
        public string WordOrigin
        {
            get { return wordOrigin; }
        }

        public string[] TranslationSuggestions {
            get { return translationSuggestions; }
        }

        public bool Initialized
        {
            get
            {
                return initialized;
            }
            set
            {
                initialized = value;
                NotifyPropertyChanged("Initialized");
            }
        }

        /// <summary>
        /// Sets field of the object to the values supplied in params. TODO: somehow exclude it from the public interface because this is to much responsibility for the user.
        /// User is intended to modify the fields through the ViewModel
        /// </summary>
        /// <param name="origin">The value for the WordOrigin field.</param>
        /// <param name="suggestions">The value for TranslationSuggestionsField.</param>
        public void UpdateFields(string origin, string[] suggestions)
        {
            this.wordOrigin = origin;
            this.translationSuggestions = suggestions;
            NotifyPropertyChanged("WordOrigin");
            NotifyPropertyChanged("TranslationSuggestions");
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

        private string wordOrigin;
        private string[] translationSuggestions;
        private bool initialized = false;
    }
}
