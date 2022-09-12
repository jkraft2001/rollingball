using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRoll : MonoBehaviour
{
    public float speed;
    public Rigidbody myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        speed = 6;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var horzMove = Input.GetAxis("Horizontal");
        var vertMove = Input.GetAxis("Vertical");

        var movement = new Vector3(horzMove, 0, vertMove);

        var relativeMovement = Camera.main.transform.TransformVector(movement);

        myRB.AddForce(relativeMovement * speed);
    }
}
