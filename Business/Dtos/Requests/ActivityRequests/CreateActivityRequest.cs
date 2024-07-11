using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Requests.ActivityRequests
{
    public class CreateActivityRequest
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string ActivityType { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
