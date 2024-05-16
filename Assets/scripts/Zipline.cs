using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class Zipline : MonoBehaviour
{
    [SerializeField] private Zipline targetZip;
    [SerializeField] private float zipSpeed = 5f;
    [SerializeField] private float zipScale = 0.2f;

    [SerializeField] private float arrivalThreshold = 0.4f;
    [SerializeField] private LineRenderer cable;
    public Transform zipTransform;
    private bool _zipping = false;
    private GameObject _localZip;
    [SerializeField] private Animator mAnimator;
    
    private void Awake()
    {
       cable.SetPosition(0, zipTransform.position);
       cable.SetPosition(1, targetZip.zipTransform.position);
    }
    
    private void Update()
    {
        if (!_zipping || _localZip == null) return;
        
        _localZip.GetComponent<Rigidbody>().AddForce((targetZip.zipTransform.position - zipTransform.position).normalized * zipSpeed * Time.deltaTime,
                ForceMode.Acceleration);

        if (Vector3.Distance(_localZip.transform.position, targetZip.zipTransform.position) <= arrivalThreshold)
        {
            ResetZipline();
        }
    } 
    public void StartZipline(GameObject player)
    {
        _localZip = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _localZip.transform.position = zipTransform.position;
        _localZip.transform.localScale = new Vector3(zipScale, zipScale, zipScale);
        _localZip.AddComponent<Rigidbody>().useGravity = false;
        _localZip.GetComponent<Collider>().isTrigger = true;

        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.parent = _localZip.transform;
        _zipping = true;

        player.GetComponent<Animator>().SetBool("Zipping",true);
    }

    private void ResetZipline()
    {
        if (!_zipping) return;

        GameObject player = _localZip.transform.GetChild(0).gameObject;
        player.GetComponent<Rigidbody>().useGravity = true;
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<CharacterController>().enabled = true;
        player.transform.parent = null;
        Destroy(_localZip);
        _localZip = null;
        _zipping = false;
        
        player.GetComponent<Animator>().SetBool("Zipping",false);
    }
}
