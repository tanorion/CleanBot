using System;
using System.Collections.Generic;
using System.Text;
using Abstractions;
using Microsoft.Extensions.Logging;

namespace CleanBot
{
    public class ConsoleInstructor : IInstructor
    {
        private readonly ILogger<ConsoleInstructor> _logger;
        private readonly IConsoleWrapper _consoleWrapper;
        private long numberOfMoves;
        private long numberOfMovesRead;
        private Position staringPosition;

        public ConsoleInstructor(ILogger<ConsoleInstructor> logger, IConsoleWrapper consoleWrapper)
        {
            _logger = logger;
            numberOfMovesRead = 0;
            _consoleWrapper = consoleWrapper;
            var numMoves= _consoleWrapper.ReadLine();
            numberOfMoves = long.Parse(numMoves);
            var startPos = _consoleWrapper.ReadLine();
            staringPosition=new Position(int.Parse(startPos.Split(" ")[0]), int.Parse(startPos.Split(" ")[1]));

        }
        public Position GetStartingPosition()
        {
            return staringPosition;
        }

        public bool TryGetNextMove(out Move move)
        {
            
            if (numberOfMovesRead < numberOfMoves)
            {
                
                var movetext = _consoleWrapper.ReadLine();
                MoveDirection direction;
                if (!MoveDirection.TryParse<MoveDirection>(movetext.Split(" ")[0],true,out direction))
                {
                    _logger.LogError($"Could not parse MoveDirection: {movetext}");
                    move = null;
                    return false;
                }
                move =new Move(direction,int.Parse(movetext.Split(" ")[1]));
                numberOfMovesRead++;
                return true;
            }
            move = null;
            return false;
        }
    }
}
