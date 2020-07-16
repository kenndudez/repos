using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JacobsCourtHotel.Areas.MainPage.Controllers
{

    [Area("MainPage")]
    [Route("MainPage")]
    public class MainController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [Route("[action]")]
        public ActionResult About()
        {
            return View();
        }

        [Route("[action]")]
        public ActionResult Rooms()
        {
            return View();
        }

        [Route("[action]")]

        public ActionResult Bookings()
        {
            return View();
        }
        [Route("[action]")]
        public ActionResult RoomDetail()
        {
            return View();
        }



        [Route("[action]")]
        public ActionResult RoomDetail2()
        {
            return View();
        }

        [Route("[action]")]
        public ActionResult RoomDetail3()
        {
            return View();
        }

        [Route("[action]")]
        public ActionResult RoomDetail4()
        {
            return View();
        }

        [Route("[action]")]
        public ActionResult Services()
        {
            return View();
        }

        [Route("[action]")]
        public ActionResult SpecialOffers()
        {
            return View();
        }

        [Route("[action]")]
        public ActionResult Contact()
        {
            return View();
        }
    }
}