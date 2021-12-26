
namespace Day4 // Note: actual namespace depends on the project name.
{
    public class Program
    {
        public struct BoardResolution
        {
            public Board Board;
            public int Turn;
        }

        public struct Game
        {
            public int[] Draw;

            public List<Board> boards = new();

            public Game(string[] bin)
            {
                Draw = bin[0].Split(',').Select(s => int.Parse(s)).ToArray();

                // Bingo boards are 5x5, parse all
                for (int x = 2; x < bin.Length; x += 6)
                {
                    Board b = new();
                    for (int r = 0; r < 5; r++)
                    {
                        var row = bin[x + r].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();

                        for (int i = 0; i < 5; i++)
                        {
                            b.Order[r * 5 + i] = row[i];
                        }
                    }

                    boards.Add(b);
                }
            }

            public bool HasWon(Board b, int turn)
            {
                return b.HasWon(Draw.AsSpan(0, turn + 1));
            }

            public List<BoardResolution> Resolve()
            {
                List<BoardResolution> solved = new();
                List<Board> relevant = new List<Board>(boards);

                for (int d = 0; d < Draw.Length; d++)
                {
                    for (int x = 0; x < relevant.Count; x++)
                    {
                        if (HasWon(relevant[x], d))
                        {
                            solved.Add(new BoardResolution()
                            {
                                Board = relevant[x],
                                Turn = d
                            });

                            relevant.RemoveAt(x);
                            x--;
                        }
                    }
                }

                return solved;
            }
        }

        public struct Board
        {
            public int[] Order = new int[25];

            public bool HasWon(Span<int> drawn)
            {
                for (int r = 0; r < 5; r++)
                {
                    bool set = true;
                    for (int c = 0; c < 5; c++)
                    {
                        if (!drawn.Contains(Order[c * 5 + r]))
                        {
                            set = false;
                            break;
                        }
                    }

                    if (set)
                    {
                        return true;
                    }
                }

                for (int c = 0; c < 5; c++)
                {
                    bool set = true;
                    for (int r = 0; r < 5; r++)
                    {
                        if (!drawn.Contains(Order[c * 5 + r]))
                        {
                            set = false;
                            break;
                        }
                    }

                    if (set)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public static int GetResolutionScore(Game g, BoardResolution board)
        {
            int mul = board.Board.Order.Where(o => !g.Draw.AsSpan(0, board.Turn + 1).Contains(o)).Aggregate(0, (a, b) => a + b);
            int draw = g.Draw[board.Turn];

            Console.WriteLine($"Undrawn values: {mul}");
            Console.WriteLine($"Last drawn: {draw}");

            return mul * draw;
        }

        public static int Part1(string[] bin)
        {
            Game g = new Game(bin);
            var res = g.Resolve();

            return GetResolutionScore(g, res.First());
        }

        public static int Part2(string[] bin)
        {
            Game g = new Game(bin);
            var res = g.Resolve();

            return GetResolutionScore(g, res.Last());
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            string[] bin = File.ReadAllLines("input4.txt");

            int score1 = Part1(bin);

            Console.WriteLine($"Final score: {score1}");

            int score2 = Part2(bin);

            Console.WriteLine($"Final score: {score2}");
        }
    }
}