using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyProjectilePrefab;
    public GameObject basicEnemyPrefab;
    public float movementSpeed = 1f;

    public float width = 10f;
    public float height = 5f;

    private float padding = 0.5f;
    private float minX;
    private float maxX;

    private bool movingRight = true;

	void Start () {
        SimplePool.Preload(enemyProjectilePrefab, 20);
        SimplePool.Preload(basicEnemyPrefab, 10);

        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));
        minX = leftMost.x + padding;
        maxX = rightMost.x - padding;

        SpawnEnemyShips();
	}
	
	void Update () {
		if (movingRight) {
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        } else {
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
        }
        
        if (transform.position.x - (width/2) < minX) {
            movingRight = true;
        } else if (transform.position.x + (width / 2) > maxX) {
            movingRight = false;
        }

        if (AllMembersDead()) {
            Invoke("SpawnEnemyShips", 2f);
        }
	}

    void SpawnEnemyShips() {
        foreach (Transform child in transform) {
            GameObject spawnedEnemy = SimplePool.Spawn(basicEnemyPrefab, child.transform.position, Quaternion.identity);
            spawnedEnemy.GetComponent<EnemyBehaviour>().maxNumberOfHits = 2;
            spawnedEnemy.transform.parent = child;
        }
        CancelInvoke();
    }

    bool AllMembersDead() {
        foreach (Transform childPosition in transform) {
            if (childPosition.GetChild(0).gameObject.activeInHierarchy) {
                return false;
            }
        }
        return true;
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0f));
    }

}
