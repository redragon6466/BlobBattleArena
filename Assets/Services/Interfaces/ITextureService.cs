using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UnityEngine;

namespace Assets.Services.Interfaces
{
    public interface ITextureService
    {
        
        //Actual Stuff here
        Sprite FindSprite(string spriteName);
        string[] GiveImageNames();
        bool SetSpriteOnRenderer(string spriteName, SpriteRenderer targetSpriteRenderer);
    }
}