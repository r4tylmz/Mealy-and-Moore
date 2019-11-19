using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//171213038 Ahmed Yilmaz
namespace AutomataHW2
{
    public class Mealy
    {
        public State GetState(string Name, List<State> s)
        {
            State st = null;
            foreach (var item in s)
                if (item.Name == Name)
                    st = item;
            return st;
        }

        public List<State> SetList(List<string> sList, List<State> list)
        {
            foreach (var item in sList) list.Add(new State(item));
            return list;
        }

        public void RunMealy()
        {
            var inputStream = new StreamReader("C:\\INPUT.txt");
            var diagramStream = new StreamReader("C:\\GECISDIYAGRAMI.txt");
            var sList = inputStream.ReadLine().Split(',').ToList();
            var aList = inputStream.ReadLine().Split(',').ToList();
            var stateList = new List<State>();
            stateList = SetList(sList, stateList);


            var inputFinder = 0;
            string newState = null;
            string line;
            string[] diagramMatrix = null;
            var croppedMatrix = new string[aList.Count * 2];
            
            Console.WriteLine("--------- GECIS DIYAGRAMI ---------");
            diagramStream.ReadLine();
            for (var i = 0; i < sList.Count; i++)
            {
                if ((line = diagramStream.ReadLine()) != null) 
                    diagramMatrix = line.Split('\t');
                Array.Copy(diagramMatrix, 2, croppedMatrix, 0, aList.Count * 2 - 1);
                Edge ed = null;
                for (var j = 0; j < aList.Count * 2; j++)
                    if (j % 2 == 0) //output ise
                    {
                        ed = new Edge();
                        ed.OldState = GetState(sList[i], stateList);
                        ed.Input = aList[inputFinder];
                        ed.Output = croppedMatrix[j];
                        stateList[i].EdgeDictionary.Add(ed.Input, ed);
                        Console.Write($"OldState:{ed.OldState.Name} ");
                        inputFinder++;
                    }
                    else //newstate ise
                    {
                        newState = diagramMatrix[j];
                        stateList[i].EdgeDictionary[ed.Input].NewState = GetState(newState, stateList);
                        Console.Write(
                            $"/ NewState:{ed.NewState.Name} / Input:{ed.Input} / Output:{stateList[i].EdgeDictionary[ed.Input].Output}\n");
                    }
                inputFinder = 0;
            }

            Console.WriteLine("-------------------------------");
            Console.Write("ALFABENIZ:");
            aList.ForEach(m=> Console.Write($" {m} "));
            Console.WriteLine("\n-------------------------------");
            Console.Write("Kelime:");
            var search = Console.ReadLine();
            Console.WriteLine("-------------------------------");
            var tempState = stateList[0];
            string outputText = null, transitions = null;
            bool control=false;
            foreach (var item in search)
            {
                if (aList.Contains(item.ToString()))
                {
                    var output = tempState.EdgeDictionary[item.ToString()].Output;
                    var input = item;
                    outputText += output;
                    transitions += $" {tempState.EdgeDictionary[item.ToString()].OldState.Name} => {tempState.EdgeDictionary[item.ToString()].NewState.Name} ({input}/{output}) |";
                    tempState = tempState.EdgeDictionary.First(m => m.Key == item.ToString()).Value.NewState;
                    control = true;
                }
                else
                {
                    Console.WriteLine("ALFABEDE OLMAYAN BIR HARF GIRISI YAPTINIZ PROGRAM CALISMAYACAKTIR.");
                    control = false;
                    break;
                }
            }

            if (control)
            {
                Console.WriteLine($"CIKTI: {outputText}");
                Console.WriteLine($"GECISLER:{transitions}");
                Console.WriteLine("-------------------------------");
            }
            inputStream.Close();
            diagramStream.Close();
        }
    }
}