using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KeyDestroyer : MonoBehaviour
{
    private Animator animator;
    private XRGrabInteractable grabInteractable;
    public bool pickedUp = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrabbed);
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        animator.SetTrigger("PickedUp");
        pickedUp = true;
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
    }

    public void DeleteObject()
    {
        Destroy(gameObject);
    }
}
