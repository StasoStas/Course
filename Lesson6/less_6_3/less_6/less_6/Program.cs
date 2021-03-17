using System;

namespace less_6
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] arr =
            {
                {"1","2","3","4" },
                {"5","6","a","8" },
                {"9","10","11","12" },
                {"13","14","15","16" }
            };
            try
            {
                int result = SumArray(arr);
                Console.WriteLine($"Сумма: {result}");
            }
            catch (MyArraySizeExceptions)
            {
                Console.WriteLine("Размерность массива должна быть 4х4");
            }
            catch (MyArrayDataException)
            {
                Console.WriteLine($"В ячейке [{MyArrayDataException.I},{MyArrayDataException.J}] не число!");
            }
            Console.ReadLine();
        }
        static int SumArray(string[,] array)
        {
            int sum = 0;
            if (array.GetLength(0) > 4 || array.GetLength(1) > 4)
            {
                throw new MyArraySizeExceptions();
            }
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    bool isInt = int.TryParse(array[i, j], out int number);
                    if (!isInt) throw new MyArrayDataException(i, j);
                    else sum += number;
                }
            }
            return sum;
        }
    }
}