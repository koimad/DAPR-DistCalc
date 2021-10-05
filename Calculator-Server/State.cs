using System;

namespace Calculator
{
    public class State
    {
        public String Key {  get; set; }
        public CalculateState Value {  get; set; }


        public override string ToString()
        {
            return $"Key : {Key}, Operation : { Value.Operation}, Next : { Value.Next}, Total : { Value.Total}";
        }
    }
}