namespace calculator
{
    public struct Operation
    {
        public string OperationValue { get; set; }
        public OpertionPriority Priority { get; set; }
        public bool LeftAssociativity { get; set; }
        public Func<float, float, float> OperationFunction { get; set; }
    }
}
