using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJoint : MonoBehaviour
{
    HingeJoint joint;
    // Start is called before the first frame update
    void Start()
    {
        joint = this.GetComponent<HingeJoint>();
        var motor = joint.motor;
        motor.force = 100;
        joint.useMotor = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
