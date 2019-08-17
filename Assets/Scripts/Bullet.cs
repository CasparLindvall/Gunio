using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void Start()
    {
        Destroy(this.gameObject, 4);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        string otherTag = col.gameObject.tag;
        switch (otherTag)
        {
            case "Ground":
                Destroy(this.gameObject);
                break;
            case "Wall":
                Destroy(this.gameObject);
                break;
            case "Turret":
                //Should place bullet outside of turret hitbox
                break;
        }
    }

}
