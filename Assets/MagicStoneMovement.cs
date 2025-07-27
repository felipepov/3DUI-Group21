using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagicStoneHoming : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform targetRock;        // Assign the rock or an empty point in front of rocks
    public float homingSpeed = 10f;     // Speed of homing
    public float homingStartDelay = 0.5f; // Delay after release
    public float stopDistance = 0.5f;   // Stop when close

    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private bool isHoming = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
            grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnRelease(SelectExitEventArgs args)
    {
        Invoke(nameof(StartHoming), homingStartDelay); // Start homing after short delay
    }

    void StartHoming()
    {
        isHoming = true;
        rb.useGravity = false;  // Disable gravity for smooth flight
    }

    void FixedUpdate()
    {
        if (isHoming && targetRock != null)
        {
            Vector3 direction = (targetRock.position - transform.position).normalized;
            rb.velocity = direction * homingSpeed;

            // Stop when close enough
            if (Vector3.Distance(transform.position, targetRock.position) <= stopDistance)
            {
                isHoming = false;
                rb.velocity = Vector3.zero;
                rb.useGravity = true; // Re-enable gravity if needed
            }
        }
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
            grabInteractable.selectExited.RemoveListener(OnRelease);
    }
}