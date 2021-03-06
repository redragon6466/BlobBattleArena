﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public interface IBrain
    {
        void TakeTurn(BlobScript me, List<BlobScript> allyBlobs, List<BlobScript> enemyBlobs);


        BrainEnum GetBrainType();
    }
}
