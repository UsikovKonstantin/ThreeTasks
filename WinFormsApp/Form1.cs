using ClassLibrarySolver;
namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            T1_Change();
            T2_Change();
            T3_Change();
        }
        List<(Thread thread, CancellationTokenSource cts)> threads = new();
        #region Task 1
        private void T1_txtbox_begin_TextChanged(object sender, EventArgs e)
        {
            T1_Change();
        }

        private void T1_txtbox_end_TextChanged(object sender, EventArgs e)
        {
            T1_Change();
        }

        private void T1_txtbox_numdiv_TextChanged(object sender, EventArgs e)
        {
            T1_Change();
        }
        
        void T1_Change()
        {
            for (int i = 0; i < threads.Count; i++)
            {
                if (threads[i].thread.ThreadState == ThreadState.Running)
                {
                    threads[i].cts.Cancel();
                    threads.RemoveAt(i);
                    break;
                }
            }
            CancellationTokenSource cts = new();
            T1_error.Clear();
            T1_result.Clear();
            T1_err_begin.Visible = true;
            T1_err_end.Visible = true;
            T1_err_numdiv.Visible = true;
            bool begin_checks = T1_Begin_Checks();
            bool end_checks = T1_End_Checks();
            bool div_checks = T1_Dividers_Checks();
            if (begin_checks && end_checks && div_checks)
            {
                var thr1 = new Thread(T1_Calculate);
                threads.Add((thr1,cts));
                thr1.Start(cts.Token);
            }
        }
        bool T1_Begin_Checks()
        {
            if (!Text_exists(T1_txtbox_begin))
            {
                T1_error.Text += $"Поле \"{T1_label_begin.Text}\" должно быть заполнено {Environment.NewLine}";
                return false;
            }
            if (!Can_be_Parsed(T1_txtbox_begin))
            {
                T1_error.Text += $"Значение в поле \"{T1_label_begin.Text}\" не может быть переведено в целое число {Environment.NewLine}";
                return false;
            }
            if (long.Parse(T1_txtbox_begin.Text) <= 2)
            {
                T1_error.Text += $"Значение в поле \"{T1_label_begin.Text}\" должно быть больше 2 {Environment.NewLine}";
                return false;
            }
            T1_err_begin.Visible = false;
            return true;
        }
        bool T1_End_Checks()
        {
            if (!Text_exists(T1_txtbox_end))
            {
                T1_error.Text += $"Поле \"{T1_label_end.Text}\" должно быть заполнено {Environment.NewLine}";
                return false;
            }
            if (!Can_be_Parsed(T1_txtbox_end))
            {
                T1_error.Text += $"Значение в поле \"{T1_label_end.Text}\" не может быть переведено в целое число {Environment.NewLine}";
                return false;
            }
            if (long.Parse(T1_txtbox_end.Text) <= 2)
            {
                T1_error.Text += $"Значение в поле \"{T1_label_end.Text}\" должно быть больше 2 {Environment.NewLine}";
                return false;
            }
            T1_err_end.Visible = false;
            return true;
        }
        bool T1_Dividers_Checks()
        {
            if (!Text_exists(T1_txtbox_numdiv))
            {
                T1_error.Text += $"Поле \"{T1_label_divisors.Text}\" должно быть заполнено {Environment.NewLine}";
                return false;
            }
            if (!Can_be_Parsed(T1_txtbox_numdiv))
            {
                T1_error.Text += $"Значение в поле \"{T1_label_divisors.Text}\" не может быть переведено в целое число {Environment.NewLine}";
                return false;
            }
            if (long.Parse(T1_txtbox_numdiv.Text) < 0)
            {
                T1_error.Text += $"Значение в поле \"{T1_label_divisors.Text}\" должно быть больше или равно 0 {Environment.NewLine}";
                return false;
            }
            T1_err_numdiv.Visible = false;
            return true;
        }
        void T1_Calculate(object obj)
        {
            CancellationToken ct = (CancellationToken)obj;
            Invoke(new Action(() => T1_result.Text = "Происходит вычисление..."));
            long beg = long.Parse(T1_txtbox_begin.Text), end = long.Parse(T1_txtbox_end.Text), div = long.Parse(T1_txtbox_numdiv.Text);
            if (beg > end)
            {
                (end, beg) = (beg, end);
            }
            var range = Solver.NumbersWithNDivisors(beg, end, div,ct);
            if (ct.IsCancellationRequested)
            {
                Invoke(new Action(() => T1_result.Text = "Вычисление отменено"));
                return;
            }
            string output = "";
            if (range.Count == 0)
            {
                Invoke(new Action(() => T1_result.Text = "Не найдено"));
            }
            for (int i = 0; i < range.Count; i++)
            {
                output += $"{i} - {range[i]} {Environment.NewLine}";
            }
            Invoke(new Action(() => T1_result.Text = output));
        }
        #endregion

        #region Task 2
        private void T2_txtbox_begin_TextChanged(object sender, EventArgs e)
        {
            T2_Change();
        }

        private void T2_txtbox_count_TextChanged(object sender, EventArgs e)
        {
            T2_Change();
        }

        private void T2_txtbox_sum_TextChanged(object sender, EventArgs e)
        {
            T2_Change();
        }
        void T2_Change()
        {
            for (int i = 0; i < threads.Count; i++)
            {
                if (threads[i].thread.ThreadState == ThreadState.Running)
                {
                    threads[i].cts.Cancel();
                    threads.RemoveAt(i);
                    break;
                }
            }
            CancellationTokenSource cts = new();
            T2_error.Clear();
            T2_result.Clear();
            T2_err_begin.Visible = true;
            T2_err_count.Visible = true;
            T2_err_sum.Visible = true;
            bool begin_checks = T2_Begin_Checks();
            bool count_checks = T2_Count_Checks();
            bool sum_checks = T2_Sum_Checks();
            if (begin_checks && count_checks && sum_checks)
            {
                var thr1 = new Thread(T2_Calculate);
                threads.Add((thr1, cts));
                thr1.Start(cts.Token);
            }
        }
        bool T2_Begin_Checks()
        {
            if (!Text_exists(T2_txtbox_begin))
            {
                T2_error.Text += $"Поле \"{T2_label_begin.Text}\" должно быть заполнено {Environment.NewLine}";
                return false;
            }
            if (!Can_be_Parsed(T2_txtbox_begin))
            {
                T2_error.Text += $"Значение в поле \"{T2_label_begin.Text}\" не может быть переведено в целое число {Environment.NewLine}";
                return false;
            }
            if (long.Parse(T2_txtbox_begin.Text) <= 2)
            {
                T2_error.Text += $"Значение в поле \"{T2_label_begin.Text}\" должно быть больше 2 {Environment.NewLine}";
                return false;
            }
            T2_err_begin.Visible = false;
            return true;
        }
        bool T2_Count_Checks()
        {
            if (!Text_exists(T2_txtbox_count))
            {
                T2_error.Text += $"Поле \"{T2_label_count.Text}\" должно быть заполнено {Environment.NewLine}";
                return false;
            }
            if (!Can_be_Parsed(T2_txtbox_count))
            {
                T2_error.Text += $"Значение в поле \"{T2_label_count.Text}\" не может быть переведено в целое число {Environment.NewLine}";
                return false;
            }
            if (long.Parse(T2_txtbox_count.Text) < 1)
            {
                T2_error.Text += $"Значение в поле \"{T2_label_count.Text}\" должно быть больше 0 {Environment.NewLine}";
                return false;
            }
            if (long.Parse(T2_txtbox_count.Text) > 1000000)
            {
                T2_error.Text += $"Значение в поле \"{T2_label_count.Text}\" должно быть меньше или равно 1000000 {Environment.NewLine}";
                return false;
            }
            T2_err_count.Visible = false;
            return true;
        }
        bool T2_Sum_Checks()
        {
            if (!Text_exists(T2_txtbox_sum))
            {
                T2_error.Text += $"Поле \"{T2_label_sum.Text}\" должно быть заполнено {Environment.NewLine}";
                return false;
            }
            if (!Can_be_Parsed(T2_txtbox_sum))
            {
                T2_error.Text += $"Значение в поле \"{T2_label_sum.Text}\" не может быть переведено в целое число {Environment.NewLine}";
                return false;
            }
            if (long.Parse(T2_txtbox_sum.Text) < 0)
            {
                T2_error.Text += $"Значение в поле \"{T2_label_sum.Text}\" должно быть больше или равно 0 {Environment.NewLine}";
                return false;
            }
            if (long.Parse(T2_txtbox_sum.Text) > 9)
            {
                T2_error.Text += $"Значение в поле \"{T2_label_sum.Text}\" должно быть меньше или равно 9 {Environment.NewLine}";
                return false;
            }
            T2_err_sum.Visible = false;
            return true;
        }
        void T2_Calculate(object obj)
        {
            CancellationToken ct = (CancellationToken)obj;
            Invoke(new Action(() => T2_result.Text = "Происходит вычисление..."));
            long beg = long.Parse(T2_txtbox_begin.Text), count = long.Parse(T2_txtbox_count.Text), sum = long.Parse(T2_txtbox_sum.Text);
            var range = Solver.NumbersWithSumOfMinMaxDivisorsEqualsN(beg, count, sum, ct);
            if (ct.IsCancellationRequested)
            {
                Invoke(new Action(() => T2_result.Text = "Вычисление отменено"));
                return;
            }
            string output = "";
            if (range.Length == 0)
            {
                Invoke(new Action(() => T2_result.Text = "Не найдено"));
            }
            for (int i = 0; i < range.Length; i++)
            {
                output += $"{i} - {range[i]} {Environment.NewLine}";
            }
            Invoke(new Action(() => T2_result.Text = output));
        }
        #endregion

        #region Task 3
        private void T3_txtbox_numbers_TextChanged(object sender, EventArgs e)
        {
            T3_Change();
        }

        private void T3_txtbox_Csys_TextChanged(object sender, EventArgs e)
        {
            T3_Change();
        }

        private void T3_txtbox_numreq_TextChanged(object sender, EventArgs e)
        {
            T3_Change();
        }
        void T3_Change()
        {
            for (int i = 0; i < threads.Count; i++)
            {
                if (threads[i].thread.ThreadState == ThreadState.Running)
                {
                    threads[i].cts.Cancel();
                    threads.RemoveAt(i);
                    break;
                }
            }
            CancellationTokenSource cts = new();
            T3_error.Clear();
            T3_result.Clear();
            T3_err_numbers.Visible = true;
            T3_err_Csys.Visible = true;
            T3_err_numreq.Visible = true;
            bool numbers_checks = T3_Numbers_Checks();
            bool Csys_checks = T3_Csys_Checks();
            bool numreq_checks = T3_Numreq_Checks();
            if (numbers_checks && Csys_checks && numreq_checks)
            {
                var thr1 = new Thread(T3_Calculate);
                threads.Add((thr1, cts));
                thr1.Start(cts.Token);
            }
        }
        bool T3_Numbers_Checks()
        {
            if (!Text_exists(T3_txtbox_numbers))
            {
                T3_error.Text += $"Поле \"{T3_label_numbers.Text}\" должно быть заполнено {Environment.NewLine}";
                return false;
            }
            string[] temp = T3_txtbox_numbers.Text.Split(',');
            for (long i = 0;i<temp.Length;i++)
            {
                try
                {
                    long.Parse(temp[i]);
                }
                catch (Exception)
                {
                    T3_error.Text += $"Поле \"{T3_label_numbers.Text}\" на позиции {i} не может быть переведено в целое число {Environment.NewLine}";
                    return false;
                }
            }
            for (long i = 0; i < temp.Length; i++)
            {
                if (long.Parse(temp[i]) < 0)
                {
                    T3_error.Text += $"Поле \"{T3_label_numbers.Text}\" на позиции {i} должно быть больше или равно 0 {Environment.NewLine}";
                    return false;
                }
            }
            T3_err_numbers.Visible = false;
            return true;
        }
        bool T3_Csys_Checks()
        {
            if (!Text_exists(T3_txtbox_Csys))
            {
                T3_error.Text += $"Поле \"{T3_label_Csys.Text}\" должно быть заполнено {Environment.NewLine}";
                return false;
            }
            if (!Can_be_Parsed(T3_txtbox_Csys))
            {
                T3_error.Text += $"Значение в поле \"{T3_label_Csys.Text}\" не может быть переведено в целое число {Environment.NewLine}";
                return false;
            }
            if (long.Parse(T3_txtbox_Csys.Text) < 2)
            {
                T3_error.Text += $"Значение в поле \"{T3_label_Csys.Text}\" должно быть больше или равно 2 {Environment.NewLine}";
                return false;
            }
            T3_err_Csys.Visible = false;
            return true;
        }
        bool T3_Numreq_Checks()
        {
            if (!Text_exists(T3_txtbox_numreq))
            {
                T3_error.Text += $"Поле \"{T3_label_numreq.Text}\" должно быть заполнено {Environment.NewLine}";
                return false;
            }
            if (!Can_be_Parsed(T3_txtbox_numreq) &&
                (T3_txtbox_numreq.Text.Length != 1 || T3_txtbox_numreq.Text.ToUpper()[0] < 'A' || T3_txtbox_numreq.Text.ToUpper()[0] > 'Z'))
            {
                T3_error.Text += $"Значение в поле \"{T3_label_numreq.Text}\" не может быть переведено в целое число {Environment.NewLine}";
                return false;
            }
            if (Can_be_Parsed(T3_txtbox_numreq) && long.Parse(T3_txtbox_numreq.Text) < 0)
            {
                T3_error.Text += $"Значение в поле \"{T3_label_numreq.Text}\" должно быть больше или равно 0 {Environment.NewLine}";
                return false;
            }
            T3_err_numreq.Visible = false;
            return true;
        }
        void T3_Calculate(object obj)
        {
            CancellationToken ct = (CancellationToken)obj;
            Invoke(new Action(() => T3_result.Text = "Происходит вычисление..."));
            string[] temp = T3_txtbox_numbers.Text.Split(',');
            long[] numbers = new long[temp.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                numbers[i] = long.Parse(temp[i]);
            }
            long Csys = long.Parse(T3_txtbox_Csys.Text); 
            long numreq = Can_be_Parsed(T3_txtbox_numreq) ? long.Parse(T3_txtbox_numreq.Text) : T3_txtbox_numreq.Text.ToUpper()[0] - 'A' + 10;
            var range = Solver.NumbersWithDigitNInBaseP(numbers, Csys, numreq);
            if (ct.IsCancellationRequested)
            {
                Invoke(new Action(() => T3_result.Text = "Вычисление отменено"));
                return;
            }
            string output = "";
            if (range.Count == 0)
            {
                Invoke(new Action(() => T3_result.Text = "Не найдено"));
            }
            for (int i = 0; i < range.Count; i++)
            {
                output += $"{i} - {range[i]} {Environment.NewLine}";
            }
            Invoke(new Action(() => T3_result.Text = output));
        }
        #endregion

        #region errors
        bool Text_exists(TextBox txtbox)
        {
            if (txtbox.Text == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        bool Can_be_Parsed(TextBox txtbox)
        {
            try
            {
                checked
                {
                    long.Parse(txtbox.Text);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}