using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;
    public FixedJoystick joystick;
    
    private void FixedUpdate(){
        float moveX = joystick.Horizontal;
        float moveY = joystick.Vertical;

        gameObject.transform.position += new Vector3(moveX,moveY,0) * speed * Time.deltaTime;
    }
}
