using GeneticAlgorithm.Entities;
using GeneticAlgorithmWEB.BLL;
using GeneticAlgorithmWEB.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace SimplePages.Controllers
{
    public class CenterGraphController : Controller
    {
        private readonly IAlgorithmWork _algorithmWork;
        private readonly IGraphBL _graphBL;

        public CenterGraphController(IAlgorithmWork algorithmWork, IGraphBL graphBL) {
            _algorithmWork = algorithmWork;
            _graphBL = graphBL;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("FindCenter");
        }

        [HttpPost]
        public ActionResult FindCentralVertex(HttpPostedFileBase upload)
        {
            if (upload == null) 
            {
                return View("~/Views/Shared/Error.cshtml", model: "Файл не был выбран");
            }
            try
            {
                string[] fileLines = ReadFile(upload.InputStream);
                Graph graph = GraphParser.ParseSimpleTxtFormat(fileLines);
                FindingVertexResponse algorithmResult = _algorithmWork.FindCentralVertex(graph);
                return View("CalculationResult", algorithmResult);
            }
            catch (FormatException e)
            {
                // Add logger
                return View("~/Views/Shared/Error.cshtml", model: e.Message);
            }
        }

        private string[] ReadFile(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            List<string> lines = new List<string>();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);
            }
            return lines.ToArray();
        }
    }
}