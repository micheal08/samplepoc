using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learn_MVC_Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Content Result sample
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public ActionResult GetEmpName(int empId)
        {
            var employees = new[]
            {
                new {EmpId = 1, EmpName = "Ram", Salary = 25000},
                new {EmpId = 2, EmpName = "Kumar", Salary = 20000},
                new {EmpId = 3, EmpName = "Vinod", Salary = 30000},
            };

            string matchingEmpName = string.Empty;
            foreach (var item in employees)
            {
                if(item.EmpId == empId)
                {
                    matchingEmpName = item.EmpName;
                    break;
                }
            }

            return new ContentResult { Content = matchingEmpName, ContentType = "text/plain" };

            //Test
            //https://localhost:44337/home/getempname?empid=2
            //return Content(matchingEmpName, "text/plain");
        }

        /// <summary>
        /// File Result sample
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public ActionResult GetPaySlip(int empId)
        {

            //File should be present in current working directory
            string fileName = "~/Payslip" + empId + ".pdf";
            return File(fileName, "application/pdf");

            //Test
            //https://localhost:44337/home/GetPaySlip?empid=2
        }

        /// <summary>
        /// Redirect Result sample
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public ActionResult Empfacebookpage(int empId)
        {
            string url = "http://facebook.com/emp" + empId;

            return Redirect(url);
        }
    }
}