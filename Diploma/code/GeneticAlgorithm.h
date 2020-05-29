﻿#pragma once

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

// ����� ��� ������ � ������������ ����������
class GeneticAlgorithm {
private:
	// ������ �� ����
	Graph* G;
	// ������ ���������
	int populationSize;
	// ���������, ���������� �� ������� � �����������
	double mutationP, crossP;
	// ������ � ������� ������ � ���������
	vector<int> population;
	// ������ ���������� ��� ������ � ���������� ������ ��������� �������
	Random rd;
	// ����� ���������� �� ����� ���������,
	// ��������� ������ �� ���������� ��� ������ ������� ������ ���������.
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
	// ����������� ������, ��������� ������ �� ����, ������ ���������,
	// ����������� ����������� � ����������� �������
	GeneticAlgorithm(Graph* g, int popSize, double pC, double pM) {
		this->G = g;
		this->populationSize = popSize;
		this->crossP = pC;
		this->mutationP = pM;
		// ��������� ��������� ���������
		for (int i = 0; i < popSize; i++) {
			population.push_back(rand() % g->Size());
		}
	}

	// ���� ��������
	void makeSelection() {
		// ����� ���������������� ���� ������ � ���������
		vector<int> e;
		for (int i = 0; i < population.size(); i++) {
			e.push_back(G->getEccentricity(population[i]));
		}
		// ���������� ����������� ��� ��������� � ��������� ���������
		// ���������� �� ����� �������, ����� ����������� ���� ���� 
		// � ������ � ������� ����������������
		vector<double> probability;
		double maxV = *max_element(e.begin(), e.end()), leftSum = 0;
		for (int i = 0; i < e.size(); i++) {
			leftSum += maxV / double(e[i]);
		}
		double x = 1.0 / leftSum;
		for (int i = 0; i < e.size(); i++) {
			probability.push_back(maxV / double(e[i]) * x);
		}
		// ���� �������� �� ������ ����������� ������������
		vector<int> nextPopulation;
		for (int i = 0; i < this->populationSize; i++) {
			nextPopulation.push_back(rd.choice(population, probability));
		}
		this->population = nextPopulation;
	}
	// ���� �����������
	void crossing() {
		// ������ ��� ���������� �����������
		vector<int> crossed;
		for (int i = 0; i < populationSize; i++) {
			// ����������� ���������� � �������� ������������ 
			if (rd.getRandomLowerOne() < crossP) {
				// ����� �������� ������ �� ���������
				int ind1 = rd.randint(0, populationSize - 1);
				int ind2 = rd.randint(0, populationSize - 1);
				// ��������� ����� ������
				int u = population[ind1], v = population[ind2];
				// ����� ���������� ����� ����� ���������
				vector<int> path = G->getPath(u, v);
				// ����� ������� �� �������� ����
				crossed.push_back(path[path.size() / 2]);
			}
		}
		// ���������� �������� � ���������
		this->population.insert(population.end(), crossed.begin(), crossed.end());
	}
	// ������� �������
	void mutation() {
		for (unsigned int i = 0; i < this->population.size(); i++) {
			// � ������ ����������� �������
			if (rd.getRandomLowerOne() < mutationP) {
				// ����� ������� 
				vector<int> n = G->getNeighbour(population[i]);
				// ����� ������ �� �������
				population[i] = rd.choice(n);	
			}
		}
	}
	// ����� ���������
	void printPopulation() {
		for (int i = 0; i < this->population.size(); i++) {
			cout << population[i] << " ";
		}
		cout << endl;
	}
	// ����� ��� ������������ ��������
	void evolutionStep() {
		this->crossing();
		this->makeSelection();
		this->mutation();
		#ifdef PRINT_GA
			this->printPopulation();
		#endif
	}
	// ����� ������� � ����������� ���������������� ����� ������ ���������
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
	// ��������� ���������
	vector<int> getPopulation() {
		return this->population;
	}
};
