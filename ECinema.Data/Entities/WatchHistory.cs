﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.Entities
{
    public class WatchHistory
    {
        public int WatchId { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }
        public int MovieId { get; set; }

        public DateTime WatchedAt { get; set; }
    }
}

