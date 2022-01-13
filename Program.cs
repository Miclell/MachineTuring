using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MachineTuring
{
    public class Ribbon
    {
        public int size = 50;
        public int startPoint = 25;
        
        public string[] value;

        private readonly string alphabet;
        private readonly List<Dictionary<string, string>> commands = new();

        public Ribbon(string alphabet, string iniValue, int size, int startPoint)
        {
            this.alphabet = alphabet;
            value = iniValue.Select(x => x.ToString()).ToArray();
            this.size = size;
            this.startPoint = startPoint;
        }

        private void AddCommand(string command) =>
            commands.Add(command.Remove(command.IndexOf(" "), command.IndexOf(" "))
                                                                                .TrimEnd(';').Split(';')
                                                                                    .ToDictionary(x => x[0].ToString(), x => x[1..]));

        public void AddCommands()
        {
            Console.WriteLine("Закончите ввод введя 0\nКоманды вводятся в формате {если этот символ}{на что заменить}{куда дальше(<>.)}{в какую команду} - 01>1;11>2");

            while (true)
            {
                string temp = Console.ReadLine();

                if (temp == "0")
                    break;
                else
                    AddCommand(temp);
            }
        }

        public void Action()
        {
            int i = 0;
            int nowCommand = 1;

            Console.WriteLine();

            while(true)
            {
                string temp = commands[nowCommand - 1][value[startPoint + i]];

                string go = Regex.Matches(temp, @"[<>.]\w").Select(x => x.Value).Aggregate((x, r) => x + r);
                string rplcmt = Regex.Matches(temp, @"\w[<>.]").Select(x => x.Value).Aggregate((x, r) => x + r)[0].ToString();

                value[startPoint + i] = rplcmt;

                Console.WriteLine(String.Join(null, Enumerable.Repeat(" ", startPoint + i)) + "v");

                Console.WriteLine(String.Join(null, value));

                switch (go[0])
                {
                    case '>':
                        i++;
                        break;
                    case '<':
                        i--;
                        break;
                }

                nowCommand = int.Parse(go[1..]);

                if (nowCommand == 0)
                    break;
            }

            Console.WriteLine("\nРезультат - " + String.Join(null, value));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите алфавит");
            string alphabet = Console.ReadLine();

            Console.WriteLine("Введите начальное значение");
            string iniValue = Console.ReadLine();

            Console.WriteLine("Введите размер ленты");
            int size = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите индекс положения головки, где 1 - 0");
            int startPoint = int.Parse(Console.ReadLine());

            Ribbon r = new(alphabet, iniValue, size, startPoint);

            r.AddCommands();

            r.Action();
        }
    }
}
