﻿// ����� ���������� �� ���������� ������������
class Experiment {
private:
	// ����� �������� ��������
	const int ITER = 100;
	// ��������� �����
	GraphDescription graph;
	// ������� ��� ���������� ������� ������ 
	// ��������� � ��������� �����������
	pair<double, double> calculateTimeErrorValue(int popSize, double pm, double pc) {
		// ����� ������ � ������� ����� ������
		int error = 0;
		double sum_time = 0.0;
		// ����������� ���������� ������
		for (int i = 0; i < ITER; i++) {
			// ������������� �����
			Graph* g1 = new Graph(graph.n, graph.m, graph.e);
			// ������������� ������������� ���������
			GeneticAlgorithm genAlg(g1, popSize, pc, pm);
			// ����� ������� ������ ���������
			double time = 0.0;
			// ��������� ���������� �������
			int R = genAlg.getBestResult(time);
			sum_time += time;
			// �������� �� ����� ��������� ������
			if (R != graph.realR) {
				error++;
			}
			delete g1;
		}
		// ���������� �������� ������� ������ � �������� ������
		double avg_time = sum_time / double(ITER);
		double avg_error = error / double(ITER) * 100.0;
		return make_pair(avg_time, avg_error);
	}
	// ������ ��������� � ����� �� ������������ ����������
	// popSize - ������ ���������, probParam - �������� ������ �� ����������
	// pmProb - ����, ���������� �� �� ����� �� ���������� ������� � �������� ��������������
	// true - �������� pm, false - �������� pc
	pair<vector<GaTestResult>, vector<GaTestResult>> testWithChangebleProb(int popSize, double probParam, bool pmProb) {
		// ���, � ������� ����� ������������ ��������
		double step = 0.1;
		// ����� �������� ��� �������� ����������
		int itarationCount = ceil(1.0 / step);
		// ����� ���������� � ����������
		cout << "popSize = " << popSize;
		if (pmProb) {
		   cout << " pm = " << probParam << endl;
		}
		else {
			cout << " pc = " << probParam << endl;
 		}
		// ����� ����� ��� ������ 
		if (pmProb) {
			cout << "pc \t AVG_TIME \t ERROR" << endl;
		}
		else {
			cout << "pm \t AVG_TIME \t ERROR" << endl;
		}
		// ���������� ��� �������� ����������� ���������
		vector<GaTestResult> time;
		vector<GaTestResult> error;
		// ������� ������ �� ����������
		for (int i = 0; i <= itarationCount; i++) {
			double pm, pc;
			// � ����������� �� ����� pmProb ������������ ��� �������� pm ��� pc
			if (pmProb) {
				pm = probParam;
				pc = i * step;
			}
			else {
				pc = probParam;
				pm = i * step;
			}
			// ���������� �������� ������� ������ � �������� ������
			pair<double, double> metering = this->calculateTimeErrorValue(popSize, pm, pc);
			double avg_time = metering.first;
			double error_percent = metering.second;
			// ����� ���������� � ���������� �����������
			cout << (pmProb ? pc : pm) << "\t" << avg_time << "\t" << error_percent << endl;
			time.push_back(GaTestResult(popSize, pm, pc, avg_time));
			error.push_back(GaTestResult(popSize, pm, pc, error_percent));
		}
		// ������� ��������� �����������
		return make_pair(time, error);
	}

public:
	Experiment(GraphDescription graph) {
		srand(time(NULL) % INT_MAX);
		this->graph = graph;
	}
	// ���� � ������������ ���������� pc
	pair<vector<GaTestResult>, vector<GaTestResult>> oneDimentionFixedGATest() {
		// ��������� ��������� ��� �����
		int populationN = 20;
		double pm = 0.4;
		return this->testWithChangebleProb(populationN, pm, true);
	}
	// ������ �������� ����� � ����������� �����������	
	pair<double, double> simpleTimeErrorTest(double pm, double pc, int popSize) {
		return this->calculateTimeErrorValue(popSize, pm, pc);
	}
	// ������ ��������� N4N
	void simpleNANTest() {
		// �������� ���������� - ���������� ������� 
		// ����� ������ � ������� ������
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
	// ������������ ��������� � ��������� ���������� pm � pc 
	pair<vector<GaTestResult>, vector<GaTestResult>> pmpcGaTest() {
		// ������ ���������
		int popSize = 20;
		// ��� ��� ����������
		double step = 0.1;
		// �������� ��������
		double pm = 0.0;
		// ����� ��������
		int sectionNumber = ceil(1.0 / step);
		// ���������� ��������� �������
		vector<GaTestResult> time;
		// ���������� ��������� ������
		vector<GaTestResult> error;
		// ������� ����������
		for (int i = 0; i <= sectionNumber; i++) {
			pm = i * step;
			// ��������� ����������� ������ �� ������� ���������
			pair<vector<GaTestResult>, vector<GaTestResult>> metrings = this->testWithChangebleProb(popSize, pm, true);
			// ���������� ����������� ������
			time.insert(time.end(), metrings.first.begin(), metrings.first.end());
			error.insert(error.end(), metrings.second.begin(), metrings.second.end());
		}
		return make_pair(time, error);
	}
	// ������������ ��������� � ��������� 
	// ������� ���������
	void nGATest() {
		// ��������� �������� ������������
		double pm = 0.2;
		double pc = 0.3;
		// ������������ �������� ��� ������� ���������
		int maxN = 50;
		// ����� ���������� � ����������
		cout << "pm = " << pm << " pc = " << pc << endl;
		vector<double> time;
		vector<double> error;
		// ������� �������� ���������
		for (int popSize = 1; popSize < maxN; popSize++) {
			// ���������� �������� � ��������� ������
			pair<double, double> metering = this->calculateTimeErrorValue(popSize, pm, pc);
			time.push_back(metering.first);
			error.push_back(metering.second);
			// ����� ���������� �� �����
			cout << "(" << popSize << ", " << metering.second << ")" << endl;
		}
		// ����� ���������� � ������� ������
		for (int i = 0; i < time.size(); i++) {
			cout << "(" << i + 1 << ", " << time[i] << ")" << endl;
		}
	}
};
