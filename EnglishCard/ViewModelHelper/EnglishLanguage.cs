using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishCard.ViewModelHelper
{
    class EnglishLanguage : Language
    {
        public override string ConnectionString
        {
            get
            {
                return "/Dictionary.sdf";
            }
        }

        public override string Name
        {
            get
            {
                return "english";
            }
        }
    }
}
