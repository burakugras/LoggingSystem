using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Responses.UserReponses
{
    public class GetListUserResponse
    {
        public string Username { get; set; }
        public string Email { get; set; }
        //public Activity Activity{ get; set; }
        public string ActivityType { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
