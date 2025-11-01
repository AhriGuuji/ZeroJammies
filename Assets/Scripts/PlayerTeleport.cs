using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTeleport : Interactable
{

    private GameObject currentTeleporter;
    
    protected override void Awake()
    {
        base.Awake();
    }
    // Update is called once per frame
    void Update()
    {

        /*if (_interact.WasPerformedThisFrame())
        {
            if (currentTeleporter != null)
            {
                transform.position = currentTeleporter.GetComponent<Teleporter>().getDestionation().position;
            }
        }*/
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(currentTeleporter != null)
            {
                transform.position = currentTeleporter.GetComponent<Teleporter>().getDestionation().position;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if(collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }
    }
}
