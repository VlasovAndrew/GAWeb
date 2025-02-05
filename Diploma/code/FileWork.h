// ����� ��� ������ ����� �� �����
vector<pair<int, int>> readFromFileWhereEdges(string file, int& N, int& M) {
	// �������� ��������� ������
	ifstream in(file);
	// ����� �����
	vector<pair<int, int>> e;
	string inputLine;
	int n = 0, m = 0;
	// ���������� ������ �����
	while (getline(in, inputLine)) {
		if (inputLine[0] == '%') {
			continue;
		}
		// ���������� �������� �����
		m++;
		stringstream s(inputLine);
		// ������ ������
		int x, y;
		s >> x >> y;
		// ���������� ������� �����
		n = max(n, max(x, y));
		// ������� � ���� ����������
		x--, y--;
		e.push_back(make_pair(x, y));
	}
	N = n;
	M = m;
	return e;
}
// ����� ��� ������������ ������
// ����� � ������, � ������� �� ������ ������ ������� ������� �����
vector<pair<int, int>> readFromFileNM(string file, int& N, int& M) {
	ifstream in(file);
	vector<pair<int, int>> e;
	int n, m;
	in >> n >> m;
	for (int i = 0; i < m; i++) {
		int x, y;
		in >> x >> y;
		e.push_back(make_pair(x, y));
	}
	N = n;
	M = m;
	return e;
}