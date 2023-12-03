using System.Collections.Generic;
using AdventOfCode._2022;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace AdventOfCode.Tests._2022
{
    [TestFixture]
    public class Day01Tests
    {
        private Mock<IInputLoader> loader;

        [SetUp]
        public void Setup()
        {
            loader = new Mock<IInputLoader>();
            loader.Setup(x => x.LoadListOfArrays<int>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                  .Returns(
                  [
                      [1000, 2000, 3000],
                      [4000],
                      [5000, 6000],
                      [7000, 8000, 9000],
                      [10000]
                  ]);
        }

        [Test]
        public void PartA()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.PartA();

            // Assert
            result.ShouldBe(24000);
        }

        [Test]
        public void PartB()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.PartB();

            // Assert
            result.ShouldBe(45000);
        }

        private Day01 CreateSut() => new(loader.Object);
    }
}
