using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SolutionTests
{
    [TestClass]
    public class ProblemBTests
    {
        [TestMethod]
        public void CharOrDefault_ShouldBeCharIfNotQuestionMark()
        {
            var c = 'x';
            Assert.AreEqual(c, ProblemB.Program.CharOrDefault(c, '!'));
        }

        [TestMethod]
        public void CharOrDefault_ShouldBeDefaultIfQuestionMark()
        {
            var def = 'x';
            Assert.AreEqual(def, ProblemB.Program.CharOrDefault('?', def));
        }

        [TestMethod]
        public void FillToBeClosest_ShouldChooseSmallerDiffWithFirstLarger()
        {
            var x = "123";
            var y = "1?2";

            //ProblemB.Program.FillToBeClosest();
        }
    }
}
