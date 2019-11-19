using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//171213038 Ahmed Yilmaz
namespace AutomataHW2
{
    public class Moore
    {
        public void RunMoore()
        {
            var inputStream = new StreamReader("C:\\INPUT.txt");
            var transitionStream = new StreamReader("C:\\GECISTABLOSU.txt");
            var outputStream = new StreamReader("C:\\OUTPUT.txt");
            var sb = new StringBuilder();
            var sList = inputStream.ReadLine()?.Split(',').ToList();
            var aList = inputStream.ReadLine()?.Split(',').ToList();
            var stateList = new List<State>();
            foreach (var item in sList) stateList.Add(new State(item));


            State nextState, currentState;
            bool control = false;
            Console.WriteLine("-------------------------------");
            Console.Write("ALFABENIZ:");
            aList.ForEach(m => Console.Write($" {m} "));
            Console.WriteLine("\n-------------------------------");
            Console.Write("Kelime:");
            var search = Console.ReadLine();
            Console.WriteLine("-------------------------------");
            var indexOfState = 0;
            control = CheckInputInAlphabet(search, aList, control);
            
            if (control)
            {
                string line;
                outputStream.ReadLine();
                while ((line = outputStream.ReadLine()) != null)
                {
                    var split = line.Split('\t');
                    var index = sList.IndexOf(split[0]);
                    stateList[index].Output = split[1];
                }
                Console.WriteLine("--------- GECIS TABLOSU ---------");
                transitionStream.ReadLine();
                while ((line = transitionStream.ReadLine()) != null)
                {
                    var split = line.Split('\t');
                    var index = sList.IndexOf(split[0]); // 0. indis oldState 1. indisten itibaren [harf,new state] seklinde oluyor
                    var k = 0;
                    Console.Write($"Old State: {split[0]} => ");
                    var j = 0;
                    for (var i = 1; i <= split.Length - 1; i++) // gelen alfabenin uzunlugu zaten bu kadar
                    {
                        Console.Write($"New State: {split[i]} (INPUT:{aList[k++]}) => ");
                        stateList[index].StateDictionary.Add(aList[j++], new State(split[i]));
                    }

                    Console.WriteLine();
                }


                Console.WriteLine("--------- CIKTI TABLOSU ---------");
                foreach (var ch in search)
                {
                    nextState = stateList[indexOfState].StateDictionary.First(m => m.Key == ch.ToString()).Value;
                    currentState = stateList[indexOfState];
                    Console.Write($"Old State: {currentState.Name} --> ");
                    Console.Write($"New State: {nextState.Name} --> OUTPUT: {stateList[indexOfState].Output}\n");
                    sb.Append(stateList[indexOfState].Output + " - ");
                    indexOfState = sList.IndexOf(nextState.Name);
                }

                nextState = stateList[indexOfState].StateDictionary
                    .First(m => m.Key == search[search.Length - 1].ToString()).Value;
                Console.WriteLine(
                    $"Old State: {stateList[indexOfState].Name} --> New State: {nextState.Name} --> OUTPUT: {stateList[indexOfState].Output}");
                sb.Append(stateList[indexOfState].Output);
                Console.WriteLine("-------------------------------");
                Console.WriteLine($"KELIME = {search}");
                Console.WriteLine($"CIKTI: {sb}");
                Console.WriteLine("-------------------------------");
            }

            inputStream.Close();
            transitionStream.Close();
            outputStream.Close();
        }

        private bool CheckInputInAlphabet(string search, List<string> aList, bool control)
        {
            foreach (var item in search)
            {
                if (aList.Contains(item.ToString()))
                    control = true;
                else
                {
                    Console.WriteLine("ALFABEDE OLMAYAN BIR HARF GIRISI YAPTINIZ PROGRAM CALISMAYACAKTIR.");
                    control = false;
                    break;
                }
            }

            return control;
        }
    }
}