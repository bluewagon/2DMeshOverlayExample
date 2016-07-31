using System;
using UnityEngine;
using System.Collections;

public class TileMouse : MonoBehaviour
{
    private Map map;
    private Vector3 position;

    public Point WorldPosition
    {
        get { return ConvertToTile(position); }
    }

    public Point ScreenPosition
    {
        get { return ConvertTileToScreen(ConvertToTile(position)); }
    }

	void Start ()
	{
	    map = Camera.main.GetComponent<Map>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private Point ConvertToTile(Vector3 position)
    {
        return new Point((int)Math.Ceiling(position.x)-1, (int)Math.Ceiling(position.y) *-1);
    }

    private Point ConvertTileToScreen(Point point)
    {
        return new Point(point.x, point.y * -1);
    }
}
