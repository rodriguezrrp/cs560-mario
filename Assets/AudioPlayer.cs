using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource intro;
    public AudioSource loop;
    IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        coroutine = PlayBGAudio(intro, loop);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayBGAudio(AudioSource intro, AudioSource loop)
    {
        intro.loop = false;
        loop.loop = true;
        double dspTime = AudioSettings.dspTime;
        intro.Play();
        //intro.PlayScheduled(dspTime);
        loop.PlayScheduled(dspTime + intro.clip.length);
        yield return null;
    }
}
