using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbleScript : MonoBehaviour
{
    //[SerializeField]
    private float timeToDestroy = 10;
    //[SerializeField]
    private float shrinkSpeed = 2.0f;

    [SerializeField]
    private int points = 5;
    private bool pointsRewarded = false;

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
            if (!pointsRewarded)
            {
                //EVENTS CODE HERE
                pointsRewarded = true;
            }
            timeToDestroy -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timeToDestroy * 0.1f);
            if (transform.localScale.x < 0.1f)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
