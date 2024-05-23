using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class PlayerJumpPad : MonoBehaviour
{
    public ThirdPersonController Controller;
    
    public Rigidbody rb;
    public float jumpAmount = 35;
    public float range = 1f;
    public bool grounded = false;
    public Camera Cam;
    void Update()
    {
        ShootRaycast();
    }

    void ShootRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, range))
        {

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                Controller.JumpHeight = jumpAmount;
                StartCoroutine(JumpBool());
            }
            else
            {
                Controller.JumpHeight = 2;
            }
        }
    }

    IEnumerator JumpBool()
    {
        grounded = true;
        yield return new WaitForSeconds(0.1f);
        grounded = false;
    }
}