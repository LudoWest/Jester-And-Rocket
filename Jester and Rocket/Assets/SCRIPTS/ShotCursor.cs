using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotCursor : MonoBehaviour
{
    private Canvas canvasRef;
    private float scaleFactor;

    [SerializeField]
    private float cursorOffset;
    [SerializeField]
    private SpringDynamics springComp;

    [SerializeField]
    float cursorSpinDistance = 360.0f;
    [SerializeField]
    float cursorShootSize = -2.0f;

    // Start is called before the first frame update
    void Start()
    {
        canvasRef = GameObject.Find("Canvas").GetComponent<Canvas>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        scaleFactor = canvasRef.GetComponent<Canvas>().scaleFactor;

        Vector3 mouseScreenPos = Input.mousePosition;
        //mouseScreenPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        GetComponent<RectTransform>().anchoredPosition = (mouseScreenPos - (Vector3.one * cursorOffset)) / scaleFactor;

        if (Input.GetMouseButtonDown(0))
        {
            springComp.AddRotOffset(cursorSpinDistance);
            springComp.ReactSize(cursorShootSize);
        }
    }

    
}
