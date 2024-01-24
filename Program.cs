using System;

namespace TicTacToe
{
    enum MenuOptions
    {
        NewGame = 1,
        AboutTheAuthor = 2,
        Exit = 3,
    }

    class TicTacToeGame
    {
        private string[] board = { " ", " ", " ", " ", " ", " ", " ", " ", " " };
        private int currentPlayerIndex = 0;

        public void Play()
        {
            int moveNumber = 0;

            do
            {
                DisplayBoard();

                string currentPlayer = GetCurrentPlayer();
                Console.Write($"{currentPlayer}'s move > ");
                int inputNumber = GetPlayerMove();

                if (MakeMove(inputNumber, currentPlayer))
                {
                    if (CheckForWinner(currentPlayer))
                    {
                        DisplayBoard();
                        Console.WriteLine($"{currentPlayer} wins!");
                        return;
                    }

                    moveNumber++;
                    currentPlayerIndex = (currentPlayerIndex + 1) % 2;
                }
                else
                {
                    Console.WriteLine("Illegal move! Try again.");
                }

            } while (moveNumber < 9);

            Console.WriteLine("It's a draw!");
        }

        private void DisplayBoard()
        {
            Console.WriteLine($" {board[0]} | {board[1]} | {board[2]} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {board[3]} | {board[4]} | {board[5]} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {board[6]} | {board[7]} | {board[8]} ");
        }

        private string GetCurrentPlayer()
        {
            return currentPlayerIndex % 2 == 0 ? "X" : "O";
        }

        private int GetPlayerMove()
        {
            int inputNumber;
            while (!int.TryParse(Console.ReadLine(), out inputNumber) || inputNumber < 1 || inputNumber > 9 || board[inputNumber - 1] != " ")
            {
                Console.WriteLine("Illegal move! Try again.");
            }

            return inputNumber;
        }

        private bool MakeMove(int position, string player)
        {
            if (board[position - 1] == " ")
            {
                board[position - 1] = player;
                return true;
            }

            return false;
        }

        private bool CheckForWinner(string player)
        {
            // Check rows, columns, and diagonals for a winner
            for (int i = 0; i < 3; i++)
            {
                if ((board[i * 3] == player && board[i * 3 + 1] == player && board[i * 3 + 2] == player) ||
                    (board[i] == player && board[i + 3] == player && board[i + 6] == player))
                {
                    return true;
                }
            }

            if ((board[0] == player && board[4] == player && board[8] == player) ||
                (board[2] == player && board[4] == player && board[6] == player))
            {
                return true;
            }

            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            MenuOptions selectedOption;

            do
            {
                Console.WriteLine("Welcome to Tic Tac Toe!");
                Console.WriteLine("_______________________");
                Console.WriteLine($"1.{MenuOptions.NewGame}");
                Console.WriteLine($"2.{MenuOptions.AboutTheAuthor}");
                Console.WriteLine($"3.{MenuOptions.Exit}");

                if (Enum.TryParse(Console.ReadLine(), out selectedOption))
                {
                    switch (selectedOption)
                    {
                        case MenuOptions.NewGame:
                            TicTacToeGame game = new TicTacToeGame();
                            game.Play();
                            break;
                        case MenuOptions.AboutTheAuthor:
                            Console.WriteLine("Author: Robin Buldu");
                            break;
                        case MenuOptions.Exit:
                            if (ConfirmExit())
                            {
                                Console.WriteLine("Goodbye!");
                                return;
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }

            } while (true);
        }

        static bool ConfirmExit()
        {
            Console.Write("Are you sure you want to quit? (y/n): ");
            string input = Console.ReadLine().ToLower();
            return input == "y";
        }
    }
}
