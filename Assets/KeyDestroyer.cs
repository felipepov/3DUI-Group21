using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KeyPickup : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrabbed);
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
    }
}
