using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float movementSpeed = 1f;
    public float rotationSpeed = 120;
    public float stoppingDistance = 0.1f;

    public Vector3 destination;
    public Animator animator;

    public bool reachedDestination;

    private Vector3 lastPosition;
    Vector3 velocity;

    private void onAwake()
    {
        animator = GetComponent<Animator>();
    }


    void Start()
    {
        movementSpeed = Random.Range(0.5f, 2f);

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != destination)
        {
            Vector3 direction = destination - transform.position;
            direction.y = 0f;
            float destinationDistance = direction.magnitude;

            if (destinationDistance >= stoppingDistance)
            {
                reachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

            }
            else
            {
                reachedDestination = true;

                // remove this GameObject
            }

            velocity = (transform.position - lastPosition) / Time.deltaTime;
            velocity.y = 0;
            var velocityMagnitude = velocity.magnitude;
            velocity = velocity.normalized;

            var fwdDot = Vector3.Dot(transform.forward, velocity);
            var rightDot = Vector3.Dot(transform.right, velocity);


            animator.SetFloat("moveSpeed", Mathf.Abs(fwdDot));
            animator.SetFloat("rotateSpeed", rightDot);

        }
    }

    public void setDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }
}
