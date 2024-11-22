using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField] TextMeshPro tmpro;
    [SerializeField] string text;
    [SerializeField] float TextDisplayTime = 0.07f; //good speed for default
    private float originalTime;
    [SerializeField] public bool PrintText;
    [SerializeField] public SoundPool sountThing;
    [SerializeField] public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        tmpro = GetComponent<TextMeshPro>();
        sountThing = GameObject.Find("AudioPool").GetComponent<SoundPool>();

        originalTime = TextDisplayTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (PrintText)
        {
            StartCoroutine(WriteText(text, originalTime));
            PrintText = false;
        }
    }


    IEnumerator WriteText(string text, float displayTimePerCharacter)
    {
        tmpro.text = string.Empty;

        for (int i = 0; i < text.Length; i++)
        {
            tmpro.text += text[i];
            sountThing.PlaySound(audioClip, Vector2.zero, 0.5f, false);
            yield return new WaitForSeconds(displayTimePerCharacter);
        }

    }

}
