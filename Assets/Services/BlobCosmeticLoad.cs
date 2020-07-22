using System.Collections;
using Assets.Services.Interfaces;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Services
{
    public class BlobCosmeticLoad : ITextureService
    {
        private Sprite[] sprites;
        private Sprite PlaceHolderSprite;
        private static BlobCosmeticLoad instance = null;
        private static readonly object padlock = new object();       

        public static BlobCosmeticLoad Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new BlobCosmeticLoad();
                    }
                    return instance;
                }
            }
        }
        /** Constructor. Attempt to load sprites into an array.
         * If length of list created is 0 or sprites list is null
         * output an error message. 
         * 
         * 
         */
        BlobCosmeticLoad()
        {
            loadSprites();
            if (sprites.Length == 0 || sprites == null)
            {
                Debug.Log("ERROR - BlobCosmeticLoad - No sprites were loaded.");
            }
            //Replace this with a reference to a default sprite of some sort. 
            PlaceHolderSprite = FindSprite("BlobSprite");
            //set up a default/failover sprite
        }

        /**Load in all objects in the resources/Sprites file. 
         * Then iterate through all objects loaded, and if they are Sprite objects, add them
         * to the private Sprites[] array. 
         * 
         */
        private void loadSprites()
        {
            //sprites = Resources.FindObjectsOfTypeAll<Sprite>();
            //sprites = Resources.LoadAll<Sprite>("resources/BlobSprites");
            sprites = Resources.LoadAll<Sprite>("BlobSprites");
            Debug.Log("A total of "+ sprites.Length + " sprites found and loaded.");
            
        }
        /**
         * findSprite
         * Takes a string. Looks in the list of sprites loaded into it's list, and tries to find a sprite with a matching name property
         * ex: if it was Kappa, it would look for a sprite with a name of Kappa in the pre-loaded list.
         * 
         */
        public Sprite FindSprite(string spriteName)
        {
            foreach (Sprite nextSprite in sprites)
            {
                if (nextSprite.name == spriteName)
                {
                    return nextSprite;
                }
            }
            return sprites[0];
        }
        /**Inputs: SpriteName - Name of a Sprite Loaded in Memory - String
         * targetSpriteRenderer - Target SpriteRenderer Object to attempt to set values on - Sprite Renderer
         * Return values
         * True - A sprite with the given input name was found, and an attempt was made to set it on target sprite renderer
         * False - A sprite with the given input name was not found. 
         * 
         */
        public bool SetSpriteOnRenderer(string spriteName, SpriteRenderer targetSpriteRenderer)
        {
            foreach (Sprite nextSprite in sprites)
            {
                if (nextSprite.name == spriteName)
                {
                    targetSpriteRenderer.sprite = nextSprite;
                    return true;
                }
            }
            return false;
        }
        /**Return an array of strings, consisting of the names of all sprites currently loaded into service memory.
         * 
         * 
         * 
         */
        public string[] GiveImageNames()
        {
            List<string> loadedImages = new List<string>();
            foreach (Sprite nextSprite in sprites)
            {
                loadedImages.Add(nextSprite.name);
            }
            string[] returnImages = loadedImages.ToArray();
            return returnImages;
        }
    }
}
