using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] float minX, maxX, minY, maxY;
    [SerializeField] Transform target;
    [SerializeField] float FolowSpeed;
    Animator anim;

    public static PlayerCamera instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {

        if(!target) return;

        transform.position = Vector3.Lerp(transform.position,
            new Vector3(
                Mathf.Clamp(target.position.x, minX, maxX),
                Mathf.Clamp(target.position.y, minY, maxY),
                -10),
                FolowSpeed * Time.fixedDeltaTime);
    }

    public void CameraShake()
    {
        anim.Play("ShakeCamera");
    }

}

