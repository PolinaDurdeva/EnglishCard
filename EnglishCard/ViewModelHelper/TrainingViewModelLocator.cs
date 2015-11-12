using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnglishCard.ViewModel;

namespace EnglishCard.ViewModelHelper
{
    public class TrainingViewModelLocator {
    
        public ITrainingViewModel GetViewModel(string name)
        {
            switch(name) {
                case "TranslationForWordTrainingViewModel":
                    return new TranslationForWordTrainingViewModel(10, 3);
                case "StubTrainingViewModel":
                    return new StubTrainingViewModel();
                case "WordForTransaltionTrainingViewModel":
                    return new WordForTransaltionTrainingViewModel(10, 3);
                case "WriteWordTrainingViewModel":
                    return new WriteWordTrainingViewModel(10);
                default:
                    throw new NoViewModelException(name);
            }
        }

        public class NoViewModelException : Exception
        {
            public NoViewModelException(string message) : base(message) { }
        }
    }
}
