using LogisticCalculationMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection;

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
            double qoptVysledek = model.Qopt();
            double pocetDavek = model.PocetDavek();
            double periodicitaZadavani = model.PeriodicitaZadavani();
            double celkoveNaklady = model.CelkoveNaklady();

            ViewBag.Qopt = qoptVysledek;
            ViewBag.PocetDavek = pocetDavek;
            ViewBag.PeriodicitaZadavani = periodicitaZadavani;
            ViewBag.CelkoveNaklady = celkoveNaklady;
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
            string? systemy = model.Systemy;
            string systemText = model.ObjUrovenText(systemy);
            double objUrovenVysledek = model.ObjUrovenVysledek(systemy);
            double prumernaZasoba = model.PrumernaZasoba();
            double objednavkyRok = model.PocetObjednavekZaRok();

            ViewBag.System = systemText;
            ViewBag.ObjednavaciUroven = objUrovenVysledek;
            ViewBag.PrumernaZasoba = prumernaZasoba;
            ViewBag.PocetObjednavek = objednavkyRok;

            return View("AnalyzaZasob", model);
        }

        // Průběžná Doba Tab
        [HttpGet]
        public IActionResult PrubeznaDoba()
        {
            return View();
        }
    }
}