public struct Input
{
    public int X;
    public int Y;

    public static Input Invalid()
    {
        return new Input() { X = -1, Y = -1 };
    }

    public bool IsValid()
    {
        return (X != -1 && Y != -1);
    }

    public static Input ParseInput(string input)
    {
        input = input.Trim();

        if (input.Length == 0 || input.Length > 3)
        {
            Console.WriteLine("Please specify a valid coordinate. It should be of form \"B6\", \"G10\", etc.");
            return Input.Invalid();
        }

        char letter = Char.ToUpper(input[0]);
        int x = Input.LetterToCoord(letter);
        if (x == -1)
        {
            Console.WriteLine("The allowed letters are from A to J.");
            return Input.Invalid();
        }

        bool y_valid = Int32.TryParse(input.Substring(1), out int y);
        if (false == y_valid)
        {
            Console.WriteLine("Please give a coordinate with a valid number, for example: \"E5\". ");
            return Input.Invalid();
        }

        if (y <= 0 || y > 10)
        {
            Console.WriteLine("The allowed numbers are from 1 to 10.");
            return Input.Invalid();
        }

        y--; // Coordinates in internal maps start from 0

        return new Input() { X = x, Y = y };
    }

    private static int LetterToCoord(char letter)
    {
        switch (letter)
        {
            case 'A': return 0;
            case 'B': return 1;
            case 'C': return 2;
            case 'D': return 3;
            case 'E': return 4;
            case 'F': return 5;
            case 'G': return 6;
            case 'H': return 7;
            case 'I': return 8;
            case 'J': return 9;
            default: return -1;
        }
    }
}