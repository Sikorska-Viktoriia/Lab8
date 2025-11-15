using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Прибрано using static System.Windows.Forms.VisualStyles.VisualStyleElement; оскільки він не використовується і може викликати конфлікти.

// ====================================================================
// 1. КЛАСИ МОДЕЛІ та ICOMPARABLE
// ====================================================================

public class MyBook : IComparable
{
    public int bookNomer { get; set; }
    public String Avtor { get; set; }
    public String Nazva { get; set; }
    public String Vydavnyctvo { get; set; }
    public Int16 RikVyhodu { get; set; }

    public MyBook(int bookNomer, String Avtor, String Nazva, String Vydavnyctvo, Int16 RikVyhodu)
    {
        this.bookNomer = bookNomer;
        this.Avtor = Avtor;
        this.Nazva = Nazva;
        this.Vydavnyctvo = Vydavnyctvo;
        this.RikVyhodu = RikVyhodu;
    }

    public override string ToString()
    {
        return $"Книга №{bookNomer} Автор: {Avtor} Назва: {Nazva} Видавництво: {Vydavnyctvo} Рік: {RikVyhodu}";
    }

    int IComparable.CompareTo(object obj)
    {
        MyBook pobj = obj as MyBook;
        if (pobj != null)
        {
            return this.bookNomer.CompareTo(pobj.bookNomer);
        }
        else
        {
            throw new ArgumentException("Параметр повинен бути типу MyBook");
        }
    }
}

public class MyBooks
{
    public MyBook[] MyBooksArray { get; set; }

    public MyBooks(int kilkistKnyh)
    {
        MyBooksArray = new MyBook[kilkistKnyh];
    }
}

// ====================================================================
// 2. ДОПОМІЖНІ КЛАСИ (IComparer)
// ====================================================================

public class bookNameComparer : IComparer
{
    int IComparer.Compare(object o1, object o2)
    {
        MyBook b1 = o1 as MyBook;
        MyBook b2 = o2 as MyBook;

        if (b1 != null && b2 != null)
        {
            return String.Compare(b1.Nazva, b2.Nazva);
        }
        else
        {
            throw new ArgumentException("Параметри не є класу MyBook");
        }
    }
}

public class bookAvtorComparer : IComparer
{
    int IComparer.Compare(object o1, object o2)
    {
        MyBook b1 = o1 as MyBook;
        MyBook b2 = o2 as MyBook;

        if (b1 != null && b2 != null)
        {
            return String.Compare(b1.Avtor, b2.Avtor);
        }
        else
        {
            throw new ArgumentException("Параметри не є класу MyBook");
        }
    }
}

// ====================================================================
// 3. КЛАС ФОРМИ (Form1)
// ====================================================================

namespace lab8._2.dot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Переконайтеся, що властивість AutoSize для label1 встановлена на False,
            // а BorderStyle встановлено на FixedSingle, щоб він міг відображати багато рядків.
        }

        // Єдиний обробник події, який має бути прив'язаний до кнопки button1
        private void button1_Click(object sender, EventArgs e)
        {
            // *** ВИПРАВЛЕННЯ: Для Label використовуємо .Text = string.Empty, а не .Clear() ***
            if (label1 != null)
            {
                label1.Text = string.Empty;
            }

            string ss = "Виведення засобами класу MyBooks несортованого масиву \n";
            ss = ss + "\n";

            // Ініціалізація та заповнення масиву книг
            MyBooks mbs1 = new MyBooks(5);
            mbs1.MyBooksArray[0] = new MyBook(7, "Еріх Марія Ремарк", "Три товариші", "Ранок", 1981);
            mbs1.MyBooksArray[1] = new MyBook(9, "Всеволод Нестайко", "У країні сонячних зайчиків", "Ранок", 1961);
            mbs1.MyBooksArray[2] = new MyBook(2, "С.И.Баскаков", "Радиотехнические цепи и сигналы ", "М.: Высшая школа", 2000);
            mbs1.MyBooksArray[3] = new MyBook(5, "Джеймс Кервуд", "Грізлі Казан ", "Київ, Молодь", 1962);
            mbs1.MyBooksArray[4] = new MyBook(4, "Ю.П.Пармузин", "Осторожно - пума", "Москва. Мисль", 1978);

            // Виведення несортованого масиву
            foreach (MyBook b in mbs1.MyBooksArray)
            {
                if (b != null) ss = ss + b.ToString() + "\n";
            }
            ss = ss + "\n";

            // 1. Сортування за замовчуванням (IComparable: bookNomer)
            Array.Sort(mbs1.MyBooksArray);

            ss = ss + "Виведення засобами класу MyBooks масиву, який посортовано по полю Номер книги \n";
            ss = ss + "\n";
            foreach (MyBook b in mbs1.MyBooksArray)
            {
                if (b != null) ss = ss + b.ToString() + "\n";
            }
            ss = ss + "\n";

            // 2. Сортування за Назвою (IComparer: bookNameComparer)
            Array.Sort(mbs1.MyBooksArray, new bookNameComparer());

            ss = ss + "Виведення засобами класу MyBooks масиву, який посортовано по полю Назва книги \n";
            ss = ss + "\n";
            foreach (MyBook b in mbs1.MyBooksArray)
            {
                if (b != null) ss = ss + b.ToString() + "\n";
            }
            ss = ss + "\n";

            // 3. Сортування за Автором (IComparer: bookAvtorComparer)
            Array.Sort(mbs1.MyBooksArray, new bookAvtorComparer());

            ss = ss + "Виведення засобами класу MyBooks масиву, який посортовано по полю Автор книги \n";
            ss = ss + "\n";
            foreach (MyBook b in mbs1.MyBooksArray)
            {
                if (b != null) ss = ss + b.ToString() + "\n";
            }

            // *** ВИВІД У LABEL1 ***
            if (label1 != null)
            {
                label1.Text = ss;
            }
        }
    }
}