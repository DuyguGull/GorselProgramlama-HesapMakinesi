using System;

namespace HesapMakinesi
{
    public partial class Bilimsel : ContentPage
    {
        private string currentEntry = ""; // Kullan�c�n�n girdi�i say�
        private string operation = ""; // Matematiksel i�lem
        private double firstNumber; // �lk say�
        private bool isWaitingForSecondNumber = false; // �kinci say�y� bekleme durumu

        public Bilimsel()
        {
            InitializeComponent();
        }

        // Say� butonlar�na t�kland���nda ekran� g�ncelle
        private void OnNumberClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            currentEntry += button.Text; // T�klanan say�y� ekle
            Display.Text = currentEntry; // Ekranda g�ster
        }

        // ��lem butonuna t�kland���nda �al���r
        private void OnOperationClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;

            if (double.TryParse(currentEntry, out firstNumber)) // �lk say�y� al
            {
                operation = button.Text; // ��lemi kaydet
                Display.Text = $"{firstNumber} {operation}"; // ��lemi ekrana yaz
                currentEntry = ""; // Mevcut girdiyi s�f�rla
                isWaitingForSecondNumber = true; // �kinci say�y� bekle
            }
        }

        // "=" butonuna t�kland���nda sonucu hesapla
        private void OnEqualsClicked(object sender, EventArgs e)
        {
            if (double.TryParse(currentEntry, out double secondNumber)) // �kinci say�y� al
            {
                double result = 0;
                bool isError = false;

                switch (operation)
                {
                    case "+":
                        result = firstNumber + secondNumber;
                        break;
                    case "-":
                        result = firstNumber - secondNumber;
                        break;
                    case "*":
                        result = firstNumber * secondNumber;
                        break;
                    case "/":
                        if (secondNumber == 0)
                        {
                            Display.Text = "Error: Div by 0"; // Division by zero error
                            isError = true;
                        }
                        else
                        {
                            result = firstNumber / secondNumber;
                        }
                        break;
                    case "mod":
                        result = firstNumber % secondNumber;
                        break;
                    case "x^y":
                        result = Math.Pow(firstNumber, secondNumber); // Exponentiation
                        break;
                    default:
                        isError = true;
                        break;
                }

                if (!isError)
                {
                    Display.Text = $"{firstNumber} {operation} {secondNumber} = {result}"; // Sonucu g�ster
                }
                else
                {
                    currentEntry = ""; // Reset if error
                }

                isWaitingForSecondNumber = false; // Reset waiting flag
            }
        }

        // Bilimsel i�lemleri ele alan metod
        private void OnScientificOperationClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string sciOperation = button.Text;

            if (double.TryParse(Display.Text, out double number))
            {
                double result = 0;
                bool isError = false;

                switch (sciOperation)
                {
                    case "2^x":
                        result = Math.Pow(2, number);
                        break;
                    case "x�":
                        result = Math.Pow(number, 2); // Kare alma
                        break;
                    case "karekok":
                        result = Math.Sqrt(number); // Karek�k
                        break;
                    case "x^y":
                        firstNumber = number; // �s alma i�in ilk say�y� kaydet
                        Display.Text = $"{number}^"; // �kinci say� bekleniyor
                        isWaitingForSecondNumber = true;
                        return; // Return early to wait for second number
                    case "10^x":
                        result = Math.Pow(10, number); // 10'un kuvveti
                        break;
                    case "log":
                        result = Math.Log10(number); // Logaritma (taban 10)
                        break;
                    case "ln":
                        result = Math.Log(number); // Do�al logaritma
                        break;
                    case "1/x":
                        result = 1 / number; // Tersi
                        break;
                    case "|x|":
                        result = Math.Abs(number); // Mutlak de�er
                        break;
                    case "pi":
                        result = Math.PI; // Pi say�s�
                        break;
                    case "e":
                        result = Math.E; // e say�s�
                        break;
                    case "exp":
                        result = Math.Exp(number); // e^x
                        break;
                    case "!":
                        result = Factorial((int)number); // Fakt�riyel hesaplama
                        break;
                    default:
                        isError = true;
                        break;
                }

                if (isError)
                {
                    Display.Text = "Error"; // Error in scientific operation
                }
                else
                {
                    Display.Text = result.ToString(); // Sonucu g�ster
                }
            }
        }

        // Fakt�riyel hesaplama
        private double Factorial(int number)
        {
            if (number < 0)
                throw new ArgumentException("Fakt�riyel i�in negatif say� kullan�lamaz!");

            double result = 1;
            for (int i = 1; i <= number; i++)
            {
                result *= i;
            }
            return result;
        }

        // Girdiyi temizleme
        private void OnClearEntryClicked(object sender, EventArgs e)
        {
            currentEntry = ""; // Ge�erli girdiyi s�f�rla
            Display.Text = ""; // Ekran� temizle
        }

        // Her �eyi temizleme
        private void OnClearClicked(object sender, EventArgs e)
        {
            currentEntry = "";
            firstNumber = 0;
            operation = "";
            Display.Text = ""; // Ekran� temizle
        }

        // Silme (?): Son karakteri kald�rma
        private void OnBackspaceClicked(object sender, EventArgs e)
        {
            if (currentEntry.Length > 0)
            {
                currentEntry = currentEntry.Substring(0, currentEntry.Length - 1);
                Display.Text = currentEntry; // G�ncel girdiyi ekrana yans�t
            }
        }

        // ��aret de�i�tirme
        private void OnChangeSignClicked(object sender, EventArgs e)
        {
            if (double.TryParse(currentEntry, out double number))
            {
                number = -number; // ��areti de�i�tir
                currentEntry = number.ToString();
                Display.Text = currentEntry;
            }
        }
    }
}
