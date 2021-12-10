using AdventOfCode._2021;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace AdventOfCode.Tests._2021
{
    [TestFixture]
    public class Day09Tests
    {
        private Mock<IInputLoader> loader;

        [SetUp]
        public void Setup()
        {
            loader = new Mock<IInputLoader>();
            loader.Setup(x => x.LoadArray<string>(It.IsAny<string>(), It.IsAny<string>()))
                  .Returns(new string []
                  {
                      "2199943210",
                      "3987894921",
                      "9856789892",
                      "8767896789",
                      "9899965678"
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
            result.ShouldBe(15);
        }

        [Test]
        public void PartB()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.PartB();

            // Assert
            result.ShouldBe(1134);
        }

        private Day09 CreateSut() => new Day09(loader.Object);
    }
}
