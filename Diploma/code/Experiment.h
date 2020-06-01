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
	// тестирование алгоритма с перебором параметров pm и pc 
	pair<vector<GaTestResult>, vector<GaTestResult>> pmpcGaTest() {
		// размер популяции
		int popSize = 20;
		// шаг для параметров
		double step = 0.1;
		// началные значения
		double pm = 0.0;
		// число итераций
		int sectionNumber = ceil(1.0 / step);
		// результаты измерений времени
		vector<GaTestResult> time;
		// результаты измерений ошибок
		vector<GaTestResult> error;
		// перебор параметров
		for (int i = 0; i <= sectionNumber; i++) {
			pm = i * step;
			// получение результатов тестов по второму параметру
			pair<vector<GaTestResult>, vector<GaTestResult>> metrings = this->testWithChangebleProb(popSize, pm, true);
			// добавление результатов тестов
			time.insert(time.end(), metrings.first.begin(), metrings.first.end());
			error.insert(error.end(), metrings.second.begin(), metrings.second.end());
		}
		return make_pair(time, error);
	}
	// тестирование алгоритма с перебором 
	// размера популяции
	void nGATest() {
		// начальные значения вероятностей
		double pm = 0.2;
		double pc = 0.3;
		// максимальное значение для размера популяции
		int maxN = 50;
		// вывод информации о параметрах
		cout << "pm = " << pm << " pc = " << pc << endl;
		vector<double> time;
		vector<double> error;
		// перебор размеров популяции
		for (int popSize = 1; popSize < maxN; popSize++) {
			// вычисление размеров и временных затрат
			pair<double, double> metering = this->calculateTimeErrorValue(popSize, pm, pc);
			time.push_back(metering.first);
			error.push_back(metering.second);
			// вывод информации на экран
			cout << "(" << popSize << ", " << metering.second << ")" << endl;
		}
		// вывод информации о времени работы
		for (int i = 0; i < time.size(); i++) {
			cout << "(" << i + 1 << ", " << time[i] << ")" << endl;
		}
	}
};
