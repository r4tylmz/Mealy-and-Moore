using System;
using System.Collections.Generic;

namespace AutomataHW2
{
    public class State
    {
        public String Name { get; set; }
        public Dictionary<string, State> StateDictionary = new Dictionary<string, State>();
        public Dictionary<string, Edge> EdgeDictionary = new Dictionary<string, Edge>();
        public string Output { get; set; }
        public State(string name) { this.Name = name; }
    }
}