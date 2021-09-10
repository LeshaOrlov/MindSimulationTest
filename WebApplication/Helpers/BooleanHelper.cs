using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Helpers
{
    public static class BooleanHelper
    {
        public static int BoolToInt(bool arg)
        {
            if (arg) return 1;
            else return 0;
        }
    }
}
