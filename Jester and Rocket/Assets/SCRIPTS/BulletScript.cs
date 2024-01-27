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
    private GameObject explosionParticles;

    [SerializeField]
    private float despawnTimer = 1;

    private float realSpeed;
    private bool blowedUp = false;
    private bool rubbled = false;

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
        
        if(collision.tag != "Player" && !blowedUp)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            foreach (Collider2D hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.GetComponent<Explodable>())
                {
                    hitCollider.GetComponent<Explodable>().explode();
                }
            }

            Collider2D[] chunkColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            foreach (Collider2D chunkCollider in chunkColliders)
            {
                if (chunkCollider.attachedRigidbody && chunkCollider.tag != "Ground")
                {
                    if(chunkCollider.tag == "Rubble")
                    {
                        chunkCollider.attachedRigidbody.bodyType = RigidbodyType2D.Dynamic;
                        chunkCollider.GetComponent<RubbleScript>().destroying = true;
                        if (!rubbled)
                        {
                            Camera.main.GetComponent<SoundScript>().PlaySounds(1, true);
                        }
                    }
                    chunkCollider.attachedRigidbody.velocity += (Vector2)((chunkCollider.transform.position - transform.position).normalized * rocketExplosionForce);
                }
            }

            if(transform.childCount > 0)
            {
                transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                transform.GetChild(0).SetParent(null, false);
            }

            Debug.Log("Boom");
            GameObject explosion = Instantiate(explosionParticles);
            explosion.transform.position = transform.position;
            Camera.main.GetComponent<SoundScript>().PlaySounds(3, true);
            Destroy(this.gameObject);
            blowedUp = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }

}
