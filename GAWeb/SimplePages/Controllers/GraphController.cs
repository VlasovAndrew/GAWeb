using GeneticAlgorithm.Entities;
using GeneticAlgorithm.Entities.Requests;
using GeneticAlgorithmWEB.BLL;
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
        // ссылка на модуль с алгоримом
        private readonly IAlgorithm _algorithmWork;
        // ссылка на молуль с логкой для работы с графом
        private readonly IGraphBL _graphBL;

        public GraphController(IAlgorithm algorithmWork, IGraphBL graphBL) {
            _algorithmWork = algorithmWork;
            _graphBL = graphBL;
        }

        // Метод для получения страницы поиска вершины.
        [HttpGet]
        public ActionResult Index()
        {
            return View("FindCenter");
        }
        // Метод для нахождения центральных вершин в графе.
        [HttpPost]
        public ActionResult FindCentralVertex(HttpPostedFileBase upload)
        {
            // проверка выбрал ли пользоватль граф
            if (upload == null) 
            {
                return View("~/Views/Shared/Error.cshtml", model: "Файл не был выбран");
            }
            try
            {
                // чтение строк файла
                Graph graph = ReadGraph(upload);
                // запуск алгоритма
                FindingVertexResponse algorithmResult = _algorithmWork.FindCentralVertex(graph);
                // возврат представления с полученными результатами
                return View("CalculationResult", algorithmResult);
            }
            catch (FormatException e)
            {
                // при выбрасывании исключения возвращается
                // представление с сообщением об ошибке
                return View("~/Views/Shared/Error.cshtml", model: e.Message);
            }
        }
        // Метод для возвращения страницы с добавлением графа.
        // Работает только для авторизированных пользователей. 
        [HttpGet]
        [Authorize]
        public ActionResult Add() {
            return View("Add");
        }
        // Метод для добавления графа.
        // Работает только для авторизированных пользователей.
        [HttpPost]
        [Authorize]
        public ActionResult Add(AddGraphRequest request) {
            // проверка, что пользователь выбрал файл
            if (request.Upload == null) {
                return View("~/Views/Shared/Error.cshtml", model: "Выберите файл с графом");
            }
            try
            {
                // чтение графа из файла
                Graph graph = ReadGraph(request.Upload);
                // проверка на существование имени файла
                if (request.Name == null) {
                    graph.Name = "Граф пользователя";
                }
                else {
                    graph.Name = request.Name;
                }
                // добавление файла в базу данных
                _graphBL.Add(graph);
                // перенаправление на гравную страницу
                return Redirect("/");
            }
            catch (FormatException e) {
                // при выбрасывании исключения возвращается представление с сообщением об ошибке
                return View("~/Views/Shared/Error.cshtml", model: e.Message);
            }
        }
        // Чтение файла из потока данных.
        private string[] ReadFile(Stream stream)
        {
            // читатель потока 
            StreamReader reader = new StreamReader(stream);
            // результат чтения
            List<string> lines = new List<string>();
            // текущая строка файла
            string line;
            // построчное чтение файла до тех пор, 
            // пока не будет достигнут конец файла
            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);
            }
            return lines.ToArray();
        }
        // Чтение графа из строк файла.
        private Graph ReadGraph(HttpPostedFileBase upload) 
        {
            // выделение строк из входного потока
            string[] fileLines = ReadFile(upload.InputStream);
            // парсинг строк
            return GraphParser.ParseTxtFormat(fileLines);
        }
    }
}