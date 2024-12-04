var input = await File.ReadAllLinesAsync("input.txt");
var matrix = input.Select(line => line.ToArray()).ToArray();

void PartOne()
{
    var answer = 0;

    var width = matrix[0].Length;
    var height = matrix.Length;

    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            if (matrix[y][x] != 'X')
            {
                continue;
            }

            if (x <= width - 4)
            {
                if (matrix[y][x + 1] == 'M' &&
                    matrix[y][x + 2] == 'A' &&
                    matrix[y][x + 3] == 'S')
                {
                    answer++;
                }

                if (y <= height - 4)
                {
                    if (matrix[y + 1][x + 1] == 'M' &&
                        matrix[y + 2][x + 2] == 'A' &&
                        matrix[y + 3][x + 3] == 'S')
                    {
                        answer++;
                    }
                }

                if (y >= 3)
                {
                    if (matrix[y - 1][x + 1] == 'M' &&
                        matrix[y - 2][x + 2] == 'A' &&
                        matrix[y - 3][x + 3] == 'S')
                    {
                        answer++;
                    }
                }
            }

            if (x >= 3)
            {
                if (matrix[y][x - 1] == 'M' &&
                    matrix[y][x - 2] == 'A' &&
                    matrix[y][x - 3] == 'S')
                {
                    answer++;
                }

                if (y <= height - 4)
                {
                    if (matrix[y + 1][x - 1] == 'M' &&
                        matrix[y + 2][x - 2] == 'A' &&
                        matrix[y + 3][x - 3] == 'S')
                    {
                        answer++;
                    }
                }

                if (y >= 3)
                {
                    if (matrix[y - 1][x - 1] == 'M' &&
                        matrix[y - 2][x - 2] == 'A' &&
                        matrix[y - 3][x - 3] == 'S')
                    {
                        answer++;
                    }
                }
            }

            if (y <= height - 4)
            {
                if (matrix[y + 1][x] == 'M' &&
                    matrix[y + 2][x] == 'A' &&
                    matrix[y + 3][x] == 'S')
                {
                    answer++;
                }
            }

            if (y >= 3)
            {
                if (matrix[y - 1][x] == 'M' &&
                    matrix[y - 2][x] == 'A' &&
                    matrix[y - 3][x] == 'S')
                {
                    answer++;
                }
            }
        }
    }

    Console.WriteLine($"Answer part one: {answer}");
}

void PartTwo()
{
    var answer = 0;

    var width = matrix[0].Length;
    var height = matrix.Length;

    for (var y = 1; y < height - 1; y++)
    {
        for (var x = 1; x < width - 1; x++)
        {
            if (matrix[y][x] is not 'A')
            {
                continue;
            }

            if (((matrix[y - 1][x - 1] is 'M' && matrix[y + 1][x + 1] is 'S') ||
                (matrix[y - 1][x - 1] is 'S' && matrix[y + 1][x + 1] is 'M'))
                &&
                ((matrix[y + 1][x - 1] is 'M' && matrix[y - 1][x + 1] is 'S') ||
                (matrix[y + 1][x - 1] is 'S' && matrix[y - 1][x + 1] is 'M')))
            {
                answer++;
                continue;
            }
        }
    }

    Console.WriteLine($"Answer part two: {answer}");
}

PartOne();
PartTwo();

Console.ReadLine();
