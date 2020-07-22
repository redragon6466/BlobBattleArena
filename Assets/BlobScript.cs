using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Assets.Services;

namespace Assets
{
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
        private int InitiativeBonus;
        [SerializeField]
        private int _maxHealth;
        private int Movement;
        private IClass _class;
        private IBrain _brain;
        private God _god;
        private Slider _healthBar;
        private bool _isStealth;
        [SerializeField]
        private int _blobGridX;
        [SerializeField]
        private int _blobGridY;

        public int GetAttack()
        {
            return Attack;
        }


        public IClass GetClass()
        {
            return _class;
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
        public int GetInitiativBonus()
        {
            return InitiativeBonus;
        }

        public BrainEnum GetBrainType()
        {
            return _brain.GetBrainType();
        }

        public bool IsStealth()
        {
            return _isStealth;
        }

        public bool IsInRange(BlobScript target)
        {
            foreach (var attack in _class.AttackList)
            {
                if (attack.InRange(this, target))
                {
                    return true;
                }
            }
            return false;
        }

        public void SetGridLocation(int x, int y)
        {
            _blobGridX = x;
            _blobGridY = y;
        }
        public Vector2 GetGridLocation()
        {
            return new Vector2(_blobGridX, _blobGridY);
        }

        public List<BaseAttack> MoveSet()
        {
            return _class.AttackList;
        }

        //Version 1 
        //Fetch the Sprite Renderer
        //Change the Sprite Value
        //Reload
        
        public bool setSprite(string SpriteName)
        {
            
            SpriteRenderer mrBlob = this.GetComponent<SpriteRenderer>();
            //Call the BlobCosmeticLoad
            Sprite sprites = Resources.Load<Sprite>("sprites/Kappa");            
            //Debug.Log("Length is" + sprites.Length);
            mrBlob.sprite = sprites;
            Debug.Log("Sprite Name = " + sprites.name );

            return false;
        }

        public void setRandomSprite()
        {
            SpriteRenderer mrBlob = this.GetComponent<SpriteRenderer>();
            //BlobCosmeticLoad.Instance.
            string[] loadedImages = BlobCosmeticLoad.Instance.GiveImageNames();
            //string randomImage = loadedImages[loadedImages.Length - 1];
            string randomImage = loadedImages[0];
            BlobCosmeticLoad.Instance.SetSpriteOnRenderer(randomImage, mrBlob);
            foreach (string x in loadedImages)
            {
                Debug.Log("Sprite Name = " + x);
            }
            Debug.Log("Sprite Name = " + randomImage);
            
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



        // Start is called before the first frame update
        void Start()
        {
            _healthBar = GetComponentInChildren<Canvas>().GetComponentInChildren<Slider>();
            _healthBar.maxValue = Health;
            _healthBar.value = Health;
            Text barText = (Text)_healthBar.GetComponentInChildren(typeof(Text));
            barText.text = string.Format("{0}/{1}", _healthBar.value, _healthBar.maxValue);
            setRandomSprite();
            //Debug.Log("");
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
            //Initiative = _class.Initiative;
            Initiative = Random.Range(0, 20) + _class.Initiative;
            InitiativeBonus = _class.Initiative;
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
}

