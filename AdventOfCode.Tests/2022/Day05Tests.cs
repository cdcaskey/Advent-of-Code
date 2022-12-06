using System.Collections.Generic;
using AdventOfCode._2022;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace AdventOfCode.Tests._2022
{
    [TestFixture]
    public class Day05Tests
    {
        private Mock<IInputLoader> loader;

        [SetUp]
        public void Setup()
        {
            loader = new Mock<IInputLoader>();
            loader.Setup(x => x.LoadListOfArrays<string>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                  .Returns(
                  [
                      [
                          "    [D]    ",
                          "[N] [C]    ",
                          "[Z] [M] [P]",
                          " 1   2   3 "
                      ],
                      [
                         "move 1 from 2 to 1",
                         "move 3 from 1 to 3",
                         "move 2 from 2 to 1",
                         "move 1 from 1 to 2"
                      ]
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
            result.ShouldBe("CMZ");
        }

        [Test]
        public void PartB()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.PartB();

            // Assert
            result.ShouldBe("MCD");
        }

        private Day05 CreateSut() => new(loader.Object);
    }
}
