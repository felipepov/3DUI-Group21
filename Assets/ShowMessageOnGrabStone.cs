using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowMessageOnGrabStone : MonoBehaviour
{
    [Header("Settings")]
    public Transform playerHead;
    public float uiDistance = 2f;            // 2 meters in front of player
    public float uiHeightOffset = 0.3f;      // Slightly above center
    public float fadeDuration = 0.8f;        // Fade animation
    public float displayDuration = 5f;       // Show for 6 seconds
    [TextArea]
    public string messageText = "These cursed rocks block the path! Perhaps that stone holds the key…";

    private CanvasGroup canvasGroup;
    private Canvas worldCanvas;
    private Text dialogueText;
    private bool isVisible = false;

    private XRGrabInteractable grabInteractable;

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

        // Get XR Grab component and hook the event
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
            grabInteractable.selectEntered.AddListener(OnGrab);
    }

    void Update()
    {
        if (!isVisible || playerHead == null) return;

        // Keep UI in front of player
        Vector3 forwardPos = playerHead.position + playerHead.forward * uiDistance;
        forwardPos.y += uiHeightOffset;
        worldCanvas.transform.position = forwardPos;

        // Make UI face player
        worldCanvas.transform.LookAt(playerHead);
        worldCanvas.transform.Rotate(0, 180f, 0);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        ShowMessage();
    }

    void CreateWorldCanvas()
    {
        GameObject canvasGO = new GameObject("GrabMessageUI");
        canvasGO.transform.SetParent(null);

        worldCanvas = canvasGO.AddComponent<Canvas>();
        worldCanvas.renderMode = RenderMode.WorldSpace;
        worldCanvas.worldCamera = Camera.main;

        canvasGO.AddComponent<GraphicRaycaster>();
        canvasGroup = canvasGO.AddComponent<CanvasGroup>();
        canvasGO.transform.localScale = Vector3.one * 0.005f;

        // Panel
        GameObject panelGO = new GameObject("Panel");
        panelGO.transform.SetParent(worldCanvas.transform, false);

        RectTransform panelRect = panelGO.AddComponent<RectTransform>();
        panelRect.sizeDelta = new Vector2(420, 140);

        Image panelImage = panelGO.AddComponent<Image>();
        panelImage.color = new Color(0, 0, 0, 0.7f);

        // Text
        GameObject textGO = new GameObject("Text");
        textGO.transform.SetParent(panelGO.transform, false);

        RectTransform textRect = textGO.AddComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        dialogueText = textGO.AddComponent<Text>();
        dialogueText.text = messageText;
        dialogueText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        dialogueText.fontSize = 18;
        dialogueText.alignment = TextAnchor.MiddleCenter;
        dialogueText.color = Color.white;
    }

    public void ShowMessage()
    {
        dialogueText.text = messageText;
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

    void OnDestroy()
    {
        if (grabInteractable != null)
            grabInteractable.selectEntered.RemoveListener(OnGrab);
    }
}