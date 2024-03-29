﻿using LogisticCalculationMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LogisticCalculationMVC.Controllers
{
    public class FormsController : Controller
    {
        // Qopt Tab
        [HttpGet]
        public IActionResult Qopt()
        {
            return View();
        }

        [HttpPost]
        public IActionResult QoptVypocet(QoptModel model)
        {
            ViewBag.Qopt = model.Qopt();
            ViewBag.PocetDavek = model.PocetDavek();
            ViewBag.PeriodicitaZadavani = model.PeriodicitaZadavani();
            ViewBag.CelkoveNaklady = model.CelkoveNaklady();

            return View("Qopt", model);
        }

        // Analyza Zásob Tab
        [HttpGet]
        public IActionResult AnalyzaZasob()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AnalyzaZasobVypocet(AnalyzaZasobModel model)
        {
            ViewBag.System = model.ObjUrovenText(model.Systemy!);
            ViewBag.ObjednavaciUroven = model.ObjUrovenVysledek(model.Systemy!);
            ViewBag.PrumernaZasoba = model.PrumernaZasoba();
            ViewBag.PocetObjednavek = model.PocetObjednavekZaRok();

            return View("AnalyzaZasob", model);
        }

        // Průběžná Doba Tab
        [HttpGet]
        public IActionResult PrubeznaDoba()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PrubeznaDobaVypocet([FromBody] PrubeznaDobaInputModel inputData)
        {
            PrubeznaDobaModel prubeznaDobaModel = new(inputData);
            int prubeznaDobaVysledek = prubeznaDobaModel.PrubeznaDobaVysledek();

            PrubeznaDobaOutputModel outputModel = new()
            {
                SystemyPrubeznaDoba = prubeznaDobaModel.PrubeznaDobaSystemyText(),
                PrubeznaDobaVysledek = prubeznaDobaVysledek,
                PocetPracovist = prubeznaDobaModel.PocetPracovist,
                PocetPracovniku = prubeznaDobaModel.PocetPracovniku,
                DavkaQ = prubeznaDobaModel.DavkaQ,
                DavkaQd = prubeznaDobaModel.DavkaQd
            };

            return Json(new { result = "success", data = outputModel });
        }
        
        
        // Employees Tab
        [HttpGet]
        public IActionResult Employees()
        {
            return View();
        }

        public JsonResult EmployeesData()
        {
            EmployeeRepository _employeeRepository = new();
            var employees = _employeeRepository.GetEmployees();
            return Json(employees);
        }

        public JsonResult EmployeesWorkplaces()
        {
            EmployeeRepository _employeeRepository = new();
            var workplaces = _employeeRepository.GetWorkplaces();
            return Json(workplaces);
        }
    }
}