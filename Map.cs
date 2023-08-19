public enum Direction
{
    N, W, S, E
};

public enum Tile
{
    Water = 0,
    Ship = 1,
    WaterHit = 2,
    ShipHit = 3
}

public class Map
{
    private Tile[] tiles;
    private int width;
    private int height;

    public Map(int width, int height)
    {
        this.width = width;
        this.height = height;
        tiles = new Tile[width * height];
    }

    public Tile GetTileAtPos(int x, int y)
    {
        return tiles[(y * width) + x];
    }

    public void SetTileAtPos(int x, int y, Tile newValue)
    {
        tiles[(y * width) + x] = newValue;
    }

    public bool IsTileFree(int x, int y)
    {
        Tile val = GetTileAtPos(x, y);
        return (val == Tile.Water || val == Tile.WaterHit);
    }

    void PlaceShip(int x, int y, Direction dir, int length)
    {
        switch (dir)
        {
            case Direction.N:
            {
                for (int diff_y = 0; diff_y < length; diff_y++)
                {
                    SetTileAtPos(x, y + diff_y, Tile.Ship);
                }
            }
            break;
            case Direction.S:
            {
                for (int diff_y = 0; diff_y < length; diff_y++)
                {
                    SetTileAtPos(x, y - diff_y, Tile.Ship);
                }
            }
            break;
            case Direction.E:
            {
                for (int diff_x = 0; diff_x < length; diff_x++)
                {
                    SetTileAtPos(x + diff_x, y, Tile.Ship);
                }
            }
            break;
            case Direction.W:
            {
                for (int diff_x = 0; diff_x < length; diff_x++)
                {
                    SetTileAtPos(x - diff_x, y, Tile.Ship);
                }
            }
            break;
        }
    }

    public void PlaceShipAtRandom(Random random, int length)
    {
        int rand_x = random.Next() % width;
        int rand_y = random.Next() % height;

        int dir = random.Next() % 4;
        for (int i = 0; i < 4; i++)
        {
            if (CheckLine(rand_x, rand_y, (Direction)dir, length))
            {
                PlaceShip(rand_x, rand_y, (Direction)dir, length);
                break;
            }
            else
            {
                dir = (dir + 1) % 4;
            }
        }
    }

    bool CheckLine(int x, int y, Direction dir, int length)
    {
        switch (dir)
        {
            case Direction.N:
            {
                if (y + length >= height)
                {
                    return false;
                }

                for (int diff_y = 0; diff_y < length; diff_y++)
                {
                    if (false == IsTileFree(x, y + diff_y))
                    {
                        return false;
                    }
                }

                return true;
            }
            case Direction.S:
            {
                if (y - length < 0)
                {
                    return false;
                }

                for (int diff_y = 0; diff_y < length; diff_y++)
                {
                    if (false == IsTileFree(x, y - diff_y))
                    {
                        return false;
                    }
                }

                return true;
            }
            case Direction.E:
            {
                if (x + length >= width)
                {
                    return false;
                }

                for (int diff_x = 0; diff_x < length; diff_x++)
                {
                    if (false == IsTileFree(x + diff_x, y))
                    {
                        return false;
                    }
                }

                return true;
            }
            case Direction.W:
            {
                if (x - length < 0)
                {
                    return false;
                }

                for (int diff_x = 0; diff_x < length; diff_x++)
                {
                    if (false == IsTileFree(x - diff_x, y))
                    {
                        return false;
                    }
                }

                return true;
            }
            default: return false;
        }
    }

    public static char GetTileChar(Tile tile)
    {
        switch (tile)
        {
            case Tile.Ship: return 'S';
            case Tile.ShipHit: return 'X';
            case Tile.Water: return '.';
            case Tile.WaterHit: return 'O';
            default: return '?';
        }
    }
}
