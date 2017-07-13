using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SapremaAPI.Models
{
    /* This is a test model to check file size when pulling information from the database
     * Reasoning behind this:
     * Instead of filtering through meditations, class/group lists on each call
     * just retrieve all the information in JSON format and use JS to filter.
     * */
    public class TestItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }
        public double Price { get; set; }
        public bool Owned { get; set; }
        public double Rating { get; set; }

    }
}
