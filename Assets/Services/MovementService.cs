using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Services
{
    public class MovementService : IMovementService
    {
        int[,] Grid;
        private const int numMovesSpiral = 300;

        private static MovementService instance = null;

        private static readonly object padlock = new object();


        MovementService()
        {
        }

        public static MovementService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MovementService();
                    }
                    return instance;
                }
            }
        }


        
        //TODO if team members go back to back can skip this to save frames, ADD CHECK FOR THIS
        /// <summary>
        /// Using a spiral pattern mark the threat provided by the given team
        /// </summary>
        public void MarkDownGridSpiral(BlobScript myPos, List<BlobScript> otherPositions)
        {
            Grid = new int[20, 20];
            var touched = new bool[20, 20];
            List<Tuple<int, int>> foundItems = new List<Tuple<int, int>>();
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    Grid[i, j] = 0;
                    touched[i, j] = false;
                }
            }

           

            foreach (var item in otherPositions)
            {
                var itemLoc = item.GetGridLocation();
                touched[(int)itemLoc.x, (int)itemLoc.y] = false;
                    foundItems.Add(new Tuple<int, int>((int)itemLoc.x, (int)itemLoc.y));
                    touched[(int)itemLoc.x, (int)itemLoc.y] = true;
            }    


            foreach (var item in foundItems)
            {
                var currX = item.Item1;
                var currY = item.Item2;
                int direction = 0;
                var count = 0;
                var toAdd = 6;
                var numMoves = 1;
                var upNumMoves = false;

                while (count < numMovesSpiral)
                {
                    //number of moves determines how
                    for (int i = 0; i < numMoves; ++i)
                    {
                        switch (direction)
                        {
                            //Up
                            case 0:
                                if (i == numMoves - 1)
                                {
                                    toAdd -= 1;
                                }
                                currX += 1;

                                try
                                {
                                    if (Grid[currX, currY] != 99)
                                    {
                                        if (toAdd < 0 && touched[currX, currY])
                                        {
                                            break;
                                        }
                                        if (toAdd > 0 && Grid[currX, currY] < 0) //safe? areas from an earlier spiral are no longer safe
                                        {
                                            Grid[currX, currY] = 0;
                                        }
                                        Grid[currX, currY] += toAdd;
                                    }
                                    touched[currX, currY] = true;
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    //swallow out of bounds
                                }
                                break;

                            //Right
                            case 1:
                                currY += 1;
                                try
                                {
                                    if (Grid[currX, currY] != 99)
                                    {
                                        if (toAdd < 0 && touched[currX, currY])// don't reduce danger areas of other spirals
                                        {
                                            break;
                                        }

                                        if (toAdd > 0 && Grid[currX, currY] < 0) //safe? areas from an earlier spiral are no longer safe
                                        {
                                            Grid[currX, currY] = 0;
                                        }
                                        Grid[currX, currY] += toAdd;
                                    }
                                    touched[currX, currY] = true;
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    //swallow out of bounds
                                }

                                if (i == numMoves - 1)
                                {
                                    upNumMoves = true;
                                }

                                break;

                            // Down
                            case 2:
                                currX -= 1;
                                try
                                {
                                    if (Grid[currX, currY] != 99)
                                    {
                                        if (toAdd < 0 && touched[currX, currY])// don't reduce danger areas of other spirals
                                        {
                                            break;
                                        }
                                        if (toAdd > 0 && Grid[currX, currY] < 0) //safe? areas from an earlier spiral are no longer safe
                                        {
                                            Grid[currX, currY] = 0;
                                        }
                                        Grid[currX, currY] += toAdd;
                                    }
                                    touched[currX, currY] = true;
                                }

                                catch (IndexOutOfRangeException)
                                {
                                    //swallow out of bounds
                                }
                                break;

                            //Left
                            case 3:
                                currY -= 1;
                                try
                                {
                                    if (Grid[currX, currY] != 99)
                                    {
                                        if (toAdd < 0 && touched[currX, currY]) // don't reduce danger areas of other spirals
                                        {
                                            break;
                                        }
                                        if (toAdd > 0 && Grid[currX, currY] < 0) //safe? areas from an earlier spiral are no longer safe
                                        {
                                            Grid[currX, currY] = 0;
                                        }

                                        Grid[currX, currY] += toAdd;
                                    }
                                    touched[currX, currY] = true;
                                }

                                catch (IndexOutOfRangeException)
                                {
                                    //swallow out of bounds
                                }

                                if (i == numMoves - 1)
                                {
                                    upNumMoves = true;
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    if (upNumMoves)
                    {
                        numMoves += 1;
                        upNumMoves = false;
                    }

                    switch (direction)
                    {
                        //Up
                        case 0:
                            direction = 3;
                            break;
                        //Right
                        case 1:
                            direction = 0;
                            break;
                        // Down
                        case 2:
                            direction = 1;
                            break;
                        //Left
                        case 3:
                            direction = 2;
                            break;
                        default:
                            break;

                    }
                    count += numMoves;
                }
                var Min = 0;

                for (int i = 0; i < Grid.GetLength(0); i++)
                {
                    for (int j = 0; j < Grid.GetLength(1); j++)
                    {
                        if (Grid[i, j] < Min)
                        {
                            Min = Grid[i, j];
                        }
                    }
                }

                for (int i = 0; i < touched.GetLength(0); i++)
                {
                    for (int j = 0; j < touched.GetLength(1); j++)
                    {
                        if (!touched[i, j])
                        {
                            Grid[i, j] = Min;
                        }
                    }
                }
            }
        }

        //Bound to range, not needed as handled clearned elsewhere
        public Vector2 FindSafestInRange(BlobScript myPos, List<BlobScript> otherPosition, int range) 
        {
            var loc = myPos.GetGridLocation();
            MarkDownGridSpiral(myPos, otherPosition);

            var leftBound = (int)(loc.x - range > 0 ? loc.x - range : 0);
            var RightBound = (int)(loc.x + range < Grid.Length ? loc.x + range : 0);

            var topBound = (int)(loc.y - range > 0 ? loc.y - range : 0 );
            var bottomBound = (int)(loc.y + range < Grid.Length ? loc.y + range : 0);

            int min = 100;
            Vector2 safest = new Vector2();
            for (int i = leftBound; i < RightBound; i++)
            {
                for (int j = topBound; j < bottomBound; j++)
                {
                    if (Grid[i,j] < min)
                    {
                        min = Grid[i, j];
                        safest.x = i;
                        safest.y = j;
                    }
                }
            }

            return safest;
        }

        //Not Bound to range, not needed as handled clearned elsewhere
        public Vector2 FindSafestInRange(BlobScript myPos, List<BlobScript> otherPosition)
        {
            var loc = myPos.GetGridLocation();
            MarkDownGridSpiral(myPos, otherPosition);

            var min = 100;
            var safest = new Vector2();
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    if (Grid[i, j] < min)
                    {
                        min = Grid[i, j];
                        safest.x = i;
                        safest.y = j;
                    }
                }
            }

            return safest;
        }



    }
}
