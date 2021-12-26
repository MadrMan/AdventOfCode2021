// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

int[] numbers = File.ReadAllLines("input1.txt").Select(s => int.Parse(s)).ToArray();

// Part 1
int greater = 0;

for (int n = 1; n < numbers.Length; n++)
{
    if (numbers[n] > numbers[n - 1])
    {
        greater++;
    }
}

Console.WriteLine($"Greater: {greater}");

// Part 2
// Init window
int windowLength = 3;
int window = 0;

for (int n = 0; n < windowLength; n++)
{
    window += numbers[n];
}

// Slide and compare
greater = 0;

for (int n = windowLength; n < numbers.Length; n++)
{
    int next = window - numbers[n - windowLength] + numbers[n];

    if (next > window)
    {
        greater++;
    }

    next = window;
}

Console.WriteLine($"Greater 3-sliding: {greater}");