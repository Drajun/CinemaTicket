using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaTicket.MovieControl
{
    public class TinyMovie
    {
        public int id { get; set; }
        public string name { get; set; }
        public string poster{ get; set; }
        public string type { get; set; }

        public TinyMovie(int id,string name,string poster,string type)
        {
            this.id = id;
            this.name = name;
            this.poster = poster;
            this.type = type;
        }
}
}