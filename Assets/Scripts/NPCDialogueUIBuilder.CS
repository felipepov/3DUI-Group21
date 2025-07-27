using UnityEngine;
using UnityEngine.UI;

public class NPCDialogueUIBuilderXR : MonoBehaviour
{
    public Transform playerHead;
    public float triggerDistance = 4f;

    private Canvas worldCanvas;
    private GameObject dialoguePanel;
    private Text dialogueText;
    private bool isDialogueVisible = false;

    // Optional: tweak your two dialogue lines here
    [TextArea] public string helpText = 
        "Hey boss, so to make your shovel we need 3 things: a handle, a stick and a shovel head. Try looking around camp and see what you find!";
    [TextArea] public string successText = 
        "Nice work! You’ve built your shovel. Before you dig up that treasure of yours, you should probably go find that key! Take the boat we made for you boss.";

    void OnEnable()
    {
        // subscribe
        ShovelCraftingManager.OnShovelCrafted += HandleShovelCrafted;
    }

    void OnDisable()
    {
        // unsubscribe
        ShovelCraftingManager.OnShovelCrafted -= HandleShovelCrafted;
    }

    void Start()
    {
        if (playerHead == null)
        {
            playerHead = Camera.main?.transform;
            if (playerHead == null)
                Debug.LogWarning("NPCDialogueUIBuilderXR: no playerHead assigned and no Camera.main!");
        }

        CreateWorldCanvas();
        SetDialogue(helpText);
        HideDialogue();
    }

    void Update()
    {
        if (playerHead == null) return;

        float dist = Vector3.Distance(transform.position, playerHead.position);
        if (dist <= triggerDistance && !isDialogueVisible) ShowDialogue();
        if (dist >  triggerDistance &&  isDialogueVisible) HideDialogue();

        if (isDialogueVisible)
        {
            worldCanvas.transform.LookAt(playerHead);
            worldCanvas.transform.Rotate(0, 180f, 0);
        }
    }

    void CreateWorldCanvas()
    {
        var cgo = new GameObject("NPCWorldCanvas");
        cgo.transform.SetParent(transform, false);
        cgo.transform.localPosition = new Vector3(0, 1.25f, 0.2f);
        cgo.transform.localScale = Vector3.one * 0.005f;

        worldCanvas = cgo.AddComponent<Canvas>();
        worldCanvas.renderMode = RenderMode.WorldSpace;
        worldCanvas.worldCamera = Camera.main;

        cgo.AddComponent<GraphicRaycaster>();
        var scaler = cgo.AddComponent<CanvasScaler>();
        scaler.dynamicPixelsPerUnit = 10;

        dialoguePanel = new GameObject("Panel");
        dialoguePanel.transform.SetParent(worldCanvas.transform, false);
        var panelRect = dialoguePanel.AddComponent<RectTransform>();
        panelRect.sizeDelta = new Vector2(300, 100);
        var panelImg  = dialoguePanel.AddComponent<Image>();
        panelImg.color = new Color(0, 0, 0, 0.7f);

        var textGO = new GameObject("Text");
        textGO.transform.SetParent(dialoguePanel.transform, false);
        var textRect = textGO.AddComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        dialogueText = textGO.AddComponent<Text>();
        dialogueText.font      = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        dialogueText.fontSize  = 16;
        dialogueText.alignment = TextAnchor.MiddleCenter;
        dialogueText.color     = Color.white;
    }

    void ShowDialogue() => SetVisible(true);
    void HideDialogue() => SetVisible(false);

    void SetVisible(bool v)
    {
        isDialogueVisible = v;
        worldCanvas.gameObject.SetActive(v);
    }

    void SetDialogue(string msg)
    {
        if (dialogueText != null)
            dialogueText.text = msg;
    }

    // 3) This runs as soon as the shovel is crafted:
    void HandleShovelCrafted()
    {
        SetDialogue(successText);
    }
}
