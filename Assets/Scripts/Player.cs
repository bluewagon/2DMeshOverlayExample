using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public int MaxMoves = 5;
    public int RemainingMoves;

    private TileMouse mouse;
    private Map map;
    public Dictionary<Point, int> possibleMoves;

    private Transform transform;

    void Awake()
    {
        transform = gameObject.transform;
    }

	// Use this for initialization
	void Start ()
	{
        map = Camera.main.GetComponent<Map>();
        mouse = GetComponent<TileMouse>();
	    RemainingMoves = MaxMoves;
        possibleMoves = map.GetPossibleMoves(GetTilePosition(), RemainingMoves);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetMouseButtonDown(0))
	    {
	        Move();
	    }
	}

    private void Move()
    {
        if (RemainingMoves <= 0)
        {
            return;
        }

        Point tilePosition = mouse.WorldPosition;
        if (!possibleMoves.ContainsKey(tilePosition))
        {
            return;
        }

        if (map.InBounds(tilePosition))
        {
            Point screenPoint = mouse.ScreenPosition;
            transform.position = new Vector3(screenPoint.x, screenPoint.y, 0);
            RemainingMoves = RemainingMoves - possibleMoves[tilePosition];
            possibleMoves = map.GetPossibleMoves(GetTilePosition(), RemainingMoves);
        }
    }

    private Point GetTilePosition()
    {
        Point position = new Point((int)transform.position.x, (int)transform.position.y * -1);
        return position;
    }
}
