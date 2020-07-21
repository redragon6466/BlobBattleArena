using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BlobCosmeticLoad 
{
    private Sprite[] sprites;
    private Sprite PlaceHolderSprite;
    private static BlobCosmeticLoad instance = null;
    private static readonly object padlock = new object();    

    BlobCosmeticLoad()
    {
        loadSprites();
        if(sprites.Length == 0 || sprites == null)
        {
            Debug.Log("Failed to find any sprites to load");
        }
        //Replace this with a reference to a default sprite of some sort. 
        PlaceHolderSprite = FindSprite("BlobSprite");
        //set up a default/failover sprite
    }
    
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
    /**Load in all objects in the resources/Sprites file. 
     * Then iterate through all objects loaded, and if they are Sprite objects, add them
     * to the private Sprites[] array. 
     * 
     */
    private void loadSprites()
    {
        sprites = Resources.FindObjectsOfTypeAll<Sprite>();
    }
    /**
     * findSprite
     * Takes a string. Looks in the list of sprites loaded into it's list, and tries to find a sprite with a matching name property
     * ex: if it was Kappa, it would look for a sprite with a name of Kappa in the pre-loaded list.
     * 
     */
    public Sprite FindSprite(string spriteName)
    {
        foreach(Sprite nextSprite in sprites)
        {
            if(nextSprite.name == spriteName)
            {
                return nextSprite;
            }
        }
        return sprites[0];
    }
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
}
