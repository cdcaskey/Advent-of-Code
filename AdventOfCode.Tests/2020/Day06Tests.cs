using AdventOfCode._2020;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace AdventOfCode.Tests._2020
{
    [TestFixture]
    public class Day06Tests
    {
        private Mock<IInputLoader> loader;

        [SetUp]
        public void Setup()
        {
            loader = new Mock<IInputLoader>();
            loader.Setup(x => x.LoadArray<string>(It.IsAny<string>(), It.IsAny<string>()))
                  .Returns(new string []
                  {
                      "abc",
                      "a\r\nb\r\nc",
                      "ab\r\nac",
                      "a\r\na\r\na\r\na",
                      "b"
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
            result.ShouldBe(11);
        }

        [Test]
        public void PartB()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.PartB();

            // Assert
            result.ShouldBe(6);
        }

        private Day06 CreateSut() => new Day06(loader.Object);
    }
}
