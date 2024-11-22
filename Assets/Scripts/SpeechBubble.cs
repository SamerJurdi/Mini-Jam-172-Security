using System.Collections;
using System.Collections.Generic; // For List
using UnityEngine;
using TMPro;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField] TextMeshPro tmpro;
    [SerializeField] string text;
    [SerializeField] float TextDisplayTime = 0.07f;
    private float originalTime;
    [SerializeField] public bool PrintText;
    [SerializeField] public SoundPool soundThing;
    [SerializeField] public AudioClip audioClip;
    private Mesh mesh;
    private Vector3[] vertices;
    [SerializeField] private float shakePower = 0.1f;
    [Range(0f, 1f)]
    [SerializeField] public float volumeControll;
    [SerializeField] private float shakeDuration = 0.5f;
    [Range(0f, 1f)]
    [SerializeField] public float pitchRange;
    public float BadWordsShakeDuration = 5f;
    public string targetWord;
    private int[] CharacterIndexes;

    void Start()
    {
        tmpro = GetComponent<TextMeshPro>();
        soundThing = GameObject.Find("AudioPool").GetComponent<SoundPool>();
        originalTime = TextDisplayTime;

        CharacterIndexes = GetCharacterIndexes(text, targetWord);
    }

    void Update()
    {
        if (PrintText)
        {
            StartCoroutine(WriteTextWithShake(text, originalTime));
            PrintText = false;
        }
    }

   
    int[] GetCharacterIndexes(string mainText, string targetWord)
    {
        List<int> indexes = new List<int>();

        int startIndex = 0;
        while ((startIndex = mainText.IndexOf(targetWord, startIndex)) != -1)
        {
            for (int i = 0; i < targetWord.Length; i++)
            {
                indexes.Add(startIndex + i);
            }
            startIndex += targetWord.Length;
        }

        return indexes.ToArray();
    }

    IEnumerator WriteTextWithShake(string text, float displayTimePerCharacter)
    {
        tmpro.text = string.Empty;
        tmpro.ForceMeshUpdate();
        mesh = tmpro.mesh;

        for (int i = 0; i < text.Length; i++)
        {
            if (System.Array.Exists(CharacterIndexes, index => index == i))
            {
                tmpro.text += $"<color=red>{text[i]}</color>";
            }
            else
            {
                tmpro.text += text[i];
            }

            tmpro.ForceMeshUpdate();
            soundThing.PlaySound(audioClip, Vector2.zero, volumeControll, false, pitchRange);

            if (System.Array.Exists(CharacterIndexes, index => index == i))
            {
                StartCoroutine(ConstantShakeCharacter(i, BadWordsShakeDuration));
            }

            yield return new WaitForSeconds(displayTimePerCharacter);
        }
    }

    IEnumerator ConstantShakeCharacter(int charIndex, float timer)
    {
        float currentShakePower = shakePower;
        float shakeDecayRate = shakePower / timer; 

        while (timer > 0)
        {
            TMP_TextInfo textInfo = tmpro.textInfo;
            if (charIndex >= textInfo.characterCount) yield break;

            TMP_CharacterInfo charInfo = textInfo.characterInfo[charIndex];
            if (!charInfo.isVisible) yield break;

            mesh = tmpro.mesh;
            vertices = mesh.vertices;

            int vertexIndex = charInfo.vertexIndex;
            Vector3 offset = new Vector3(
                Random.Range(-currentShakePower, currentShakePower),
                Random.Range(-currentShakePower, currentShakePower),
                0f
            );

            vertices[vertexIndex + 0] = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices[vertexIndex + 0] + offset;
            vertices[vertexIndex + 1] = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices[vertexIndex + 1] + offset;
            vertices[vertexIndex + 2] = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices[vertexIndex + 2] + offset;
            vertices[vertexIndex + 3] = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices[vertexIndex + 3] + offset;

            mesh.vertices = vertices;
            tmpro.canvasRenderer.SetMesh(mesh);

            currentShakePower = Mathf.Max(0, currentShakePower - (shakeDecayRate * Time.deltaTime));
            timer -= Time.deltaTime;

            yield return null;
        }
    }
}
