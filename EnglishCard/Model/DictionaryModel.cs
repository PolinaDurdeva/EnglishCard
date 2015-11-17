using System.ComponentModel;
using System.Data.Linq;

namespace EnglishCard.Model
{
    public class DictionaryModel : DataContext
    {
        // Pass the connection string to the base class.
        public DictionaryModel() : base(App.Lang.ConnectionString) {

        }

        public DictionaryModel(string connectionString) : base(connectionString)
        {

            //this.Words = this.GetTable<Word>();
        }

        // Specify a table for the words.
        public Table<Word> Words
        {
            get
            {
                return this.GetTable<Word>();
            }
        }
    }
}
