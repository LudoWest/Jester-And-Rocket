using UnityEngine;
using UnityEngine.UI;

public class PulseImage : MonoBehaviour
{
    public Image imageToPulse;
    public Color pulseColor = Color.white;
    public float pulseDuration = 1.0f;
    public float pulseSpeed = 1.0f;

    private bool isPulsing = false;
    private float startTime;

    private void Start()
    {
        //StartPulse();
    }

    void Update()
    {
        if (isPulsing)
        {
            PulseImageEffect();
        }
    }

    public void StartPulse()
    {
        if (!isPulsing)
        {
            isPulsing = true;
            startTime = Time.time;
        }
    }

    void PulseImageEffect()
    {
        float elapsed = Time.time - startTime;
        float t = Mathf.PingPong(elapsed * pulseSpeed / pulseDuration, 1f);
        Color lerpedColor = Color.Lerp(new Color(1, 1, 1, 0), pulseColor, t);

        imageToPulse.color = lerpedColor;

        if (elapsed >= pulseDuration)
        {
            isPulsing = false;
            imageToPulse.color = new Color(1,1,1,0); // Ensure it ends on transparent
        }
    }
}