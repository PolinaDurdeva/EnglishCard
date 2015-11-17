using System.ComponentModel;
using System.Data.Linq;

namespace EnglishCard.Model
{
    public class DictionaryModel : DataContext
    {
        public static string DBConnectionString = "/Dictionary.sdf";
        public static string DBConnectionStringFr = "/DictionaryFr.sdf";
        public static bool useFrench = false;

        // Pass the connection string to the base class.
        public DictionaryModel() : base(DBConString()) {

        }

        public DictionaryModel(string connectionString) : base(connectionString)
        {

            //this.Words = this.GetTable<Word>();
        }

        public static string DBConString()
        {
            if(App.Language)
            {
                return EnglishCard.Resources.AppResources.EnDBPath;
            } else
            {
                return EnglishCard.Resources.AppResources.EnDBPath;
            }
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
