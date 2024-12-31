using System.Text.RegularExpressions;

var input = File
    .ReadAllLines("input.txt");

var rom = new ROM
{
    RegisterA = long.Parse(Regex.Match(input[0], @"\D*(?<input>\d*)").Groups["input"].Value),
    RegisterB = long.Parse(Regex.Match(input[1], @"\D*(?<input>\d*)").Groups["input"].Value),
    RegisterC = long.Parse(Regex.Match(input[2], @"\D*(?<input>\d*)").Groups["input"].Value),
    Code = input[4]["Program:".Length..].Split(',').Select(long.Parse).ToArray(),
};

IEnumerable<long> RunProgram(ROM rom)
{
    var instructionSize = 2;
    var instructionPointer = 0;
    long registerA = rom.RegisterA, registerB = rom.RegisterB, registerC = rom.RegisterC;
    List<long> result = [];

    long GetComboOperandValue(long operand)
    {
        return operand switch
        {
            4 => registerA,
            5 => registerB,
            6 => registerC,
            7 => throw new InvalidOperationException("7 is not a valid operand."),
            _ => operand,
        };
    }

    while (instructionPointer < rom.Code.Length)
    {
        switch (rom.Code[instructionPointer], rom.Code[instructionPointer + 1])
        {
            case (0, var adv):

                registerA = (long)(registerA / Math.Pow(2, GetComboOperandValue(adv)));
                break;

            case (1, var bxl):
                registerB ^= bxl;
                break;

            case (2, var bst):
                registerB = GetComboOperandValue(bst) % 8;
                break;

            case (3, var jnz):
                if (registerA != 0)
                {
                    instructionPointer = (int)jnz;
                    continue;
                }

                break;

            case (4, _):
                registerB ^= registerC;
                break;

            case (5, var outp):
                result.Add(GetComboOperandValue(outp) % 8);
                break;

            case (6, var bdv):
                registerB = (long)(registerA / Math.Pow(2, GetComboOperandValue(bdv)));
                break;

            case (7, var cdv):
                registerC = (long)(registerA / Math.Pow(2, GetComboOperandValue(cdv)));
                break;
        }

        instructionPointer += instructionSize;
    }

    return result;
}

long? FindRegisterA(ROM rom, long registerA, int depth)
{
    registerA <<= 3;

    for (long i = 0; i < 8; i++)
    {
        var testValue = registerA + i;

        var output = RunProgram(rom with { RegisterA = testValue }).ToArray();

        if (output[0] == rom.Code[^depth])
        {
            if (depth == rom.Code.Length)
            {
                return testValue;
            }
            else
            {
                var result = FindRegisterA(rom, testValue, depth + 1);

                if (result is not null)
                {
                    return result;
                }
            }
        }
    }

    return null;
}

void PartOne()
{
    var answer = string.Join(',', RunProgram(rom));

    Console.WriteLine($"Answer part one: {answer}");
}

void PartTwo()
{
    long? answer = 0;

    answer = FindRegisterA(rom, 0, 1);

    Console.WriteLine($"Answer part two: {string.Join(',', answer)}");
}

PartOne();
PartTwo();

Console.ReadLine();

internal record ROM
{
    public required long RegisterA { get; init; }

    public required long RegisterB { get; init; }

    public required long RegisterC { get; init; }

    public required long[] Code { get; init; }
}
