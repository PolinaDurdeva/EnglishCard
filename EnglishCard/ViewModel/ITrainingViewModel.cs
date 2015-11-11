using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnglishCard.ViewModelHelper;

namespace EnglishCard.ViewModel
{
    public interface ITrainingViewModel
    {
        TrainingTask GetTask();
        bool UpdateTask();
        bool SuggestAnswer(string answer);
        void Reset();
    }
}
