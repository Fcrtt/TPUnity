using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    Camera cam;
    GameManager gameManager;
    private PlayCell newPlayCell;
    private GameObject newParent;
    private PlayCell oldPlayCell;
    private GameObject oldParent;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(camRay, out hit, 500))
        {
            newPlayCell = hit.collider.gameObject.GetComponent<PlayCell>();
            newParent = newPlayCell.transform.parent.gameObject;
            if (newPlayCell != null && oldPlayCell == null)
            {
                newPlayCell.OnHoverEnter();
                oldPlayCell = newPlayCell;
                oldParent = newParent;
            }
            else if (newPlayCell.name != oldPlayCell.name || oldParent.name != newParent.name)
            {
                oldPlayCell.OnHoverExit();
                newPlayCell.OnHoverEnter();
                oldPlayCell = newPlayCell;
                oldParent = newParent;
            }

            if (Input.GetMouseButtonDown(0))
            {
                gameManager.PlayInCell(newPlayCell);
            }
        }
        else
        {
            oldPlayCell = newPlayCell;
            oldParent = newParent;
            if (oldPlayCell)
            {
                oldPlayCell.OnHoverExit();
            }
        }
    }
}
