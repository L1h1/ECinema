﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.Entities
{
    public class Actor
    {
        public int ActorId { get; set; }  
        public string ActorName { get; set; }  
        public DateTime? DateOfBirth { get; set; }  
        public string Bio { get; set; }  
    }
}