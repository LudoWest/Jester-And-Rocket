using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmSpin : MonoBehaviour
{
    [SerializeField]
    private float spinSpeed = 5.0f;
    private RectTransform rect;
    private Vector3 currentRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        currentRotation = new Vector3(rect.localRotation.x, rect.localRotation.y, rect.localRotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        currentRotation += new Vector3(0, spinSpeed * Time.deltaTime, 0);
        rect.localRotation = Quaternion.Euler(currentRotation);
    }
}
