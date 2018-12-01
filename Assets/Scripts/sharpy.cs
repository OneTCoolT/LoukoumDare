﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sharpy : MonoBehaviour {

	public Vector3 aimedPosition;
	public float speed = 1f;
	private float SPEED_BOOST = 3f;
	private float SPEED_STANDARD = 1f;
	private float x;
	private float y;
	public float timeSinceLastChangeState = 0;
	public int state = 0;
	private int STANDARD = 0;
	private int BOOST = 1;
	private float currentJerk = 0;
	public float jerkIncrementBase = 0.5f;

	public float BOOST_TIME_DURATION = 1f;
	public float STANDARD_TIME_DURATION = 2f;
	public float timeToNextJerk = 2f;
	public float JERK_DELAY = 0.5f;
	public float MAX_JERK_RANGE = 0.01f;

	void Start () {
		// aimedPosition = new Vector3(Random.Range (1, 10), Random.Range (0, 10));
	}

	void checkState () {
		Debug.Log (this.timeSinceLastChangeState);
		this.timeSinceLastChangeState += Time.deltaTime;
		if (state == BOOST) {
			if (this.timeSinceLastChangeState > this.BOOST_TIME_DURATION) {
				this.timeSinceLastChangeState =  0;	
				this.state = STANDARD;
				this.speed = SPEED_STANDARD;
			}
		} else if (state == STANDARD) {
			if (this.timeSinceLastChangeState > this.STANDARD_TIME_DURATION) {
				this.timeSinceLastChangeState =  0;	
				this.state = BOOST;
				this.speed = SPEED_BOOST;
			}
		}

	}
	void checkIncrementJerk () {
		if (timeToNextJerk > JERK_DELAY) {
			this.currentJerk = Random.Range (0f, MAX_JERK_RANGE);
			timeToNextJerk = 0;
		}
		timeToNextJerk += Time.deltaTime;
	}

	void Update () {
		float AngleRad = Mathf.Atan2(aimedPosition.y - transform.position.y, aimedPosition.x - transform.position.x);
		float AngleDeg = (180 / Mathf.PI) * AngleRad;
		this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg); 

		Vector3 diffPosition = aimedPosition - transform.position;
		Vector3 vectorMotion = diffPosition.normalized * speed * Time.deltaTime;
		this.checkState ();
		this.checkIncrementJerk();

		// compute shift right/left
		// Vector3 vectorShift = transform.right.normalized * 0.01f;

		if (diffPosition.magnitude < speed * Time.deltaTime) {
			transform.position = aimedPosition;

		} else {
			transform.Translate (vectorMotion);
		}
	}
}
