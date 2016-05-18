using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ActivitiesWebpage.Models
{
    public class Activity
    {
        [JsonProperty(PropertyName = "id")]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Id is required.")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "dateStarted")]
        [Display(Name = "Start Date")]
        public DateTime DateStarted { get; set; }

        [JsonProperty(PropertyName = "dateEnded")]
        [Display(Name = "End Date")]
        public DateTime DateEnded { get; set; }

        [JsonProperty(PropertyName = "gamesPlayed")]
        [Display(Name = "Games Played")]
        public int GamesPlayed { get; set; }
    }
}