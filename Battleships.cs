using System.Text;

public partial class Battleships
{
    private Random random = new Random();
    private static int map_height = 10;
    private static int map_width = 10;
    private Map player_map;
    private Map enemy_map;

    public Battleships()
    {
        player_map = new Map(map_width, map_height);
        enemy_map = new Map(map_width, map_height);
    }

    public void StartNewGame()
    {
        for (int y = 0; y < map_height; y++)
        {
            for (int x = 0; x < map_width; x++)
            {
                player_map.SetTileAtPos(x, y, Tile.Water);
                enemy_map.SetTileAtPos(x, y, Tile.Water);
            }
        }

        enemy_map.PlaceShipAtRandom(random, 5);
        enemy_map.PlaceShipAtRandom(random, 4);
        enemy_map.PlaceShipAtRandom(random, 4);

        player_map.PlaceShipAtRandom(random, 5);
        player_map.PlaceShipAtRandom(random, 4);
        player_map.PlaceShipAtRandom(random, 4);
    }

    public bool Turn(Input input)
    {
        Shoot(enemy_map, input.X, input.Y);

        if (CheckIfAllShipsSank(enemy_map))
        {
            Console.Clear();
            Console.WriteLine("***** VICTORY *****\n");
            Console.WriteLine("Congratulations! You have defeated the enemy!");
            Console.WriteLine("Enter anything to start again.");
            return false;
        }

        ShootAtRandom(player_map);

        if (CheckIfAllShipsSank(player_map))
        {
            Console.Clear();
            Console.WriteLine("***** DEFEAT *****\n");
            Console.WriteLine("Better luck next time...");
            Console.WriteLine("Enter anything to start again.");
            return false;
        }

        return true;
    }

    public void RenderGameState()
    {
        Console.Clear();
        Console.WriteLine("===== ENEMY MAP =====\n");
        PrintMap(enemy_map, false);
        Console.WriteLine("===== PLAYER MAP =====\n");
        PrintMap(player_map, true);
    }
 
    private bool Shoot(Map map, int x, int y)
    {
        Tile t = map.GetTileAtPos(x, y);
        if (t == Tile.Water || t == Tile.WaterHit)
        {
            map.SetTileAtPos(x, y, Tile.WaterHit);
            return false;
        }
        else if (t == Tile.Ship || t == Tile.ShipHit)
        {
            map.SetTileAtPos(x, y, Tile.ShipHit);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ShootAtRandom(Map map)
    {
        while (true)
        {
            int x = random.Next() % map_width;
            int y = random.Next() % map_height;
            Tile t = map.GetTileAtPos(x, y);
            if (t == Tile.WaterHit || t == Tile.ShipHit)
            {
                continue;
            }
            else
            {
                Shoot(map, x, y);
                break;
            }
        }
    }

    private bool CheckIfAllShipsSank(Map map)
    {
        for (int y = 0; y < map_height; y++)
        {
            for (int x = 0; x < map_width; x++)
            {
                Tile t = map.GetTileAtPos(x, y);
                if (t == Tile.Ship)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void PrintMap(Map map, bool print_ships)
    {
        StringBuilder str = new StringBuilder();
        str.Append("   ABCDEFGHIJ\n");
        for (int y = 0; y < map_height; y++)
        {
            str.Append(y + 1);
            str.Append(y + 1 < 10 ? "  " : " ");

            for (int x = 0; x < map_width; x++)
            {
                Tile t = map.GetTileAtPos(x, y);
                if (t == Tile.Ship && false == print_ships)
                {
                    str.Append('.');
                }
                else
                {
                    str.Append(Map.GetTileChar(t));
                }
            }
            str.Append('\n');
        }
        str.Append('\n');
        Console.Write(str.ToString());
    }
}
