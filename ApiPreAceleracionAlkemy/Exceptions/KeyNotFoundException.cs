﻿using System;

namespace ApiPreAceleracionAlkemy.Exceptions
{
    public class KeyNotFoundException : Exception
    {
        public KeyNotFoundException(string message) : base(message) { }
    }
}
