using AdventOfCode._2021;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace AdventOfCode.Tests._2021
{
    [TestFixture]
    public class Day07Tests
    {
        private Mock<IInputLoader> loader;

        [SetUp]
        public void Setup()
        {
            loader = new Mock<IInputLoader>();
            loader.Setup(x => x.LoadArray<int>(It.IsAny<string>(), It.IsAny<string>()))
                  .Returns(new int [] { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 });
        }

        [Test]
        public void PartA()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.PartA();

            // Assert
            result.ShouldBe(37);
        }

        [Test]
        public void PartB()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.PartB();

            // Assert
            result.ShouldBe(168);
        }

        private Day07 CreateSut() => new Day07(loader.Object);
    }
}
