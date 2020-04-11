using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class Movement : MonoBehaviour
{
    public float walkSpeed = 0.1f;
    public Camera activeCam;
    public Rigidbody2D rigidBody;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 mousePos = activeCam.ScreenToWorldPoint(Input.mousePosition);

        float mouseX = mousePos.x;
        float mouseY = mousePos.y;

        float lookAngle = Mathf.Round(Vector2.Angle(new Vector2(0, 0 - 1), new Vector2(mouseX - transform.position.x, mouseY - transform.position.y)) / 45) * 45;
        float walkAngle = Vector2.Angle(new Vector2(0, -1), new Vector2(h, v));

        float speed = walkSpeed;

        if (h < 0) walkAngle = 360 - walkAngle;

        if (mouseX < transform.position.x) {
            spriteRenderer.flipX = true;
            lookAngle = 360 - lookAngle;
        }
        if (mouseX > transform.position.x) {
            spriteRenderer.flipX = false;
        }

        if (h == 0 && v == 0) {
            if (lookAngle == 0 || lookAngle == 360)         animator.Play("south_idle");
            else if (lookAngle == 45 || lookAngle == 315)   animator.Play("southeast_idle");
            else if (lookAngle == 90 || lookAngle == 270)   animator.Play("east_idle");
            else if (lookAngle == 135 || lookAngle == 225)  animator.Play("northeast_idle");
            else if (lookAngle == 180)                      animator.Play("north_idle");
        }
        else {
            if (Mathf.Abs(lookAngle - walkAngle) <= 90 || Mathf.Abs(lookAngle - walkAngle) > 270) {
                speed = walkSpeed;

                if (lookAngle == 0 || lookAngle == 360)         animator.Play("south_walk");
                else if (lookAngle == 45 || lookAngle == 315)   animator.Play("southeast_walk");
                else if (lookAngle == 90 || lookAngle == 270)   animator.Play("east_walk");
                else if (lookAngle == 135 || lookAngle == 225)  animator.Play("northeast_walk");
                else if (lookAngle == 180)                      animator.Play("north_walk");
            }
            else {
                speed = walkSpeed * 0.6f;

                if (lookAngle == 0 || lookAngle == 360)         animator.Play("south_walk_reverse");
                else if (lookAngle == 45 || lookAngle == 315)   animator.Play("southeast_walk_reverse");
                else if (lookAngle == 90 || lookAngle == 270)   animator.Play("east_walk_reverse");
                else if (lookAngle == 135 || lookAngle == 225)  animator.Play("northeast_walk_reverse");
                else if (lookAngle == 180)                      animator.Play("north_walk_reverse");
            }
        }

        Vector3 moveDir = new Vector3(h, v, 0).normalized;

        rigidBody.MovePosition(rigidBody.transform.position + moveDir * speed);
    }
}
