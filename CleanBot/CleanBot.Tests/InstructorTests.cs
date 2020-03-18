using System;
using System.Collections.Generic;
using System.Text;
using Abstractions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CleanBot.Tests
{
  
    public class InstructorTests
    {
        

        [Fact]
        public void instuctor_should_read_starting_position_correctly()
        {
            var consoleMock = new Mock<IConsoleWrapper>();
            consoleMock.SetupSequence(x => x.ReadLine())
                .Returns("2")
                .Returns("1 2")
                .Returns("E 2")
                .Returns("N 1");
            IInstructor _instructor = new ConsoleInstructor(Mock.Of<ILogger<ConsoleInstructor>>(), consoleMock.Object);
            var pos=_instructor.GetStartingPosition();
            Assert.Equal(1, pos.X);
            Assert.Equal(2, pos.Y);
        }

        [Fact]
        public void instuctor_should_read_moves_correctly()
        {
            var consoleMock = new Mock<IConsoleWrapper>();
            consoleMock.SetupSequence(x => x.ReadLine())
                .Returns("2")
                .Returns("1 2")
                .Returns("E 2")
                .Returns("N 1");
            IInstructor _instructor = new ConsoleInstructor(Mock.Of<ILogger<ConsoleInstructor>>(), consoleMock.Object);

            Move move;
            Assert.True(_instructor.TryGetNextMove(out move));
            Assert.Equal(2, move.Distance);
            Assert.Equal(MoveDirection.E, move.Direction);
            Assert.True(_instructor.TryGetNextMove(out move));
            Assert.Equal(1 ,move.Distance);
            Assert.Equal(MoveDirection.N, move.Direction);
            Assert.False(_instructor.TryGetNextMove(out move));
        }
    }
}
