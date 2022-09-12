using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollACharacter : MonoBehaviour
{

    [Tooltip("How fast does the player go?")]
    public float speed;
    [Tooltip("How much drag will be applied to the player when isGrounded is true")]
    public float groundedDrag;
    [Tooltip("Opposite of grounded drag")]
    public float airBourneDrag;
    [Tooltip("Are we touching the ground?")]
    public bool isGrounded;
    [Tooltip("What layers are considered ground?")]
    public LayerMask goundMask;

    [Header("Mouse Controls")]
    public bool shouldShowCursor;
    public KeyCode showCursorOnKeyDown;

    private Rigidbody myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();

        if (!shouldShowCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //we calculate the relative movement from the main camera based on player input
        var horzMove = Input.GetAxis("Horizontal");
        var vertMove = Input.GetAxis("Vertical");
        var movement = new Vector3(horzMove, 0, vertMove);
        var relativeMovement = Camera.main.transform.TransformVector(movement);
        relativeMovement.y = 0;

        if (isGrounded)
        {
            //if we are grounded, we can move and we apply drag
            myRB.drag = groundedDrag;
            myRB.AddForce(relativeMovement * speed);
            
        }
        else
        {
            //if we are not groudned, we can't control movment and apply 0 drag
            myRB.drag = airBourneDrag;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(showCursorOnKeyDown))
        {
            //if our cursor is locked and we hit the show cursor key set in the inspector, then we show our cursor and unlock it
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            
        }


        //check to see if we are grounded with a raycast
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 0.6f, goundMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
