using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Abstractions;
using Microsoft.Extensions.Logging;

namespace CleanBot
{
    public class CleaningBotVector : ICleaningBot
    {
        private readonly ILogger<CleaningBotVector> _logger;
        private Position _staringPosition;
        private Position _currentPosition;
        private HashSet<Position> intersections;
        private List<LineSegment> VertialSegments;
        private List<LineSegment> HorizontalSegments;

        public CleaningBotVector(ILogger<CleaningBotVector> logger)
        {

            _logger = logger;
            intersections = new HashSet<Position>();
            _staringPosition=new Position(0,0);
            _currentPosition = new Position(0, 0);
            VertialSegments=new List<LineSegment>();
            HorizontalSegments=new List<LineSegment>();
            
        }
        public void SetStartingPosition(Position startingPosition)
        {
            _staringPosition = startingPosition;
            _currentPosition = startingPosition;
            VertialSegments.Add(new LineSegment()
            {
                End = startingPosition,
                Start = startingPosition,
                StaticPositionValue = startingPosition.Y,
                Vertical = true
            });
      
        }

        public void Move(Move move)
        {
            Position start = null;
            switch (move.Direction )
            {
                    
                case MoveDirection.E:
                    start =new Position(_currentPosition.X, _currentPosition.Y); 
                    _currentPosition.MoveX(move.Distance);
                    HorizontalSegments.Add(new LineSegment()
                    {
                        Start = start,
                        End = new Position(_currentPosition.X, _currentPosition.Y),
                        StaticPositionValue = start.Y,
                        Vertical = false,
                    });

                    break;
                case MoveDirection.W:
                    start = new Position(_currentPosition.X, _currentPosition.Y);
                    _currentPosition.MoveX(-move.Distance);
                    HorizontalSegments.Add(new LineSegment()
                    {
                        Start = new Position(_currentPosition.X, _currentPosition.Y),
                        End = start,
                        StaticPositionValue = start.Y,
                        Vertical = false,
                    });
                    break;
                case MoveDirection.N:
                    start = new Position(_currentPosition.X, _currentPosition.Y);
                    _currentPosition.MoveY(-move.Distance);
                    VertialSegments.Add(new LineSegment()
                    {
                        Start = new Position(_currentPosition.X, _currentPosition.Y),
                        End = start,
                        StaticPositionValue = start.X,
                        Vertical = true,
                    });
                    
                    break;
                case MoveDirection.S:
                    start = new Position(_currentPosition.X, _currentPosition.Y);
                    _currentPosition.MoveY(move.Distance);
                    VertialSegments.Add(new LineSegment()
                    {
                        Start = start,
                        End = new Position(_currentPosition.X, _currentPosition.Y),
                        StaticPositionValue = start.X,
                        Vertical = true,
                    });
                    break;
            }
        }

        public long GetTotalTilesCleaned()
        {
            var allNotCombined = false;
            var horizontalI = 0;
            var verticalI = 0;
            while (!allNotCombined)
            {
                var allNotCombinedVertical = false;
               
                for (int i = verticalI; i < VertialSegments.Count-1; i++)
                {
                    for (int j = i+1; j < VertialSegments.Count; j++)
                    {
                        LineSegment segment;
                        if (LineSegment.TryCombineSegment(VertialSegments[i], VertialSegments[j], out segment))
                        {
                            allNotCombinedVertical = true;
                            VertialSegments.Add(segment);
                            VertialSegments.RemoveAt(i);
                            VertialSegments.RemoveAt(j);
                            break;
                        }
                    }

                    if (allNotCombinedVertical)
                    {
                        verticalI = i;
                        break;
                    }
                }






                var allNotCombinedHorizontal = false;

                for (int i = horizontalI; i < HorizontalSegments.Count-1; i++)
                {
                    for (int j = i+1; j < HorizontalSegments.Count; j++)
                    {
                        LineSegment segment;
                        if (LineSegment.TryCombineSegment(HorizontalSegments[i], HorizontalSegments[j], out segment))
                        {
                            allNotCombinedHorizontal = true;
                            HorizontalSegments.Add(segment);
                            HorizontalSegments.RemoveAt(i);
                            HorizontalSegments.RemoveAt(j);
                            break;
                        }
                    }

                    if (allNotCombinedHorizontal)
                    {
                        horizontalI = i;
                        break;
                    }
                }

                allNotCombined = !allNotCombinedHorizontal && !allNotCombinedVertical;
            }

            foreach (var horizontalSegment in HorizontalSegments)
            {
                foreach (var vertialSegment in VertialSegments)
                {
                    Position point;
                    if (LineSegment.Intersect(horizontalSegment, vertialSegment, out point))
                    {
                        intersections.Add(new Position(point.X,point.Y));
                    }
                }
            }

            long total=0;
            foreach (var horizontalSegment in HorizontalSegments)
            {
                total += horizontalSegment.End.X - horizontalSegment.Start.X +1;
            }

            foreach (var vertialSegment in VertialSegments)
            {
                total += vertialSegment.End.Y - vertialSegment.Start.Y +1;
            }

            total -= intersections.Count;

            return total;
        }

        
        }
    }