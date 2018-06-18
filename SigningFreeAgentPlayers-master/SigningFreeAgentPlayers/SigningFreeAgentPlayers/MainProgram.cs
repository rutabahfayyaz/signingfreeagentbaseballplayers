using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigningFreeAgentPlayers
{
    class MainProgram
    {
        static CreatePlayer[,] p_;
        static void Main(string[] args)
        {

            CreatePlayer[,] p = CreatePlayers(); 
            FreeAgentPlayers(p, 2, 2, 5);  
            Console.ReadLine();
        }


        static public CreatePlayer[,] CreatePlayers()
        {

            // creating  2 players on each position.
            // player1 player2 on the 1st Position
            

            CreatePlayer player1, player2, player11, player12;
            player1 = new CreatePlayer("Tooba", 2, 1);  // players name, VORP , Cost
            player2 = new CreatePlayer("Wasia", 3, 1);
            player11 = new CreatePlayer("Naymat", 3, 1);
            player12 = new CreatePlayer("Rutabah", 4, 1);
            CreatePlayer[,] p = { { null, null, null }, { null, player1, player2 }, { null, player11, player12 } }; // 
            return p;
        }


        static public void TotalCost(CreatePlayer[,] p, int N, int P)
        {

            p_ = new CreatePlayer[N + 1, P + 1]; //  new Helper table created

            for (int i = 1; i < p.GetLength(1); i++)
            {
                int sum = 0;
                int max = Int32.MinValue;
                for (int j = 1; j < p.GetLength(0); j++)
                {
                    p_[i, j] = new CreatePlayer();
                    p_[i, j].name = p[i, j].name;
                    sum = sum + p[i, j].cost;
                    if (p[i, j].VORP > max)
                    {
                        max = p[i, j].VORP;
                        p_[i, j].VORP = max;//  at "0 to i" position-max VORP

                    }
                    p_[i, j].cost = sum; //  "0 to i" position-total cost
                }

            }
        }

        static public void FreeAgentPlayers(CreatePlayer[,] p, int N, int P, int X)
        {

            //p = set of players, N = number of players , P =  available players at each positions , X = Total money available.
            int[,] v = new int[N + 1, X + 1]; // 2D array table created 
            int[,] c = new int[N + 1, X + 1]; // 2D array table created
            int[,] who = new int[N + 1, X + 1];  //2D array table created 
            TotalCost(p, N, P);
            int Xo = X;    //copy of original budget created
            X = X / N;    // budget divided for all positions equally
            for (int x = 0; x <= X; x++)
            {
                who[N, x] = 0;
                v[N, x] = Int32.MinValue;
                for (int k = 1; k <= P; k++)
                {
                    if (p_[N, k].cost <= X && p_[N, k].VORP >= v[N, x])
                    {
                        v[N, x] = p[N, k].VORP;
                        who[N, x] = k;
                    }
                }
            }

            for (int i = N - 1; i >= 1; i--)
            {
                for (int x = 0; x <= X; x++)
                {
                    v[i, x] = v[i + 1, x];
                    who[i, x] = 0;
                    for (int k = 1; k <= P; k++)
                    {
                        if (p_[i, k].cost <= x && v[i + 1, x - p_[i, k].cost] + p_[i, k].VORP > v[i, x])
                        {
                            v[i, x] = v[i + 1, x - p_[i, k].cost] + p_[i, k].VORP;
                            who[i, x] = k;

                        }

                    }
                }
            }

            Console.WriteLine("Available Positions: " + N + "\nPlayers available on each position: " + P);
            Console.WriteLine("Total amount that can be used " + Xo);

            int amt = X;
            for (int i = 1; i <= N; i++)
            {
                int k = who[i, amt];
                if (k != 0)
                {
                    Console.WriteLine("Signed player for position [" + i + "] " + p_[i, k]);
                    amt = amt - p_[i, k].cost;
                }

            }

            Console.WriteLine("Amount spent " + (X - amt));
            Console.WriteLine("Aggregate of VORP of the squad " + v[1, X]);


        }

        static public void printArray(int[,] arr)
        {

            var rowCount = arr.GetLength(0);
            var colCount = arr.GetLength(1);
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                    Console.Write(String.Format("{0}\t", arr[row, col]));
                Console.WriteLine();
            }
        }
        static public void printArray(CreatePlayer[,] arr)
        {

            var rowCount = arr.GetLength(0);
            var colCount = arr.GetLength(1);
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                    Console.Write(String.Format("{0}\t", (arr[row, col] == null ? "x" : arr[row, col].ToString())));
                Console.WriteLine();
            }
        }
    }




}