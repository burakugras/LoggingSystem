using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes
{
    public class Log
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
        public string ActivityType { get; set; }
    }
}
