﻿using GeneticAlgorithm.Entities;
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
        private readonly IAlgorithm _algorithmWork;
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
            if (upload == null) 
            {
                return View("~/Views/Shared/Error.cshtml", model: "Файл не был выбран");
            }
            try
            {
                Graph graph = ReadGraph(upload);
                FindingVertexResponse algorithmResult = _algorithmWork.FindCentralVertex(graph);
                return View("CalculationResult", algorithmResult);
            }
            catch (FormatException e)
            {
                return View("~/Views/Shared/Error.cshtml", model: e.Message);
            }
        }
        // Метод для возвращения страницы с добавлением графа.
        [HttpGet]
        [Authorize]
        public ActionResult Add() {
            return View("Add");
        }
        // Метод для добавления графа.
        [HttpPost]
        [Authorize]
        public ActionResult Add(AddGraphRequest request) {
            if (request.Upload == null) {
                return View("~/Views/Shared/Error.cshtml", model: "Выберите файл с графом");
            }
            try
            {
                Graph graph = ReadGraph(request.Upload);
                if (request.Name == null) {
                    graph.Name = "Граф пользователя";
                }
                else {
                    graph.Name = request.Name;
                }
                _graphBL.Add(graph);
                return Redirect("/");
            }
            catch (FormatException e) {
                return View("~/Views/Shared/Error.cshtml", model: e.Message);
            }
        }
        // Чтение файла из потока данных.
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
        // Чтение графа из строк файла.
        private Graph ReadGraph(HttpPostedFileBase upload) 
        {
            string[] fileLines = ReadFile(upload.InputStream);
            return GraphParser.ParseTxtFormat(fileLines);
        }
    }
}