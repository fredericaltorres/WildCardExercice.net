using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WildCardExercice.net
{
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
        public void IsMatch_OneStarOrOnePlus()
        {
            var text = "abcd";
            var patterns = new List<string>() { "a*d", "ab*", "abc*" };
            patterns.ForEach((pattern) => {
                Assert.IsTrue(new WildCard().IsMatch(text, pattern));
                Assert.IsTrue(new WildCard().IsMatch(text, pattern.Replace("*", "+")));
            });
            Assert.IsTrue(new WildCard().IsMatch(text, "abcd*"));
        }

        [TestMethod]
        public void IsMatch_NonSequentialMultiStarOrPlus()
        {
            var text = "abcde";
            var patterns = new List<string>() { "a*c*e", "a**c**e" };
            patterns.ForEach((pattern) => {
                Assert.IsTrue(new WildCard().IsMatch(text, pattern));
                Assert.IsTrue(new WildCard().IsMatch(text, pattern.Replace("*", "+")));
            });

            text = "abcdefghi";
            patterns = new List<string>() {
                "a*c*e*g*i", "a*i", "ab*hi", "ab*f*hi", "ab*f?hi", "ab*f?h*", "?b*f?h*", "*b*f?h*"
            };
            patterns.ForEach((pattern) => {
                Assert.IsTrue(new WildCard().IsMatch(text, pattern));
                Assert.IsTrue(new WildCard().IsMatch(text, pattern.Replace("*", "+")));
            });
        }

        [TestMethod]
        public void IsMatch_SequentialMultiStar()
        {
            var text = "abcd";
            var patterns = new List<string>() {"a***d" };
            patterns.ForEach((pattern) => {
                Assert.IsTrue(new WildCard().IsMatch(text, pattern));
                Assert.IsTrue(new WildCard().IsMatch(text, pattern.Replace("*", "+")));
            });
        }

        [TestMethod]
        public void IsMatch_StarMatchingZeroChar()
        {
            Assert.IsTrue(new WildCard().IsMatch("abcd", "a*bcd"));
            Assert.IsTrue(new WildCard().IsMatch("abcd", "abcd*"));
            Assert.IsTrue(new WildCard().IsMatch("abcd", "*abcd"));
        }

        [TestMethod]
        public void IsMatch_Plus()
        {
            Assert.IsTrue(new WildCard().IsMatch("abcd", "a+d"));
            Assert.IsTrue(new WildCard().IsMatch("abcd", "ab+"));
            Assert.IsTrue(new WildCard().IsMatch("abcd", "abc+"));
        }

        [TestMethod]
        public void IsMatch_PlusMatchingZeroCharMustFail()
        {
            Assert.IsFalse(new WildCard().IsMatch("abcd", "a+bcd"));
            Assert.IsFalse(new WildCard().IsMatch("abcd", "abcd+"));
            Assert.IsFalse(new WildCard().IsMatch("abcd", "+abcd"));
        }
    }
}
