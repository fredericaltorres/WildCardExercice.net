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
            IWildCard dpImpl = new DynamicProgrammingWildCard();
            for(var i = 0; i < MAX_LOOP; i++)
                PerformanceWithImplementation(dpImpl);
            sw.Stop();
            var elapsedMilliseconds = sw.ElapsedMilliseconds;
        }

        [TestMethod]
        public void PerformanceWithRecursion()
        {
            var sw = Stopwatch.StartNew();
            var rImpl = new RecursiveWildCard();
            for(var i = 0; i < MAX_LOOP; i++)
                PerformanceWithImplementation(rImpl);
            Debug.WriteLine($"MaxRecursionLevel:{RecursiveWildCard.MaxRecursionLevel}");
            sw.Stop();
            var elapsedMilliseconds = sw.ElapsedMilliseconds;
        }

        public void PerformanceWithImplementation(IWildCard w)
        {
            var text     = "abcdefghi-abcdefghi-abcdefghi";
            var patterns = new List<string>() {

                "abcdefghi-abcdefghi-abcdefghi",
                "a*c*e*g*i-a*c*e*g*i-a*c*e*g*i",
                "a?c?e?g?i-a?c?e?g?i-a?c?e?g?i",
                "a*c?e*g?i-a*c?e*g?i-a*c?e*g?i",
                "*-***",
                "*?-*?-*?",
                "*??-*??-*??",
            };
            patterns.ForEach((pattern) => {
                Assert.IsTrue(w.IsMatch(text, pattern));
            });
        }
    }
}
