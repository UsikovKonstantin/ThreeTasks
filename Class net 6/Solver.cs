namespace ClassLibrarySolver
{
    public static class Solver
    {
        #region Task 1
        /// <summary>
        /// Возвращает список чисел, находящихся в диапазоне [start, finish], с n делителями (не считая базовых).
        /// </summary>
        /// <param name="start"> начало диапазона </param>
        /// <param name="finish"> конец диапазона </param>
        /// <param name="n"> количество делителей </param>
        /// <returns> список чисел с n делителями </returns>
        public static List<long> NumbersWithNDivisors(long start, long finish, long n, object obj)
        {
            CancellationToken ct = (CancellationToken)obj;
            List<long> result = new List<long>();
            for (long x = start; x <= finish; x++)
            {
                if (ct.IsCancellationRequested)
                {
                    return null;
                }
                if (CountDivisors(x, n) == n)
                {
                    result.Add(x);
                }
            }
            return result;
        }

        /// <summary>
        /// Возвращает количество делителей числа (не считая базовых).
        /// Если количество делителей в какой-то момент станет больше n, функция завершит выполнение.
        /// </summary>
        /// <param name="x"> число для посчёта его делителей </param>
        /// <param name="n"> ограничитель количесва делителей </param>
        /// <returns> количество делителей числа </returns>
        public static long CountDivisors(long x, long n)
        {
            long count = 0;
            long SqrtN = (long)Math.Sqrt(x);
            if (x == SqrtN * SqrtN && x != 1)
            {
                count++;
                if (count > n)
                {
                    return count;
                }
                SqrtN--;
            }
            for (long i = 2; i <= SqrtN; i++)
            {
                if (x % i == 0)
                {
                    count += 2;
                    if (count > n)
                    {
                        return count;
                    }
                }
            }
            return count;
        }
        #endregion

        #region Task 2
        /// <summary>
        /// Возвращает массив из count чисел, больших start, сумма минимального и максимального делителей которых заканчивается на n
        /// </summary>
        /// <param name="start"> начальное число (не включительно) </param>
        /// <param name="count"> количество чисел </param>
        /// <param name="n"> на что заканчивается сумма делителей </param>
        /// <returns> массив из count найденных чисел </returns>
        public static long[] NumbersWithSumOfMinMaxDivisorsEqualsN(long start, long count, long n, object obj)
        {
            CancellationToken ct = (CancellationToken)obj;
            long[] result = new long[0];

            try
            {
                result = new long[count];
            }
            catch (Exception)
            {
                return NumbersWithSumOfMinMaxDivisorsEqualsN(start, (long)(count * 0.9), n, obj);
            }
            //long[] result = new long[count];
            long ind = 0;
            try
            {
                checked
                {
                    long x = start + 1;
                    while (ind < count)
                    {
                        if (ct.IsCancellationRequested)
                        {
                            return null;
                        }
                        if (SumOfMinMaxDivisors(x) % 10 == n)
                        {
                            result[ind] = x;
                            ind++;
                        }
                        x++;
                    }
                }
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }

        /// <summary>
        /// Возвращает сумму минимального и максимального делителей числа (не считая базовых)
        /// </summary>
        /// <param name="x"> число </param>
        /// <returns> сумма минимального и максимального делителей </returns>
        public static long SumOfMinMaxDivisors(long x)
        {
            long SqrtN = (long)Math.Sqrt(x);
            for (long i = 2; i <= SqrtN; i++)
            {
                if (x % i == 0)
                {
                    return i + (x / i);
                }
            }
            return 0;
        }

        #endregion

        #region Task 3
        /// <summary>
        /// Возвращает список чисел, составленный из заданных чисел numbers, запись которых в системе P имеет цифру n
        /// </summary>
        /// <param name="numbers"> заданные числа в системе 10 </param>
        /// <param name="P"> необходимое основание </param>
        /// <param name="n"> искомая цифра </param>
        /// <returns> список чисел </returns>
        public static List<long> NumbersWithDigitNInBaseP(long[] numbers, long P, long n)
        {
            List<long> result = new List<long>();
            foreach (long number in numbers)
            {
                long x = number;
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
        #endregion
    }
}