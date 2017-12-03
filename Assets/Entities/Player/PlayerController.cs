using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject Projectile;
    public float shipSpeed = 15f;
    public float padding = 1f;
    public float projectileSpeed;
    public float fireRate = 0.2f;
    public float health = 250;

    float xmin;
    float xmax;
    

    void Start ()
    {
        Camera camera;
        camera = Camera.main;
        float distance = transform.position.z - camera.transform.position.z;
        Vector3 leftmost = camera.ViewportToWorldPoint(new Vector3(0,0,distance));
        Vector3 rightmost = camera.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
    }
	
	void Update () {

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * shipSpeed * Time.deltaTime;
        } 
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * shipSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, fireRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
        // Restrict player to gamespace
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}

    void Fire()
    {
        Vector3 offset = new Vector3(0, 1, 0);
        GameObject beam = Instantiate(Projectile, transform.position + offset, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile missle = collider.gameObject.GetComponent<Projectile>();
        if (missle)
        {
            Debug.Log("Player Collided With Missle");
            missle.Hit();
            health -= missle.GetDamage();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
