public static class Program
{
    static void Main(string[] args)
    {
#if DEBUG
        Battleships.RunTests();
#endif

        Console.WriteLine("==============================");
        Console.WriteLine("=--------BATTLESHIPS---------=");
        Console.WriteLine("==============================");
        Console.WriteLine();
        Console.WriteLine("Welcome to the Battleships game!\n");
        Console.WriteLine("To shoot missiles, please specify coordinates like this: \"B6\", \"G10\", etc.");
        Console.WriteLine("Both the player and the enemy have one ship with length of 5 and two with length of 5.");
        Console.WriteLine("The game ends when there are no ships left.\n");
        Console.WriteLine("If you want to exit, type \"exit\", if you want to start again, type \"new\".\n");
        Console.WriteLine("Good luck!");

        Console.ReadKey();

        Battleships game = new Battleships();
        game.StartNewGame();
        game.RenderGameState();

        while (true)
        {
            string? target = Console.ReadLine();
            if (target != null)
            {
                if (target.ToLower() == "exit")
                {
                    break;
                }

                if (target.ToLower() == "new")
                {
                    game.StartNewGame();
                    game.RenderGameState();
                    continue;
                }

                Input input = Input.ParseInput(target);
                if (input.IsValid())
                {
                    if (false == game.Turn(input))
                    {
                        Console.ReadKey();
                        game.StartNewGame();                        
                    }

                    game.RenderGameState();
                }
            }           
        }
    }
}
