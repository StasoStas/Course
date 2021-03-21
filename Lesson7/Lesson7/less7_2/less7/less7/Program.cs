using System;

namespace Lesson_7_3
{
    class Program
    {
        class Cross
        {

            static readonly int SIZE_X = 3;
            static readonly int SIZE_Y = 3;
            static readonly int toWin = 3; // условие победы
            static int STEP = 0; // счетчик ходов

            static readonly char[,] field = new char[SIZE_Y, SIZE_X];

            static readonly char PLAYER_DOT = 'X';
            static readonly char AI_DOT = 'O';
            static readonly char EMPTY_DOT = '.';

            static Random random = new Random();

            private static void InitField()
            {
                for (int i = 0; i < SIZE_Y; i++)
                {
                    for (int j = 0; j < SIZE_X; j++)
                    {
                        field[i, j] = EMPTY_DOT;
                    }
                }
            }

            private static void PrintField()
            {
                Console.Clear();
                Console.WriteLine("-------");
                for (int i = 0; i < SIZE_Y; i++)
                {
                    Console.Write("|");
                    for (int j = 0; j < SIZE_X; j++)
                    {
                        Console.Write(field[i, j] + "|");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("-------");
            }

            private static void SetSym(int y, int x, char sym)
            {
                field[y, x] = sym;
            }

            private static bool IsCellValid(int y, int x)
            {
                if (x < 0 || y < 0 || x > SIZE_X - 1 || y > SIZE_Y - 1)
                {
                    return false;
                }

                return field[y, x] == EMPTY_DOT;
            }

            private static bool IsFieldFull()
            {
                for (int i = 0; i < SIZE_Y; i++)
                {
                    for (int j = 0; j < SIZE_X; j++)
                    {
                        if (field[i, j] == EMPTY_DOT)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }

            private static bool CheckWin(char sym)
            {
                for (int i = 0; i < SIZE_X; i++)
                {
                    for (int j = 0; j < SIZE_Y; j++)
                    {   // проверяем
                        if (CheckLine(i, j, 1, 0, toWin, sym)) return true;   // линию по х
                        if (CheckLine(i, j, 1, 1, toWin, sym)) return true;   // по диагонали х у
                        if (CheckLine(i, j, 0, 1, toWin, sym)) return true;   // линию по у
                        if (CheckLine(i, j, 1, -1, toWin, sym)) return true;  // по диагонали х -у
                    }
                }
                return false;
            }

            private static bool CheckLine(int x, int y, int direction_x, int direction_y, int depthOfCheck, char sym)
            {
                int endX = x + (depthOfCheck - 1) * direction_x;
                int endY = y + (depthOfCheck - 1) * direction_y;
                if (!IsCellInField(endX, endY)) return false; // не выйдет-ли проверяемая линия за пределы поля
                for (int i = 0; i < depthOfCheck; i++)
                {   // одинаковые-ли символы в ячейках
                    if (field[y + i * direction_y, x + i * direction_x] != sym) return false;
                }
                return true;
            }

            private static bool IsCellInField(int x, int y)
            {
                if (x < 0 || y < 0 || x > SIZE_X - 1 || y > SIZE_Y - 1)
                {
                    return false;
                }
                return true;
            }

            private static void PlayerStep()
            {
                int x;
                int y;
                do
                {
                    Console.WriteLine($"Введите координаты X Y (1-{SIZE_X})");
                    x = Int32.Parse(Console.ReadLine()) - 1;
                    y = Int32.Parse(Console.ReadLine()) - 1;
                } while (!IsCellValid(y, x));
                SetSym(y, x, PLAYER_DOT);
            }

            private static void AiStep()
            {
                int x;
                int y;
                if (STEP == 1)
                {
                    x = 1;
                    y = 1;
                    if (!IsCellValid(x, y))
                    {
                        x = 0;
                        y = 0;
                        SetSym(y, x, AI_DOT);
                    }
                }
                else
                {
                    for (int i = 0; i < SIZE_X; i++)
                    {
                        for (int j = 0; j < SIZE_Y; j++)
                        {
                            if (IsCellValid(i, j))
                            {
                                SetSym(i, j, PLAYER_DOT);
                                if (CheckWin(PLAYER_DOT))
                                {
                                    SetSym(i, j, AI_DOT);
                                    break;
                                }
                                else
                                {
                                    SetSym(i, j, EMPTY_DOT);
                                }
                            }
                        }
                    }
                }
            }

            static void Main()
            {
                InitField();
                PrintField();

                while (true)
                {
                    STEP++;
                    PlayerStep();
                    PrintField();
                    if (CheckWin(PLAYER_DOT))
                    {
                        Console.WriteLine("Player Win!");
                        break;
                    }
                    if (IsFieldFull())
                    {
                        Console.WriteLine("DRAW");
                        break;
                    }
                    AiStep();
                    PrintField();
                    if (CheckWin(AI_DOT))
                    {
                        Console.WriteLine("SkyNet Win!");
                        break;
                    }
                    if (IsFieldFull())
                    {
                        Console.WriteLine("DRAW");
                        break;
                    }
                }

                Console.ReadLine();
            }
        }
    }
}