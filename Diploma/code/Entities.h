﻿#pragma once
#include <vector>

using std::vector;
using std::pair;

using edges = vector<pair<int, int>>;
// ��������, ���������� 
// �� �������� ���������� � ������ ����� ��������
enum GraphType
{
	None = 0,
	ER = 1,
	BA = 2,
	GEOM = 3,
};
// �������� ����� � ���� ������ ������, 
// �������� � ������� �����
class GraphDescription {
public:
	int n, m;
	edges e;
	int realR;
	GraphDescription() {}

	GraphDescription(int n, int m, edges e, int realR) {
		this->n = n;
		this->m = m;
		this->e = e;
		this->realR = realR;
	}
};

// �������� ���������� ��������������� �����
// ��������� ������������� ��������� � �������� �������
class GaTestResult {
public:
	GaTestResult() {}

	GaTestResult(double popSize, double pm, double pc, double functionValue) {
		this->pm = pm;
		this->pc = pc;
		this->popSize = popSize;
		this->functionValue = functionValue;
	}

	double pc, pm;
	int popSize;
	double functionValue;
};