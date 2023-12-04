using AdventOfCode._2023;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace AdventOfCode.Tests._2023
{
    [TestFixture]
    public class Day03Tests
    {
        private Mock<IInputLoader> loader;

        [SetUp]
        public void Setup()
        {
            var maxX = 10;
            var maxY = 10;

            loader = new Mock<IInputLoader>();
            loader.Setup(x => x.LoadGridofChars(It.IsAny<string>(), out maxX, out maxY, It.IsAny<char>(), It.IsAny<string>()))
                .Returns(new char[,]
                {
                    { '4', '.', '.', '.', '6', '.', '.', '.', '.', '.' },
                    { '6', '.', '.', '.', '1', '.', '.', '.', '.', '6' },
                    { '7', '.', '3', '.', '7', '.', '5', '.', '.', '6' },
                    { '.', '*', '5', '.', '*', '.', '9', '.', '$', '4' },
                    { '.', '.', '.', '.', '.', '.', '2', '.', '.', '.' },
                    { '1', '.', '.', '.', '.', '+', '.', '.', '*', '5' },
                    { '1', '.', '6', '#', '.', '.', '.', '7', '.', '9' },
                    { '4', '.', '3', '.', '.', '5', '.', '5', '.', '8' },
                    { '.', '.', '3', '.', '.', '8', '.', '5', '.', '.' },
                    { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.' },
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
            result.ShouldBe(4361);
        }

        [Test]
        public void PartB()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.PartB();

            // Assert
            result.ShouldBe(467835);
        }

        private Day03 CreateSut() => new(loader.Object);
    }
}
