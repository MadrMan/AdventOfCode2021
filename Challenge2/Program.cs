// See https://aka.ms/new-console-template for more information


enum Direction
{
    Forward,
    Up,
    Down
};

struct Instruction
{
    public Direction direction;
    public int distance;
};

namespace MyApp // Note: actual namespace depends on the project name.
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Instruction[] instructions = File.ReadAllLines("input2.txt").Select(s =>
            {
                var parts = s.Split(' ');
                return new Instruction()
                {
                    direction = (Direction)Enum.Parse(typeof(Direction), parts[0], true),
                    distance = int.Parse(parts[1])
                };
            }).ToArray();

            {
                int position = 0;
                int depth = 0;

                foreach (var inst in instructions)
                {
                    if (inst.direction == Direction.Forward)
                        position += inst.distance;
                    else if (inst.direction == Direction.Up)
                        depth -= inst.distance;
                    else if (inst.direction == Direction.Down)
                        depth += inst.distance;
                }

                Console.WriteLine($"Final position: {position}");
                Console.WriteLine($"Final depth: {depth}");
                Console.WriteLine($"Final mul: {position * depth}");
            }

            {
                int position = 0;
                int depth = 0;
                int aim = 0;

                foreach (var inst in instructions)
                {
                    if (inst.direction == Direction.Forward)
                    {
                        position += inst.distance;
                        depth += aim * inst.distance;
                    }
                    else if (inst.direction == Direction.Up)
                    {
                        aim -= inst.distance;
                    }
                    else if (inst.direction == Direction.Down)
                    {
                        aim += inst.distance;
                    }
                }

                Console.WriteLine($"Final position: {position}");
                Console.WriteLine($"Final depth: {depth}");
                Console.WriteLine($"Final mul: {position * depth}");
            }
        }
    }
}