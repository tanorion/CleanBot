using System;
using System.Collections.Generic;
using System.Text;
using Abstractions;
using Microsoft.Extensions.Logging;

namespace CleanBot
{
    public class CleaningService : ICleaningService
    {
        private readonly ILogger<CleaningService> _logger;
        private readonly IInstructor _instructor;
        private readonly ICleaningBot _cleaningBot;

        public CleaningService(ILogger<CleaningService> logger,IInstructor instructor, ICleaningBot cleaningBot)
        {
            _logger = logger;
            _instructor = instructor;
            _cleaningBot = cleaningBot;
        }
        public void Clean()
        {
            var staringPosition = _instructor.GetStartingPosition();
            _logger.LogDebug($"CleaingService starting position is x:{staringPosition.X}, y:{staringPosition.Y}");
            _cleaningBot.SetStartingPosition(staringPosition);
           
            Move move;
            while (_instructor.TryGetNextMove(out move))
            {
                _cleaningBot.Move(move);
            }

          
            _logger.LogDebug($"CleaingService finished");
        }

        public long GetTilesCleaned()
        {
            return _cleaningBot.GetTotalTilesCleaned();
        }
    }
}
