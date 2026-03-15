using System;
using System.Text;
namespace DamerauLevenshteinHomework
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            while (true)
            {
                try
                {
                    Console.Write("Введите первую строку: ");
                    string? firstInput = Console.ReadLine();

                    if (string.Equals(firstInput, "exit", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("\nВыход из программы. До свидания!");
                        break;
                    }

                    if (firstInput == null)
                    {
                        Console.WriteLine("Ошибка: ввод не может быть пустым. Попробуйте снова.\n");
                        continue;
                    }

                    Console.Write("Введите вторую строку: ");
                    string? secondInput = Console.ReadLine();

                    if (secondInput == null)
                    {
                        Console.WriteLine("Ошибка: ввод не может быть пустым. Попробуйте снова.\n");
                        continue;
                    }

                    int distance = DamerauLevenshteinDistance(firstInput, secondInput);

                    Console.WriteLine($"\nРезультат: \"{firstInput}\" -> \"{secondInput}\" = {distance}\n");
                    Console.WriteLine("--- Следующая пара строк ---\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка: {ex.Message}\n");
                }
            }
        }
        public static int DamerauLevenshteinDistance(string str1, string str2)
        {
            string s1 = str1.ToUpperInvariant();
            string s2 = str2.ToUpperInvariant();

            int n = s1.Length;
            int m = s2.Length;

            if (n == 0) return m;
            if (m == 0) return n;

            int[,] matrix = new int[n + 1, m + 1];

            for (int i = 0; i <= n; i++)
                matrix[i, 0] = i;
            for (int j = 0; j <= m; j++)
                matrix[0, j] = j;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (s1[i - 1] == s2[j - 1]) ? 0 : 1;

                    int delete = matrix[i - 1, j] + 1;      
                    int insert = matrix[i, j - 1] + 1;     
                    int replace = matrix[i - 1, j - 1] + cost; 

                    matrix[i, j] = Math.Min(Math.Min(delete, insert), replace);

                    if (i > 1 && j > 1 &&
                        s1[i - 1] == s2[j - 2] &&
                        s1[i - 2] == s2[j - 1])
                    {
                        int transposition = matrix[i - 2, j - 2] + 1;
                        matrix[i, j] = Math.Min(matrix[i, j], transposition);
                    }
                }
            }

            return matrix[n, m];
        }
        private static void PrintDistance(string s1, string s2)
        {
            int dist = DamerauLevenshteinDistance(s1, s2);
            Console.WriteLine($"'{s1}', '{s2}' -> {dist}");
        }

    }
}