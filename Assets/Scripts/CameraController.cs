using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    public Vector2 cameraMinBounds;
    public Vector2 cameraMaxBounds;

    public float cameraSizeMin = 15f;
    public float cameraSizeMax = 35f;

    public float speed = 1;
    void Awake()
    {
        Input.simulateMouseWithTouches = true;
    }

    void OnMouseDown()
    {
        moveChar(Input.mousePosition);
    }

    void FixedUpdate()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            switch (Input.touchCount)
            {


                case 1:
                    Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                    Camera.main.transform.Translate(((-touchDeltaPosition.x < 0 && Camera.main.transform.position.x <= cameraMinBounds.x) ||
                        (-touchDeltaPosition.x > 0 && Camera.main.transform.position.x >= cameraMaxBounds.x)) ? 0 : -touchDeltaPosition.x * speed,
                        ((-touchDeltaPosition.y < 0 && Camera.main.transform.position.y <= cameraMinBounds.y) ||
                        (-touchDeltaPosition.y > 0 && Camera.main.transform.position.y >= cameraMaxBounds.y)) ? 0 : -touchDeltaPosition.y * speed, 0);
                    break;
                case 2:
                    Touch touch1 = Input.GetTouch(0);
                    Touch touch2 = Input.GetTouch(1);

                    float prevMag = ((touch1.position - touch1.deltaPosition) - (touch2.position - touch2.deltaPosition)).magnitude;
                    float touchMag = (touch1.position - touch2.position).magnitude;
                    // ... change the orthographic size based on the change in distance between the touches.
                    Camera.main.orthographicSize += (prevMag - touchMag) * speed;

                    // Make sure the orthographic size never drops below zero.
                    Camera.main.orthographicSize = Mathf.Min(Mathf.Max(Camera.main.orthographicSize, cameraSizeMin), cameraSizeMax);
                    break;
            }
        }
        else if (Input.touchCount==1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchpos = Input.GetTouch(0).position;

            moveChar(new Vector3(touchpos.x,touchpos.y,0));
        }

    }


    private void moveChar(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        Physics.Raycast(ray, out hit);
        GameController.Instance.selectedChar.Remember<Vector3>("lastTargetPos", hit.point);
    }
}