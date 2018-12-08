using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WildCardExercice.net
{
    public class WildCard
    {
        const char WILDCARD_ANY_CHAR_ZERO_OR_MORE = '*';
        const char WILDCARD_ANY_CHAR_ONE_OR_MORE = '+';
        const char WILDCARD_ANY_CHAR = '?';

        public bool IsMatch(String s, String p)
        {
            return helper(s, p, 0, 0);
        }
        bool helper(String s, String pattern, int sX, int patternX)
        {
            if(patternX == pattern.Length)
                return sX == s.Length;

            if( pattern[patternX] == WILDCARD_ANY_CHAR_ZERO_OR_MORE ||
                pattern[patternX] == WILDCARD_ANY_CHAR_ONE_OR_MORE )
            {
                // Skip the * or any *** or + or any +++
                while (patternX < pattern.Length && (
                    pattern[patternX] == WILDCARD_ANY_CHAR_ZERO_OR_MORE ||
                    pattern[patternX] == WILDCARD_ANY_CHAR_ONE_OR_MORE ))
                {
                    patternX += 1;
                }

                while(sX < s.Length)
                {
                    if(helper(s, pattern, sX, patternX)) 
                        return true;
                    sX += 1;
                }
                return helper(s, pattern, sX, patternX);
            }
            else if(sX < s.Length) {

                if( (s[sX] == pattern[patternX]) || (pattern[patternX] == WILDCARD_ANY_CHAR) )
                    return helper(s, pattern, sX+1, patternX+1);
            }
            return false;
        }
    }

    [TestClass]
    public class WildCardUnitTests
    {
        [TestMethod]
        public void IsMatch_JustStringExpression()
        {
            Assert.IsTrue (new WildCard().IsMatch("", ""));
            Assert.IsFalse(new WildCard().IsMatch("", "a"));

            Assert.IsTrue (new WildCard().IsMatch("a", "a"));
            Assert.IsFalse(new WildCard().IsMatch("a", "z"));

            Assert.IsTrue (new WildCard().IsMatch("ab", "ab"));
            Assert.IsFalse(new WildCard().IsMatch("ab", "az"));

            Assert.IsTrue (new WildCard().IsMatch("abc", "abc"));
            Assert.IsFalse(new WildCard().IsMatch("abc", "abcd"));
            Assert.IsFalse(new WildCard().IsMatch("abc", "Zbc"));
            Assert.IsFalse(new WildCard().IsMatch("abc", "aZc"));
            Assert.IsFalse(new WildCard().IsMatch("abc", "abZ"));

            Assert.IsTrue(new WildCard().IsMatch("abcde", "abcde"));

        }
        [TestMethod]
        public void IsMatch_QuestionMark()
        {
           
            Assert.IsTrue(new WildCard().IsMatch("abc", "a?c"));
            Assert.IsFalse(new WildCard().IsMatch("abc", "a?Z"));

            Assert.IsTrue(new WildCard().IsMatch("abc", "?bc"));
            Assert.IsFalse(new WildCard().IsMatch("abc", "?Zc"));
            Assert.IsFalse(new WildCard().IsMatch("abc", "?bZ"));
            Assert.IsFalse(new WildCard().IsMatch("abc", "?bcZ"));

            Assert.IsTrue(new WildCard().IsMatch("abc", "ab?"));
            Assert.IsTrue(new WildCard().IsMatch("abc", "???"));
            Assert.IsTrue(new WildCard().IsMatch("a?c?e", "a?c?e"));

            Assert.IsFalse(new WildCard().IsMatch("abc", "abc?"));
        }
        [TestMethod]
        public void IsMatch_Star()
        {
            Assert.IsTrue(new WildCard().IsMatch("abcd", "a*d"));
            Assert.IsTrue(new WildCard().IsMatch("abcd", "ab*"));
            Assert.IsTrue(new WildCard().IsMatch("abcd", "abc*"));
            Assert.IsTrue(new WildCard().IsMatch("abcd", "abcd*"));
        }

        [TestMethod]
        public void IsMatch_NonSequentialMultiStar()
        {
            Assert.IsTrue(new WildCard().IsMatch("a*c*e", "a*c*e"));
        }

        [TestMethod]
        public void IsMatch_SequentialMultiStar()
        {
            Assert.IsTrue(new WildCard().IsMatch("abcd", "a***d"));
        }

        [TestMethod]
        public void IsMatch_StarMatchingZeroChar()
        {
            Assert.IsTrue(new WildCard().IsMatch("abcd", "a*bcd"));
        }

        [TestMethod]
        public void IsMatch_Plus()
        {
            Assert.IsTrue(new WildCard().IsMatch("abcd", "a+d"));
            Assert.IsTrue(new WildCard().IsMatch("abcd", "ab+"));
            Assert.IsTrue(new WildCard().IsMatch("abcd", "abc+"));
            Assert.IsTrue(new WildCard().IsMatch("abcd", "abcd+"));
        }

        [TestMethod]
        public void IsMatch_PlusMatchingZeroCharMustFail()
        {
            Assert.IsTrue(new WildCard().IsMatch("abcd", "a+bcd"));
        }
    }
}
