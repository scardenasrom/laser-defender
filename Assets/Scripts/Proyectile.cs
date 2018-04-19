using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour {

    public int damage = 1;
    public Animation laserHitAnimation;

    private float minY;
    private float maxY;

	void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightTop = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));
        minY = leftBottom.y;
        maxY = rightTop.y;
    }
	
	void FixedUpdate () {
		if (transform.position.y >= maxY) {
            SimplePool.Despawn(gameObject);
        } else if (transform.position.y <= minY) {
            SimplePool.Despawn(gameObject);
        }
	}

    public void Hit() {
        SimplePool.Despawn(gameObject);
    }

}
