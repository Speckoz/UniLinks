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
                .Split(".900").ToList();

            var names = new HashSet<string>();

            items.ForEach(x => names.Add(x));

            names.ToList().ForEach(x => Console.WriteLine(x));
        }

        Console.ReadKey();
        Console.Clear();
    }
}
