using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Utils
{
    public static class AbbrevationUtility
    {
        private static readonly SortedDictionary<float, string> abbrevations = new SortedDictionary<float, string>
    {
        {1000,"K"},
        {1000000, "M" },
        {1000000000, "B" },
        {1000000000000, "T" }
    };

        public static string AbbreviateNumber(float number)
        {
            for (int i = abbrevations.Count - 1; i >= 0; i--)
            {
                KeyValuePair<float, string> pair = abbrevations.ElementAt(i);
                if (Mathf.Abs(number) >= pair.Key)
                {
                    float roundedNumber = number / pair.Key;
                    return roundedNumber.ToString("F2") + pair.Value;
                }
            }
            if (Mathf.Abs(number) < 1000)
            {
                return number.ToString("F2");
            }

            return number.ToString();
        }
    }
}
