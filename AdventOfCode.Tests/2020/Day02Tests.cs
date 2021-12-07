using AdventOfCode._2020;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace AdventOfCode.Tests._2020
{
    [TestFixture]
    public class Day02Tests
    {
        private Mock<IInputLoader> loader;

        [SetUp]
        public void Setup()
        {
            loader = new Mock<IInputLoader>();
            loader.Setup(x => x.LoadArray<string>(It.IsAny<string>(), It.IsAny<string>()))
                  .Returns(new string[]
                  {
                      "1-3 a: abcde",
                      "1-3 b: cdefg",
                      "2-9 c: ccccccccc"
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
            result.ShouldBe(2);
        }

        [Test]
        public void PartB()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.PartB();

            // Assert
            result.ShouldBe(1);
        }

        private Day02 CreateSut() => new Day02(loader.Object);
    }
}
