using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardZoom : MonoBehaviour
{
    public GameObject Canvas;
    private GameObject zoomCard;

    // called before start
    public void Awake()
    {
        Canvas = GameObject.Find("Main Canvas");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onHoverEnter()
    {
        zoomCard = Instantiate(gameObject, new Vector2(Input.mousePosition.x, Input.mousePosition.y + 250), Quaternion.identity);
        zoomCard.transform.SetParent(Canvas.transform, true);
        zoomCard.layer = LayerMask.NameToLayer("Zoom");
        
        RectTransform rect = zoomCard.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(240, 344);
    }

    public void onHoverExit()
    {
        Destroy(zoomCard);
    }
}
