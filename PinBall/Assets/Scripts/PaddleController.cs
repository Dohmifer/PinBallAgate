using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public KeyCode input;
    public float springPower = 1000;
    private HingeJoint hinge;

    // menyimpan angka target position saat ditekan dan dilepas
    private float targetPressed;
    private float targetRelease;

    // Start is called before the first frame update
    void Start()
    {
        hinge = GetComponent<HingeJoint>();

        targetPressed = hinge.limits.max;
        targetRelease = hinge.limits.min;
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
    }

    public void ReadInput()
    {
        //Mengambil Komponent Spring pada Hinge Joint
        JointSpring jointSpring = hinge.spring;

        //Mengubah value spring saat input tekan dan di lepas
        if(Input.GetKey(input))
        {
            jointSpring.spring = springPower;
        }
        else
        {
            jointSpring.spring = 0;
        }

        //mengubah spring pada hinge joint dengan value yang sudah di ubah
        hinge.spring = jointSpring;
    }
}
