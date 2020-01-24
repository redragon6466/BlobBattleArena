﻿using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlobScript : MonoBehaviour
{


    [SerializeField]
    private int Health;
    [SerializeField]
    private int Attack;
    [SerializeField]
    private int Defense;
    [SerializeField]
    private int Initiative;
    [SerializeField]
    private int Movement;
    private IClass _class;
    private IBrain _brain;
    private God _god;

    public int GetAttack()
    {
        return Attack;
    }

    public int GetDefense()
    {
        return Defense;
    }

    public int GetHealth()
    {
        return Health;
    }


    public int GetMovement()
    {
        return Movement;
    }

    public int GetInitiative()
    {
        return Initiative;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            _god.KillBlob(this);
        }
    }

    [SerializeField]
    private Tuple<int, int> position { get; set; }


    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetClass(IClass clas, IBrain brain, God god)
    {
        _class = clas;
        _brain = brain;
        Health = _class.Health;
        Attack = _class.Attack;
        Defense = _class.Defense;
        Movement = _class.Movement;
        Initiative = Random.Range(0, 20);
        _god = god;
    }

    public void TakeTurn(BlobScript me, List<BlobScript> allyBlobs, List<BlobScript> enemyBlobs)
    {
        _brain.TakeTurn(me, allyBlobs, enemyBlobs);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
