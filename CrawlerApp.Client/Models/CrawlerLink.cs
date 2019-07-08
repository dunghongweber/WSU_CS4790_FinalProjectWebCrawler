using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CrawlerApp.Client.Models
{
    public class CrawlerLink
    {
        [Key]
        public Int32 Id { get; set; }

        [Required]
        public string Link { get; set; }
        [Required]
        public Int32 Crawled { get; set; }
    }
}
