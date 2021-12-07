using AdventOfCode._2021;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace AdventOfCode.Tests._2021
{
    [TestFixture]
    public class Day01Tests
    {
        private Mock<IInputLoader> loader;

        [SetUp]
        public void Setup()
        {
            loader = new Mock<IInputLoader>();
            loader.Setup(x => x.LoadArray<int>(It.IsAny<string>(), It.IsAny<string>()))
                  .Returns(new[] { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 });
        }

        [Test]
        public void PartA()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.PartA();

            // Assert
            result.ShouldBe(7);
        }

        [Test]
        public void PartB()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.PartB();

            // Assert
            result.ShouldBe(5);
        }

        private Day01 CreateSut() => new Day01(loader.Object);
    }
}
