using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FormulaExtentions
{
    public static class Formulas
    {
        public static long GetValueByLevelMultiplier(float baseValue, float multiplier, int level)
        {
            float value = Mathf.Ceil(baseValue * Mathf.Pow(multiplier, level));
            return Convert.ToInt64(value);
        }
        public static float GetValueByLevelMultiplierFloat(float baseValue, float multiplier, int level)
        {
            return (float)Math.Ceiling(baseValue * Mathf.Pow(multiplier, level));
        }
        public static int GetRandomEnumByCoefficient(int enumLenght)
        {
            List<int> chanceForEnumTypes = new List<int>();

            for (int i = 0; i < enumLenght; i++)
            {
                chanceForEnumTypes.Add((enumLenght - i) + (chanceForEnumTypes.Count != 0 ? chanceForEnumTypes.Last() : 0));
            }

            float randomChancePercantage = UnityEngine.Random.value * chanceForEnumTypes.Last();
            int selectedEnumIndex = 0;
            for (int i = 0; i < chanceForEnumTypes.Count; i++)
            {
                if (randomChancePercantage < chanceForEnumTypes[i])
                {
                    selectedEnumIndex = i;
                    break;
                }
            }

            return selectedEnumIndex;
        }
    }

}
