namespace ai
{
    public class GameMessage
    {
        public int maxTurnTime;
        public int player;
        public int[][] board;
    }

    public class OppGameMessage : GameMessage
    {
        public OppGameMessage(GameMessage gameMessage, int initScore)
        {
            maxTurnTime = gameMessage.maxTurnTime;
            player = gameMessage.player;
            board = gameMessage.board;
            score = initScore;
        }
        public int score;
    }
}