using System;
using System.Collections.Generic;
using System.Text;

namespace GlovobolieApp
{
    public static class EnvConfig
    {
        public enum Env
        {
            TEST = 0,
            PROD = 1
        }
        public static Env CurrentEnvironment { get; } = Env.TEST;
    }
}
