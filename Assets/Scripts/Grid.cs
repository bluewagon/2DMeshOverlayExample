using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{
    public int xSize;
    public int ySize;

    private Vector3[] vertices;
    private Mesh mesh;
    private List<List<TileOverlay>> tileGrid;
    private List<Point> lastPoints;

    public void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Moves Overlay";

        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        for (int y = 0, i = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y * -1);
            }
        }

        int[] triangles = new int[(xSize) * ySize * 6];
        tileGrid = new List<List<TileOverlay>>();
        for (int ti = 0, vi = 0, y = 0; y < ySize - 1; y++, vi++)
        {
            tileGrid.Add(new List<TileOverlay>());
            for (int x = 0; x < xSize - 1; x++, ti += 6, vi++)
            {
                // Save the triangles to turn on and off later
                Dictionary<int, int> tileTriangles = new Dictionary<int, int>();
                tileTriangles.Add(ti, vi);
                tileTriangles.Add(ti + 1, vi + xSize + 1);
                tileTriangles.Add(ti + 2, vi + xSize);
                tileTriangles.Add(ti + 3, vi);
                tileTriangles.Add(ti + 4, vi + 1);
                tileTriangles.Add(ti + 5, vi + xSize + 1);
                tileGrid[y].Add(new TileOverlay(tileTriangles));
            }
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }

        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }

    public void SetOverlay(List<Point> points)
    {
        Reset();
        int[] triangles = mesh.triangles;
        foreach (Point point in points)
        {
            foreach (KeyValuePair<int, int> pair in tileGrid[point.y][point.x].Triangles)
            {
                triangles[pair.Key] = pair.Value;
            }
        }
        mesh.triangles = triangles;
        lastPoints = points;
    }

    private void Reset()
    {
        if (lastPoints == null)
        {
            return;
        }

        int[] triangles = mesh.triangles;
        foreach (Point point in lastPoints)
        {
            foreach (KeyValuePair<int, int> pair in tileGrid[point.y][point.x].Triangles)
            {
                triangles[pair.Key] = 0;
            }
        }
        mesh.triangles = triangles;
    }
}
