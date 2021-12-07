using AdventOfCode._2021;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace AdventOfCode.Tests._2021
{
    [TestFixture]
    public class Day03Tests
    {
        private Mock<IInputLoader> loader;

        [SetUp]
        public void Setup()
        {
            loader = new Mock<IInputLoader>();
            loader.Setup(x => x.LoadArray<string>(It.IsAny<string>(), It.IsAny<string>()))
                  .Returns(new string[]
                  {
                      "00100",
                      "11110",
                      "10110",
                      "10111",
                      "10101",
                      "01111",
                      "00111",
                      "11100",
                      "10000",
                      "11001",
                      "00010",
                      "01010"
                  });
        }

        [Test]
        public void PartA()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.PartA();

            // Assert
            result.ShouldBe(198);
        }

        [Test]
        public void PartB()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.PartB();

            // Assert
            result.ShouldBe(230);
        }

        private Day03 CreateSut() => new Day03(loader.Object);
    }
}
