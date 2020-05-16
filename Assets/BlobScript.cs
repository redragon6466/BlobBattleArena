using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BlobScript : MonoBehaviour
{
    [SerializeField]
    private int Health;
    [SerializeField]
    private int Attack;
    [SerializeField]
    private int SpecialAttack;
    [SerializeField]
    private int Defense;
    [SerializeField]
    private int Initiative;
    [SerializeField]
    private int _maxHealth;
    private int Movement;
    private IClass _class;
    private IBrain _brain;
    private God _god;
    private Slider _healthBar;
    private bool _isStealth;

    public int GetAttack()
    {
        return Attack;
    }

    public int GetSpecialAttack()
    {
        return SpecialAttack;
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

    public BrainEnum GetBrainType()
    {
        return _brain.GetBrainType();
    }

    public bool IsStealth()
    {
        return _isStealth;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            _god.KillBlob(this);
        }
    }


    public void RestoreHealth(int heal)
    {
        Health += heal;

        if (Health > _maxHealth)
        {
            Health = _maxHealth;
        }
    }

    [SerializeField]
    private Tuple<int, int> position { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        _healthBar = GetComponentInChildren<Canvas>().GetComponentInChildren<Slider>();
        _healthBar.maxValue = Health;
        _healthBar.value = Health;
        Text barText = (Text)_healthBar.GetComponentInChildren(typeof(Text));
        barText.text = string.Format("{0}/{1}", _healthBar.value, _healthBar.maxValue);
    }

    public void Awake()
    {
        int gameStatusCount = FindObjectsOfType<BlobScript>().Length;
        if (gameStatusCount > 6)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject); //when the scene changes don't destroy the game object that owns this
        }
    }

    public void SetClass(IClass clas, IBrain brain, God god)
    {
        _class = clas;
        _brain = brain;
        Health = _class.Health;
        _maxHealth = _class.Health;
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
        _healthBar.value = Health;
        Text barText = (Text)_healthBar.GetComponentInChildren(typeof(Text));
        barText.text = string.Format("{0}/{1}", _healthBar.value, _healthBar.maxValue);
    }
}
