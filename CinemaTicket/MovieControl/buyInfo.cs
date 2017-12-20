using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaTicket.MovieControl
{
    public class buyInfo
    {
        public int movieID;
        public string movieName;
        public string playTime;
        public string cinemaName;
        public decimal movieTotalPrice;
        public string seats;
        public string area;
        public string thisID { get; set; }

        public buyInfo(int? movieID,string movieName, string playTime, string cinemaName, decimal? movieTotalPrice, string seats,string area)
        {
            this.movieID = (int)movieID;
            this.movieName = movieName;
            this.playTime = playTime;
            this.cinemaName = cinemaName;
            this.movieTotalPrice = (decimal)movieTotalPrice;
            this.seats = seats;
            this.area = area;
        }
    }
}