using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishCard.ViewModelHelper
{
    public static class LanguageLocator
    {
        public static Language Default()
        {
            return new EnglishLanguage();
        }

        public static Language GetLanguage(string name)
        {
            return langs.Where(l => l.Name == name).First();
        }

        public static IEnumerable<Language> GetLanguages()
        {
            return langs;
        }

        private static Language[] langs = new Language[] { new EnglishLanguage(), new FrehchLanguage() };
    }
}
