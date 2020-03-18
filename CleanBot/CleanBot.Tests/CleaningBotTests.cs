using System;
using System.Collections.Generic;
using System.Text;
using Abstractions;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CleanBot.Tests
{
    public class CleaningBotTests
    {

        [Fact]
        public void cleaning_bot_should_add_start_position_to_cleaned_tiles()
        {
            var bot = new CleaningBot(Mock.Of<ILogger<CleaningBot>>());
            bot.SetStartingPosition(new Position(1, 1));
            Assert.Equal(1, bot.GetTotalTilesCleaned());
        }
        [Fact]
        public void cleaning_bot_should_return_correct_tiles_for_simple_case()
        {
            var bot=new CleaningBot(Mock.Of<ILogger<CleaningBot>>());
            bot.SetStartingPosition(new Position(1,1));
            bot.Move(new Move(MoveDirection.N,1));
            Assert.Equal(2,bot.GetTotalTilesCleaned());
        }

        [Fact]
        public void cleaning_bot_should_not_add_tile_twice_in_y_move()
        {
            var bot = new CleaningBot(Mock.Of<ILogger<CleaningBot>>());
            bot.SetStartingPosition(new Position(1, 1));
            bot.Move(new Move(MoveDirection.N, 1));
            bot.Move(new Move(MoveDirection.S, 1));
            Assert.Equal(2, bot.GetTotalTilesCleaned());
        }

        [Fact]
        public void cleaning_bot_should_not_add_tile_twice_in_x_move()
        {
            var bot = new CleaningBot(Mock.Of<ILogger<CleaningBot>>());
            bot.SetStartingPosition(new Position(1, 1));
            bot.Move(new Move(MoveDirection.E, 1));
            bot.Move(new Move(MoveDirection.W, 1));
            Assert.Equal(2, bot.GetTotalTilesCleaned());
        }

        [RunnableInDebugOnlyAttribute]
        //worst case scenario will take atleast approximately 75gig of memory, think of that before running!
        public void cleaning_bot_should_work_in_wost_case_scenario()
        {
            var bot = new CleaningBot(Mock.Of<ILogger<CleaningBot>>());
            bot.SetStartingPosition(new Position(100000, 100000));
            MoveDirection direction = MoveDirection.W;
            for (int i = 0; i < 10000; i++)
            {
                bot.Move(new Move(direction, 200000));
                bot.Move(new Move(MoveDirection.N,1));
                direction = direction == MoveDirection.W ? MoveDirection.E : MoveDirection.W;
            }
          
            Assert.Equal(2000000000, bot.GetTotalTilesCleaned());
        }
    }
}
