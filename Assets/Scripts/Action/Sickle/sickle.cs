using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sickle : MonoBehaviour
{
    public bool equiped;
    bool attacking = false;
    public int baseDamage = 10;
    public float durability = 10;
    // public PlayerLook playerVision;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(equiped && !attacking && Input.GetMouseButton(0))
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        attacking = true;

        anim.Play("attack_1");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length * .7f);

        attacking = false;
    }

    public void HandleCollision(GameObject hit)
    {
        if(!attacking)
            return;
        Debug.Log("Hit: " + hit.name);

        IDamageable damageableObject = hit.GetComponent<IDamageable>();
        if(damageableObject == null)
            return;

        damageableObject.TakeDamage(baseDamage);
        durability--;
    }
}
