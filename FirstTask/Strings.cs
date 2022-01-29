using System.Collections.Generic;

namespace FirstTask
{
    public static class Strings
    {
        /// <summary>
        /// For example:
        /// "ggkljg" => 4
        /// </summary>
        /// <param name="str" ></param>
        /// <returns> maximum count of unique sequential characters. </returns>
        public static int MaxCountOfUniqueSequentialCharacters(this string str)
        {
            int max = -1;
            for (int i = 0; i < str.Length; i++)
            {
                HashSet<char> items = new HashSet<char> { str[i] };
                int j = i + 1;
                while (j < str.Length && !items.Contains(str[j]))
                {
                    items.Add(str[j]);
                    j++;
                }

                if (j - i > max)
                {
                    max = j - i;
                }
            }
            return max;
        }
    }
}
