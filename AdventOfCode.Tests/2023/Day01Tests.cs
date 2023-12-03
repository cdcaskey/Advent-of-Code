using System.Collections.Generic;
using AdventOfCode._2023;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace AdventOfCode.Tests._2023
{
    [TestFixture]
    public class Day01Tests
    {
        private Mock<IInputLoader> loader;

        [SetUp]
        public void Setup()
        {
            loader = new Mock<IInputLoader>();
        }

        [Test]
        public void PartA()
        {
            // Arrange
            loader.Setup(x => x.LoadArray<string>(It.IsAny<string>(), It.IsAny<string>()))
                  .Returns(new string[]
                  {
                      "1abc2",
                      "pqr3stu8vwx",
                      "a1b2c3d4e5f",
                      "treb7uchet"
                  });
            var sut = CreateSut();

            // Act
            var result = sut.PartA();

            // Assert
            result.ShouldBe(142);
        }

        [Test]
        public void PartB()
        {
            // Arrange
            loader.Setup(x => x.LoadArray<string>(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new string[]
                {
                    "two1nine",
                    "eightwothree",
                    "abcone2threexyz",
                    "xtwone3four",
                    "4nineeightseven2",
                    "zoneight234",
                    "7pqrstsixteen"
                });
            var sut = CreateSut();

            // Act
            var result = sut.PartB();

            // Assert
            result.ShouldBe(281);
        }

        private Day01 CreateSut() => new(loader.Object);
    }
}
