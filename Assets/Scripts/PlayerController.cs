using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int health = 1000;
    public float movementSpeed = 10f;
    public GameObject projectile;
    public float projectileSpeed = 10f;
    public float fireRate = 0.2f;

    private Rigidbody2D rb;

    private float padding = 0.55f;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

	void Start () {
        SimplePool.Preload(projectile, 10);
        rb = GetComponent<Rigidbody2D>();
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));
        minX = leftMost.x + padding;
        maxX = rightMost.x - padding;
        minY = leftMost.y + padding;
        maxY = rightMost.y - padding;
	}
	
	void FixedUpdate () {
        Vector2 pos = rb.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        rb.position = pos;

        float horizontalAxis = Input.GetAxis("Horizontal");
        float horizontalSpeed = horizontalAxis * movementSpeed;
        /*float verticalAxis = Input.GetAxis("Vertical");
        float verticalSpeed = verticalAxis * movementSpeed;
        rb.velocity = new Vector2(horizontalSpeed, verticalSpeed);*/
        rb.velocity = new Vector2(horizontalSpeed, 0);

        if (Input.GetKeyDown(KeyCode.Space)) {
            InvokeRepeating("Fire", 0.00001f, fireRate);
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            CancelInvoke("Fire");
        }
	}

    void Fire() {
        Vector3 startPosition = transform.position + new Vector3(0, 0.6f, 0);
        GameObject spawnedProjectile = SimplePool.Spawn(projectile, startPosition, Quaternion.identity);
        spawnedProjectile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        Proyectile proyectile = collider.GetComponent<Proyectile>();
        if (proyectile) {
            health -= proyectile.damage;
            proyectile.Hit();
            if (health <= 0) {
                Destroy(gameObject);
            }
        }
    }

}
