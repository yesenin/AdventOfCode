using Common;

namespace AdventOfCode.Year2019;

public class Day02Part1 : IProblemWithInput
{
    public long GetAnswer()
    {
        long answer = 0;
        var lines = Input!.Split('\n', StringSplitOptions.TrimEntries);
        
        var space = lines.First().Split(',').Select(int.Parse).ToList();

        var operations = new List<Operation>();
        var pos = 0;
        while (pos < space.Count)
        {
            operations.Add(OperationFactory.GetOperation(space[pos], space.Skip(pos + 1).Take(3).ToArray()));
            pos += 4;
        }
        
        var i = 0;
        
        while (operations[i].Type() != "HALT")
        {
            pos = 0;
            operations = new List<Operation>();
            while (pos < space.Count)
            {
                operations.Add(OperationFactory.GetOperation(space[pos], space.Skip(pos + 1).Take(3).ToArray()));
                pos += 4;
            }
            space = operations[i].Invoke(space);
            i++;
        }

        answer = space[0];
        
        return answer;
    }

    public string Url => "!TBD";
    public string Title => "!TBD";
    public string? Input { get; set; }

    private abstract class Operation()
    {
        public abstract string Type();
        public abstract List<int> Invoke(List<int> space);
    }

    class AddOperation(int address1, int address2, int resultAddress) : Operation
    {
        public override string Type() => "ADD";
        
        public override List<int> Invoke(List<int> space)
        {
            space[resultAddress] = space[address1] + space[address2];
            return space;
        }
    }

    class MulOperation(int address1, int address2, int resultAddress) : Operation
    {
        public override string Type() => "MUL";
        
        public override List<int> Invoke(List<int> space)
        {
            space[resultAddress] = space[address1] * space[address2];
            return space;
        }
    }

    class HaltOperation : Operation
    {
        public override string Type() => "HALT";
        
        public override List<int> Invoke(List<int> space)
        {
            return space;
        }
    }

    class NoOperation : Operation
    {
        public override string Type() => "NOOP";
        
        public override List<int> Invoke(List<int> space)
        {
            return space;
        }
    }

    static class OperationFactory
    {
        public static Operation GetOperation(int opCode, int[] args)
        {
            switch (opCode)
            {
                case 1:
                    return new AddOperation(args[0], args[1], args[2]);
                case 2:
                    return new MulOperation(args[0], args[1], args[2]);
                case 3:
                    return new HaltOperation();
                default:
                    return new NoOperation();
            }
        }
    }
} 

