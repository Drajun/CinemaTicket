namespace CinemaTicket.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderModels
    {
        public int Id { get; set; }

        public int movieID { get; set; }

        [Required]
        [StringLength(50)]
        public string buyer { get; set; }

        [Required]
        [StringLength(50)]
        public string cinemaName { get; set; }

        [Required]
        [StringLength(10)]
        public string area { get; set; }

        [Required]
        [StringLength(50)]
        public string seats { get; set; }

        [Required]
        [StringLength(20)]
        public string playTimes { get; set; }

        public DateTime purchaseDate { get; set; }

        public decimal totalPrice { get; set; }

        [Required]
        [StringLength(10)]
        public string getNum { get; set; }

        public string remarks { get; set; }
        
        
        public string movieName { get; set; }
    }
}
