using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class end_key : MonoBehaviour
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
        SceneManager.LoadScene("EndMenu", LoadSceneMode.Single); // Load the end menu scene when the key is picked up
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
