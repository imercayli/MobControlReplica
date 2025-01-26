using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Extentions
{
    public static class ConversionExtentions
    {
        public static Vector2 ScreenToCanvasPosition(this Vector2 screenPosition, Canvas canvas)
        {
            if (canvas.renderMode != RenderMode.ScreenSpaceCamera)
            {
                Debug.LogError("RenderMode should be ScreenSpaceCamera. Other render modes not supported yet!");
                return Vector2.zero;
            }

            var screenSize = new Vector2(Screen.width, Screen.height);

            var viewPortPosition = new Vector2(screenPosition.x / screenSize.x, screenPosition.y / screenSize.y);

            var canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

            return new Vector2(canvasSize.x * (viewPortPosition.x - .5f), canvasSize.y * (viewPortPosition.y - .5f));
        }

        public static Vector2 Rotate(this Vector2 v, float degrees)
        {
            var radians = degrees * Mathf.Deg2Rad;
            var sin = Mathf.Sin(radians);
            var cos = Mathf.Cos(radians);

            var tx = v.x;
            var ty = v.y;

            return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
        }

        public static Vector3 Right(this Vector3 b, Vector3 refAxis)
        {
            var bSubA = b - refAxis;
            var cSubA = -refAxis;

            var cross = Vector3.Cross(bSubA, cSubA);

            return cross;
        }

        public static Vector2 XZ(this Vector3 b)
        {
            return new Vector2(b.x, b.z);
        }

        public static Vector3 X_Z(this Vector2 b, float yValue)
        {
            return new Vector3(b.x, yValue, b.y);
        }

        public static bool DistanceCheck(this Vector3 origin, Vector3 pointToCheck, float distanceToCheck)
        {
            // square the distance we compare with
            if ((origin - pointToCheck).sqrMagnitude < distanceToCheck * distanceToCheck)
                return true;
            else
                return false;
        }

    }

    public static class ListExtentions
    {
        public static List<T> ShuffleList<T>(this List<T> list)
        {
            return list.OrderBy(elem => Guid.NewGuid()).ToList();
        }

        public static List<int> ShuffleInt<T>(this IList<T> list)
        {
            var newLocation = new List<int>();

            int n = list.Count;

            for (int i = 0; i < list.Count; i++)
            {
                newLocation.Add(i);
            }

            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;

                int tempValue = newLocation[n];
                newLocation[n] = newLocation[k];
                newLocation[k] = tempValue;
            }

            return newLocation;
        }

        public static void ShuffleWithGivenArray<T>(this IList<T> list, List<int> shuffleList)
        {
            var tempList = new List<T>(list);

            for (int i = 0; i < list.Count; i++)
            {
                list[i] = tempList[shuffleList[i]];
            }
        }

        public static T RandomItem<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public static T LastItem<T>(this List<T> list)
        {
            return list[list.Count - 1];
        }

        public static void ActivateRandomGameObject(this List<GameObject> list)
        {
            list.ForEach(o=>o.SetActive(false));
            list.RandomItem().SetActive(true);
        }
        
        public static List<T> ReorderListFromMiddle<T>(this List<T> list)
        {
            if (list == null || list.Count <= 2)
            {
                return list; 
            }

            List<T> result = new List<T>();
            int midIndex = list.Count / 2; // Find the middle index

            result.Add(list[midIndex]); // Add the middle element to the result list

            // Start from the middle, alternate adding elements to the left and right
            int left = midIndex - 1;
            int right = midIndex + 1;

            while (left >= 0 || right < list.Count)
            {
                if (left >= 0)
                {
                    result.Add(list[left]);
                    left--;
                }
            
                if (right < list.Count)
                {
                    result.Add(list[right]);
                    right++;
                }
            }

            return result;
        }
    }

    public static class FloatExtensions
    {
        public static bool EqualTo(this float a, float b)
        {
            return Mathf.Abs(a - b) < Mathf.Epsilon;
        }

        public static bool GreaterThan(this float a, float b)
        {
            return a - b > Mathf.Epsilon;
        }

        public static bool LessThan(this float a, float b)
        {
            return a - b < Mathf.Epsilon;
        }
    }

    public static class StringExtensions
    {
        public static string RemoveDiacritics(this string text)
        {
            return String.Join("", text.Normalize(NormalizationForm.FormD)
                                    .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
                                    .Replace("ı", "i");

            //var normalizedString = text.Normalize(NormalizationForm.FormD);
            //var stringBuilder = new StringBuilder();

            //foreach (var c in normalizedString)
            //{
            //    var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            //    if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            //    {
            //        stringBuilder.Append(c);
            //    }
            //}

            //return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string GetOrdinalSuffix(this int num)
        {
            string str = "";
            if (num.ToString().EndsWith("11")) str = "th";
            else if (num.ToString().EndsWith("12")) str = "th";
            else if (num.ToString().EndsWith("13")) str = "th";
            else if (num.ToString().EndsWith("1")) str = "st";
            else if (num.ToString().EndsWith("2")) str = "nd";
            else if (num.ToString().EndsWith("3")) str = "rd";
            else str = "th";
            return num.ToString() + str;
        }

        public static string TitleCase(this string text)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            return textInfo.ToTitleCase(text); 
        }

        public static string AddSpace(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                    newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }
    }

    public static class IntegerExtensions
    {
        public static string LargeIntToString(this int Value)
        {
            string abbrevated = Value.ToString();
            string suffix = "";
            string source = abbrevated;

            if (abbrevated.Length > 15)
            {
                abbrevated = abbrevated.Remove(abbrevated.Length - 15) + "." + source.Substring(abbrevated.Length - 15, 1);
                suffix = "Q";
            }
            else if (abbrevated.Length > 12)
            {
                abbrevated = abbrevated.Remove(abbrevated.Length - 12) + "." + source.Substring(abbrevated.Length - 12, 1);
                suffix = "T";
            }
            else if (abbrevated.Length > 9)
            {
                abbrevated = abbrevated.Remove(abbrevated.Length - 9) + "." + source.Substring(abbrevated.Length - 9, 1);
                suffix = "B";
            }
            else if (abbrevated.Length > 6)
            {
                abbrevated = abbrevated.Remove(abbrevated.Length - 6) + "." + source.Substring(abbrevated.Length - 6, 1);
                suffix = "M";
            }
            else if (abbrevated.Length > 3)
            {
                abbrevated = abbrevated.Remove(abbrevated.Length - 3) + "." + source.Substring(abbrevated.Length - 3, 1);
                suffix = "K";
            }

            return abbrevated + suffix;
        }
    }

    public static class LongExtensions
    {
        public static string LargeLongToString(this long Value)
        {
            string abbrevated = Value.ToString();
            string suffix = "";
            string source = abbrevated;

            if (abbrevated.Length > 15)
            {
                abbrevated = abbrevated.Remove(abbrevated.Length - 15) + "." + source.Substring(abbrevated.Length - 15, 1);
                suffix = "Q";
            }
            else if (abbrevated.Length > 12)
            {
                abbrevated = abbrevated.Remove(abbrevated.Length - 12) + "." + source.Substring(abbrevated.Length - 12, 1);
                suffix = "T";
            }
            else if (abbrevated.Length > 9)
            {
                abbrevated = abbrevated.Remove(abbrevated.Length - 9) + "." + source.Substring(abbrevated.Length - 9, 1);
                suffix = "B";
            }
            else if (abbrevated.Length > 6)
            {
                abbrevated = abbrevated.Remove(abbrevated.Length - 6) + "." + source.Substring(abbrevated.Length - 6, 1);
                suffix = "M";
            }
            else if (abbrevated.Length > 3)
            {
                abbrevated = abbrevated.Remove(abbrevated.Length - 3) + "." + source.Substring(abbrevated.Length - 3, 1);
                suffix = "K";
            }

            return abbrevated + suffix;
        }
    }

    public static class RectExtensions
    {
        public static Vector2 GetRandomPointInRect(this Rect rect)
        {
            return new Vector2(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMin, rect.yMax));
        }
    }

    public static class TransformExtensions
    {
        public static bool IsTransformLookingAt(this Transform transform, Vector3 position, float maxAngleForLooking = 80)
        {
            return Vector3.Angle(transform.forward, position - transform.position) < maxAngleForLooking;
        }
    }

    public static class BehaviourExtensions
    {
    }
}