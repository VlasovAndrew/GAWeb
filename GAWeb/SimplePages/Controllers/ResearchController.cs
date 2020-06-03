using GeneticAlgorithm.Entities;
using GeneticAlgorithm.Entities.Requests;
using GeneticAlgorithm.Entities.Response;
using GeneticAlgorithmWEB.BLL.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SimplePages.Controllers
{
    public class ResearchController : Controller
    {
        // ссылки на модули для работы с 
        // графами и генетическим алгоритмом
        private readonly IGraphBL _graphBL;
        private readonly IAlgorithm _algorithmWork;

        public ResearchController(IGraphBL graphBL, IAlgorithm algorithmWork)
        {
            _graphBL = graphBL;
            _algorithmWork = algorithmWork;
        }
        // Метод получения страницы для выбора параметров для запуска генетического алгоритма.
        [HttpGet]
        public ActionResult Index()
        {
            // получение информации о сохраненных графах
            IEnumerable<GraphInfo> graphs = _graphBL.GetAllGraphInfo();
            // возврат представления для выбора параметров с информацией о графах
            return View("ResearchParametrs", model: graphs);
        }
        // Метод запуска алгоритма с выбранными параметрами.
        [HttpPost]
        public ActionResult ResearchAlgorithm(ResearchRequest request) {
            // получение ответа от алгоритма
            ResearchAlgorithmResponse response = _algorithmWork.ResearchAlgorithm(request);
            // возврат представления с полученными результатами работы алгоритма
            return View("ResearchResult", model: new AlgorithmResultResponse() { 
                ResearchRequest = request,
                AlgorithmResponse = response,
            });
        }
    }
}