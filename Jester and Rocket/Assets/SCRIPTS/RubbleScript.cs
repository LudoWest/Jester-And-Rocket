using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbleScript : MonoBehaviour
{
    //[SerializeField]
    private float timeToDestroy = 10;
    //[SerializeField]
    private float shrinkSpeed = 2.0f;

    public bool destroying = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (destroying)
        {
            timeToDestroy -= Time.deltaTime;
            if(timeToDestroy < 0)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * shrinkSpeed);
                if(transform.localScale.x < 0.1f)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

}
