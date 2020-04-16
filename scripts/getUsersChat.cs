using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Logikoz.Desafios
{
    internal class Program
    {
        private static void Main(string[] args) => Execute();

        private static void Execute()
        {
            while (true)
            {
                Console.WriteLine("Informe o id: ");
                string id = Console.ReadLine();

                using (var client = new WebClient())
                {
                    string value;
                    try
                    {
                        value = client.DownloadString($"https://us.bbcollab.com/collab/api/csa/recordings/{id}/chat");
                    }
                    catch
                    {
                        continue;
                    }

                    var items = string.Join(".900", Regex.Matches(value, @"\<v(.+?)\>").Cast<Match>().Select(x => x.Groups[1].Value))
                        .Split(".900").OrderBy(x => x).ToList();

                    var names = new HashSet<string>();

                    if (items.Any(x => x.Contains('#')))
                        items.ForEach(x =>
                        {
                            if (x.Contains("#"))
                            {
                                int index = x.IndexOf($" #");
                                if (index > 0)
                                    names.Add(x.Substring(0, index));
                            }
                            else
                                names.Add(x);
                        });

                    names.ToList().ForEach(x => Console.WriteLine(x));
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
