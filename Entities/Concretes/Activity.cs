using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes
{
    public class Activity : Entity<int>
    {
        public int UserId { get; set; }
        public string ActivityType { get; set; }
        public DateTime Date {  get; set; }
        public string Description { get; set; }
        public User User { get; set; }

    }
}
