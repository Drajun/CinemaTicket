using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CinemaTicket.Models
{
    /// <summary>
    /// 电影信息
    /// </summary>
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
        [Display(Name = "拍摄地区")]
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

    /// <summary>
    /// 订单信息
    /// 已迁移至MovieOrderModel
    /// </summary>
    #region 订单模型
    /*public class MovieOrder
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id
        {
            get; set;
        }

        [Required]
        [Display(Name ="电影ID")]
        public int movieID
        {
            get;set;
        }

        [Required]
        [Display(Name ="购买者ID")]
        public string buyer
        {
            get;set;
        }

        [Required]
        [Display(Name ="影院名称")]
        public string cinemaName
        {
            get;set;
        }

        [Required]
        [Display(Name ="影院地区")]
        public string area
        {
            get;set;
        }

        [Required]
        [Display(Name ="座位")]
        public string seats
        {
            set;get;
        }

        [Required]
        [Display(Name ="场次")]
        public string playTime
        {
            get;set;
        }

        [Required]
        [Display(Name ="购买日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime purchaseDate
        {
            get;set;
        }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name ="总价")]
        public decimal totalPrice
        {
            get;set;
        }

        [Required]
        [Display(Name ="取票号")]
        public string getNum
        {
            get;set;
        }

        [Display(Name ="备注")]
        public string remarks
        {
            get;set;
        }
    }*/
    #endregion

    public class MovieDBContext : DbContext{
        public DbSet<MovieModels> Movies { get; set; }
        //public DbSet<MovieOrder> Orders { get; set; }
    }
}