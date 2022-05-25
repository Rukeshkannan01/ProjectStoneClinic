using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoneClinic.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using StoneClinic.DAL;
namespace StoneClinic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Home()
        {
            return View();
        }


        // --------- Login Controller ---------- //
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LoginVerify(Staff s)
        {
            StoneClinicDAL lobj = new StoneClinicDAL();
            int verify = lobj.LoginValidate(s);
            if (verify == 1)
            {
                //ViewBag.AlertMsg = "Login Successfully";
                TempData["SuccessMessage"] = "Logged in successfully.";
                return RedirectToAction("Home");
            }
            else
            {                
                //return RedirectToAction("ErrorMessage");
                ViewBag.AlertMsg = "*Incorrect Username or Password";
                return View("Login");
            }                 
        }


        // --------- Add Patient Controller ---------- // 
        public IActionResult PatientInfo()
        {
            return View();
        }        
        public IActionResult AddPatient(Patient p)
        {
            StoneClinicDAL addobj = new StoneClinicDAL();
            int inst = addobj.AddPatientInfo(p);
            if (inst == 1)
            {
                //return RedirectToAction("SuccessMessage", p);
                TempData["SuccessMessage"] = "Patient information is successfully added.";
                return RedirectToAction("PatientInfo",p);
            }
            else
                return View("Home");
            //try
            //{

            //}
            //catch
            //{
            //    return View("PatientInfo");
            //}
        }


        // --------- Add Doctor Controller ---------- // 
        public IActionResult DoctorInfo()
        {
            return View();
        }        
        public IActionResult AddDoctor(Doctor d)
        {
            StoneClinicDAL addobj = new StoneClinicDAL();
            int inst = addobj.AddDoctorInfo(d);
            if (inst == 1)
            {
                TempData["SuccessMessage"] = "Doctor information is successfully added.";
                return RedirectToAction("DoctorInfo",d);
                //return RedirectToAction("SuccessMessage", d);
            }
            else
                return View("Home");
                //TempData["SuccessMessage"] = "Saved Successfully";
        }       


        // ---------- Schedule Controller ----------- //
        public IActionResult ScheduleAppointment()
        {
            return View();
        }
        public IActionResult InsertAppiontment(Appointment a)
        {
            try 
            {
                StoneClinicDAL addobj = new StoneClinicDAL();
                int inst = addobj.ScheduleAppointment(a);
                if (inst == 1)
                {
                    //return RedirectToAction("SuccessMessage", a);
                    TempData["SuccessMessage"] = "Appointment scheduled successfully.";
                    return RedirectToAction("DoctorInfo",a);
                }
                else
                    return View("Home");
            }
            catch
            {
                return View("ErrorMessage");
            }
            
        }

        

        // ------------ Cancel appointment controller ----------- //
        public IActionResult CancelAppointment()
        {
            return View();
        }
        public IActionResult ListAppointment()
        {
            StoneClinicDAL listobj = new StoneClinicDAL();
            List<Appointment> AppointmentList = new List<Appointment>();
            AppointmentList = listobj.ShowAppointment();
            return View(AppointmentList);
        }
        //public IActionResult DisplayAppointment(int PatientID, string VisitDate)
        //{
        //    StoneClinicDAL delobj = new StoneClinicDAL();
        //    List<Appointment> dlist = new List<Appointment>();
        //    dlist = delobj.ShowAppointment();
        //    return View(dlist);
        //}
        public ActionResult Delete(int id, int aptid)
        {
            try
            {
                StoneClinicDAL sdb = new StoneClinicDAL();
                if (sdb.DeleteAppointment(id, aptid))
                {                   
                    TempData["SuccessMessage"] = "Appointment Canceled Successfully.";                   
                }
                return RedirectToAction("DeleteAppointment");
            }
            catch
            {
                return View("ErrorMessage");
            }
        }

        //public IActionResult DeleteList(int del)
        //{
        //    StoneClinicDAL cobj = new StoneClinicDAL();
        //    int result = cobj.CancelAppointment(del);
        //    if (result == 1)
        //        return Content("appointment canceled");
        //    else
        //        return View("Home");
        //}

        public IActionResult DeleteAppointment()
        {
            StoneClinicDAL delobj = new StoneClinicDAL();
            List<Appointment> dlist = new List<Appointment>();
            dlist = delobj.ShowAppointment();
            return View(dlist);
        }




        public ActionResult ShowById(int patientId, string visitDate)
        {
            try
            {
                StoneClinicDAL showobj = new StoneClinicDAL();
                List<Appointment> AppointmentList = new List<Appointment>();
                AppointmentList = showobj.ListAppointment(patientId, visitDate);
                return RedirectToAction("ListAppointment");
            }
            catch
            {
                return View("DeleteAppointment");
            }
        }


        public IActionResult SuccessMessage()
        {
            return View();
        }

        public string OpenModelPopup()
        {
            //can send some data also.  
            return "<h1>This is Modal Popup Window</h1>";
        }
        public IActionResult ErrorMessage()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
