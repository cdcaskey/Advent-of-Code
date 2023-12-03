using AdventOfCode._2020;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace AdventOfCode.Tests._2020
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
                      "..##.......",
                      "#...#...#..",
                      ".#....#..#.",
                      "..#.#...#.#",
                      ".#...##..#.",
                      "..#.##.....",
                      ".#.#.#....#",
                      ".#........#",
                      "#.##...#...",
                      "#...##....#",
                      ".#..#...#.#"
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
            result.ShouldBe(336);
        }

        private Day03 CreateSut() => new(loader.Object);
    }
}
