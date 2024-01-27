using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundScript : MonoBehaviour
{

    public static bool paused = false;
    [SerializeField]
    private float timeSlowRate = 5.0f;
    [SerializeField]
    private SpringDynamics pauseIcon;
    [SerializeField]
    private SpringDynamics pauseIcon2;
    [SerializeField]
    private Image pauseShade;
    [SerializeField]
    private AudioSource song;

    [SerializeField]
    private Image destructoMeter;
    private float destructoMeterCurrentScore;
    [SerializeField]
    private float destructoMeterMaxScore;

    [SerializeField]
    private CameraController mainCamScript;

    // Start is called before the first frame update
    void Start()
    {
        pauseIcon.AltSizeTeleport();
        pauseIcon.SwitchSize();

        pauseIcon2.AltSizeTeleport();
        pauseIcon2.SwitchSize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            pauseIcon.SwitchSize();
            pauseIcon2.SwitchSize();
            
        }

        if (paused)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0.0f, Time.unscaledDeltaTime * timeSlowRate);
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
        else
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1.0f, Time.unscaledDeltaTime * timeSlowRate);
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }

        destructoMeter.fillAmount = Mathf.Lerp(destructoMeter.fillAmount, destructoMeterCurrentScore / destructoMeterMaxScore, Time.deltaTime);

        Color newShadeColor = Color.black;
        newShadeColor.a = Mathf.Lerp(0.5f, 0.0f, Time.timeScale);
        pauseShade.color = newShadeColor;

        song.pitch = Time.timeScale;
    }

    //EVENTS CODE... HERE??
    //I GENUINELY DONT UNDERSTAND HOW EVENTS WORK BUT IF THIS GETS CALLED THEN ALL IS GOOD
    void AddScore(float scoreToAdd)
    {
        destructoMeterCurrentScore += scoreToAdd;
    }

}
