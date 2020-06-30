using Assets.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Services
{
    public class GridService : IGridService
    {
        private List<Tuple<int, int, Vector2>> _ohGodThereHasToBeABetterWay;
        private const int _widthInBlobs = 21;
        private const int _heightInBlobs = 11;

        private static GridService instance = null;
        private static readonly object padlock = new object();
        private static readonly object teamOnePoolLock = new object();
        private static readonly object teamTwoPoolLock = new object();

        GridService()
        {
        }

        public static GridService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new GridService();
                    }
                    return instance;
                }
            }
        }

        public Vector2 ConvertToPoint(int x, int y)
        {
            if (x > _widthInBlobs || x < 0 || y > _heightInBlobs || y< 0) //top left 0 based coord system
            {
                return new Vector2(0,0);
            }

            var xPoint = 2.5f * x - 26.13f;
            var yPoint = -2.638f * y + 12.73f;
            return new Vector2(xPoint, yPoint);
        }
        public Vector2 PointToGrid(int x, int y)
        {
            if (x > _widthInBlobs || x < 0 || y > _heightInBlobs || y < 0) //top left 0 based coord system
            {
                return new Vector2(0, 0);
            }

            var xPoint = 2.5f * x - 26.13f;
            var yPoint = -2.638f * y + 12.73f;
            return new Vector2(xPoint, yPoint);
        }

        public int GetDistance(Vector2 start, Vector2 end)
        {
            return (int)(Math.Abs(end.x - start.x) + Math.Abs(end.y - start.y));
        }

        public Vector2 ConvertToPoint(Vector2 grid)
        {
            if (grid.x > _widthInBlobs || grid.x < 0 || grid.y > _heightInBlobs || grid.y < 0) //top left 0 based coord system
            {
                return new Vector2(0, 0);
            }

            var xPoint = 2.5f * grid.x - 26.13f;
            var yPoint = -2.638f * grid.y + 12.73f;
            return new Vector2(xPoint, yPoint);
        }

        public Vector2 PointToGrid(Vector2 point)
        {
            if (point.x > _widthInBlobs || point.x < 0 || point.y > _heightInBlobs || point.y < 0) //top left 0 based coord system
            {
                return new Vector2(0, 0);
            }

            var xPoint = 2.5f * point.x - 26.13f;
            var yPoint = -2.638f * point.y + 12.73f;
            return new Vector2(xPoint, yPoint);
        }
    }
}
