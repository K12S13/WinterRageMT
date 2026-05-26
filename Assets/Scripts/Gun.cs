using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera cm;
    public GameObject bulletPrefab;
    public Transform muzzlePoint;

    public float bulletSpeed = 100f;

    private void Shoot()
    {
        Ray ray = cm.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(1000);
        }

        Vector3 direction = (targetPoint - muzzlePoint.position).normalized;

        GameObject bullet = Instantiate(
            bulletPrefab,
            muzzlePoint.position,
            Quaternion.LookRotation(direction)
        );

        Bullet bulletState = bullet.GetComponent<Bullet>();

        bulletState.isPlayerBullet = true;
    

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        rb.linearVelocity = direction * bulletSpeed;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
}