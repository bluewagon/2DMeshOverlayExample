using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Map : MonoBehaviour
{
    public int NumberTilesWide;
    public int NumberTilesHeight;
    //public GameObject Overlay;
    public Grid overlayGrid;

    private List<List<Tile>> grid;
    public Tile this[int x, int y]
    {
        get { return grid[y][x]; }
    }

    public static readonly Point[] DIRS = new[]
    {
        new Point(1, 0),
        new Point(0, -1),
        new Point(-1, 0),
        new Point(0, 1)
    };

    void Awake()
    {
        LoadMap();
        overlayGrid.Generate();
    }

    void Start()
    {
        
    }

    public void LoadMap()
    {
        grid = new List<List<Tile>>();
        for (int y = 0; y < NumberTilesHeight; y++)
        {
            grid.Add(new List<Tile>());
            for (int x = 0; x < NumberTilesWide; x++)
            {
                grid[y].Add(new Tile(new Point(x, y), 1));
            }
        }
    }

    public Dictionary<Point, int> GetPossibleMoves(Point start, int maxMoves)
    {
        BreadthFirstSearch search = new BreadthFirstSearch();
        Dictionary<Point, int> moves = search.SearchDistance(this, start, maxMoves);
        overlayGrid.SetOverlay(moves.Keys.ToList());
        return moves;
    }

    public bool InBounds(Point point)
    {
        return InBounds(point.x, point.y);
    }

    public bool InBounds(int x, int y)
    {
        return y >= 0 && y < grid.Count &&
                x >= 0 && x < grid[y].Count;
    }

    public IEnumerable<Point> Neighbors(Point origin)
    {
        foreach (Point point in DIRS)
        {
            int x = origin.x + point.x;
            int y = origin.y + point.y;
            if (InBounds(x, y))
            {
                Point next = grid[y][x].Location;
                yield return next;
            }
        }
    }
}