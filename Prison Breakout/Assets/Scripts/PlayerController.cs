using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool hasItem = false;
    public Transform itemHoldPosition;
    public Transform itemDropPosition;
    public Camera playerCamera;
    public float interactRange = 15f;
    public float dropForce = 5f;

    private GameObject currentItem;
    private Rigidbody currentItemRb;
    private int originalLayer;

    public GameObject fuse;
    public GameObject toilet;
    private int screwsDestroyedCount = 0;
    private Animator toiletAnimator;

    public FuseLever fuseLeverScript;

    public AudioSource ToiletMoveSound;

    void Start()
    { 
        toiletAnimator = toilet.GetComponent<Animator>(); //Get toilet animation
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //Check if player is pressing E
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactRange)) //Check if within range
            {
                if (hasItem && currentItem.CompareTag("Fuse") && hit.collider.CompareTag("FuseLever")) //Check if looking at fuselever and holding fuse
                {
                    PlaceFuse(hit.collider.gameObject);
                }
                else if (!hasItem && IsValidItem(hit.collider.gameObject)) //Check if user isnt holding item and can pick a valid item up
                {
                    PickUpItem(hit.collider.gameObject);
                }
                else if (hasItem && hit.collider.CompareTag("Screw") && currentItem.CompareTag("Screwdriver")) //Check if looking at screw and holding screwdriver
                {
                    UnscrewScrew(hit.collider.gameObject);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.G) && hasItem) //If user presses G and has an item then drop item
        {
            DropItem();
        }
    }

    bool IsValidItem(GameObject item) //Valid items with these tags that can be picked up
    {
        return item.CompareTag("Screwdriver") || item.CompareTag("Fuse") || item.CompareTag("sewageposter");
    }

    void PickUpItem(GameObject item)
    {
        if (!hasItem) //If player doesnt have item
        {
            item.SetActive(false); //Disable item

            item.transform.position = itemHoldPosition.position; //Move item to held possition

            Quaternion rotation = Quaternion.LookRotation(playerCamera.transform.forward, Vector3.up); //set initial rotation to face same direction as player camera
            Vector3 eulerAngles = rotation.eulerAngles;
            if (item.CompareTag("sewageposter"))
            {
                //Custom rotation for the sewage poster
                eulerAngles.x = 0;
                eulerAngles.y += 90;
                eulerAngles.z = 0;
            }
            if (item.CompareTag("Screwdriver"))
            {
                //Custom rotation for the screwdriver
                eulerAngles.x = 0;
                eulerAngles.y += -90;
                eulerAngles.z = 0;
            }
            else
            {
                // Standard rotation adjustment for other items
                eulerAngles.x = 0;
                eulerAngles.z = 0;
            }


            rotation = Quaternion.Euler(eulerAngles); //Apply rotation
            item.transform.rotation = rotation;

            item.transform.parent = itemHoldPosition; //Parent the item to the hold position

            item.SetActive(true); //Enable item

            Rigidbody itemRb = item.GetComponent<Rigidbody>(); //Set item's rigidbody kinematic to true to prevent physics interactions while holding
            if (itemRb != null)
            {
                itemRb.isKinematic = true;
            }

            hasItem = true; //Set to player holidng item
            currentItem = item; //Store reference of current item

            originalLayer = item.layer; //Store item layer
            item.layer = LayerMask.NameToLayer("HeldItem"); //Change item layer to held item

            Debug.Log($"You picked up a {item.tag}!");
        }
        else
        {
            Debug.Log("You already have an item!");
        }
    }

    void DropItem() //Drop item function
    {
        currentItem.transform.parent = null; //Detach item from parent

        currentItem.transform.position = itemDropPosition.position; //Move item to item drop position

        Rigidbody itemRb = currentItem.GetComponent<Rigidbody>();
        if (itemRb != null)
        {
            itemRb.isKinematic = false; //Disable kinematic mode to allow for physic interactions
            itemRb.useGravity = true; //Enable gravity on item
            itemRb.AddForce(playerCamera.transform.forward * dropForce, ForceMode.Impulse); //Apply a drop force
        }

        hasItem = false; //Set to play not holdingh item

        currentItem.layer = originalLayer; //Set item layer back to its original layer

        Debug.Log("You dropped the item!");
    }

    void UnscrewScrew(GameObject screw)
    {
        if (currentItem != null && currentItem.CompareTag("Screwdriver")) //Check is current item held is screwdriver
        {
            Destroy(screw); //Destroy screw looking at
            Debug.Log("You unscrewed the screw!");

            screwsDestroyedCount++; //Add 1 to screwdestroyedcount

            if (screwsDestroyedCount >= 4) //If 4 or more screws destroyed
            {
                MoveToilet(); //Move toilet
            }
        }
        else
        {
            Debug.Log("You need a screwdriver to unscrew this!");
        }
    }

    void PlaceFuse(GameObject fuseLever) //Place fuse function
    {
        if (fuse != null)  //If fuse hasnt been placed
        {
            fuse.SetActive(true); //Set fuse to active
        }

        Animator animator = fuse.GetComponent<Animator>();
        if (animator != null) //If animator is valid
        {
            animator.SetTrigger("Place"); //Enable fuse place animation
            Debug.Log("Fuse placed!");

            if (fuseLeverScript != null) //If fuseleverscript is valid
            {
                fuseLeverScript.isFusePlaced = true; //Set isFusePlaced to true in fuseLeverScript
            }

            currentItem.SetActive(false); //Disable current held item
            hasItem = false; //Set player to not holding an item
            currentItem = null; //Clear current item reference
        }
        else
        {
            Debug.Log("No animator found on the fuse lever!");
        }
    }


    void MoveToilet() //Move toilet function
    {
        if (toiletAnimator != null) //If animator is valid
        {
            toiletAnimator.SetTrigger("Move"); //Move toilet animation
            ToiletMoveSound.Play();
        }

        Debug.Log("Toilet moved!");
    }

}