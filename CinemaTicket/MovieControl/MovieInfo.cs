using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;

namespace CinemaTicket.MovieControl
{
    public class MovieInfo
    {
        private WebClient myWebClient;

        public MovieInfo()
        {
            myWebClient = new WebClient();
            myWebClient.Credentials = CredentialCache.DefaultCredentials;
        }

        //从百度百科中获取电影基本信息
        public string getMovieInfoByBaidu(string name)
        {
            string url = "https://baike.baidu.com/item/" + name;
         
            Byte[] pageData = myWebClient.DownloadData(url);
            string pageHTML = Encoding.UTF8.GetString(pageData);

            int startIndex = pageHTML.IndexOf("<h2 class=\"headline-1 custom-title \">") + "<h2 class=\"headline-1 custom-title \">".Length;
            int endIndex = pageHTML.IndexOf("<a name=\"2_2\"") - "<div class=\"anchor-list\">".Length - 1;

            string movieInfo = pageHTML.Substring(startIndex, endIndex - startIndex);
            return movieInfo;
        }
    }
}