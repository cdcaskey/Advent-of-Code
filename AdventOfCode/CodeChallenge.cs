using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public abstract class CodeChallenge
    {
        protected static IInputLoader inputLoader;

        static CodeChallenge()
        {
            inputLoader = new InputLoader();
        }

        public CodeChallenge(IInputLoader loader)
        {
            inputLoader = loader;
        }
    }
}
