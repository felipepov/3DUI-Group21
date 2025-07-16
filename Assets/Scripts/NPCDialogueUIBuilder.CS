using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class NPCDialogueUIBuilderXR : MonoBehaviour
{
    public Transform playerHead;
    public float triggerDistance = 4f;

    private GameObject dialoguePanel;
    private bool isDialogueVisible = false;

    void Start()
    {
        if (playerHead == null)
        {
            // Try to auto-find the XR Rig's main camera
            Camera mainCam = Camera.main;
            if (mainCam != null)
                playerHead = mainCam.transform;
            else
                Debug.LogWarning("NPCDialogueUIBuilderXR: No playerHead assigned and Camera.main not found.");
        }
    }

    void Update()
    {
        if (playerHead == null) return;

        float distance = Vector3.Distance(transform.position, playerHead.position);

        if (distance <= triggerDistance && !isDialogueVisible)
        {
            ShowDialogue();
        }
        else if (distance > triggerDistance && isDialogueVisible)
        {
            HideDialogue();
        }
    }

    private void ShowDialogue()
    {
        isDialogueVisible = true;

        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasGO = new GameObject("Canvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasGO.AddComponent<GraphicRaycaster>();
        }

        dialoguePanel = new GameObject("NPCDialoguePanel");
        dialoguePanel.transform.SetParent(canvas.transform, false);

        RectTransform panelRect = dialoguePanel.AddComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0.5f, 0);
        panelRect.anchorMax = new Vector2(0.5f, 0);
        panelRect.pivot = new Vector2(0.5f, 0);
        panelRect.anchoredPosition = new Vector2(0, 50);
        panelRect.sizeDelta = new Vector2(600, 150);

        Image panelImage = dialoguePanel.AddComponent<Image>();
        panelImage.color = new Color(0, 0, 0, 0.7f);

        GameObject textGO = new GameObject("DialogueText");
        textGO.transform.SetParent(dialoguePanel.transform, false);

        RectTransform textRect = textGO.AddComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0, 0);
        textRect.anchorMax = new Vector2(1, 1);
        textRect.offsetMin = new Vector2(10, 10);
        textRect.offsetMax = new Vector2(-10, -10);

        Text dialogueText = textGO.AddComponent<Text>();
        dialogueText.text = "To make a shovel we need 3 things: a handle, a stick and a shovel head. Try looking around camp and see what you find!";
        dialogueText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        dialogueText.fontSize = 20;
        dialogueText.alignment = TextAnchor.MiddleCenter;
        dialogueText.color = Color.white;
    }

    private void HideDialogue()
    {
        isDialogueVisible = false;

        if (dialoguePanel != null)
        {
            Destroy(dialoguePanel);
        }
    }
}
