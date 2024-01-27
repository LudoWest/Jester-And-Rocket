using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour
{
    private float despawnTimer = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        despawnTimer -= Time.deltaTime;
        if (despawnTimer < 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
