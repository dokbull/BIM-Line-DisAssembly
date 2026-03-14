using System;

class MathUtil
{
    /// <summary>
    /// 기울기 값을 구함
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="y1"></param>
    /// <param name="y2"></param>
    /// <returns></returns>
    public static double slope(double x1, double x2, double y1, double y2)
    {
        double result = (y2-y1)/(x2-x1);
        return result;
    }

    /// <summary>
    /// y 절편 값을 구함
    /// </summary>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="slope"></param>
    /// <returns></returns>
    public static double yIntercept(double y2, double x2, double slope)
    {
        double result = y2 - (slope * x2);
        return result;
    }

    public static double y(double x, double slope, double yIntercept)
    {
        return slope * x + yIntercept;
    }

    /// <summary>
    /// 등가속도 운동시 이동 거리 계산
    /// </summary>
    /// <param name="startSpeed"></param>
    /// <param name="accSpeeed"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static double moveLength(double startSpeed, double accSpeeed, double time)
    {
        // S = Vo t + ½ at ²( s ; 거리   Vo; 처음 속도   a ; 가속도    t ; 시간  )

        double factor1 = startSpeed * time;
        double factor2 = 0.5 * accSpeeed * Math.Pow(time, 2);

        return factor1 + factor2;
    }

    public static double distance(double x1, double y1, double x2, double y2)
    {
        double xx = x2 - x1;
        double yy = y2 - y1;

        return Math.Sqrt(xx * xx + yy * yy);
    }

    public static bool AABB(CRect src, CRect dst)
    {
        if (src.x > dst.x + dst.w)
            return false;

        if (src.x + src.w < dst.x)
            return false;

        if (src.y > dst.y + dst.h)
            return false;

        if (src.y + src.h < dst.y)
            return false;

        return true;
    }

    public static bool AABB(double x1, double y1, double w1, double h1,
        double x2, double y2, double w2, double h2)
    {
        CRect src = new CRect((float)x1, (float)y1, (float)w1, (float)h1);
        CRect dst = new CRect((float)x2, (float)y2, (float)w2, (float)h2);

        return AABB(src, dst);
    }
}