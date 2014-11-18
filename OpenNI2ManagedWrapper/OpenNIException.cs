using System;

namespace OpenNI2
{

    public class OpenNIException : Exception
    {
        internal OpenNIException(string message) : base(message)
        {
        }

        internal OpenNIException() : base()
        {
        }
    }
}
