using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class frmCalculator : Form
    {
        public frmCalculator()
        {
            InitializeComponent();
        }

        bool isTypingNumber = false;
        private void NhapSo(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            NhapSo(btn.Text);
        }

        private void NhapSo(string so)
        {
            if (isTypingNumber)
            {
                // Xoá số 0 ở đầu số
                if (lblDisplay.Text == "0")
                    lblDisplay.Text = "";

                lblDisplay.Text += so;
            }
            else
            {
                lblDisplay.Text = so;
                isTypingNumber = true;
            }
        }

        enum PhepToan { None, Cong, Tru, Nhan, Chia };
        PhepToan phepToan;
        double nho;
        private void NhapPhepToan(object sender, EventArgs e)
        {
            if (nho != 0)
                TinhKetQua();
            Button btn = (Button)sender;
            switch (btn.Text)
            {
                case "+": phepToan = PhepToan.Cong; break;
                case "-": phepToan = PhepToan.Tru; break;
                case "*": phepToan = PhepToan.Nhan; break;
                case "/": phepToan = PhepToan.Chia; break;
            }

            nho = double.Parse(lblDisplay.Text);
            isTypingNumber = false;
        }

        private void TinhKetQua()
        {
            // Tính toán dựa trên: nho, phepToan, lblDisplay.Text
            double tam = double.Parse(lblDisplay.Text);
            double ketQua = 0.0;
            switch (phepToan)
            {
                case PhepToan.Cong: ketQua = nho + tam; break;
                case PhepToan.Tru: ketQua = nho - tam; break;
                case PhepToan.Nhan: ketQua = nho * tam; break;
                case PhepToan.Chia: ketQua = nho / tam; break;
            }

            // Gán kết quả tính được lên lblDisplay.Text
            lblDisplay.Text = ketQua.ToString();
        }

        private void btnBang_Click(object sender, EventArgs e)
        {
            TinhKetQua();
            isTypingNumber = false;
            nho = 0;
            phepToan = PhepToan.None;
        }

        private void frmCalculator_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    NhapSo("" + e.KeyChar);
                    break;
            }
        }

        private void btnPhanTram_Click(object sender, EventArgs e)
        {
            lblDisplay.Text = (double.Parse(lblDisplay.Text) / 100).ToString();
        }

        private void btnCan_Click(object sender, EventArgs e)
        {
            lblDisplay.Text = Math.Sqrt(double.Parse(lblDisplay.Text)).ToString();
        }

        private void btnDoiDau_Click(object sender, EventArgs e)
        {
            lblDisplay.Text = (-1 * double.Parse(lblDisplay.Text)).ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text != "")
                lblDisplay.Text = lblDisplay.Text.Remove(lblDisplay.Text.Length - 1);
            if (lblDisplay.Text == "")
                lblDisplay.Text = "0";
        }

        private void btnXoaNho_Click(object sender, EventArgs e)
        {
            nho = 0;
            lblDisplay.Text = "0";
        }

        private void btnThapPhan_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã tồn tại dấu chấm trong lblDisplay.Text hay chưa
            if (lblDisplay.Text.Contains("."))
            {
                if (lblDisplay.Text == "0.")
                {
                    lblDisplay.Text = "";
                    NhapSo("0.");
                }
                return;
            }

            lblDisplay.Text += ".";
        }
    }
}