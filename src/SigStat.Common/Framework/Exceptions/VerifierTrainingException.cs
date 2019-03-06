using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Framework.Exceptions
{
    public class VerifierTrainingException : Exception
    {
        public VerifierTrainingException()
        {
        }

        public VerifierTrainingException(string message)
            : base(message)
        {
        }

        public VerifierTrainingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}
