using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour
{

    public Transform Target;
    private Transform transform;

    void Awake()
    {
        transform = gameObject.transform;
    }

	
	void Update ()
	{
	    transform.position = new Vector3(Target.position.x, 
            Target.position.y, transform.position.z);
	}
}
