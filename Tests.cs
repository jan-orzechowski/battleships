#if DEBUG

using System.Diagnostics;

public partial class Battleships
{
    public static void RunTests()
    {
        InputTests();

        Battleships game = new Battleships();
        game.StartNewGame();

        Debug.Assert(game.FullGameTest());       
    }

    private static void InputTests()
    {
        Debug.Assert(Input.ParseInput("A1").IsValid());
        Debug.Assert(Input.ParseInput("G10").IsValid());
        Debug.Assert(Input.ParseInput("A0").IsValid() == false);
        Debug.Assert(Input.ParseInput("Z5").IsValid() == false);
        Debug.Assert(Input.ParseInput("D3a").IsValid() == false);
        Debug.Assert(Input.ParseInput("D 3").IsValid());
        Debug.Assert(Input.ParseInput("   D3   ").IsValid());
        Console.Clear();
    }

    private bool FullGameTest()
    {
        int turns_limit = map_height * map_width;       
        for (int turn = 0; turn < turns_limit; turn++)
        {
            ShootAtRandom(enemy_map);
            if (CheckIfAllShipsSank(enemy_map))
            {
                return true;
            }

            ShootAtRandom(player_map);
            if (CheckIfAllShipsSank(player_map))
            {
                return true;
            }
        }
        return false;
    }
}

#endif
