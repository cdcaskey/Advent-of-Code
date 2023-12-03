using System.Collections.Generic;
using AdventOfCode._2022;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace AdventOfCode.Tests._2022
{
    [TestFixture]
    public class Day03Tests
    {
        private Mock<IInputLoader> loader;

        [SetUp]
        public void Setup()
        {
            loader = new Mock<IInputLoader>();
            loader.Setup(x => x.LoadArray<string>(It.IsAny<string>(), It.IsAny<string>()))
                  .Returns(
                  [
                      "vJrwpWtwJgWrhcsFMMfFFhFp",
                      "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
                      "PmmdzqPrVvPwwTWBwg",
                      "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
                      "ttgJtRGJQctTZtZT",
                      "CrZsJsPPZsGzwwsLwLmpwMDw"
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
            result.ShouldBe(157);
        }

        [Test]
        public void PartB()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.PartB();

            // Assert
            result.ShouldBe(70);
        }

        private Day03 CreateSut() => new(loader.Object);
    }
}
