using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    private float timer = 385.0f;
    private bool isTimer = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isTimer)
        {
            timer -= Time.deltaTime;
            DisplayTime();
        }
    }

    void DisplayTime()
    {
        int minutes = Mathf.FloorToInt(timer / 60.0f);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        text.text = "You only have " + string.Format("{0:00}:{1:00}",minutes,seconds) + " Minutes Left";
        if(timer < 20.0f)
        {
            Camera.main.GetComponent<CameraController>().CameraShake(1.0f);
        }
        if (timer < 0.0f)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void StartTimer()
    {
        isTimer = true;
    }

    public void StopTimer()
    {
        isTimer = false;
    }

    public void ResetTimer()
    {
        timer = 180.0f;
    }
}
