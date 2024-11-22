using System.Collections;
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
    [SerializeField] private float shakeDuration = 0.5f;

    void Start()
    {
        tmpro = GetComponent<TextMeshPro>();
        soundThing = GameObject.Find("AudioPool").GetComponent<SoundPool>();
        originalTime = TextDisplayTime;
    }

    void Update()
    {
        if (PrintText)
        {
            StartCoroutine(WriteTextWithShake(text, originalTime));
            PrintText = false;
        }
    }

    IEnumerator WriteTextWithShake(string text, float displayTimePerCharacter)
    {
        tmpro.text = string.Empty;
        tmpro.ForceMeshUpdate();
        mesh = tmpro.mesh;

        for (int i = 0; i < text.Length; i++)
        {
            tmpro.text += text[i];
            tmpro.ForceMeshUpdate();
            soundThing.PlaySound(audioClip, Vector2.zero, 0.5f, false);

            StartCoroutine(ShakeCharacter(i));
            yield return new WaitForSeconds(displayTimePerCharacter);
        }
    }

    IEnumerator ShakeCharacter(int charIndex)
    {
        float shakeTimer = 0f;

        while (shakeTimer < shakeDuration)
        {
            TMP_TextInfo textInfo = tmpro.textInfo;
            if (charIndex >= textInfo.characterCount) yield break;

            TMP_CharacterInfo charInfo = textInfo.characterInfo[charIndex];
            if (!charInfo.isVisible) yield break;

            mesh = tmpro.mesh;
            vertices = mesh.vertices;

            int vertexIndex = charInfo.vertexIndex;
            Vector3 offset = new Vector3(
                Random.Range(-shakePower, shakePower),
                Random.Range(-shakePower, shakePower),
                0f
            );

            vertices[vertexIndex + 0] = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices[vertexIndex + 0] + offset;
            vertices[vertexIndex + 1] = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices[vertexIndex + 1] + offset;
            vertices[vertexIndex + 2] = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices[vertexIndex + 2] + offset;
            vertices[vertexIndex + 3] = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices[vertexIndex + 3] + offset;

            mesh.vertices = vertices;
            tmpro.canvasRenderer.SetMesh(mesh);

            shakeTimer += Time.deltaTime;
            yield return null;
        }
    }
}
