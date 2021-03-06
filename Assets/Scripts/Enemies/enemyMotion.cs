﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMotion : MonoBehaviour {
	public Vector3 aimedPosition;
	public float speed = 1f;
	private float x;
	private float y;
	public float damage = 10;
	public float damageOnPlayerHit = 1;
	enemyHealth _enemyHealth;
	PlayerHealth playerHealth;
	public float attackRange = 2.5f;

	// Use this for initialization
	void Start () {
		aimedPosition = new Vector3(Random.Range (-3, 3), Random.Range (-3, 3));
		playerHealth = GameObject.Find("player").GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		aimedPosition = GameObject.Find("player").transform.position;
		Vector3 diffPosition = aimedPosition - transform.position;

		if (diffPosition.magnitude < this.attackRange) {
			GetComponent <enemyAttack> ().checkAttack ();
		} else {
			if (diffPosition.magnitude < speed * Time.deltaTime) {
				transform.position = aimedPosition;
				
			} else {
				transform.Translate (diffPosition.normalized * speed * Time.deltaTime);
			}
		}
	}
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
			// Destroy(collision.gameObject.GetComponent<bulletControler>() );
			_enemyHealth = GetComponent <enemyHealth> ();
			_enemyHealth.TakeDamage ((int)(collision.gameObject.GetComponent<bulletControler> ().damage), new Vector3(0, 0, 0));
			Debug.Log (collision.gameObject.GetComponent<bulletControler> ().damage);
        }
		if (collision.gameObject.tag == "Player")
		{
			// Destroy(gameObject);
			playerHealth.TakeDamage ((int)(this.damageOnPlayerHit));
		}
    }
}
