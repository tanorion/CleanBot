using System;
using Abstractions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CleanBot.Tests
{
    public class CleaningServiceTests
    {
        private bool getbool;
        private Mock<IInstructor> instructorMock;
       [Fact]
        public void should_return_correct_tiles_cleand()
        {

            instructorMock=new Mock<IInstructor>();
            instructorMock.Setup(x => x.GetStartingPosition()).Returns(new Position(0, 0));

            getbool = true;
             var move=new Move(MoveDirection.N,1);
            instructorMock.Setup(x => x.TryGetNextMove(out move)).Returns(getbool).Callback(GetMoveBool);


            var cleanBotMock = new Mock<ICleaningBot>();
            cleanBotMock.Setup(x => x.GetTotalTilesCleaned()).Returns(1);
            var cleaningService = new CleaningService(Mock.Of<ILogger<CleaningService>>(), instructorMock.Object,cleanBotMock.Object);
            cleaningService.Clean();
            Assert.Equal(1,cleaningService.GetTilesCleaned());
            cleanBotMock.Verify(x=>x.SetStartingPosition(It.IsAny<Position>()),Times.Once);
            cleanBotMock.Verify(x => x.Move(It.IsAny<Move>()), Times.Once);
            cleanBotMock.Verify(x => x.GetTotalTilesCleaned(), Times.Once);
        }

        private void GetMoveBool()
        {
          
            getbool = false;
            var move = new Move(MoveDirection.N, 1);
            instructorMock.Setup(x => x.TryGetNextMove(out move)).Returns(getbool).Callback(GetMoveBool);
        }
    }
}
