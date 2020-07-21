using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Services.Interfaces
{
    public interface IGridService
    {
        Vector2 ConvertToPoint(int x, int y);
        Vector2 PointToGrid(int x, int y);
        Vector2 ConvertToPoint(Vector2 grid);
        Vector2 PointToGrid(Vector2 point);

        int GetDistance(Vector2 start, Vector2 end);
        Vector2 ConvertToPointVec(Vector2 vec);
        Vector2 PointToGridVec(Vector2 vec);
        bool SpaceOccupiedVec(Vector2 vec);
        bool SpaceOccupied(int x, int y);
    }
}
