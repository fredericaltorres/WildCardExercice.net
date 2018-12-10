namespace WildCardExercice.net
{
    /*
     

    string     :"ab"
    pattern    :"az"

            Pattern
                0   1   2
                    a   z
        -------------------
        0       T   F   F    [0,0] is T  
        1   a   F   T   F    [1,1] is T
        2   b   F   F   F

    string     :"abc"
    pattern    :"ab+"

            Pattern
                0   1   2   3   
                    a   b   +
        -----------------------
        0       T   F   F   F
        1   a   F   T   F   F
        2   b   F   F   T   F
        3   c   F   F   F   T



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
        public bool IsMatch(string s, string pp)
        {
            var p = pp;
            while(p.Contains("**")) // Replace ** with *
                p = p.Replace("**", "*");
            while(p.Contains("++")) // Replace ++ with +
                p = p.Replace("++", "+");

            var str     = s.ToCharArray();
            var pattern = p.ToCharArray();
            int dim2    = pattern.Length+1;
            int dim1    = str.Length+1;
            bool[,] T   = new bool[dim1, dim2];
            T[0, 0]     = true;

            if(pattern[0] == '*')
            {
                T[0, 1] = true;
            }

            for(var i = 1; i < dim1; i++)
            {
                for(var j = 1; j < dim2; j++)
                {
                    if( (pattern[j-1] == '?') || (i-1 < str.Length && str[i-1] == pattern[j-1]) ) {

                        T[i, j] = T[i-1, j-1];
                    }
                    else if(pattern[j-1] == '*')
                    {
                        T[i, j] = T[i-1, j] /* left */ || T[i, j-1] /* or above */ ;
                    }
                    else if(pattern[j-1] == '+')
                    {
                        T[i, j] = T[i-1, j] /* left */ || T[i, j-1] /* or above */ ;
                    }
                }
            }
            return T[str.Length, dim2-1];
        }
    }
}
