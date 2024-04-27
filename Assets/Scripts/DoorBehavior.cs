using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public Material doorMaterial; // Reference to the door's material
    public Color openColor = Color.grey; // Color when the door is shot
    public Color closedColor = Color.blue; // Color when the door is closed
    public float fadeTime = 1f; // Time for the door to fade out
    public float triggerRadius = 2f; // Radius of the trigger area
    [SerializeField]
    private bool isShot = false;
    [SerializeField]
    private bool isOpen = false;
    private MeshRenderer meshRenderer;
    private Collider doorCollider;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        doorCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (isShot && !isOpen)
        {
            if (Vector3.Distance(transform.position, Camera.main.transform.position) <= triggerRadius)
            {
                OpenDoor();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isOpen)
        {
            CloseDoor();
        }
    }

    public void Shoot()
    {
        if (!isShot)
        {
            isShot = true;
            doorMaterial.color = openColor;
        }
    }

    private void OpenDoor()
    {
        isOpen = true;
        doorCollider.isTrigger = true;
        meshRenderer.enabled = false;
    }

    private void CloseDoor()
    {
        isOpen = false;
        isShot = false;
        doorCollider.isTrigger = false;
        meshRenderer.enabled = true;
        doorMaterial.color = closedColor;
     
    }

   
}
