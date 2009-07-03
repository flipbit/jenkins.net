using System;

namespace Hudson.Core
{
    public class HudsonException : ApplicationException
    {
        public HudsonException(string message) : base(message)
        {            
        }
    }
}