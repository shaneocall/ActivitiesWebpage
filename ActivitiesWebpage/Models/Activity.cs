using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActivitiesWebpage.Models
{
    public class Activity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateEnded { get; set; }
        public int GamesPlayed { get; set; }
    }
}