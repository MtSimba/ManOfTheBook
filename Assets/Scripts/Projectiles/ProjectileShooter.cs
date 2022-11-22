using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    private Camera cam;
    private GameObject projectile;
    private Transform firePoint;
    public float projectileSpeed = 30;

    private Vector3 destination;

    public ProjectileShooter(Camera camC, GameObject projectileC, Transform firePointC)
    {
        cam = camC;
        projectile = projectileC;
        firePoint = firePointC;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            Debug.Log("Fireball SHOT");
            ShootProjectile();
        }
    }

    public void ShootProjectile()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
            destination = ray.GetPoint(1000);

        InstantiateProjectile();
    }

    void InstantiateProjectile()
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
    }
}
