using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShovelCraftingManager : MonoBehaviour
{
    // 1) Declare the event
    public static event Action OnShovelCrafted;

    [Header("Sockets")]
    public XRSocketInteractor socketHandle;
    public XRSocketInteractor socketHead;
    public XRSocketInteractor socketStick;
    [Header("Shovel Parts")]
    public GameObject GrabShovelHandle;
    public GameObject GrabShovelHead;
    public GameObject GrabShovelStick;
    public GameObject OutlineHandle;
    public GameObject OutlineHead;
    public GameObject OutlineStick;
    [Header("Complete Shovel")]
    public GameObject grabShovel;

    private bool crafted = false;
    public bool isCrafted = false;

    void OnEnable()
    {
        socketHandle.selectEntered.AddListener(_ => CheckCraftCondition());
        socketHead.selectEntered.AddListener(_ => CheckCraftCondition());
        socketStick.selectEntered.AddListener(_ => CheckCraftCondition());
        socketHandle.selectExited.AddListener(_ => CheckCraftCondition());
        socketHead.selectExited.AddListener(_ => CheckCraftCondition());
        socketStick.selectExited.AddListener(_ => CheckCraftCondition());
    }

    void OnDisable()
    {
        socketHandle.selectEntered.RemoveListener(_ => CheckCraftCondition());
        socketHead.selectEntered.RemoveListener(_ => CheckCraftCondition());
        socketStick.selectEntered.RemoveListener(_ => CheckCraftCondition());
        socketHandle.selectExited.RemoveListener(_ => CheckCraftCondition());
        socketHead.selectExited.RemoveListener(_ => CheckCraftCondition());
        socketStick.selectExited.RemoveListener(_ => CheckCraftCondition());
    }

    private void CheckCraftCondition()
    {
        if (crafted) return;

        if (socketHandle.hasSelection &&
            socketHead.hasSelection &&
            socketStick.hasSelection)
        {
            CraftShovel();
        }
    }

    private void CraftShovel()
    {
        crafted = true;
        isCrafted = true;

        // hide parts
        GrabShovelHandle.SetActive(false);
        GrabShovelHead.SetActive(false);
        GrabShovelStick.SetActive(false);
        OutlineHandle.SetActive(false);
        OutlineHead.SetActive(false);
        OutlineStick.SetActive(false);

        // show finished shovel
        grabShovel.SetActive(true);

        // 2) Fire the event
        OnShovelCrafted?.Invoke();
    }
}
