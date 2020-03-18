using System.Collections.Generic;
using Abstractions;
using Microsoft.Extensions.Logging;

namespace CleanBot
{
    public class CleaningBot : ICleaningBot
    {
        private readonly ILogger<CleaningBot> _logger;
        private Position _staringPosition;
        private Position _currentPosition;
        private HashSet<string> hashTilesCleaned;

        public CleaningBot(ILogger<CleaningBot> logger)
        {

            _logger = logger;
            hashTilesCleaned=new HashSet<string>();
            _staringPosition=new Position(0,0);
            _currentPosition = new Position(0, 0);
        }
        public void SetStartingPosition(Position startingPosition)
        {
            _staringPosition = startingPosition;
            _currentPosition = startingPosition;
            hashTilesCleaned.Add($"{_currentPosition.X} {_currentPosition.Y}");
        }

        public void Move(Move move)
        {
            switch (move.Direction )
            {
                case MoveDirection.E:
                    for (int i = 1; i <= move.Distance; i++)
                    {
                        hashTilesCleaned.Add($"{_currentPosition.X + i} {_currentPosition.Y}");
                    }
                    _currentPosition.MoveX(move.Distance);
                    break;
                case MoveDirection.W:
                    for (int i = 1; i <= move.Distance; i++)
                    {
                        hashTilesCleaned.Add($"{_currentPosition.X - i} {_currentPosition.Y}");
                    }
                    _currentPosition.MoveX(-move.Distance);
                    break;
                case MoveDirection.N:
                    for (int i = 1; i <= move.Distance; i++)
                    {
                        hashTilesCleaned.Add($"{_currentPosition.X} {_currentPosition.Y-i}");
                    }
                    _currentPosition.MoveY(-move.Distance);
                    break;
                case MoveDirection.S:
                    for (int i = 1; i <= move.Distance; i++)
                    {
                        hashTilesCleaned.Add($"{_currentPosition.X } {_currentPosition.Y+i}");
                    }
                    _currentPosition.MoveY(move.Distance);
                    break;
            }
        }

        public long GetTotalTilesCleaned()
        {
            return hashTilesCleaned.Count;
        }
    }
}
