using CinemaTicket.Models;
using CinemaTicket.MovieControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace CinemaTicket.Controllers
{
    public class MovieController : Controller
    {
        private MovieDBContext db = new MovieDBContext();
        private MovieOrderModel db_order = new MovieOrderModel();
        //自动填充
        [AllowAnonymous]
        public ActionResult AutoComplete(string term)
        {
            //从数据库中取term开头的标题
            var model = db.Movies.Where(m => m.name.StartsWith(term))
                .Take(10)
                .Select(m => new
                {
                    label = m.name
                });

            //返回Json数据
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        // GET: Movie
        public ActionResult Index(string searchString, string typeString, string areaString)
        {
            //获取全部电影类型
            var typeList = new List<String>();
            var typeQry = from d in db.Movies
                          orderby d.type
                          select d.type;
            typeList.AddRange(typeQry.Distinct());
            ViewBag.movieType = typeList;

            //获取地区
            var areaList = new List<String>();
            var areaQry = from d in db.Movies
                          orderby d.releaseTime descending
                          select d.area;
            areaList.AddRange(areaQry.Distinct());
            ViewBag.movieArea = areaList;

            //获取全部电影
            var movies = from m in db.Movies
                         select m;
            //筛选电影名
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.name.Contains(searchString));
            }
            //筛选地区
            if (!String.IsNullOrEmpty(areaString) && !areaString.Equals("全部"))
            {
                movies = movies.Where(x => x.area == areaString);
            }
            //筛选类型
            if (!String.IsNullOrEmpty(typeString) && !typeString.Equals("全部"))
            {
                movies = movies.Where(x => x.type == typeString);
            }

            //如果是Ajax请求就返回部分网页
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Movies", movies);
            }

            return View(movies);
        }


        private MovieInfo movieInfo = new MovieInfo();
        public ActionResult MovieInfo(int? id, string name, string type)
        {
            if (id == null || name == null || type == null)
            {
                Index(null, null, null);
                return View("Index");
            }
            else
            {
                List<TinyMovie> RecommendList = new List<TinyMovie>();
                //获取同类型的全部电影
                var recommend = from m in db.Movies
                                where m.type == type
                                select m;
                //去重复
                recommend = recommend.Where(x => x.name != name);
                //将当前电影信息加入List
                foreach (var r in recommend)
                {
                    TinyMovie tMovie = new TinyMovie(r.id, r.name, r.poster, r.type);
                    RecommendList.Add(tMovie);
                }
                ViewBag.Recommend = RecommendList;

                //获取与id相符的电影
                var movieByID = from m in db.Movies
                                where m.id == id
                                select m;

                //获取电影信息
                ViewBag.MovieInfo = HttpUtility.HtmlDecode(movieInfo.getMovieInfoByBaidu(name));

                return View(movieByID);
            }
        }

        public ActionResult selectCinema(int? id, string name)
        {
            if (id == null || name == null)
            {
                Index(null, null, null);
                return View("Index");
            }
            else
            {
                //获取与id相符的电影
                var movieByID = from m in db.Movies
                                where m.id == id
                                select m;

                //获取连续四天的日期
                List<string> mAndDList = new List<string>();
                mAndDList.Add("今天" + DateTime.Now.ToString("MM月dd日"));
                mAndDList.Add(DateTime.Now.AddDays(1).ToString("MM月dd日"));
                mAndDList.Add(DateTime.Now.AddDays(2).ToString("MM月dd日"));
                mAndDList.Add(DateTime.Now.AddDays(3).ToString("MM月dd日"));
                ViewBag.monthAndDay = mAndDList;

                return View(movieByID);
            }
        }

        public ActionResult selectSeat(string date, string cinema, int? id,string area)
        {
            if (String.IsNullOrEmpty(date) || String.IsNullOrEmpty(cinema) || id==null)
            {
                Index(null, null, null);
                return View("Index");
            }
            //获取与id相符的电影
            var movieByID = from m in db.Movies
                            where m.id == id
                            select m;

            ViewBag.cinemaAndTime = date + "+" + cinema+"+"+area;
            return View(movieByID);
        }


        [Authorize(Roles = "ordinary")]
        public ActionResult pay(int? id,string movieName,string playTime,string cinemaName, decimal? movieTotalPrice,string seats,string area)
        {
            if (id==null || String.IsNullOrEmpty(movieName) || String.IsNullOrEmpty(playTime)|| String.IsNullOrEmpty(cinemaName)|| movieTotalPrice==null||String.IsNullOrEmpty(seats)||String.IsNullOrEmpty(area))
            {
                Index(null, null, null);
                return View("Index");
            }
            else
            {
                buyInfo buy = new buyInfo(id,movieName, playTime, cinemaName, movieTotalPrice, seats, area);
                ViewBag.buyInfo = buy;

                var movieByID = from m in db.Movies
                                where m.id == id
                                select m;

                return View(movieByID);
            }
            
        }

        [Authorize(Roles = "ordinary")]
        public ActionResult success(int? movieID,string playTime, string cinemaName, decimal? movieTotalPrice, string seats, string area,string movieName)
        {
            if(movieID==null||String.IsNullOrEmpty(playTime) || String.IsNullOrEmpty(cinemaName) || movieTotalPrice==null|| String.IsNullOrEmpty(seats) || String.IsNullOrEmpty(area) || String.IsNullOrEmpty(movieName))
            {
                Index(null, null, null);
                return View("Index");
            }
            else
            {
                //取票号
                int getNum = ((int)movieID + cinemaName.GetHashCode() + playTime.GetHashCode() + seats.GetHashCode()) * -1 / 10000;
                //当前用户ID
                string userID = User.Identity.GetUserId();

                try
                {
                    //将订单保存
                    OrderModels order = new OrderModels();
                    order.Id = -1;
                    order.movieID = (int)movieID;
                    order.playTimes = playTime;
                    order.cinemaName = cinemaName;
                    order.totalPrice = (decimal)movieTotalPrice;
                    order.seats = seats;
                    order.area = area;
                    order.movieName = movieName;
                    order.purchaseDate = DateTime.Now;
                    order.getNum = getNum.ToString();
                    order.buyer = userID;
                    db_order.OrderModels.Add(order);
                    db_order.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                }

                ViewBag.getNum = getNum;
                return View();
            }
            
        }

        [Authorize(Roles = "ordinary")]
        public ActionResult orderList()
        {
            string buyerID= User.Identity.GetUserId();
            var myOrder = from o in db_order.OrderModels
                          where o.buyer == buyerID
                          select o;
            return View(myOrder);
        }

        public ActionResult getTicketFlow()
        {
            return View();
        }
    }
}