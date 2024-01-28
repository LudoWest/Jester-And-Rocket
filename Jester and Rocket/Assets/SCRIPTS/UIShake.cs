using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShake : MonoBehaviour
{
    private RectTransform rect;
    private Vector2 originalPos;
    [SerializeField]
    public float shakeIntensity;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        originalPos = rect.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        rect.anchoredPosition = originalPos + new Vector2(Random.Range(-1,1),Random.Range(-1,1)).normalized * shakeIntensity * Time.timeScale;
    }
}
