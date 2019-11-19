namespace AutomataHW2
{
    public class Edge
    {
        public State OldState { get; set; }
        public State NewState { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
    }
}