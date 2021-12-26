using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Test
{
    [TestClass]
    public class Day3Test
    {
        [TestMethod]
        public void Part2()
        {
            string[] bin = File.ReadAllLines("test3.txt");

            Assert.AreEqual(230, Day3.Program.Part2(bin));
        }
    }

    [TestClass]
    public class Day4Test
    {
        [TestMethod]
        public void Part1()
        {
            string[] bin = File.ReadAllLines("test4.txt");

            Assert.AreEqual(4512, Day4.Program.Part1(bin));
        }

        [TestMethod]
        public void Part2()
        {
            string[] bin = File.ReadAllLines("test4.txt");

            Assert.AreEqual(1924, Day4.Program.Part2(bin));
        }
    }

    [TestClass]
    public class Day14Test
    {
        [TestMethod]
        public void Part1()
        {
            string[] bin = File.ReadAllLines("test14.txt");

            Assert.AreEqual(1588u, Day14.Program.Part1(bin));
        }

        [TestMethod]
        public void Part2()
        {
            string[] bin = File.ReadAllLines("test14.txt");

            Assert.AreEqual(2188189693529u, Day14.Program.Part2(bin));
        }
    }
}