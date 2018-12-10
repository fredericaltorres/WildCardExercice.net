namespace WildCardExercice.net
{
    /*
     string     :"xaylmz"
     pattern    :"x?y*z"

            Pattern
                0   1   2   3   4   5
                    x   ?   y   *   z
        ------------------------------
        0       T   F   F   F   F   F
        1   x   F   T   F   F   F   F
        2   a   F   F   T   F   F   F       
        3   y   F   F   F   T   T   F
        4   l   F   F   F   F   T   F
        5   m   F   F   F   F   T   F
        6   z   F   F   F   F   T   T

    ! in [2,2] eval x? match xa use [i-1, j-1] value
    ! in [3,2] eval x?y match xal - No
    ! in [2,3] eval x? match xay - No
    ! in [3,3] eval x?y match xay - Yes
    ! in [4,3] eval x?y* match xay - Yes
    ! in [4,4] eval x?y* match xayl - Yes

    - Final answer is in [5,6] == T
    - Space O(m x n)
    - Time O(m x n)

            leetcode.com/problems/wildcard-matching

    */
    /// <summary>
    /// Wildcard implementation using dynamic programming
    /// Using dynamic programming: https://www.youtube.com/watch?v=3ZDZ-N0EPV0
    /// </summary>
    public class DynamicProgrammingWildCard : IWildCard
    {
        public bool IsMatch(string s, string p)
        {
            var str = s.ToCharArray();
            var pattern = p.ToCharArray();
            var writeIndex = 0;
            var isFirst = true;

            //// Replace multiple * with 1
            //// a**b**c -> a*b*c
            //for(var i = 0; i < pattern.Length; i++)
            //{
            //    if(pattern[i] == '*')
            //    {
            //        if(isFirst)
            //        {
            //            pattern[writeIndex++] = pattern[i];
            //            isFirst = false;
            //        }
            //    }
            //    else {
            //        pattern[writeIndex++] = pattern[i];
            //        isFirst = true;
            //    }
            //}

            int dim2 = pattern.Length+1;
            bool[,] T = new bool[str.Length+1, dim2];
            T[0, 0] = true;

            for(var i = 0; i < T.Length; i++)
            {
                for(var j = 0; j < dim2; j++)
                {
                    if(pattern[j-1] == '?' || str[i-1] == pattern[j-1]) {
                        T[i, j] = T[i-1, j-1];
                    }
                    else
                    {
                        T[i, j] = T[i-1, j] /* left */ || T[i, j-1] /* or above */ ;
                    }
                }
            }
            var r = T[str.Length+1, dim2];
            return r;
        }
    }
}
