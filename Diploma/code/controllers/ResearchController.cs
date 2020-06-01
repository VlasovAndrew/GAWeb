namespace SimplePages.Controllers
{
    public class ResearchController : Controller
    {
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
            IEnumerable<GraphInfo> graphs = _graphBL.GetAllGraphInfo();
            return View("ResearchParametrs", model: graphs);
        }
        // Метод запуска алгоритма с выбранными параметрами.
        [HttpPost]
        public ActionResult ResearchAlgorithm(ResearchRequest request) {
            ResearchAlgorithmResponse response = _algorithmWork.ResearchAlgorithm(request);
            return View("ResearchResult", model: new AlgorithmResultResponse() { 
                ResearchRequest = request,
                AlgorithmResponse = response,
            });
        }
    }
}