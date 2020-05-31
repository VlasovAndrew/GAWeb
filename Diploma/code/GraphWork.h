﻿#pragma once
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
	// ������� ���������
	vector<vector<int>> adjacency;
	// ������� ��� ���������� ����� ���������
	vector<vector<int>> distance;
	// ��������� ���������� ����� ���������
	vector<vector<vector<int>>> path;
	// ����� �� ��������� ���������������
	vector<bool> calculatedEccentricity;
	vector<int> eccentricity;
	
	void initializeMatrix() {
		// ��������� �������������� ���� ������
		// ������� ���������
		adjacency.resize(this->N, vector<int>());
		// ������� n x n ��� ����������
		distance.resize(this->N, vector<int>(this->N, INT_MAX));
		// ������� n x n ��� ����� 
		path.resize(this->N, vector<vector<int>>(this->N));
		// ����� ��� ����������� ��� �� ������ �������������� ���� �������
		calculatedEccentricity.resize(this->N, false);
		// ������ ��� �������� ��������� ����������������
		eccentricity.resize(this->N, INT_MAX);
		// �������������� ������
		for (int i = 0; i < this->N; i++) {
			this->distance[i][i] = 0;
			this->path[i][i] = vector<int>(1, i);
		}
	}

public:
	Graph(int n, int m, vector<pair<int, int>> adj) {
		this->N = n, this->M = m;
		initializeMatrix();
		// �������� ������ ��������� ��� �������� ��� �����
		for (int i = 0; i < m; i++) {
			int x = adj[i].first, y = adj[i].second;
			this->adjacency[x].push_back(y);
			this->adjacency[y].push_back(x);
		}
	}

	// ������ ������ � ������ �� ���������� �������
	void bfsFromVertex(int x) {
		// ���������� ������������� ��������� ������ � ������
		// ������� ������
		queue<int> q;
		// ���������� ��������� �������
		q.push(x);
		// ���������� �� ��������� ������� �� ���� ���������
		vector<int> d(this->N, INT_MAX);
		// ��������� ������������� ����������
		d[x] = 0;
		// ����� �� ��� ���, ���� ������� �� �����
		while (!q.empty()) {
			// ���������� �� �������
			int u = q.front();
			q.pop();
			// ��������� ���� �� ������ �� ��������������� �������
			vector<int> currentPath = this->path[x][u];
			// �������� ���� �������� ������
			for (unsigned int i = 0; i < this->adjacency[u].size(); i++) {
				// ���������� �������� �������
				int v = this->adjacency[u][i];
				// ���� ������� �� ���� ��������, �� 
				// ��� ����������� � �������
				if (d[v] > INT_MAX / 2) {
					// ���������� ������� � ������� ����
					currentPath.push_back(v);
					// ���������� ���������� ����
					this->path[x][v] = currentPath;
					this->path[v][x] = currentPath;
					currentPath.pop_back();
					// ���������� ���������� ����
					d[v] = d[u] + 1;
					// ���������� ������� � �������
					q.push(v);
				}
			}
		}
		// ���������� ��������� ����������
		for (unsigned int i = 0; i < d.size(); i++) {
			this->distance[x][i] = d[i];
			this->distance[i][x] = d[i];
		}
	}
	// ��������� ���� ����� ���������,
	// ���� ���� ��� ����������, �� 
	// ����������� ����� � ������
	vector<int> getPath(int x, int y) {
		if (this->path[x][y].empty()) {
			this->bfsFromVertex(x);
		}
		return this->path[x][y];
	}
	// ��������� ��������������� �������
	int getEccentricity(int x) {
		if (x >= this->N) {
			throw exception("Vertex doesn't exist in graph");
		}
		// ��������, ��� �������������� ��� ������ �����
		if (!this->calculatedEccentricity[x]) {
			// ������ ������
			this->bfsFromVertex(x);
			// ������� �������, ��� �� �������������� ������
			this->calculatedEccentricity[x] = true;
			int result = -INT_MAX;
			// ����� ����� ��������� �������
			for (int i = 0; i < this->N; i++) {
				result = max(this->distance[x][i], result);
			}
			this->eccentricity[x] = result;
		}
		return this->eccentricity[x];
	}
	// ��������� ������� �������� �������
	vector<int> getNeighbour(int x) {
		return this->adjacency[x];
	}
	// ��������� �������� �����
	int Size() {
		return this->N;
	}
};



