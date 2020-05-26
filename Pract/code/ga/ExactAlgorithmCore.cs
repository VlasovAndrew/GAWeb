namespace GA
{
    public class ExactAlgorithmCore
    {
        // ���������� ������������ ���������.
        public int FindRadius(GraphContext graphContext) {
            int R = int.MaxValue;
            for (int v = 0; v < graphContext.N; v++) {
                R = Math.Min(R, graphContext.GetEccentricity(v));
            }
            return R;
        }
    }
}
