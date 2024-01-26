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
    private float explosionRadius = 3;
    [SerializeField]
    private float rocketExplosionForce = 100;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag != "Player")
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            foreach (Collider2D hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.GetComponent<Explodable>())
                {
                    Debug.Log(hitCollider.gameObject.name);
                    hitCollider.GetComponent<Explodable>().explode();
                }
            }

            Collider2D[] chunkColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            foreach (Collider2D chunkCollider in chunkColliders)
            {
                if (chunkCollider.attachedRigidbody && chunkCollider.tag != "Ground")
                {
                    chunkCollider.attachedRigidbody.bodyType = RigidbodyType2D.Dynamic;
                    chunkCollider.attachedRigidbody.velocity += (Vector2)((chunkCollider.transform.position - transform.position).normalized * rocketExplosionForce);
                }
            }

            if(transform.childCount > 0)
            {
                transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                transform.GetChild(0).SetParent(null, false);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }

}
