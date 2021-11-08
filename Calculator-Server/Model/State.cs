using System;

namespace Calculator
{
    public class State
    {
        #region Properties

        public String Key { get; set; }
        public CalculateState Value { get; set; }

        #endregion

        #region Methods

        #region Public

        public override String ToString()
        {
            return $"Key : {Key}, Operation : {Value.Operation}, Next : {Value.Next}, Total : {Value.Total}";
        }

        #endregion

        #endregion
    }
}