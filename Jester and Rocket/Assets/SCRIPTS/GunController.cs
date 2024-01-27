using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private float gunRotationSpeed = 0.1f;
    [SerializeField]
    private float gunReactionForce = -1.0f;
    [SerializeField]
    private float gunReactionRotForce = 1.0f;
    [SerializeField]
    private float gunCameraShakeForce = 1.0f;

    [SerializeField]
    private float reloadSpeed = 2.0f;
    [SerializeField]
    private float reloadDelay = 1.0f;

    [SerializeField]
    private SpringDynamics gunReactor;
    [SerializeField]
    private CameraController cameraShaker;
    [SerializeField]
    private GameObject rocketPrefab;
    [SerializeField]
    private SoundScript audioCue;


    public bool reloading = false;
    private float reloadTimer = -1.0f;

    // Start is called before the first frame update
    void Start()
    {
        reloadTimer = -reloadDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (!RoundScript.paused)
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos = Vector3.Scale(Camera.main.ScreenToWorldPoint(mouseScreenPos), new Vector3(1.0f, 1.0f, 0.0f));
            Vector3 cursorAngle = mouseScreenPos - transform.position;
            transform.right = Vector3.Lerp(transform.right, cursorAngle.normalized, gunRotationSpeed * Time.deltaTime);


            if (Input.GetMouseButtonDown(0) && !reloading)
            {
                gunReactor.ReactRight(gunReactionForce);
                gunReactor.ReactRot(gunReactionRotForce);
                cameraShaker.CameraShake(gunCameraShakeForce);
                audioCue.PlaySounds(2,true);
                GameObject newRocket = Instantiate(rocketPrefab);
                newRocket.transform.position = transform.position;
                newRocket.transform.rotation = transform.rotation;
                reloading = true;

                Cursor.visible = false;
            }
            else if (reloading)
            {
                reloadTimer += Time.deltaTime * reloadSpeed;
                if (reloadTimer > 5.0f)
                {
                    reloading = false;
                    reloadTimer = -reloadDelay;
                }
            }
        }
        
    }

}
