using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    [SerializeField]
    private float rocketSpeed = 60;
    [SerializeField]
    private float rocketSpeedUp = 2;
    [SerializeField]
    private float rocketOffset = 3;

    [SerializeField]
    private float despawnTimer = 1;

    private float realSpeed;

    // Start is called before the first frame update
    void Start()
    {
        transform.position += transform.right * rocketOffset;
    }

    // Update is called once per frame
    void Update()
    {
        realSpeed = Mathf.Lerp(realSpeed, rocketSpeed, Time.deltaTime * rocketSpeedUp);
        transform.position += transform.right * realSpeed * Time.deltaTime;
        despawnTimer -= Time.deltaTime;
        if(despawnTimer < 0.0f)
        {
            Destroy(this.gameObject);
        }
    }

}
