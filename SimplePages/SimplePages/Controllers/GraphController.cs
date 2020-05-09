using GeneticAlgorithm.Entities;
using GeneticAlgorithmWEB.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace SimplePages.Controllers
{
    public class GraphController : Controller
    {
        private readonly IAlgorithmWork _algorithmWork;
        private readonly IGraphParser _graphParser;

        public GraphController(IAlgorithmWork algorithmWork, IGraphParser graphParser) {
            _algorithmWork = algorithmWork;
            _graphParser = graphParser;
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
                return View("~/Views/Shared/Error.cshtml");
            }
            try
            {
                string[] fileLines = ReadFile(upload.InputStream);
                Graph graph = _graphParser.ParseSimpleTxtFormat(fileLines);
                AlgorithmResult algorithmResult = _algorithmWork.FindCentralVertex(graph);
                return View("CalculationResult", algorithmResult);
            }
            catch (FormatException e)
            {
                // Add logger
                return View("~/Views/Shared/Error.cshtml", e);
            }
        }

        public string GetWorkPath() {
            return _algorithmWork.WorkDir();
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