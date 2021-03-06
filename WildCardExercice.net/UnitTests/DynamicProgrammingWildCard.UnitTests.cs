﻿using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WildCardExercice.net
{
    [TestClass]
    public class DynamicProgrammingWildCard_UnitTests
    {
        /// <summary>
        /// Find a way to re factor
        /// </summary>        
        IWildCard w = new DynamicProgrammingWildCard();

        [TestMethod]
        public void IsMatch_JustStringExpression()
        {
            Assert.IsTrue (w.IsMatch("", ""));
            Assert.IsFalse(w.IsMatch("", "a"));

            Assert.IsTrue (w.IsMatch("a", "a"));
            Assert.IsFalse(w.IsMatch("a", "z"));

            Assert.IsTrue (w.IsMatch("ab", "ab"));
            Assert.IsFalse(w.IsMatch("ab", "az"));

            Assert.IsTrue (w.IsMatch("abc", "abc"));
            Assert.IsFalse(w.IsMatch("abc", "abcd"));
            Assert.IsFalse(w.IsMatch("abc", "Zbc"));
            Assert.IsFalse(w.IsMatch("abc", "aZc"));
            Assert.IsFalse(w.IsMatch("abc", "abZ"));

            Assert.IsTrue (w.IsMatch("abcde", "abcde"));
        }

        [TestMethod]
        public void IsMatch_QuestionMark()
        {
            Assert.IsTrue (w.IsMatch("abc", "a?c"));
            Assert.IsFalse(w.IsMatch("abc", "a?Z"));

            Assert.IsTrue (w.IsMatch("abc", "?bc"));
            Assert.IsFalse(w.IsMatch("abc", "?Zc"));
            Assert.IsFalse(w.IsMatch("abc", "?bZ"));
            Assert.IsFalse(w.IsMatch("abc", "?bcZ"));

            Assert.IsTrue (w.IsMatch("abc", "ab?"));
            Assert.IsTrue (w.IsMatch("abc", "???"));
            Assert.IsTrue (w.IsMatch("a?c?e", "a?c?e"));

            Assert.IsFalse(w.IsMatch("abc", "abc?"));
        }

        [TestMethod]
        public void IsMatch_OneStarOrOnePlus()
        {
            var text     = "abcd";
            var patterns = new List<string>() { "a*d", "ab*", "abc*" };

            patterns.ForEach((pattern) => {

                Assert.IsTrue(w.IsMatch(text, pattern));
                Assert.IsTrue(w.IsMatch(text, pattern.Replace("*", "+")));
            });
            Assert.IsTrue(w.IsMatch(text, "abcd*"));
        }

        [TestMethod]
        public void IsMatch_NonSequentialMultiStarOrPlus()
        {
            var text     = "abcde";
            var patterns = new List<string>() { "a*c*e", "a**c**e", "a***c***e", "a****c****e" };

            patterns.ForEach((pattern) => {

                Assert.IsTrue(w.IsMatch(text, pattern));
                Assert.IsTrue(w.IsMatch(text, pattern.Replace("*", "+")));
            });

            text     = "abcdefghi";
            patterns = new List<string>() {

                "a*c*e*g*i", "a*i", "ab*hi", "ab*f*hi", "ab*f?hi", "ab*f?h*", "?b*f?h*", "*b*f?h*"
            };
            patterns.ForEach((pattern) => {
                
                Assert.IsTrue(w.IsMatch(text, pattern));
                Assert.IsTrue(w.IsMatch(text, pattern.Replace("*", "+")));
            });
        }

        [TestMethod]
        public void IsMatch_SequentialMultiStar()
        {
            var text     = "abcd";
            var patterns = new List<string>() {"a***d" };

            patterns.ForEach((pattern) => {

                Assert.IsTrue(w.IsMatch(text, pattern));
                Assert.IsTrue(w.IsMatch(text, pattern.Replace("*", "+")));
            });
        }

        [TestMethod]
        public void IsMatch_StarMatchingZeroChar()
        {
            Assert.IsTrue(w.IsMatch("abcd", "a*bcd"));
            Assert.IsTrue(w.IsMatch("abcd", "abcd*"));
            Assert.IsTrue(w.IsMatch("abcd", "*abcd"));
        }

        [TestMethod]
        public void IsMatch_Plus()
        {
            Assert.IsTrue(w.IsMatch("abc", "ab+"));

            Assert.IsTrue(w.IsMatch("abcd", "a+d"));
            Assert.IsTrue(w.IsMatch("abcd", "ab+"));
            Assert.IsTrue(w.IsMatch("abcd", "abc+"));
        }

        //[TestMethod]
        public void IsMatch_PlusMatchingZeroCharMustFail()
        {
            Assert.IsFalse(w.IsMatch("abcd", "abcd+"));
            Assert.IsFalse(w.IsMatch("abcd", "+abcd"));
        } 

        //[TestMethod]
        public void IsMatch_PlusMatchingZeroCharMustFail_case_2()
        {
            Assert.IsFalse(w.IsMatch("abcd", "a+bcd"));
        }
    }
}
