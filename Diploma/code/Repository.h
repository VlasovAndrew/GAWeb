#pragma once

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

using std::make_pair;
using std::ifstream;
using std::stringstream;

using std::pair;


class Repository {
private:
	struct GraphParams
	{
		string fileName;
		int realR;
		GraphParams(string fileN, int r) {
			this->fileName = fileN;
			this->realR = r;
		}
	};

	vector<int> RADIUS_BA = { 4, 4, 5, 4, 5, 5, 5 };
	vector<int> RADIUS_GEOM = { 9, 9, 8, 8, 8, 8, 8 };
	vector<int> RADIUS_ER = { 5, 4, 4, 4, 3, 3, 3 };
	int TEST = 4;

	vector<string> BA_FILE = {
		"BA_Graph\\BarabasiAlbertGraph1_M2.txt",
		"BA_Graph\\BarabasiAlbertGraph2_M2.txt",
		"BA_Graph\\BarabasiAlbertGraph3_M2.txt",
		"BA_Graph\\BarabasiAlbertGraph4_M2.txt",
		"BA_Graph\\BarabasiAlbertGraph5_M2.txt",
		"BA_Graph\\BarabasiAlbertGraph6_M2.txt",
		"BA_Graph\\BarabasiAlbertGraph7_M2.txt" };

	vector<string> GEOM_FILE = {
		"GEOM_Graph\\GeometricGraph1_R01.txt",
		"GEOM_Graph\\GeometricGraph2_R01.txt",
		"GEOM_Graph\\GeometricGraph3_R01.txt",
		"GEOM_Graph\\GeometricGraph4_R01.txt",
		"GEOM_Graph\\GeometricGraph5_R01.txt",
		"GEOM_Graph\\GeometricGraph6_R01.txt",
		"GEOM_Graph\\GeometricGraph7_R01.txt" };

	vector<string> ER_FILE = {
		"ERDOSRENYI_Graph\\ErdosRenyi1_P001.txt",
		"ERDOSRENYI_Graph\\ErdosRenyi2_P001.txt",
		"ERDOSRENYI_Graph\\ErdosRenyi3_P001.txt",
		"ERDOSRENYI_Graph\\ErdosRenyi4_P001.txt",
		"ERDOSRENYI_Graph\\ErdosRenyi5_P001.txt",
		"ERDOSRENYI_Graph\\ErdosRenyi6_P001.txt",
		"ERDOSRENYI_Graph\\ErdosRenyi7_P001.txt" };

	GraphParams getGraphData(string graphType) {
		string fileName;
		int real_r = 0;
		if (graphType == "BA_GRAPH") {
			fileName = BA_FILE[TEST];
			real_r = RADIUS_BA[TEST];
			cout << "BA_GRAPH" << endl;
		}
		else if (graphType == "GEOM_GRAPH") {
			fileName = GEOM_FILE[TEST];
			real_r = RADIUS_GEOM[TEST];
			cout << "GEOM_GRAPH" << endl;
		}
		else if (graphType == "ERDOSH_GRAPH") {
			fileName = ER_FILE[TEST];
			real_r = RADIUS_ER[TEST];
			cout << "ERDOSH_GRAPH" << endl;
		}
		return GraphParams(fileName, real_r);
	}


public:
	GraphDescription getGraph(string graphType) {
		GraphParams params = this->getGraphData(graphType);
		int n, m;
		edges e = readFromFileNM(params.fileName, n, m);

		cout << "N = " << n << " M = " << m << endl;
		return GraphDescription(n, m, e, params.realR);
	}
};