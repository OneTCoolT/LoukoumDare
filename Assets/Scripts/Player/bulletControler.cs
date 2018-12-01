﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletControler : MonoBehaviour {
    private Vector3 moveDirection = Vector3.zero;

    void Update()
    {
        moveDirection = new Vector3(1,0,0) * Time.deltaTime*5;
        transform.Translate(moveDirection);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ennemy")
        {
            Destroy(gameObject);
        }
    }
}