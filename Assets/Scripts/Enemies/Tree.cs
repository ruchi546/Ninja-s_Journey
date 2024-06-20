using UnityEngine;

public class Tree : Enemy 
{ 

    void FixedUpdate() 
    {
        PerformMovement();
    }

    void PerformMovement() 
    {
        target = GameObject.FindWithTag("Player").transform;
        if (target == null) 
        {
            return;
        }

        animator.SetBool("isWalking", true);

        if (Vector2.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius) 
        {
            movement = (target.position - transform.position).normalized;

            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            Vector2 temp = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            rb.MovePosition(temp);

            animator.SetFloat("moveX", movement.x);
            animator.SetFloat("moveY", movement.y);
        }
        else 
        {
            transform.position = Vector2.MoveTowards(transform.position, homePosition.position, moveSpeed * Time.deltaTime);
        }
    }
}