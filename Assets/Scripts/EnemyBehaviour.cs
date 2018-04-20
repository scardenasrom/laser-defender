using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public int maxNumberOfHits = 2;
    public GameObject laser;
    public float laserSpeed = 5f;
    public float shotsPerSeconds = 0.5f;
    public int pointsPerKill = 25;
	
	void Update () {
        float probability = Time.deltaTime * shotsPerSeconds;
        if (Random.value < probability) {
            Fire();
        }
	}

    void Fire() {
        Vector3 startPosition = transform.position + new Vector3(0, -1f, 0);
        GameObject spawnedLaser = SimplePool.Spawn(laser, startPosition, Quaternion.identity);
        spawnedLaser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, (-1) * laserSpeed, 0);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        Proyectile proyectile = collider.GetComponent<Proyectile>();
        if (proyectile) {
            proyectile.Hit();
            maxNumberOfHits = maxNumberOfHits - proyectile.damage;
            if (maxNumberOfHits <= 0) {
                ScoreKeeper scoreText = GameObject.Find("ScoreText").GetComponent<ScoreKeeper>();
                scoreText.Score(pointsPerKill);
                SimplePool.Despawn(gameObject);
            }
        }
    }

}
