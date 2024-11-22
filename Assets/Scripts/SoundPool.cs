using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPool : MonoBehaviour
{

    [SerializeField] private AudioSource audioSourcePrefab;
    [SerializeField] private int poolSize = 10;
    private Queue<AudioSource> audioPool;

    void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        audioPool = new Queue<AudioSource>();

        for (int i = 0; i < poolSize; i++)
        {
            AudioSource audioSource = Instantiate(audioSourcePrefab, transform);
            audioSource.gameObject.SetActive(false);
            audioPool.Enqueue(audioSource);
        }
    }
    public void PlaySound(AudioClip clip, Vector3 position, float volume = 1f, bool loop = false)
    {
        if (audioPool.Count > 0)
        {
            AudioSource audioSource = audioPool.Dequeue();
            audioSource.transform.position = position;
            audioSource.clip = clip;
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.volume = volume;
            audioSource.loop = loop;
            audioSource.gameObject.SetActive(true);
            audioSource.Play();

            if (!loop)
            {
                StartCoroutine(ReturnToPoolAfterPlayback(audioSource));
            }
        }
    }
    private System.Collections.IEnumerator ReturnToPoolAfterPlayback(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.Stop();
        audioSource.clip = null;
        audioSource.gameObject.SetActive(false);
        audioPool.Enqueue(audioSource);
    }
}