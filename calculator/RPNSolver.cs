namespace calculator
{
    public class RPNSolver
    {
        private Dictionary<string, Operation> operations = new Dictionary<string, Operation>()
        {
            {
                "+",
                new Operation
                {
                    OperationValue = "+",
                    LeftAssociativity = true,
                    Priority = OpertionPriority.LOW,
                    OperationFunction = (a, b) => a + b,
                }
            },
            {
                "-",
                new Operation
                {
                    OperationValue = "-",
                    LeftAssociativity = true,
                    Priority = OpertionPriority.LOW,
                    OperationFunction = (a, b) => a - b,
                }
            },
            {
                "*",
                new Operation
                {
                    OperationValue = "*",
                    LeftAssociativity = true,
                    Priority = OpertionPriority.MEDIUM,
                    OperationFunction = (a, b) => a * b,
                }
            },
            {
                "/",
                new Operation
                {
                    OperationValue = "/",
                    LeftAssociativity = true,
                    Priority = OpertionPriority.MEDIUM,
                    OperationFunction = (a, b) => a / b,
                }
            },
            {
                "(",
                new Operation
                {
                    OperationValue = "(",
                    LeftAssociativity = true,
                    Priority = OpertionPriority.HIGH,
                }
            },
            {
                ")",
                new Operation
                {
                    OperationValue = ")",
                    LeftAssociativity = true,
                    Priority = OpertionPriority.HIGH,
                }
            },
        };

        private Stack<Operation> _operationsStack = new Stack<Operation>();
        private string _equation;

        public RPNSolver(string equation)
        {
            _equation = equation.Replace("(", "( ").Replace(")", " )");
        }

        private List<string> _ParseToPolishNotation()
        {
            List<string> output = new List<string>();
            string[] tokens = _equation.Split(" ");

            foreach (string token in tokens)
            {
                if (float.TryParse(token, out float _))
                {
                    output.Add(token);
                }
                else if (operations.ContainsKey(token))
                {
                    Operation operation = operations[token];

                    if (operation.OperationValue == "(")
                    {
                        _operationsStack.Push(operation);
                    }
                    else if (operation.OperationValue == ")")
                    {
                        while (
                            _operationsStack.TryPeek(out Operation topOperation)
                            && topOperation.OperationValue != "("
                        )
                        {
                            Operation poppedOperation = _operationsStack.Pop();
                            output.Add(poppedOperation.OperationValue);
                        }
                        _operationsStack.Pop();
                    }
                    else
                    {
                        while (
                            _operationsStack.TryPeek(out Operation topOperation)
                            && topOperation.OperationValue != "("
                            && operation.Priority <= topOperation.Priority
                        )
                        {
                            Operation poppedOperation = _operationsStack.Pop();
                            output.Add(poppedOperation.OperationValue);
                        }
                        _operationsStack.Push(operation);
                    }
                }
            }
            while (_operationsStack.TryPop(out Operation op))
            {
                output.Add(op.OperationValue);
            }
            return output;
        }

        public float Solve()
        {
            List<string> RPN = _ParseToPolishNotation();
            Stack<float> tokenStack = new Stack<float>();

            foreach (string token in RPN)
            {
                if (float.TryParse(token, out float parsedValue))
                {
                    tokenStack.Push(parsedValue);
                }
                else if (operations.ContainsKey(token))
                {
                    Operation operation = operations[token];

                    float rightPart = tokenStack.Pop();
                    float leftPart = tokenStack.Pop();

                    tokenStack.Push(operation.OperationFunction(leftPart, rightPart));
                }
            }
            return tokenStack.Pop();
        }
    }
}
