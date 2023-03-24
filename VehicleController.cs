using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{

    public float stoppingDistance = 2.0f;

    public Vector3 destination;
    public bool reachedDestination;
    private Vector3 lastPosition;

    public WheelCollider WheelFL;
    public WheelCollider WheelFR;
    public WheelCollider WheelRL;
    public WheelCollider WheelRR;
    public Transform WheelFLtrans;
    public Transform WheelFRtrans;
    public Transform WheelRLtrans;
    public Transform WheelRRtrans;
    public Vector3 eulertest;
    private bool braked = false;
    private float maxBrakeTorque = 5000;
    private Rigidbody rb;
    public Transform centreofmass;
    public float maxTorque = 200;

    public float target_speed = 1;
    public float k = 0.1f;  // look forward gain
    public float Lfc = 2.0f;  // [m] look-ahead distance
    public float Kp = 1.0f;  // speed proportional gain
    public float dt = 0.1f;  // [s] time tick
    public float WB = 2.9f;  // [m] wheel base of vehicle

    private void onAwake()
    {
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centreofmass.transform.localPosition;

        reachedDestination = false;
    }

    void FixedUpdate()
    {
        if (!braked)
        {
            WheelFL.brakeTorque = 0;
            WheelFR.brakeTorque = 0;
            WheelRL.brakeTorque = 0;
            WheelRR.brakeTorque = 0;
        }

        float ai = proportional_control(target_speed, transform.InverseTransformDirection(rb.velocity).z);
        float delta = pure_pursuit_control();

        WheelRR.motorTorque = maxTorque * ai;
        WheelRL.motorTorque = maxTorque * ai;

        WheelFL.steerAngle = 30 * (delta);
        WheelFR.steerAngle = 30 * (delta);

    }

    public float pure_pursuit_control()
    {
        float alpha = Mathf.Atan2(destination.x - transform.localPosition.x,
                                  destination.z - transform.localPosition.z) - transform.localEulerAngles.y * Mathf.Deg2Rad;
        float delta = Mathf.Atan2(2.0f * WB * Mathf.Sin(alpha), 1.0f);
        return delta;
    }

    public float proportional_control(float target, float current)
    {
        float a = Kp * (target - current);

        return a;
    }

    void Update()
    {
        HandBrake();

        //for tyre rotate
        WheelFLtrans.Rotate(WheelFL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        WheelFRtrans.Rotate(WheelFR.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        WheelRLtrans.Rotate(WheelRL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        WheelRRtrans.Rotate(WheelRL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        //changing tyre direction
        Vector3 temp = WheelFLtrans.localEulerAngles;
        Vector3 temp1 = WheelFRtrans.localEulerAngles;
        temp.y = WheelFL.steerAngle - (WheelFLtrans.localEulerAngles.z);
        WheelFLtrans.localEulerAngles = temp;
        temp1.y = WheelFR.steerAngle - WheelFRtrans.localEulerAngles.z;
        WheelFRtrans.localEulerAngles = temp1;
        eulertest = WheelFLtrans.localEulerAngles;

        if (Vector3.Distance(transform.localPosition, destination) < stoppingDistance)
        {
            reachedDestination = true;
        }

    }
    void HandBrake()
    {
        //Debug.Log("brakes " + braked);
        if (Input.GetButton("Jump"))
        {
            braked = true;
        }
        else
        {
            braked = false;
        }
        if (braked)
        {
            WheelRL.brakeTorque = maxBrakeTorque * 20;//0000;
            WheelRR.brakeTorque = maxBrakeTorque * 20;//0000;
            WheelRL.motorTorque = 0;
            WheelRR.motorTorque = 0;
        }
    }

    public void setDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }
}
