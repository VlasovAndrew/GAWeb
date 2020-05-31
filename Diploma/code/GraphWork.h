#pragma once
#include <vector>
#include <queue>
#include <iostream>
#include <algorithm>
#include <stdio.h>
#include <string>

using std::vector;
using std::queue;
using std::pair;
using std::exception;
using std::string;
using std::to_string;
using std::cin;
using std::cout;
using std::endl;

using std::max;

class Graph {
private:
	int N, M;
	// матрица смежности
	vector<vector<int>> adjacency;
	// матрица для расстояний между вершинами
	vector<vector<int>> distance;
	// найденные расстояния между вершинами
	vector<vector<vector<int>>> path;
	// флаги на найденные эксцентриситеты
	vector<bool> calculatedEccentricity;
	vector<int> eccentricity;
	
	void initializeMatrix() {
		// начальная инициальизация всех матриц
		// матрица смежности
		adjacency.resize(this->N, vector<int>());
		// матрица n x n для расстояний
		distance.resize(this->N, vector<int>(this->N, INT_MAX));
		// матрица n x n для путей 
		path.resize(this->N, vector<vector<int>>(this->N));
		// флаги для отображения был ли найден эксцентриситет этой вершины
		calculatedEccentricity.resize(this->N, false);
		// вектор для хранения найденных эксцентриситетов
		eccentricity.resize(this->N, INT_MAX);
		// инициализвация матриц
		for (int i = 0; i < this->N; i++) {
			this->distance[i][i] = 0;
			this->path[i][i] = vector<int>(1, i);
		}
	}

public:
	Graph(int n, int m, vector<pair<int, int>> adj) {
		this->N = n, this->M = m;
		initializeMatrix();
		// создание списка смежности для хранения для графа
		for (int i = 0; i < m; i++) {
			int x = adj[i].first, y = adj[i].second;
			this->adjacency[x].push_back(y);
			this->adjacency[y].push_back(x);
		}
	}

	// запуск обходя в ширину из переданной вершины
	void bfsFromVertex(int x) {
		// реализация классического алгоритма поиска в ширину
		// очередь обхода
		queue<int> q;
		// добавление стартовой вершины
		q.push(x);
		// расстояния от стартовой вершины до всех остальных
		vector<int> d(this->N, INT_MAX);
		// начальная инициализация расстояния
		d[x] = 0;
		// обход до тех пор, пока очередь не пуста
		while (!q.empty()) {
			// извлечение из очереди
			int u = q.front();
			q.pop();
			// получение пути от старта до рассматриваемой вершины
			vector<int> currentPath = this->path[x][u];
			// просмотр всех соседних вершин
			for (unsigned int i = 0; i < this->adjacency[u].size(); i++) {
				// извлечение соседней вершины
				int v = this->adjacency[u][i];
				// если вершина не была посещена, то 
				// она добавляется в очередь
				if (d[v] > INT_MAX / 2) {
					// добавление вершины в текущий путь
					currentPath.push_back(v);
					// сохранение найденного пути
					this->path[x][v] = currentPath;
					this->path[v][x] = currentPath;
					currentPath.pop_back();
					// увеличение найденного пути
					d[v] = d[u] + 1;
					// добавление вершины в очередь
					q.push(v);
				}
			}
		}
		// сохранение найденных расстояний
		for (unsigned int i = 0; i < d.size(); i++) {
			this->distance[x][i] = d[i];
			this->distance[i][x] = d[i];
		}
	}
	// получение пути между вершинами,
	// если путь еще неизвестен, то 
	// запускается обход в ширину
	vector<int> getPath(int x, int y) {
		if (this->path[x][y].empty()) {
			this->bfsFromVertex(x);
		}
		return this->path[x][y];
	}
	// получение эксцентриситета вершины
	int getEccentricity(int x) {
		if (x >= this->N) {
			throw exception("Vertex doesn't exist in graph");
		}
		// проверка, что эксцентриситет был найден ранее
		if (!this->calculatedEccentricity[x]) {
			// запуск обхода
			this->bfsFromVertex(x);
			// пометка вершины, что ее эксцентриситет найден
			this->calculatedEccentricity[x] = true;
			int result = -INT_MAX;
			// поиск самой удаленной вершины
			for (int i = 0; i < this->N; i++) {
				result = max(this->distance[x][i], result);
			}
			this->eccentricity[x] = result;
		}
		return this->eccentricity[x];
	}
	// получение соседей заданной вершины
	vector<int> getNeighbour(int x) {
		return this->adjacency[x];
	}
	// получение размеров графа
	int Size() {
		return this->N;
	}
};



