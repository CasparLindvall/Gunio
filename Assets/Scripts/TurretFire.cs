using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFire : MonoBehaviour
{
    public Rigidbody2D bulletPrefab;
    public float bulletSpeed = 10;
    public float firerate = 2.0f;
    public float MaxDistanceToFire;
    public bool shootStraight;

    private Transform target;
    private float yOffset = 0.2f;
    public bool upsideDown;


    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
        if(upsideDown)
            yOffset *= -1;

    }


    // Either shoot straight ahead or towards the player.
    void Start()
    {
        if (shootStraight)
            InvokeRepeating("FireStraight", 0.1f, firerate);
        else
            InvokeRepeating("Fire", 1f, firerate);
    }

    void Fire()
    {
        var currDistance = Vector3.Distance(transform.position, target.position);
        if (currDistance < MaxDistanceToFire)
        {
            var oldRotation = transform.rotation;
            transform.LookAt(target);

            Vector3 newPos = transform.position;
            newPos.y += yOffset;
            newPos.z = 0;
            var bullet = Instantiate(bulletPrefab, newPos, oldRotation);
            bullet.velocity = transform.forward * bulletSpeed;
            transform.rotation = oldRotation;
        } 
            
    }

    void FireStraight()
    {
        Vector3 newPos = transform.position;
        newPos.y += yOffset;
        newPos.z = 0;
        var bulletStraight = Instantiate(bulletPrefab, newPos, transform.rotation);
        bulletStraight.velocity = -transform.right * bulletSpeed;
    }
}
