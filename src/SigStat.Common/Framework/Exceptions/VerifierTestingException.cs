using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Framework.Exceptions
{
    public class VerifierTestingException : Exception
    {
        public VerifierTestingException()
        {
        }

        public VerifierTestingException(string message)
            : base(message)
        {
        }

        public VerifierTestingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
