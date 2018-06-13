using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeManager : MonoBehaviour {

    private Vector3 rayPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        Ray ray = new Ray(headPosition, gazeDirection);

        rayPos = ray.GetPoint(3.0f);
	}

    public Vector3 GetRayPos()
    {
        return rayPos;
    }

}
