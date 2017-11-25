using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CinemaTicket.Models
{
    public class MovieModels
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id
        {
            get;set;
        }

        [StringLength(40,MinimumLength=1)]
        [Display(Name = "类型")]
        public string type
        {
            get;set;
        }

        [StringLength(40,MinimumLength =1)]
        [Display(Name = "片名")]
        public string name
        {
            get;set;
        }

        [Required]
        [Display(Name = "电影海报")]
        public string poster
        {
            get;set;
        }

        [Required]
        [Display(Name = "上映地区")]
        public string area
        {
            get;set;
        }

        [Display(Name = "上映时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime releaseTime
        {
            get;set;
        }

        [Required]
        [DataType(DataType.Currency)]
        [Range(1,100)]
        [Display(Name = "票价")]
        public decimal price
        {
            get;set;
        }

        [Display(Name = "备注")]
        public string remarks
        {
            get;set;
        }
    }

    public class MovieDBContext : DbContext{
        public DbSet<MovieModels> Movies { get; set; }
    }
}