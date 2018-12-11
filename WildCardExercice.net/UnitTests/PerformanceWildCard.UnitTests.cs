using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WildCardExercice.net
{
    [TestClass]
    public class PerformanceWildCard_UnitTests
    {
        const int MAX_LOOP = 1000;

        [TestMethod]
        public void PerformanceWithDynamicProgramming()
        {
            var sw = Stopwatch.StartNew();
            IWildCard impl = new DynamicProgrammingWildCard();
            for(var i = 0; i < MAX_LOOP; i++)
                PerformanceWithImplementation(impl);
            sw.Stop();
            var elapsedMilliseconds = sw.ElapsedMilliseconds;
        }

        [TestMethod]
        public void PerformanceWithRecursion()
        {
            var sw = Stopwatch.StartNew();
            IWildCard impl = new RecursiveWildCard();
            for(var i = 0; i < MAX_LOOP; i++)
                PerformanceWithImplementation(impl);
            Debug.WriteLine($"MaxRecursionLevel:{RecursiveWildCard.MaxRecursionLevel}");
            sw.Stop();
            var elapsedMilliseconds = sw.ElapsedMilliseconds;
        }

        public void PerformanceWithImplementation(IWildCard w)
        {
            var text     = "abcdefghi-abcdefghi-abcdefghi";
            var successfullPatterns = new List<string>() {

                "abcdefghi-abcdefghi-abcdefghi",
                "a*c*e*g*i-a*c*e*g*i-a*c*e*g*i",
                "a?c?e?g?i-a?c?e?g?i-a?c?e?g?i",
                "a*c?e*g?i-a*c?e*g?i-a*c?e*g?i",
                "*-*-*",
                "*?-*?-*?",
                "*??-*??-*??",
            };
            successfullPatterns.ForEach((pattern) => {
                Assert.IsTrue(w.IsMatch(text, pattern));
            });
            var failedPatterns = new List<string>() {

                "abcdefghi-abcdefghi-abcdefgh",
                "a*c*e*g*i-aZc*e*g*i-a*c*e*g*i",
                "a?c?e?g?i-a?c?e?g?Z-a?c?e?g?i",
                "a*c?e*g?i-a*c?e*g?i-a*c?e*g?Z",
                "*-*Z*",
                "*?-*?Z*?",
                "*??-*??Z*??",
            };
            failedPatterns.ForEach((pattern) => {
                Assert.IsFalse(w.IsMatch(text, pattern));
            });
        }
    }
}
