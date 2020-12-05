using System;
using System.Collections.Generic;
using System.Text;

namespace _2019
{
    public enum Modes
    {
        Parameter = 0,
        Immediate = 1,
        Relative = 2
    }

    public enum Instruction
    {
        Add = 1,
        Mult = 2,
        Input = 3,
        Output = 4,
        JumpIfTrue = 5,
        JumpIfFalse = 6,
        LessThan = 7,
        Equals = 8,
        AdjRel = 9,
        Halt = 99,
        Error
    }
}
