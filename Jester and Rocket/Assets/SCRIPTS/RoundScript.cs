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
    private float songTimer = 180.0f;
    private float alarmTime = 5000.0f;

    [SerializeField]
    private Image destructoMeter;
    private float destructoMeterCurrentScore;
    [SerializeField]
    private float destructoMeterMaxScore;
    [SerializeField]
    private SpringDynamics alarmHolder;
    [SerializeField]
    private PulseImage alarmPulser;

    [SerializeField]
    private CameraController mainCamScript;

    [SerializeField]
    private SpringDynamics leftClickBox;
    [SerializeField]
    private SpringDynamics spaceBarBox;
    [SerializeField]
    private SpringDynamics WASDBox;
    [SerializeField]
    private GameObject timerHolder;
    [SerializeField]
    private SpringDynamics directionArrows;
    [SerializeField]
    private GameObject invisibleWall;

    [SerializeField]
    private AudioClip fiftyPercentSpeed;
    [SerializeField]
    private AudioClip doubleSpeed;

    private bool introDone = false;
    private bool partOne = false;
    private bool partTwo = false;
    private bool firstEntered = false;
    private bool songPlaying = false;
    private AudioClip firstSong;

    // Start is called before the first frame update
    void Start()
    {
        pauseIcon.AltSizeTeleport();
        pauseIcon.SwitchSize();

        pauseIcon2.AltSizeTeleport();
        pauseIcon2.SwitchSize();

        firstSong = song.clip;
		RubbleScript.OnRubbleDestroyed += RubbleScript_OnRubbleDestroyed;
    }

	private void RubbleScript_OnRubbleDestroyed(object sender, RubbleScript.OnRubbleDestroyedEventArgs e) {
		AddScore(e.pointsToAdd);
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
        destructoMeter.gameObject.GetComponent<UIShake>().shakeIntensity = destructoMeter.fillAmount * 8;

        Color newShadeColor = Color.black;
        newShadeColor.a = Mathf.Lerp(0.5f, 0.0f, Time.timeScale);
        pauseShade.color = newShadeColor;

        song.pitch = Time.timeScale;

        //Song Tech
        if (songPlaying)
        {
            songTimer -= Time.deltaTime;
            if(songTimer < 0)
            {
                if(song.clip == fiftyPercentSpeed)
                {
                    Camera.main.GetComponent<SoundScript>().PlaySoundsEcho(0);
                    Camera.main.GetComponent<SoundScript>().PlaySoundsEcho(0);
                    alarmTime = 5.0f;
                    alarmHolder.SwitchPos();
                    alarmPulser.StartPulse();
                    timerHolder.transform.GetComponentInChildren<UIShake>().shakeIntensity = 7;
                    song.clip = doubleSpeed;
                    songTimer = 88.0f;
                    song.Play();
                }
                else if (song.clip != doubleSpeed)
                {
                    Camera.main.GetComponent<SoundScript>().PlaySoundsEcho(0);
                    alarmTime = 5.0f;
                    alarmHolder.SwitchPos();
                    alarmPulser.StartPulse();
                    timerHolder.transform.GetComponentInChildren<UIShake>().shakeIntensity = 4;
                    song.clip = fiftyPercentSpeed;
                    songTimer = 117.0f;
                    song.Play();
                }
            }
        }

        if(alarmTime < 0)
        {
            alarmHolder.SwitchPos();
            alarmTime = 5000.0f;
        }
        else
        {
            alarmTime -= Time.deltaTime;
        }

        //Intro Tech
        if (!introDone)
        {
            if (leftClickBox.transform.childCount < 2 && !partOne)
            {
                leftClickBox.GetComponentInChildren<Rigidbody2D>().WakeUp();
                leftClickBox.GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(-5, 5);
                leftClickBox.GetComponentInChildren<Rigidbody2D>().angularVelocity = 10.0f;
                spaceBarBox.SwitchPos();
                partOne = true;
            }
            else if(spaceBarBox.transform.childCount < 2 && !partTwo)
            {
                spaceBarBox.GetComponentInChildren<Rigidbody2D>().WakeUp();
                spaceBarBox.GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(-5, 5);
                spaceBarBox.GetComponentInChildren<Rigidbody2D>().angularVelocity = 10.0f;
                WASDBox.SwitchPos();
                partTwo = true;
            }
            else if (WASDBox.transform.childCount < 2)
            {
                WASDBox.GetComponentInChildren<Rigidbody2D>().WakeUp();
                WASDBox.GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(-5, 5);
                WASDBox.GetComponentInChildren<Rigidbody2D>().angularVelocity = 10.0f;
                directionArrows.SwitchPos();
                Destroy(invisibleWall);
                introDone = true;
            }

        }
    }

    void AddScore(float scoreToAdd)
    {
        destructoMeterCurrentScore += scoreToAdd;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !firstEntered)
        {
            GetComponent<AudioLowPassFilter>().enabled = false;
            timerHolder.GetComponent<SpringDynamics>().SwitchPos();
            timerHolder.transform.GetComponentInChildren<Timer>().StartTimer();
            Camera.main.GetComponent<SoundScript>().PlaySoundsEcho(4);
            song.PlayDelayed(5.0f);
            songPlaying = true;
        }
    }

}
