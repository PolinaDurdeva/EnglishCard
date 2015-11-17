using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishCard.ViewModelHelper
{
    class FrehchLanguage : Language
    {
        public override string ConnectionString
        {
            get
            {
                return "/DictionaryFr.sdf";
            }
        }

        public override string Name
        {
            get
            {
                return "french";
            }
        }
    }
}
