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
    

    public int GetInitiative()
    {
        return Initiative;
    }

    [SerializeField]
    private Tuple<int, int> position { get; set; }


    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetClass(IClass clas, IBrain brain)
    {
        _class = clas;
        _brain = brain;
        Health = _class.Health;
        Attack = _class.Attack;
        Defense = _class.Defense;
        Movement = _class.Movement;
        Initiative = Random.Range(0, 20);
    }

    public void TakeTurn(GameObject me, GameObject[] allyBlobs, GameObject[] enemyBlobs)
    {
        Debug.Log("Blob Takes a turn at:" + Initiative);

        _brain.TakeTurn(me, allyBlobs, enemyBlobs);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
