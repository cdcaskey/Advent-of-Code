using AdventOfCode._2020;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace AdventOfCode.Tests._2020
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
                  .Returns(new[] { 1721, 979, 366, 299, 675, 1456 });
        }

        [Test]
        public void PartA()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.PartA();

            // Assert
            result.ShouldBe(514579);
        }

        [Test]
        public void PartB()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.PartB();

            // Assert
            result.ShouldBe(241861950);
        }

        private Day01 CreateSut() => new Day01(loader.Object);
    }
}
