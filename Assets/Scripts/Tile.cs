using UnityEngine;
using System.Collections;

public class Tile
{
    public Point Location { get; private set; }
    public int Cost { get; private set; }
    public bool IsPassable
    {
        get { return Cost > 0; }
    }

    public Tile(Point point, int cost)
    {
        Location = point;
        Cost = cost;
    }
}
