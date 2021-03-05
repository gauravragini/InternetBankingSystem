using System;

namespace IBS.Exceptions
{
    public class DataEntryException : Exception
    {
        public DataEntryException() : base() { }
        public DataEntryException(string message) : base(message) { }
    }
    public class DataValidationException : Exception
    {
        public DataValidationException() : base() { }
        public DataValidationException(string message) : base(message) { }
    }
    public class NoAccountException : Exception
    {
        public NoAccountException() : base() { }
        public NoAccountException(string message) : base(message) { }
    }
    public class InterestException : Exception
    {
        public InterestException() : base() { }
        public InterestException(string message) : base(message) { }
    }
}
