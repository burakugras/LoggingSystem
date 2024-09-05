using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCutingConcerns.Logging.DecriptionTools
{
    public class Parameter
    {
        public string Name { get; set; }
        public List<string> Value { get; set; }
        public string Type { get; set; }
    }
}
