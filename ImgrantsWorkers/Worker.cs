using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgrantsWorkers
{
    public class Worker
    {
        public int ID;
        public string Name;
        public string Nationality;
        public DateTime Birthday;
        public DateTime CreatedAt;
        public DateTime UpdatedAt;

        override
        public string ToString()
        {
            return $@"{Name} - ({Nationality}) - ({Birthday}) - @{CreatedAt}/{UpdatedAt}";
        }
    }
}
