using CinemaTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaTicket.Controllers
{
    public class MovieController : Controller
    {
        private MovieDBContext db = new MovieDBContext();
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
        public ActionResult Index(string searchString,string typeString, string areaString)
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
                           orderby d.area
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
            if (!String.IsNullOrEmpty(areaString)&&!areaString.Equals("全部"))
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
    }
}