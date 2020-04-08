using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class Movement : MonoBehaviour
{
    public GameObject obj;
    public Camera mainCam;
    public float speed = 0.1f;
    public float lookAngle = 0;
    public float walkAngle = 0;
    public float mouseX = 0;
    public float mouseY = 0;
    public Rigidbody2D rigidBody;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public void FixedUpdate()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(obj.transform.position.y * 100f) * -1;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        mouseX = mousePos.x;
        mouseY = mousePos.y;

        lookAngle = Vector2.Angle(new Vector2(0, 0 - 1), new Vector2(mouseX - obj.transform.position.x, mouseY - obj.transform.position.y));
        walkAngle = Vector2.Angle(new Vector2(0, -1), new Vector2(h, v));

        /*
        if (h < 0) spriteRenderer.flipX = true;
        if (h > 0) spriteRenderer.flipX = false;

        if (h == 0 && v < 0)    animator.runtimeAnimatorController = S;
        if (h > 0 && v < 0)     animator.runtimeAnimatorController = SE;
        if (h > 0 && v == 0)    animator.runtimeAnimatorController = E;
        if (h > 0 && v > 0)     animator.runtimeAnimatorController = NE;
        if (h == 0 && v > 0)    animator.runtimeAnimatorController = N;
        if (h < 0 && v > 0)     animator.runtimeAnimatorController = NE;
        if (h < 0 && v == 0)    animator.runtimeAnimatorController = E;
        if (h < 0 && v < 0)     animator.runtimeAnimatorController = SE;
        */

        if (h < 0) walkAngle = -walkAngle;

        if (mouseX < obj.transform.position.x) { spriteRenderer.flipX = true; lookAngle = -lookAngle;}
        if (mouseX > obj.transform.position.x) spriteRenderer.flipX = false;

        if (h == 0 && v == 0) {
            if      (Mathf.Abs(lookAngle) < 22.5)  animator.Play("south_idle");
            else if (Mathf.Abs(lookAngle) < 67.5)  animator.Play("southeast_idle");
            else if (Mathf.Abs(lookAngle) < 112.5) animator.Play("east_idle");
            else if (Mathf.Abs(lookAngle) < 157.5) animator.Play("northeast_idle");
            else if (Mathf.Abs(lookAngle) < 180)   animator.Play("north_idle");
        }
        else {
            if (Mathf.Abs(lookAngle - walkAngle) <= 90) {
                speed = 0.1f;

                if      (Mathf.Abs(lookAngle) < 22.5)  animator.Play("south_walk");
                else if (Mathf.Abs(lookAngle) < 67.5)  animator.Play("southeast_walk");
                else if (Mathf.Abs(lookAngle) < 112.5) animator.Play("east_walk");
                else if (Mathf.Abs(lookAngle) < 157.5) animator.Play("northeast_walk");
                else if (Mathf.Abs(lookAngle) < 180)   animator.Play("north_walk");
            }
            else {
                speed = 0.08f;

                if      (Mathf.Abs(lookAngle) < 22.5)  animator.Play("south_walk_reverse");
                else if (Mathf.Abs(lookAngle) < 67.5)  animator.Play("southeast_walk_reverse");
                else if (Mathf.Abs(lookAngle) < 112.5) animator.Play("east_walk_reverse");
                else if (Mathf.Abs(lookAngle) < 157.5) animator.Play("northeast_walk_reverse");
                else if (Mathf.Abs(lookAngle) < 180)   animator.Play("north_walk_reverse");
            }
        }

        Vector3 moveDir = new Vector3(h, v, 0).normalized;

        rigidBody.MovePosition(rigidBody.transform.position + moveDir * speed);
    }
}
