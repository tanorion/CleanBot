namespace Abstractions
{
    public interface IInstructor
    {
        Position GetStartingPosition();
        bool TryGetNextMove(out Move move);
    }
}