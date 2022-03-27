using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Filter
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {

        }
        public NotFoundException(string message) :base(message)
        {

        }
    }
}
