using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    RectTransform outerPad, innerPad;
    float handleRange = 1;
    Vector3 input = Vector3.zero;
    Canvas canvas;

    public float Horizontal { get { return input.normalized.x; } }
    public float Vertical { get { return input.normalized.y; } }

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("JoyStickCanvas").GetComponent<Canvas>();
        outerPad = gameObject.GetComponent<RectTransform>();
        innerPad = transform.GetChild(0).GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        innerPad.anchoredPosition = Vector2.zero;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        
        Vector2 radius = outerPad.sizeDelta / 2;
        var tempPosition = (eventData.position - outerPad.anchoredPosition * canvas.scaleFactor) / (radius * canvas.scaleFactor);

        //Debug.Log("eventData.position : " + eventData.position + " outerPad.anchoredPosition : " + outerPad.anchoredPosition
        //    + "\neventData.position - outerPad.anchoredPosition * canvas.scaleFactor : " + (eventData.position - outerPad.anchoredPosition * canvas.scaleFactor)
        //    + "\ncanvas.scaleFactor : " + canvas.scaleFactor);

        innerPad.anchoredPosition = tempPosition.magnitude > handleRange ? tempPosition.normalized * radius : tempPosition * radius;
        input = tempPosition;
    }
}