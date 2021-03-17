using System;
using System.Collections.Generic;
using System.Text;

namespace less_6
{
    [Serializable]
    class MyArraySizeExceptions : Exception { }
    class MyArrayDataException : Exception
    {
        static public int I { get; set; }
        static public int J { get; set; }
        public MyArrayDataException(int i, int j)
        {
            I = i;
            J = j;
        }
    }
}
