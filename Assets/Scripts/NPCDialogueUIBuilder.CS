using UnityEngine;
using UnityEngine.UI;

public class NPCDialogueUIBuilderXR : MonoBehaviour
{
    public Transform playerHead;
    public float triggerDistance = 4f;

    private GameObject dialoguePanel;
    private Canvas worldCanvas;
    private bool isDialogueVisible = false;

    void Start()
    {
        if (playerHead == null)
        {
            Camera mainCam = Camera.main;
            if (mainCam != null)
                playerHead = mainCam.transform;
            else
                Debug.LogWarning("NPCDialogueUIBuilderXR: No playerHead assigned and Camera.main not found.");
        }

        CreateWorldCanvas();
        HideDialogue();
    }

void Update()
{
    if (playerHead == null)
        return;

    float distance = Vector3.Distance(transform.position, playerHead.position);

    if (distance <= triggerDistance && !isDialogueVisible)
        ShowDialogue();
    else if (distance > triggerDistance && isDialogueVisible)
        HideDialogue();

    if (isDialogueVisible)
    {
        worldCanvas.transform.LookAt(playerHead);
        worldCanvas.transform.Rotate(0, 180f, 0);  // 🔧 Correct the orientation so it faces player properly!
    }
}


   void CreateWorldCanvas()
{
    GameObject canvasGO = new GameObject("NPCWorldCanvas");
    canvasGO.transform.SetParent(transform, false);
    canvasGO.transform.localPosition = new Vector3(0, 1.25f, 0.2f);
    canvasGO.transform.localRotation = Quaternion.identity;
    canvasGO.transform.localScale = Vector3.one * 0.005f;  // 💡 FIX: Shrink canvas to reasonable size!

    worldCanvas = canvasGO.AddComponent<Canvas>();
    worldCanvas.renderMode = RenderMode.WorldSpace;
    worldCanvas.worldCamera = Camera.main;

    CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
    scaler.dynamicPixelsPerUnit = 10;

    canvasGO.AddComponent<GraphicRaycaster>();

    // Panel
    dialoguePanel = new GameObject("Panel");
    dialoguePanel.transform.SetParent(worldCanvas.transform, false);

    RectTransform panelRect = dialoguePanel.AddComponent<RectTransform>();
    panelRect.sizeDelta = new Vector2(300, 100);

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

    Text dialogueText = textGO.AddComponent<Text>();
    dialogueText.text = "To make a shovel we need 3 things: a handle, a stick and a shovel head. Try looking around camp and see what you find!";
    dialogueText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
    dialogueText.fontSize = 16;
    dialogueText.alignment = TextAnchor.MiddleCenter;
    dialogueText.color = Color.white;
}


    void ShowDialogue()
    {
        isDialogueVisible = true;
        worldCanvas.gameObject.SetActive(true);
    }

    void HideDialogue()
    {
        isDialogueVisible = false;
        worldCanvas.gameObject.SetActive(false);
    }
}
