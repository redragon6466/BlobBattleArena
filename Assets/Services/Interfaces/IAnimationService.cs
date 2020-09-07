using Assets.Services.Containers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Services
{
    public interface IAnimationService
    {
        void StepSquare(Vector2 startLoc, Vector2 endLoc, GameObject toMove);
        void WalkRoute(Vector2[] pathWay, GameObject toMove);
        //bool AreDone();
    }
}