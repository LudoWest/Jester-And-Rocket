using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> sounds = new List<AudioClip>();
    [SerializeField]
    private GameObject soundPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySounds(int soundID, bool randomPitch)
    {
        GameObject newsound = Instantiate(soundPrefab);
        newsound.GetComponent<AudioSource>().clip = sounds[soundID];
        if (randomPitch)
        {
            newsound.GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
        }
        newsound.GetComponent<AudioSource>().Play();
    }
}
