using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FireMessageUIXR : MonoBehaviour
{
    [Header("Settings")]
    public Transform playerHead;
    public float displayDuration = 6f;
    public float fadeDuration = 0.8f;

    private CanvasGroup canvasGroup;
    private GameObject dialoguePanel;
    private Canvas worldCanvas;
    private Text dialogueText;

    private string fireMessage = "To reach the treasure, you must complete all the tasks… What you seek may lie on the other islands. How will you cross?";
    private bool isVisible = false;

    void Start()
    {
        if (playerHead == null)
        {
            Camera mainCam = Camera.main;
            if (mainCam != null)
                playerHead = mainCam.transform;
        }

        CreateWorldCanvas();
        HideInstant();
    }

    void CreateWorldCanvas()
    {
        GameObject canvasGO = new GameObject("FireMessageCanvas");
        canvasGO.transform.SetParent(playerHead, false);
        canvasGO.transform.localPosition = new Vector3(0, 0.5f, 3f); // In front of player
        canvasGO.transform.localRotation = Quaternion.identity;
        canvasGO.transform.localScale = Vector3.one * 0.005f;

        worldCanvas = canvasGO.AddComponent<Canvas>();
        worldCanvas.renderMode = RenderMode.WorldSpace;
        worldCanvas.worldCamera = Camera.main;

        canvasGO.AddComponent<GraphicRaycaster>();
        canvasGroup = canvasGO.AddComponent<CanvasGroup>();

        // Panel
        dialoguePanel = new GameObject("Panel");
        dialoguePanel.transform.SetParent(worldCanvas.transform, false);

        RectTransform panelRect = dialoguePanel.AddComponent<RectTransform>();
        panelRect.sizeDelta = new Vector2(350, 120);

        Image panelImage = dialoguePanel.AddComponent<Image>();
        panelImage.color = new Color(0, 0, 0, 0.7f);

        // Text
        GameObject textGO = new GameObject("Text");
        textGO.transform.SetParent(dialoguePanel.transform, false);

        RectTransform textRect = textGO.AddComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        dialogueText = textGO.AddComponent<Text>();
        dialogueText.text = fireMessage;
        dialogueText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        dialogueText.fontSize = 18;
        dialogueText.alignment = TextAnchor.MiddleCenter;
        dialogueText.color = Color.white;
    }

    public void ShowMessage()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInAndAutoHide());
    }

    IEnumerator FadeInAndAutoHide()
    {
        yield return StartCoroutine(FadeCanvas(0, 1)); // Fade in
        yield return new WaitForSeconds(displayDuration);
        yield return StartCoroutine(FadeCanvas(1, 0)); // Fade out
    }

    IEnumerator FadeCanvas(float from, float to)
    {
        isVisible = true;
        float elapsed = 0;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = to;

        if (to == 0) isVisible = false;
    }

    void HideInstant()
    {
        canvasGroup.alpha = 0;
        isVisible = false;
    }

    // ✅ Touch FireBowl to re-show UI
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand")) // Make sure your hand or controller collider has this tag
        {
            ShowMessage();
        }
    }
}