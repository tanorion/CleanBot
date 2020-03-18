namespace Abstractions
{
    public interface ICleaningBot
    {
        void SetStartingPosition(Position startingPosition);
        void Move(Move move);
        long GetTotalTilesCleaned();
    }
}