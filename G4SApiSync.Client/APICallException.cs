using System;
using System.Collections.Generic;
using System.Text;

namespace G4SApiSync.Client
{
    public class APICallException : Exception
    {

        public APICallException(string Message) : base($"G4S API returned: {Message}")
        { }

        //public APICallException(string Path, Exception InnerException) : base($"Directory is not empty. '{Path}'.", InnerException)
        //{ }

    }
}
