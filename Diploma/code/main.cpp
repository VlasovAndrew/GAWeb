#include <fstream>
#include <map>
#include <sstream>
#include <iomanip>
#include <algorithm>

#include "GraphWork.h"
#include "GeneticAlgorithm.h"
#include "OtherGA.h"
#include "FileWork.h"
#include "Entities.h"
#include "Repository.h"
#include "ExactAlgorithm.h"

using std::make_pair;
using std::ifstream;
using std::stringstream;

using std::pair;

double badTimeFunction(double t1, int n, int m) {
	double n32 = pow(n, 1.5);
	//double C1 = 1.0, C2 = 1.0;
	double C1 = 10.0, C2 = 70.0;
	double t2 = (m + n32) * C2 / (sqrt(n) * m + n32) * t1 / C1;
	return t2;
}
// Класс отвечающий за проведение эксперимента
class Experiment {
private:
	// число тестовых итераций
	const int ITER = 100;
	// Параметры графа
	GraphDescription graph;
	// функция для вичисления времени работы 
	// алгортима с заданными параметрами
	pair<double, double> calculateTimeErrorValue(int popSize, double pm, double pc) {
		// число ошибок и среднее время работы
		int error = 0;
		double sum_time = 0.0;
		// итеративное выполнение тестов
		for (int i = 0; i < ITER; i++) {
			// инициализация графа
			Graph* g1 = new Graph(graph.n, graph.m, graph.e);
			// инициализация генетического алгоритма
			GeneticAlgorithm genAlg(g1, popSize, pc, pm);
			// замер времени работы алгортима
			double time = 0.0;
			// получение найденного радиуса
			int R = genAlg.getBestResult(time);
			sum_time += time;
			// проверка на верно найденный радиус
			if (R != graph.realR) {
				error++;
			}
			delete g1;
		}
		// вычисление среднего времени работы и процента ошибок
		double avg_time = sum_time / double(ITER);
		double avg_error = error / double(ITER) * 100.0;
		return make_pair(avg_time, avg_error);
	}
	// запуск алгоритма с одним из изменяющихся параметров
	// popSize - размер популяции, probParam - значение одного из параметров
	// pmProb - флаг, отвечающий за то какой из параметров передан в качестве фиксированного
	// true - параметр pm, false - параметр pc
	pair<vector<GaTestResult>, vector<GaTestResult>> testWithChangebleProb(int popSize, double probParam, bool pmProb) {
		// шаг, с которым будет перебираться параметр
		double step = 0.1;
		// число итераций для перебора параметров
		int itarationCount = ceil(1.0 / step);
		// вывод информации о параметрах
		cout << "popSize = " << popSize;
		if (pmProb) {
		   cout << " pm = " << probParam << endl;
		}
		else {
			cout << " pc = " << probParam << endl;
 		}
		// вывод шапки для данных 
		if (pmProb) {
			cout << "pc \t AVG_TIME \t ERROR" << endl;
		}
		else {
			cout << "pm \t AVG_TIME \t ERROR" << endl;
		}
		// переменные для хранения результатов измерений
		vector<GaTestResult> time;
		vector<GaTestResult> error;
		// перебор одного из параметров
		for (int i = 0; i <= itarationCount; i++) {
			double pm, pc;
			// в зависимости от флага pmProb перебирается или параметр pm или pc
			if (pmProb) {
				pm = probParam;
				pc = i * step;
			}
			else {
				pc = probParam;
				pm = i * step;
			}
			// вычисление среднего времени работы и процента ошибок
			pair<double, double> metering = this->calculateTimeErrorValue(popSize, pm, pc);
			double avg_time = metering.first;
			double error_percent = metering.second;
			// вывод информации о полученных результатах
			cout << (pmProb ? pc : pm) << "\t" << avg_time << "\t" << error_percent << endl;
			time.push_back(GaTestResult(popSize, pm, pc, avg_time));
			error.push_back(GaTestResult(popSize, pm, pc, error_percent));
		}
		// возврат найденных результатов
		return make_pair(time, error);
	}

public:
	Experiment(GraphDescription graph) {
		srand(time(NULL) % INT_MAX);
		this->graph = graph;
	}
	// Тест с изменяющимся параметром pc
	pair<vector<GaTestResult>, vector<GaTestResult>> oneDimentionFixedGATest() {
		// параметры алгоритма для теста
		int populationN = 20;
		double pm = 0.4;
		return this->testWithChangebleProb(populationN, pm, true);
	}
	// запуск простого теста с переданными параметрами	
	pair<double, double> simpleTimeErrorTest(double pm, double pc, int popSize) {
		return this->calculateTimeErrorValue(popSize, pm, pc);
	}
	// запуск алгоритма N4N
	void simpleNANTest() {
		// действия аналогичны - измеряется среднее 
		// время работы и процент ошибки
		int error = 0;
		double avg_time = 0.0;
		for (int i = 0; i < ITER; i++) {
			Graph* g1 = new Graph(graph.n, graph.m, graph.e);
			cout << "Start OTHER genetic algorithm" << endl;
			SimpleGeneticAlgorithm sgen(g1, 50, 10, 0.7, 0.1);
			double time = 0.0;
			int R = sgen.getBestResult(time);
			avg_time += time;
			if (R != graph.realR)
				error++;
		}
		cout << "AVG Time = " << avg_time / double(ITER) << endl;
		cout << "Error = " << double(error) / double(ITER) << endl;
	}
	// тестирование алгоритма с перебор параметров pm и pc 
	pair<vector<GaTestResult>, vector<GaTestResult>> pmpcGaTest() {
		int popSize = 20;
		double step = 0.1;
		double pm = 0.0;
		int sectionNumber = ceil(1.0 / step);
		vector<GaTestResult> time;
		vector<GaTestResult> error;
		for (int i = 0; i <= sectionNumber; i++) {
			pm = i * step;
			pair<vector<GaTestResult>, vector<GaTestResult>> metrings = this->testWithChangebleProb(popSize, pm, true);
			time.insert(time.end(), metrings.first.begin(), metrings.first.end());
			error.insert(error.end(), metrings.second.begin(), metrings.second.end());
		}
		return make_pair(time, error);
	}
	// тестирование алгоритма с перебором 
	// размера популяции
	void nGATest() {
		double pm = 0.2;
		double pc = 0.3;
		int maxN = 50;
		cout << "pm = " << pm << " pc = " << pc << endl;
		vector<double> time;
		vector<double> error;
		for (int popSize = 1; popSize < maxN; popSize++) {
			pair<double, double> metering = this->calculateTimeErrorValue(popSize, pm, pc);
			time.push_back(metering.first);
			error.push_back(metering.second);
			cout << "(" << popSize << ", " << metering.second << ")" << endl;
		}
		for (int i = 0; i < time.size(); i++) {
			cout << "(" << i + 1 << ", " << time[i] << ")" << endl;
		}
	}
};
// класс, отвечающий сохранение и вывод
// результатов исследований
class Representation {
public:
	void saveLatexTable(string fileName, vector<GaTestResult> data) {
		fileName += ".latexTbl";
		string rowEnd = "\\\\";
		string columnSep = " & ";

		vector<double> pc;
		vector<double> pm;
					
		for (GaTestResult testRes : data)
		{
			pc.push_back(testRes.pc);
			pm.push_back(testRes.pm);
		}
		
		sort(pc.begin(), pc.end());
		sort(pm.begin(), pm.end());
		
		auto endPc = unique(pc.begin(), pc.end());
		auto endPm = unique(pm.begin(), pm.end());

		pc.erase(endPc, pc.end());
		pm.erase(endPm, pm.end());


		ofstream out(fileName);
	
		out << "pm/pc" << columnSep;
		for (int i = 0; i < pc.size(); i++) {
			out << pc[i];
			if (i != pc.size() - 1) {
				out << columnSep;
			}
		}
		out << rowEnd << endl;

		double lastPm = data[0].pm;
		for (int i = 0; i < data.size(); i++) {
			double pm = data[i].pm;
			if (lastPm != pm) {
				lastPm = pm;
				out << rowEnd << endl;
				out << pm;
			}
			else {
				out << columnSep << data[i].functionValue;
			}
		}
		cout << rowEnd << endl;
	}
	string createFileName(vector<string> params) {
		string result = "";
		for (int i = 0; i < params.size(); i++) {
			result += params[i] + "_";
		}
		return result;
	}
	void saveInSimpleList(string fileName, vector<GaTestResult> data) {
		fileName += ".txt";
		ofstream out(fileName);
		for (GaTestResult testRes : data)
		{
			out << testRes.pm << " " << testRes.pc << " " << testRes.functionValue << endl;
		}
	}
};

int main() {
	map<string, string> config;
	ifstream in("config.txt");
	string s;
	while (getline(in, s)) {
		stringstream ss(s);
		string key, value;
		ss >> key >> value;
		config[key] = value;
	}

	Repository repo;
	string configGraphType = config["GRAPH_TYPE"];
	GraphDescription graphDescription = repo.getGraph(configGraphType);

	Experiment experiment(graphDescription);
	string test = config["TEST_TYPE"];

	cout << test << endl;
	cout << configGraphType << endl;

	Representation fileRepresent;
	if (test == "FIXED_PM_PC_OUR_GA_TEST") {
		pair<vector<GaTestResult>, vector<GaTestResult>>
			metrings = experiment.oneDimentionFixedGATest();
	}
	else if (test == "PM_PC_OUR_GA_TEST") {
		string popSize = "50";
		pair<vector<GaTestResult>, vector<GaTestResult>>
			metrigs = experiment.pmpcGaTest();
		string n = to_string(graphDescription.n);
		string m = to_string(graphDescription.m);

		vector<string> timeFileNameSections = { "TIME", configGraphType, n, m, popSize };
		vector<string> errorFileNameSections = { "ERROR", configGraphType, n, m, popSize };

		string timeFile = fileRepresent.createFileName(timeFileNameSections);
		string errorFile = fileRepresent.createFileName(errorFileNameSections);

		fileRepresent.saveInSimpleList(timeFile, metrigs.first);
		fileRepresent.saveInSimpleList(errorFile, metrigs.second);

		fileRepresent.saveLatexTable(timeFile, metrigs.first);
		fileRepresent.saveLatexTable(errorFile, metrigs.second);
	}
	else if (test == "N_OUR_GA_TEST") {
		experiment.nGATest();
	}
	else if (test == "SIMPLE_N4N_TEST") {
		experiment.simpleNANTest();
	}
	else if (test == "SIMPLE_TIME_ERROR_TEST") {
		double pm = 0.1;
		double pc = 0.7;
		int popSize = 20;
		pair<double, double> res = experiment.simpleTimeErrorTest(pm, pc, popSize);
		cout << "Time = " << res.first << endl;
		cout << "Error = " << res.second << endl;
	}
		
	cout << "Success" << endl;
}