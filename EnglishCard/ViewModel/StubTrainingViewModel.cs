using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnglishCard.ViewModelHelper;

namespace EnglishCard.ViewModel
{
    public class StubTrainingViewModel : ITrainingViewModel
    {
        public StubTrainingViewModel() { }

        public TrainingTask GetTask()
        {
            var task = new TrainingTask();
            task.Initialized = false;
            task.UpdateFields("", new string[] { "", "", "" });
            return task;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public bool SuggestAnswer(string answer)
        {
            return false;
        }

        public bool UpdateTask()
        {
            return false;
        }
    }
}
