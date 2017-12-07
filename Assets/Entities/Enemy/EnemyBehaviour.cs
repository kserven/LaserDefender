using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    public GameObject projectile;
    public AudioClip enemyLaserSFX;
    public AudioClip enemyDestroyedSFX;
    public float projectileSpeed = 10;
    public float health = 150;
    public float shotsPerSecond = 0.5f;
    public int scoreValue = 150;

    private ScoreKeeper scoreKeeper;

    private void Start()
    {
            scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }
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
                Die();
            }
        }
    }
    void Fire()
    {
        AudioSource.PlayClipAtPoint(enemyLaserSFX, transform.position, 1f);
        Vector3 startPosition = transform.position;
        GameObject missle = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        missle.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(enemyDestroyedSFX, transform.position, 1f);
        Destroy(gameObject);
        scoreKeeper.Score(scoreValue);
    }
}
