using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using NetDosUno.Models;
using Newtonsoft.Json;
using static Models.Modelo;

namespace NetDosUno.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Asteroids()
        { 
          
                return View();
        }

        [HttpPost]
        public IActionResult Asteroids(string start, string end, [Bind("near_earth_object")] Rootobject rootObj)
        {
            //Rootobject modelo;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://api.nasa.gov/neo/rest/v1/feed?start_date=" + start + "&end_date=" + end + "&api_key=GzRydzlaSEfI60xIHFvraQcNVEqw5Dc4Pr8PHZfU");
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                rootObj = JsonConvert.DeserializeObject<Rootobject>(json);
                return View(rootObj);
            }

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
