using System;
using System.Collections.Generic;

namespace ai
{
    public static class AI
    {
        public static int[] NextMove(GameMessage gameMessage)
        {

            

            int[] nextMove = new int[] { 0, 0 };
            
            
            int nextMoveScore = -1000;
            int curScore = 0;


           


            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if (gameMessage.board[i][j] == 0)
                    {

                        if (IsValidMove(gameMessage, i, j, gameMessage.player))
                        {
                            curScore = PlayerScore(gameMessage, i, j);
                            GameMessage oppEval = gameMessage;
                            oppEval.board[i][j] = gameMessage.player;
                            curScore = curScore + GetBestOppScore(oppEval, i, j, GetOppPlayer(gameMessage));
                            if (curScore > nextMoveScore)
                            {


                                nextMoveScore = curScore;
                                nextMove = new[] { i, j };

                            }
                        }
                    }
                    
                }
            }



            return nextMove;
        }

        public static List<int[]> GetAllValidMoves(GameMessage gameMessage)
        {
            List<int[]> validMoves = new List<int[]>();
            for (int x = 0; x > 8; x++)
            {
                for (int y = 0; y > 8; y++)
                {
                    if (gameMessage.board[y][x] == gameMessage.player)
                    {
                        for (int xPeek = -1; xPeek <= 1; xPeek++)
                        {
                            for (int yPeek = -1; yPeek <= 1; yPeek++)
                            {
                                if (xPeek != 0 && yPeek != 0)
                                {
                                    if (gameMessage.board[x + xPeek][y + yPeek] != 0 && gameMessage.board[x + xPeek][y + yPeek] != gameMessage.player)
                                    {
                                        bool found = false;
                                        int xPos = x + xPeek;
                                        int yPos = x + yPeek;
                                        while (!found)
                                        {
                                            
                                            if (xPos > 7 || xPos < 0 || yPos > 7 || yPos < 0)
                                            {
                                                found = true;
                                            }
                                            else if (gameMessage.board[yPos][xPos] != 0 && gameMessage.board[yPos][xPos] != gameMessage.player)
                                            {

                                            }
                                            else if (gameMessage.board[yPos][xPos] == 0)
                                            {
                                                validMoves.Add(new int[] { yPos, xPos });
                                            }
                                            xPos = x + xPeek;
                                            yPos = x + yPeek;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                
            }
            return validMoves;
        }
        public static int[] BestMove(int row, int column, int score, int newscore, int[] nextStep)
        {
            if (score > newscore)
            {
                return nextStep;
            }
            else
            {
                int[] newMove = new[] { row, column };
                return newMove;
            }
        }

        

        
        public static int GetOppPlayer(GameMessage gameMessage)
        {
            if(gameMessage.player == 1)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }


        public static bool legalMove(int r, int c, int color, bool flip, GameMessage gameMessage)
        {
            // Initialize boolean legal as false
            bool legal = false;

            // If the cell is empty, begin the search
            // If the cell is not empty there is no need to check anything 
            // so the algorithm returns boolean legal as is
            if (gameMessage.board[r][c] == 0)
            {
                // Initialize variables
                int posX;
                int posY;
                bool found;
                int current;

                // Searches in each direction
                // x and y describe a given direction in 9 directions
                // 0, 0 is redundant and will break in the first check
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        // Variables to keep track of where the algorithm is and
                        // whether it has found a valid move
                        posX = c + x;
                        posY = r + y;
                        found = false;
                        current = gameMessage.board[posY][posX];

                        // Check the first cell in the direction specified by x and y
                        // If the cell is empty, out of bounds or contains the same color
                        // skip the rest of the algorithm to begin checking another direction
                        if (current == -1 || current == 0 || current == color)
                        {
                            continue;
                        }

                        // Otherwise, check along that direction
                        while (!found)
                        {
                            posX += x;
                            posY += y;
                            current = gameMessage.board[posY][posX];

                            // If the algorithm finds another piece of the same color along a direction
                            // end the loop to check a new direction, and set legal to true
                            if (current == color)
                            {
                                found = true;
                                legal = true;

                                // If flip is true, reverse the directions and start flipping until 
                                // the algorithm reaches the original location
                                if (flip)
                                {
                                    posX -= x;
                                    posY -= y;
                                    current = gameMessage.board[posY][posX];

                                    while (current != 0)
                                    {
                                        gameMessage.board[posY][posX] = color;
                                        posX -= x;
                                        posY -= y;
                                        current = gameMessage.board[posY][posX];
                                    }
                                }
                            }
                            // If the algorithm reaches an out of bounds area or an empty space
                            // end the loop to check a new direction, but do not set legal to true yet
                            else if (current == -1 || current == 0)
                            {
                                found = true;
                            }
                        }
                    }
                }
            }

            return legal;
        }

        public static bool IsValidMove(GameMessage gameMessage, int row, int column, int player)
        {
            // Initialize boolean legal as false
            bool legal = false;
            int oppPlayer;
            if(player == 1)
            {
                oppPlayer = 2;
            }
            else
            {
                oppPlayer = 1;
            }

            // If the cell is empty, begin the search
            // If the cell is not empty there is no need to check anything 
            // so the algorithm returns boolean legal as is
            if (gameMessage.board[row][column] == 0)
            {
                // Initialize variables
                int posX;
                int posY;
                bool found;
                int current;

                // Searches in each direction
                // x and y describe a given direction in 9 directions
                // 0, 0 is redundant and will break in the first check
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        // Variables to keep track of where the algorithm is and
                        // whether it has found a valid move
                        posX = column + x;
                        posY = row + y;
                        
                        found = false;

                        if (posY < 0 || posY > 7 || posX > 7 || posX < 0)
                        {
                            current = -1;

                        }
                        else if (x == 0 && y == 0)
                        {
                            current = -1;
                        }
                        else
                        {

                            current = gameMessage.board[posY][posX];
                        }

                        // Check the first cell in the direction specified by x and y
                        // If the cell is empty, out of bounds or contains the same color
                        // skip the rest of the algorithm to begin checking another direction
                        if (current == oppPlayer)
                        {
                            while (!found)
                            {
                                posX += x;
                                posY += y;
                                if (posY < 0 || posY > 7 || posX > 7 || posX < 0)
                                {
                                    found = true;
                                    current = -1;
                                }
                                else
                                {
                                    current = gameMessage.board[posY][posX];
                                }

                                // If the algorithm finds another piece of the same color along a direction
                                // end the loop to check a new direction, and set legal to true
                                if (current == gameMessage.player)
                                {
                                    found = true;
                                    Console.WriteLine("I declare x=" + posX + " y=" + posY + " to be a player");
                                    //legal = true;

                                    posX -= x;
                                    posY -= y;
                                    current = gameMessage.board[posY][posX];
                                    bool flip = false;
                                    while (current != 0)
                                    {
                                        if(gameMessage.board[posY][posX] ==oppPlayer)
                                        {
                                            flip = true;
                                        }
                                        posX -= x;
                                        posY -= y;
                                        
                                        current = gameMessage.board[posY][posX];
                                        if(flip == true && current == 0)
                                        {
                                            legal = true;
                                            
                                        }
                                    }

                                }
                                // If the algorithm reaches an out of bounds area or an empty space
                                // end the loop to check a new direction, but do not set legal to true yet
                                else if (current != gameMessage.player || current != oppPlayer)
                                {
                                    found = true;
                                    break;
                                }
                                if(current == 0)
                                {
                                    found = true;
                                    break;

                                }
                                if(current == -1)
                                {
                                    found = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return legal;
        }
        // Checks whether a players move is valid given a row and column
        public static bool IsValidMoveOG(GameMessage gameMessage, int row, int column, int player)
        {
            int oppPlayer;
            if (player == 1)
            {
                oppPlayer = 2;
            }
            else
            {
                oppPlayer = 1;
            }

            


            if (gameMessage.board[row][column] == 0 )
            {
                bool maybeValid = false;

                //Checks for vertical play
                if (row <= 5)
                {
                    for (int i = row + 1; i < 7; i++)
                    {
                        if (gameMessage.board[i][column] == oppPlayer)
                        {
                            maybeValid = true;
                        }
                        else if (gameMessage.board[i][column] == player && maybeValid == true)
                        {
                            Console.WriteLine("93");
                            return true;
                        }
                        else
                        {
                            maybeValid = false;
                            break;
                        }
                    }
                }
                maybeValid = false;
                //Checks for horizontal play

                if (row >= 2)
                {
                    for (int i = row - 1; i >= 1; i--)
                    {
                        if (gameMessage.board[i][column] == oppPlayer)
                        {
                            maybeValid = true;
                        }
                        else if (gameMessage.board[i][column] == player && maybeValid == true)
                        {
                            Console.WriteLine("116");
                            return true;
                        }
                        else
                        {
                            maybeValid = false;
                            break;
                        }
                    }
                }
                maybeValid = false;
                if (column <= 5)
                {
                    for (int i = column + 1; i < 7; i++)
                    {
                        if (gameMessage.board[row][i] == oppPlayer)
                        {
                            maybeValid = true;
                        }
                        else if (gameMessage.board[row][i] == player && maybeValid == true)
                        {
                            Console.WriteLine("137");
                            return true;
                        }
                        else
                        {
                            maybeValid = false;
                            break;
                        }
                    }
                }
                maybeValid = false;
                if (column >= 2)
                {
                    for (int i = column - 1; i >= 1; i--)
                    {
                        if (gameMessage.board[row][i] == oppPlayer)
                        {
                            maybeValid = true;
                        }
                        else if (gameMessage.board[row][i] == player && maybeValid == true)
                        {
                            Console.WriteLine("158");
                            return true;
                        }
                        else
                        {
                            maybeValid = false;
                            break;
                        }
                    }
                }
                maybeValid = false;
                int rowCheck = row;
                int columnCheck = column;

                if (row <= 5 && column <= 5)
                {
                    while (rowCheck < 8 && rowCheck >= 0 && columnCheck < 8 && columnCheck >= 0)
                    {
                        rowCheck = rowCheck++;
                        columnCheck = columnCheck++;
                        if (gameMessage.board[rowCheck][columnCheck] == oppPlayer)
                        {
                            maybeValid = true;
                        }
                        else if (gameMessage.board[rowCheck][columnCheck] == player && maybeValid == true)
                        {
                            Console.WriteLine("180");
                            return true;
                        }
                        else
                        {
                            maybeValid = false;
                            rowCheck = row;
                            columnCheck = column;
                            break;
                        }
                    }
                }
                maybeValid = false;
                rowCheck = row;
                columnCheck = column;
                if (row >= 2 && column <= 5)
                {
                    while (rowCheck < 8 && rowCheck >= 0 && columnCheck < 8 && columnCheck >= 0)
                    {
                        rowCheck = rowCheck--;
                        columnCheck = columnCheck++;
                        if (gameMessage.board[rowCheck][columnCheck] == oppPlayer)
                        {
                            maybeValid = true;
                        }
                        else if (gameMessage.board[rowCheck][columnCheck] == player && maybeValid == true)
                        {
                            Console.WriteLine("211");
                            return true;
                        }
                        else
                        {
                            maybeValid = false;
                            rowCheck = row;
                            columnCheck = column;
                            break;
                        }
                    }
                }
                maybeValid = false;
                rowCheck = row;
                columnCheck = column;
                if (row >= 2 && column >= 2)
                {
                    while (rowCheck < 8 && rowCheck >= 0 && columnCheck < 8 && columnCheck >= 0)
                    {
                        rowCheck = rowCheck--;
                        columnCheck = columnCheck--;
                        if (gameMessage.board[rowCheck][columnCheck] == oppPlayer)
                        {
                            maybeValid = true;
                        }
                        else if (gameMessage.board[rowCheck][columnCheck] == player && maybeValid == true)
                        {
                            Console.WriteLine("238");
                            return true;
                        }
                        else
                        {
                            maybeValid = false;
                            rowCheck = row;
                            columnCheck = column;
                            break;
                        }
                    }
                }
                maybeValid = false;
                rowCheck = row;
                columnCheck = column;
                if (row <= 5 && column >= 2)
                {
                    while (rowCheck < 8 && rowCheck >= 0 && columnCheck < 8 && columnCheck >= 0)
                    {
                        rowCheck = rowCheck++;
                        columnCheck = columnCheck--;
                        if (gameMessage.board[rowCheck][columnCheck] == oppPlayer)
                        {
                            maybeValid = true;
                        }
                        else if (gameMessage.board[rowCheck][columnCheck] == player && maybeValid == true)
                        {
                            Console.WriteLine("265");
                            return true;
                        }
                        else
                        {
                            maybeValid = false;
                            rowCheck = row;
                            columnCheck = column;
                            break;
                        }
                    }
                }
                return false;

            }
            else
            {
                return false;
            }


        }
        public static int GetBestOppScore(GameMessage gameMessage, int row, int column, int player)
        {
            int score = 100;
            if (row + 1 < 8)
            {
                if (IsValidMove(gameMessage, row + 1, column, player))
                {
                    if(score > OppScore(gameMessage, row +1, column))
                    {
                        score = OppScore(gameMessage, row + 1, column);
                    }
                }
            }
            if (row - 1 >= 0)
            {
                if (IsValidMove(gameMessage, row - 1, column, player))
                {
                    if (score > OppScore(gameMessage, row - 1, column))
                    {
                        score = OppScore(gameMessage, row - 1, column);
                    }
                }
            }
            if(column-1 >= 0)
            {
                if (IsValidMove(gameMessage, row, column-1, player))
                {
                    if (score > OppScore(gameMessage, row, column-1))
                    {
                        score = OppScore(gameMessage, row, column-1);
                    }
                }
            }
            if(column +1 < 8)
            {
                if (IsValidMove(gameMessage, row , column+1, player))
                {
                    if (score > OppScore(gameMessage, row , column+1))
                    {
                        score = OppScore(gameMessage, row , column+1);
                    }
                }
            }
            if(row+1 < 8 && column +1 < 8)
            {
                if (IsValidMove(gameMessage, row + 1, column+1, player))
                {
                    if (score > OppScore(gameMessage, row + 1, column+1))
                    {
                        score = OppScore(gameMessage, row + 1, column+1);
                    }
                }
            }
            if(row-1 >=0 && column -1 >= 0)
            {
                if (IsValidMove(gameMessage, row -1, column-1, player))
                {
                    if (score > OppScore(gameMessage, row - 1, column-1))
                    {
                        score = OppScore(gameMessage, row - 1, column-1);
                    }
                }
            }
            if(row+1 < 8 && column -1 >= 0)
            {
                if (IsValidMove(gameMessage, row +1, column-1, player))
                {
                    if (score > OppScore(gameMessage, row + 1, column-1))
                    {
                        score = OppScore(gameMessage, row + 1, column-1);
                    }
                }
            }
            if (row -1 >=0  && column +1 < 8)
            {
                if (IsValidMove(gameMessage, row - 1, column+1, player))
                {
                    if (score > OppScore(gameMessage, row - 1, column+1))
                    {
                        score = OppScore(gameMessage, row - 1, column+1);
                    }
                }
            }
            return score;
        }

        // Rates a move. TODO: Change -100 to -30
        public static int OppScore(GameMessage gameMessage, int row, int column)
        {
            if(IsValidMove(gameMessage,row,column, gameMessage.player))
            switch (row)
            {
                case 0:
                    if (column == 0 || column == 7)
                    {
                        return -60;
                    }
                    else
                    {
                        return -30;
                    }

                case 1:
                    if (column == 0 || column == 7)
                    {
                        return -30;
                    }
                    else
                    {
                        return 30;
                    }
                case 2:
                    if (column == 0 || column == 7)
                    {
                        return -30;
                    }
                    else if (column == 1 || column == 6)
                    {
                        return +30;
                    }
                    else
                    {
                        return 0;
                    }
                case 3:
                    if (column == 0 || column == 7)
                    {
                        return -30;
                    }
                    else if (column == 1 || column == 6)
                    {
                        return 30;
                    }
                    else
                    {
                        return 0;
                    }
                case 4:
                    if (column == 0 || column == 7)
                    {
                        return -30;
                    }
                    else if (column == 1 || column == 6)
                    {
                        return 30;
                    }
                    else
                    {
                        return 0;
                    }
                case 5:
                    if (column == 0 || column == 7)
                    {
                        return -30;
                    }
                    else if (column == 1 || column == 6)
                    {
                        return 30;
                    }
                    else
                    {
                        return 0;
                    }
                case 6:
                    if (column == 0 || column == 7)
                    {
                        return -30;
                    }
                    else
                    {
                        return 30;
                    }
                case 7:
                    if (column == 0 || column == 7)
                    {
                        return -60;
                    }
                    else
                    {
                        return -30;
                    }
                default:
                    return 0;
            }
            else
            {
                return 0;
            }
        }

        public static int PlayerScore(GameMessage gameMessage, int row, int column)
        {
            switch (row)
            {
                case 0:
                    if (column == 0 || column == 7)
                    {
                        return 100;
                    }
                    else
                    {
                        return 90;
                    }

                case 1:
                    if (column == 0 || column == 7)
                    {
                        return 90;
                    }
                    else
                    {
                        return 0;
                    }
                case 2:
                    if (column == 0 || column == 7)
                    {
                        return 90;
                    }
                    else if (column == 1 || column == 6)
                    {
                        return 0;
                    }
                    else
                    {
                        return 50;
                    }
                case 3:
                    if (column == 0 || column == 7)
                    {
                        return 90;
                    }
                    else if (column == 1 || column == 6)
                    {
                        return 0;
                    }
                    else
                    {
                        return 50;
                    }
                case 4:
                    if (column == 0 || column == 7)
                    {
                        return 90;
                    }
                    else if (column == 1 || column == 6)
                    {
                        return 0;
                    }
                    else
                    {
                        return 50;
                    }
                case 5:
                    if (column == 0 || column == 7)
                    {
                        return 90;
                    }
                    else if (column == 1 || column == 6)
                    {
                        return 0;
                    }
                    else
                    {
                        return 50;
                    }
                case 6:
                    if (column == 0 || column == 7)
                    {
                        return 90;
                    }
                    else
                    {
                        return 0;
                    }
                case 7:
                    if (column == 0 || column == 7)
                    {
                        return 100;
                    }
                    else
                    {
                        return 90;
                    }
                default:
                    return 0;
            }
        }

        public static bool isEmpty(GameMessage gameMessage, int check, int check2)
        {
            if (gameMessage.board[check][check2] == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    
}
