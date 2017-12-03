using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    public GameObject projectile;
    public float projectileSpeed = 10;
    public float health = 150;
    public float shotsPerSecond = 0.5f;

    private void Update()
    {
        float probability = Time.deltaTime * shotsPerSecond;
        if (Random.value < probability) {
            Fire();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile missle = collider.gameObject.GetComponent<Projectile>();
        if (missle)
        {
            missle.Hit();
            health -= missle.GetDamage();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
        GameObject missle = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        missle.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }
}
