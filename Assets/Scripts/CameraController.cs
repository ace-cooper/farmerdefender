using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{


    public Vector2 cameraMinBounds;
    public Vector2 cameraMaxBounds;

    public float cameraSizeMin = 15f;
    public float cameraSizeMax = 35f;

    public float speed = 1;
    private bool moved = false;
    private bool clicked = false;
    private Vector3 mousePos;
    private Vector3 prevMousePos;
    private Vector3 cameraOffset;
    public State clickState;

    void Awake()
    {
        Input.simulateMouseWithTouches = true;
    }

    void OnMouseUp()
    {
       if (!moved)
        {
            moveChar();
        }
        moved = false;
        clicked = false;
    }

    void OnMouseDown()
    {
#if UNITY_EDITOR
        if (EventSystem.current.IsPointerOverGameObject()) return;
#else
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
#endif
        mousePos = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Physics.Raycast(ray, out hit);
        cameraOffset = hit.point - Camera.main.transform.position;
        clicked = true;
    }

    void OnMouseDrag()
    {
#if UNITY_EDITOR
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (Input.mousePosition != mousePos)
        {
            Vector3 touchDeltaPosition = Input.mousePosition - mousePos;
            moved = true;
            Camera.main.transform.Translate(((-touchDeltaPosition.x < 0 && Camera.main.transform.position.x <= cameraMinBounds.x) ||
                (-touchDeltaPosition.x > 0 && Camera.main.transform.position.x >= cameraMaxBounds.x)) ? 0 : -touchDeltaPosition.x * speed * Time.deltaTime,
                ((-touchDeltaPosition.y < 0 && Camera.main.transform.position.y <= cameraMinBounds.y) ||
                (-touchDeltaPosition.y > 0 && Camera.main.transform.position.y >= cameraMaxBounds.y)) ? 0 : -touchDeltaPosition.y * speed * Time.deltaTime, 0);

        }
#else
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
        if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.touchCount == 1)
        {
                moved = true;
         Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                        Camera.main.transform.Translate(((-touchDeltaPosition.x < 0 && Camera.main.transform.position.x <= cameraMinBounds.x) ||
                            (-touchDeltaPosition.x > 0 && Camera.main.transform.position.x >= cameraMaxBounds.x)) ? 0 : -touchDeltaPosition.x * speed,
                            ((-touchDeltaPosition.y < 0 && Camera.main.transform.position.y <= cameraMinBounds.y) ||
                            (-touchDeltaPosition.y > 0 && Camera.main.transform.position.y >= cameraMaxBounds.y)) ? 0 : -touchDeltaPosition.y * speed, 0);

        }
#endif

    }

    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                moved = true;
                switch (Input.touchCount)
                {


                    /*case 1:
                        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                        Camera.main.transform.Translate(((-touchDeltaPosition.x < 0 && Camera.main.transform.position.x <= cameraMinBounds.x) ||
                            (-touchDeltaPosition.x > 0 && Camera.main.transform.position.x >= cameraMaxBounds.x)) ? 0 : -touchDeltaPosition.x * speed,
                            ((-touchDeltaPosition.y < 0 && Camera.main.transform.position.y <= cameraMinBounds.y) ||
                            (-touchDeltaPosition.y > 0 && Camera.main.transform.position.y >= cameraMaxBounds.y)) ? 0 : -touchDeltaPosition.y * speed, 0);
                        break;*/
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
            /*else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
                Vector2 touchpos = Input.GetTouch(0).position;

                moveChar();
            }*/
            
        }

    }


    private void moveChar()
    {
        Vector3 position=Vector3.zero;
#if UNITY_EDITOR 
        if (EventSystem.current.IsPointerOverGameObject()) return;
        position = Input.mousePosition;
#else
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
        position = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);
#endif
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

            Physics.Raycast(ray, out hit);
            GameController.Instance.selectedChar.Remember("lastTargetPos", hit.point);
            GameController.Instance.selectedChar.Remember("priorityTarget", true);

            GameController.Instance.selectedChar.TransitionToState(clickState);
        
    }


    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}