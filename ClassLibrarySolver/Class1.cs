﻿using System;
using System.Collections.Generic;

namespace ClassLibrarySolver
{
    public static class Solver
    {
        /// <summary>
        /// Возвращает список чисел, находящихся в диапазоне [start, finish], с n делителями (не считая базовых).
        /// </summary>
        /// <param name="start"> начало диапазона </param>
        /// <param name="finish"> конец диапазона </param>
        /// <param name="n"> количесвто делителей </param>
        /// <returns> список чисел с n делителями </returns>
        public static List<int> NumbersWithNDivisors(int start, int finish, int n)
        {
            List<int> result = new List<int>();
            for (int x = start; x <= finish; x++)
            {
                if (CountDivisors(x) == n)
                {
                    result.Add(x);
                }
            }
            return result;
        }

        /// <summary>
        /// Возвращает количество делителей числа (не считая базовых)
        /// </summary>
        /// <param name="x"> число для посчёта его делителей </param>
        /// <returns> количество делителей числа </returns>
        public static int CountDivisors(int x)
        {
            int count = 0;
            int SqrtN = (int)Math.Sqrt(x);
            if (x == SqrtN * SqrtN && x != 1)
            {
                count++;
                SqrtN--;
            }
            for (int i = 2; i <= SqrtN; i++)
            {
                if (x % i == 0)
                {
                    count += 2;
                }
            }
            return count;
        }



        /// <summary>
        /// Возвращает массив из count чисел, больших start, сумма минимального и максимального делителей которых заканчивается на n
        /// </summary>
        /// <param name="start"> начальное число (не включительно) </param>
        /// <param name="count"> количество чисел </param>
        /// <param name="n"> на что заканчивается сумма делителей </param>
        /// <returns> массив из count найденных чисел </returns>
        public static int[] NumbersWithSumOfMinMaxDivisorsEqualsN(int start, int count, int n)
        {
            int[] result = new int[count];
            int ind = 0;
            int x = start + 1;
            while (ind < count)
            {
                if (SumOfMinMaxDivisors(x) % 10 == n)
                {
                    result[ind] = x;
                    ind++;
                }
                x++;
            }
            return result;
        }

        /// <summary>
        /// Возвращает сумму минимального и максимального делителей числа (не считая базовых)
        /// </summary>
        /// <param name="x"> число </param>
        /// <returns> сумма минимального и максимального делителей </returns>
        public static int SumOfMinMaxDivisors(int x)
        {
            int SqrtN = (int)Math.Sqrt(x);
            for (int i = 2; i <= SqrtN; i++)
            {
                if (x % i == 0)
                {
                    return i + (x / i);
                }
            }
            if (x == SqrtN * SqrtN && x != 1)
            {
                return 2 * SqrtN;
            }
            return 0;
        }



        /// <summary>
        /// Возвращает список чисел, составленный из заданных чисел numbers, запись которых в системе P имеет цифру n
        /// </summary>
        /// <param name="numbers"> заданные числа в системе 10 </param>
        /// <param name="P"> необходимое основание </param>
        /// <param name="n"> искомая цифра </param>
        /// <returns> список чисел </returns>
        public static List<int> NumbersWithDigitNInBaseP(int[] numbers, int P, int n)
        {
            List<int> result = new List<int>();
            foreach (int number in numbers)
            {
                int x = number;
                while (x > 0)
                {
                    if (x % P == n)
                    {
                        result.Add(number);
                        break;
                    }
                    x /= P;
                }
            }
            return result;
        }
    }
}