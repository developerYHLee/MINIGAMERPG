using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    RectTransform outerPad, innerPad;
    float handleRange = 1;
    Vector3 input = Vector3.zero; //조이스틱의 방향을 전달하기 위한 변수
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
        
        Vector2 radius = outerPad.sizeDelta / 2; //outerPad의 RectTransform
        
        //anchoredPosition : anchor에서 pivot까지의 거리, 즉 pivot - anchor
        //중심점에서 터치한 곳까지의 거리를 구한 후, 반지름으로 나눈다. 1보다 크면 원을 넘어간 것이고, 1보다 작으면 원 안에 있다.
        //canvas.scaleFactor를 해준 이유는 outerPad는 고정되어 있기 때문에, 해상도가 달라질 때마다 크기나 위치를 바꿔줘야 하기 때문에 canvas.scaleFactor를 곱해준다.
        var tempPosition = (eventData.position - outerPad.anchoredPosition * canvas.scaleFactor) / (radius * canvas.scaleFactor);

        //Debug.Log("eventData.position : " + eventData.position + " outerPad.anchoredPosition : " + outerPad.anchoredPosition
        //    + "\neventData.position - outerPad.anchoredPosition * canvas.scaleFactor : " + (eventData.position - outerPad.anchoredPosition * canvas.scaleFactor)
        //    + "\ncanvas.scaleFactor : " + canvas.scaleFactor);

        //원을 넘어가면 원을 넘어가지 못하게 정규화 해준 뒤 반지름을 곱해주고,
        //원을 넘어가지 않으면 그 상태로 반지름을 곱해준다.
        innerPad.anchoredPosition = tempPosition.magnitude > handleRange ? tempPosition.normalized * radius : tempPosition * radius;
        
        input = tempPosition;
    }
}