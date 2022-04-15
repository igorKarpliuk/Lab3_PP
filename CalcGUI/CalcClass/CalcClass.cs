using System;
using System.Runtime.InteropServices;


namespace Cc
{
    public class CalcClass
    {

        [DllImport("..\\..\\..\\x64\\Release\\c_evaluator.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern Int64 SumInt(Int64 left, Int64 right);
        
        [DllImport("..\\..\\..\\x64\\Release\\c_evaluator.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern Int64 SubtractInt(Int64 left, Int64 right);
        [DllImport("..\\..\\..\\x64\\Release\\c_evaluator.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern Int64 MultiplyInt(Int64 left, Int64 right);
        [DllImport("..\\..\\..\\x64\\Release\\c_evaluator.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern Int64 DivideInt(Int64 left, Int64 right);
        [DllImport("..\\..\\..\\x64\\Release\\c_evaluator.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern Int64 FactorialInt(Int64 left);
        [DllImport("..\\..\\..\\x64\\Release\\c_evaluator.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern Int64 ModInt(Int64 left, Int64 right);
        [DllImport("..\\..\\..\\x64\\Release\\c_evaluator.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern Int64 AbsInt(Int64 x);        
        [DllImport("..\\..\\..\\x64\\Release\\c_evaluator.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern Int64 IAbsInt(Int64 x);
        

        /// <summary>
        /// Функція складання числа а і b
        /// </summary>
        /// <param name="a">доданок</param>
        /// <param name="b">доданок</param>
        /// <returns>сума</returns>
        public static int Add(long a, long b)
        {
            return (int)SumInt(a, b);
        }
        
        /// <summary>
        /// функція віднімання чисел а і b
        /// </summary>
        /// <param name="a">зменшуване</param>
        /// <param name="b">від’ємне</param>
        /// <returns>різниця</returns>
        public static int Sub(long a, long b)
        {
            return (int)SubtractInt(a, b);
        }
        /// <summary>
        /// функція множення чисел а і b
        /// </summary>
        /// <param name="a">множник</param>
        /// <param name="b">множник</param>
        /// <returns>добуток</returns>
        public static int Mult(long a, long b)
        {
            return (int)MultiplyInt(a, b);
        }
        /// <summary>
        /// функція знаходження частки
        /// </summary>
        /// <param name="a">ділене</param>
        /// <param name="b">дільник</param>
        /// <returns>частка</returns>
        public static int Div(long a, long b)
        {
            return (int)DivideInt(a, b);
        }
        /// <summary>
        /// функція ділення по модулю
        /// </summary>
        /// <param name="a">ділене</param>
        /// <param name="b">дільник</param>
        /// <returns>остача</returns>
        public static int Mod(long a, long b)
        {
            return (int)ModInt(a, b);
        }
        /// <summary>
        /// унарний плюс
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static int ABS(long a)
        {
            return (int)AbsInt(a);
        }
        /// <summary>
        /// унарний мінус
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static int IABS(long a)
        {
            return (int)IAbsInt(a);
        }
        

    }
}
