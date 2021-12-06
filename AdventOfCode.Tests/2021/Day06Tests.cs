using AdventOfCode._2021;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace AdventOfCode.Tests._2021
{
    [TestFixture]
    public class Day06Tests
    {
        private Mock<IInputLoader> loader;

        [SetUp]
        public void Setup()
        {
            loader = new Mock<IInputLoader>();
            loader.Setup(x => x.LoadArray<int>(It.IsAny<string>(), It.IsAny<string>()))
                  .Returns(new int [] { 3, 4, 3, 1, 2 });
        }

        [Test]
        public void PartA()
        {
            // Arrange
            CreateSut();

            // Act
            var result = Day06.PartA();

            // Assert
            result.ShouldBe(5934);
        }

        [Test]
        public void PartB()
        {
            // Arrange
            CreateSut();

            // Act
            var result = Day06.PartB();

            // Assert
            result.ShouldBe(26984457539);
        }

        private Day06 CreateSut() => new Day06(loader.Object);
    }
}
