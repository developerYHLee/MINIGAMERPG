using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FallObjectScript : MonoBehaviour
{
    public GameObject _background, _result;

    // Start is called before the first frame update
    void Start()
    {
        _background = GameObject.Find("AvoidPoopCanvas").transform.Find("Background").gameObject;
        _result = GameObject.Find("AvoidPoopCanvas").transform.Find("Result").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * 5f * Time.deltaTime);
        if (gameObject.GetComponent<RectTransform>().localPosition.y <= -600) Destroy(gameObject);
    }

    public abstract void OnTriggerEnter2D(Collider2D collision);
}
