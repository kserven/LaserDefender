using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour {
    public GameObject enemyPrefab;
    public AudioClip enemySpawnSFX;
    public float width = 10f;
    public float height = 5f;
    public float enemySpeed = 5f;
    public float padding = 1f;
    public float spawnDelay = 0.5f;

    private float xmin;
    private float xmax;
    private bool enemyDirectionRight = true;

	// Use this for initialization
	void Start () {
        Camera camera;
        camera = Camera.main;
        float distance = transform.position.z - camera.transform.position.z;
        Vector3 leftmost = camera.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = camera.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = (leftmost.x + padding);
        xmax = (rightmost.x - padding);

       SpawnUntilFull();
    }

    private void Update()
    {
        if (enemyDirectionRight)
        {
            transform.position += Vector3.right * enemySpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * enemySpeed * Time.deltaTime;
        }
        float LeftofFormation = transform.position.x - (0.5f * width);
        float RightofFormation = transform.position.x + (0.5f * width);
        if (LeftofFormation < xmin)
        {
            enemyDirectionRight = true;
        }    
        else if (RightofFormation > xmax)
        {
            enemyDirectionRight = false;
        }

        if (AllMembersDead())
        {
           SpawnUntilFull();
        }
    }

    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }
        return null;
    }

    bool AllMembersDead()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }


    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if (freePosition)
        {
            AudioSource.PlayClipAtPoint(enemySpawnSFX, transform.position, 1f);
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if (NextFreePosition()) {
            Invoke("SpawnUntilFull", spawnDelay);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
}
