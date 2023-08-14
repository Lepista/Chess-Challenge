using ChessChallenge.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ChessBot
{
    public struct MyBot
    {
        private static readonly int[] KnightOffsets = { -17, -15, -10, -6, 6, 10, 15, 17 };

        public void Think(GameState gameState, TimeSpan remainingTime)
        {
            List<Move> legalMoves = GenerateLegalMoves(gameState);
            Move bestMove = ChooseBestMove(gameState, legalMoves);
            gameState.PlayMove(bestMove);
        }

        private List<Move> GenerateLegalMoves(GameState gameState)
        {
            List<Move> legalMoves = new List<Move>();
            int currentPlayer = gameState.CurrentPlayer;

            foreach (Piece piece in gameState.Pieces)
            {
                if (piece.Color != currentPlayer)
                    continue;

                switch (piece.Type)
                {
                    case PieceType.Knight:
                        legalMoves.AddRange(GenerateKnightMoves(piece, gameState.Board));
                        break;
                    // Add logic for other pieces...
                }
            }

            return legalMoves;
        }

        private List<Move> GenerateKnightMoves(Piece knight, Square[,] board)
        {
            List<Move> moves = new List<Move>();

            foreach (int offset in KnightOffsets)
            {
                int destIndex = knight.Position + offset;
                if (IsValidSquare(destIndex))
                {
                    Square destSquare = board[destIndex / 8, destIndex % 8];
                    if (destSquare.IsEmpty || destSquare.Piece.Color != knight.Color)
                    {
                        moves.Add(new Move(knight.Position, destIndex));
                    }
                }
            }

            return moves;
        }

        private bool IsValidSquare(int index)
        {
            return index >= 0 && index < 64;
        }

        private Move ChooseBestMove(GameState gameState, List<Move> legalMoves)
        {
            Random random = new Random();
            int randomIndex = random.Next(0, legalMoves.Count);
            Move bestMove = legalMoves[randomIndex];
            return bestMove;
        }
    }
}
