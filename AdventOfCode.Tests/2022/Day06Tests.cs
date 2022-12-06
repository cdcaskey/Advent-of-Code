using System.Collections.Generic;
using System.Web;
using AdventOfCode._2022;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace AdventOfCode.Tests._2022
{
    [TestFixture]
    public class Day06Tests
    {
        private Mock<IInputLoader> loader;

        [SetUp]
        public void Setup()
        {
            loader = new Mock<IInputLoader>();
        }

        [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7)]
        [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
        [TestCase("nppdvjthqldpwncqszvftbrmjlhg", 6)]
        [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
        [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
        public void PartA(string input, int expectedResult)
        {
            // Arrange
            loader.Setup(x => x.LoadInput(It.IsAny<string>())).Returns(input);
            var sut = CreateSut();

            // Act
            var result = sut.PartA();

            // Assert
            result.ShouldBe(expectedResult);
        }

        [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
        [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
        [TestCase("nppdvjthqldpwncqszvftbrmjlhg", 23)]
        [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
        [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
        public void PartB(string input, int expectedResult)
        {
            // Arrange
            loader.Setup(x => x.LoadInput(It.IsAny<string>())).Returns(input);
            var sut = CreateSut();

            // Act
            var result = sut.PartB();

            // Assert
            result.ShouldBe(expectedResult);
        }

        private Day06 CreateSut() => new(loader.Object);
    }
}
