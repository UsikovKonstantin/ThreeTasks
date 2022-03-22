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
        CancellationTokenSource cts = new();
        List<Thread> threads = new();
        void T1_Change()
        {
            foreach (var t in threads)
            {
                if (t.ThreadState == ThreadState.Running)
                {
                    cts.Cancel();
                }
            }
            CancellationToken token = cts.Token;
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
                threads.Add(thr1);
                thr1.Start(token);
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