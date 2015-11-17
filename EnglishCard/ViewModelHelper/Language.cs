using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishCard.ViewModelHelper
{
    public abstract class Language
    {
        public abstract string Name
        {
            get;
        }

        public abstract string ConnectionString
        {
            get;
        }
    }
}
