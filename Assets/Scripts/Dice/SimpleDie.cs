using UnityEngine;

namespace Dice
{
    public class SimpleDie : IDice
    {
        public readonly int Sides;

        public SimpleDie(int sides)
        {
            Sides = sides;
        }

        public int Roll()
        {
            return Random.Range(1, Sides + 1);
        }

        public override string ToString()
        {
            return "d" + Sides;
        }
    }
}