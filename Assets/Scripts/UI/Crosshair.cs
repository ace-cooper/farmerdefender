using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour {

    public float turnSpeed = 1;
    public Color color;
    public Image image;
    public Transform target;

    
	void Update () {
        transform.position = target.position;
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed);
	}

    void OnEnable()
    {
        setColor(color);
    }

    public void setColor(Color c)
    {
     
        image.color = c;
    }
}
