using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Actor
{
    public class InsertActorRequest : IRequest<int>  
    {
        public string ActorName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Bio { get; set; }
    }
}
