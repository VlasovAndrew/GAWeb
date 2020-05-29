#pragma once

#include <vector>
#include <algorithm>
#include <set>
#include <ctime>

#include "GraphWork.h"
#include "Random.h"
#include "Strassen.h"
#include "VectorOutput.h"

using std::begin;
using std::end;
using std::max_element;
using std::min_element;
using std::pair;
using std::vector;
using std::set;

//  ласс дл€ работы с генетическим алгоритмом
class GeneticAlgorithm {
private:
	// —сылка на граф
	Graph* G;
	// –азмер попул€ции
	int populationSize;
	// ѕараметры, отвечающие за мутацию и скрещивание
	double mutationP, crossP;
	// ¬ектор с набором вершин в попул€ции
	vector<int> population;
	// ћодуль декотратор дл€ работы с генерацией псевдо случайных величин
	Random rd;
	// ћетод отвечающий за старт алгоритма,
	// принимает ссылку на переменную дл€ записи времени работы алгоритма.
	void startAlgorithm(double& time) {
		int stepN = 20;
		double start = clock();
		for (int i = 0; i < stepN; i++) {
			this->evolutionStep();
		}
		double finish = clock();
		string message = "Time of genetic algorithm: ";
		time = (finish - start) / CLOCKS_PER_SEC;
		#ifdef PRINT_TIME
			cout << "" << (finish - start) / CLOCKS_PER_SEC << endl;
		#endif
	}

public:
	//  онструктор класса, принимает ссылку на граф, размер попул€ции,
	// веро€тность скрещивани€ и веро€тность мутации
	GeneticAlgorithm(Graph* g, int popSize, double pC, double pM) {
		this->G = g;
		this->populationSize = popSize;
		this->crossP = pC;
		this->mutationP = pM;
		// √енераци€ начальной попул€ции
		for (int i = 0; i < popSize; i++) {
			population.push_back(rand() % g->Size());
		}
	}

	// Ётап селекции
	void makeSelection() {
		// ѕоиск эксцентриситетов всех вершин в попул€ции
		vector<int> e;
		for (int i = 0; i < population.size(); i++) {
			e.push_back(G->getEccentricity(population[i]));
		}
		// вычисление веро€тности дл€ попадани€ в следующее поколение
		// вычисление ее таким образом, чтобы веро€тность была выше 
		// у вершин с меньшим эксцентриситетом
		vector<double> probability;
		double maxV = *max_element(e.begin(), e.end()), leftSum = 0;
		for (int i = 0; i < e.size(); i++) {
			leftSum += maxV / double(e[i]);
		}
		double x = 1.0 / leftSum;
		for (int i = 0; i < e.size(); i++) {
			probability.push_back(maxV / double(e[i]) * x);
		}
		// этап селекции на основе вычисленных веро€тностей
		vector<int> nextPopulation;
		for (int i = 0; i < this->populationSize; i++) {
			nextPopulation.push_back(rd.choice(population, probability));
		}
		this->population = nextPopulation;
	}
	// этап скрещивани€
	void crossing() {
		// вектор дл€ результата скрещивани€
		vector<int> crossed;
		for (int i = 0; i < populationSize; i++) {
			// скрещивание происходит с заданной веро€тностью 
			if (rd.getRandomLowerOne() < crossP) {
				// выбор индексов вершин из попул€ции
				int ind1 = rd.randint(0, populationSize - 1);
				int ind2 = rd.randint(0, populationSize - 1);
				// получение самих вершин
				int u = population[ind1], v = population[ind2];
				// поиск кротчайших путей между вершинами
				vector<int> path = G->getPath(u, v);
				// выбор вершины из середины пути
				crossed.push_back(path[path.size() / 2]);
			}
		}
		// добавление потомков в попул€цию
		this->population.insert(population.end(), crossed.begin(), crossed.end());
	}
	// процесс мутации
	void mutation() {
		for (unsigned int i = 0; i < this->population.size(); i++) {
			// с учетом веро€тности мутации
			if (rd.getRandomLowerOne() < mutationP) {
				// поиск соседей 
				vector<int> n = G->getNeighbour(population[i]);
				// выбор одного из соседей
				population[i] = rd.choice(n);	
			}
		}
	}
	// вывод попул€ции
	void printPopulation() {
		for (int i = 0; i < this->population.size(); i++) {
			cout << population[i] << " ";
		}
		cout << endl;
	}
	// метод дл€ эволюционной итерации
	void evolutionStep() {
		this->crossing();
		this->makeSelection();
		this->mutation();
		#ifdef PRINT_GA
			this->printPopulation();
		#endif
	}
	// поиск вершины с минимальным эксцентриситетом после работы алгоритма
	int getBestResult(double& time) {
		this->startAlgorithm(time);
		vector<int> e;
		for (int i = 0; i < population.size(); i++) {
			int x = G->getEccentricity(population[i]);
			e.push_back(x);
		}
	 	vector<int> ind = getCentralVertex(e);
		#ifdef PRINT_GA
			this->printPopulation();
		#endif
		return this->G->getEccentricity(population[ind[0]]);
	}
	// получение попул€ции
	vector<int> getPopulation() {
		return this->population;
	}
};
