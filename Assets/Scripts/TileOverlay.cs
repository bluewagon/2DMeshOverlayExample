using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileOverlay
{
    public Dictionary<int, int> Triangles;
    //private Dictionary<int, Vector3> vertices;

    public TileOverlay(Dictionary<int, int> triangles)
    {
        this.Triangles = triangles;
    }
}
