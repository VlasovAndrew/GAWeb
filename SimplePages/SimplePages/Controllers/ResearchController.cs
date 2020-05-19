using GeneticAlgorithm.Entities;
using GeneticAlgorithmWEB.BLL.Interfaces;
using SimplePages.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SimplePages.Controllers
{
    public class ResearchController : Controller
    {
        private readonly IGraphBL _graphBL;
        private readonly IAlgorithmWork _algorithmWork;

        public ResearchController(IGraphBL graphBL, IAlgorithmWork algorithmWork)
        {
            _graphBL = graphBL;
            _algorithmWork = algorithmWork;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<GraphInfo> graphs = _graphBL.GetAllGraphInfo();
            return View("ResearchParametrs", model: graphs);
        }

        [HttpPost]
        public ActionResult ResearchAlgorithm(ResearchRequest request) {
            ResearchAlgorithmResponse response = _algorithmWork.ResearchAlgorithm(request);
            return View("ResearchResult", model: new AlgorithmResultModel() { 
                ResearchRequest = request,
                AlgorithmResponse = response,
            });
        }
    }
}