using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    [SerializeField]
    private IClass Class;


    [SerializeField]
    private Tuple<int, int> position { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
