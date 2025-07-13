using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShovelCraftingManager : MonoBehaviour
{
    [Header("Sockets")]
    public XRSocketInteractor socketHandle;
    public XRSocketInteractor socketHead;
    public XRSocketInteractor socketStick;

    [Header("Shovel Parts")]
    public GameObject grabShovelHandle;  // GrabShovelHandle GameObject
    public GameObject grabShovelHead;    // GrabShovelHead GameObject
    public GameObject grabShovelStick;   // GrabShovelStick GameObject

    [Header("Complete Shovel")]
    public GameObject grabShovel;        // GrabShovel GameObject (should be inactive initially)

    private bool crafted = false;

    private void OnEnable()
    {
        socketHandle.selectEntered.AddListener(OnSocketChanged);
        socketHead.selectEntered.AddListener(OnSocketChanged);
        socketStick.selectEntered.AddListener(OnSocketChanged);

        socketHandle.selectExited.AddListener(OnSocketChanged);
        socketHead.selectExited.AddListener(OnSocketChanged);
        socketStick.selectExited.AddListener(OnSocketChanged);
    }

    private void OnDisable()
    {
        socketHandle.selectEntered.RemoveListener(OnSocketChanged);
        socketHead.selectEntered.RemoveListener(OnSocketChanged);
        socketStick.selectExited.RemoveListener(OnSocketChanged);

        socketHandle.selectExited.RemoveListener(OnSocketChanged);
        socketHead.selectExited.RemoveListener(OnSocketChanged);
        socketStick.selectExited.RemoveListener(OnSocketChanged);
    }

    private void OnSocketChanged(SelectEnterEventArgs args) => CheckCraftCondition();
    private void OnSocketChanged(SelectExitEventArgs args) => CheckCraftCondition();

    private void CheckCraftCondition()
    {
        if (crafted)
            return;

        if (socketHandle.hasSelection && socketHead.hasSelection && socketStick.hasSelection)
        {
            CraftShovel();
        }
    }

    private void CraftShovel()
    {
        crafted = true;

        // Deactivate parts
        grabShovelHandle.SetActive(false);
        grabShovelHead.SetActive(false);
        grabShovelStick.SetActive(false);

        // Activate completed shovel
        grabShovel.SetActive(true);
    }
}
