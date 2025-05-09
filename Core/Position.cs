﻿namespace Core;

public class Position(double x, double y)
{
    public double X { get; set; } = x;
    public double Y { get; set; } = y;

    public double Distance(Position p) => Distance(this, p);

    public static double Distance(Position a, Position b) => Math.Sqrt((b.X - a.X)*(b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y));

    public override string ToString() => $"({X}, {Y})";
}