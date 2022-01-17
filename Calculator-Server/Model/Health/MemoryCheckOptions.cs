using System;

namespace Calculator
{
    public class MemoryCheckOptions
    {
        #region Properties

        // Failure threshold (in bytes)
        public Int64 Threshold { get; set; } = 1024L * 1024L * 1024L;

        #endregion
    }
}