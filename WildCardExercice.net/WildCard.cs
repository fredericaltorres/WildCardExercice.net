﻿using System;

namespace WildCardExercice.net
{
    public class WildCard
    {
        const char WILDCARD_ANY_CHAR_ZERO_OR_MORE = '*';
        const char WILDCARD_ANY_CHAR_ONE_OR_MORE  = '+';
        const char WILDCARD_ANY_CHAR              = '?';

        public bool IsMatch(String s, String p)
        {
            return helper(s, p, 0, 0);
        }
        bool helper(String s, String pattern, int sX, int patternX)
        {
            if(patternX == pattern.Length)
                return sX == s.Length;

            if(pattern[patternX] == WILDCARD_ANY_CHAR_ZERO_OR_MORE || pattern[patternX] == WILDCARD_ANY_CHAR_ONE_OR_MORE)
            {
                var isPlusChar = pattern[patternX] == WILDCARD_ANY_CHAR_ONE_OR_MORE;
                if(isPlusChar) // With the + we must at least mach one char, accept the + to match the current char
                    sX += 1;

                // Skip the * or any *** or + or any +++
                while ( patternX < pattern.Length && (
                    pattern[patternX] == WILDCARD_ANY_CHAR_ZERO_OR_MORE ||
                    pattern[patternX] == WILDCARD_ANY_CHAR_ONE_OR_MORE )
                )
                {
                    patternX += 1;
                }

                while(sX < s.Length)
                {
                    if(helper(s, pattern, sX, patternX)) 
                        return true;
                    sX += 1; // If we failed matching, skip one char try to skip two...
                }
                return helper(s, pattern, sX, patternX); // Do not forget the final evaluation in case we reach the end of the string
            }
            else if(sX < s.Length) {
                // Direct char to char or char to ? match
                if( (s[sX] == pattern[patternX]) || (pattern[patternX] == WILDCARD_ANY_CHAR) )
                    return helper(s, pattern, sX+1, patternX+1); // Move to next char
            }
            return false;
        }
    }
}