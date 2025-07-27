using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class KeyDestroyer : MonoBehaviour
{
    private Animator animator;
    private XRGrabInteractable grabInteractable;
    public bool pickedUp = false;

    public GameObject teleportBoat;

    void Start()
    {
        teleportBoat.SetActive(false);
        animator = GetComponent<Animator>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrabbed);
    }

    /**void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("PickedUp");
            pickedUp = true;
        }
    }**/

    void OnGrabbed(SelectEnterEventArgs args)
    {
        animator.SetTrigger("PickedUp");
        pickedUp = true;
        teleportBoat.SetActive(true);
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        }
    }

    public void DeleteObject()
    {
        Destroy(gameObject);
    }
}
