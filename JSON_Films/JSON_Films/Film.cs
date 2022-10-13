using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_Films
{
    internal class Film
    {
        public int Film_ID { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string Director { get; set; }
        public DateTime Release_Date { get; set; }

        public override string ToString()
        {
            return $"{Film_ID}, {Title}, {Synopsis}, {Director}, {Release_Date:d}";
        }
    }
}
