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
    public class AddWordsViewModel : INotifyPropertyChanged
    {
        private DictionaryModel dictionaryDB;
        public AddWordsViewModel()
        {
            dictionaryDB = new DictionaryModel();
        }
        public void AddWord(Word newWords)
        {
            // Add a word to the data context.
            dictionaryDB.Words.InsertOnSubmit(newWords);

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
