using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// Клас Employee реалізує IComparable<T> (для сортування за ID) та ICloneable (для поверхневого копіювання).
/// </summary>
public class Employee : IComparable<Employee>, ICloneable
{
    public int EmployeeID { get; set; }
    public string Name { get; set; }
    public decimal Salary { get; set; }

    public Employee(int id, string name, decimal salary)
    {
        EmployeeID = id;
        Name = name;
        Salary = salary;
    }

    // РЕАЛІЗАЦІЯ ICOΜPARABLE<T>: Сортування за EmployeeID
    public int CompareTo(Employee other)
    {
        if (other == null) return 1;
        return this.EmployeeID.CompareTo(other.EmployeeID);
    }

    // РЕАЛІЗАЦІЯ ICLONEABLE: Поверхневе копіювання
    public object Clone()
    {
        return this.MemberwiseClone();
    }

    public override string ToString()
    {
        // Виведення у форматі ID | Ім'я | Зарплата (валюта)
        return $"ID:{EmployeeID,-4} Name: {Name,-20} Salary: {Salary:C0}";
    }
}

/// <summary>
/// Клас CompanyData демонструє необхідність ГЛИБОКОГО копіювання, оскільки містить посилальний тип (List<Employee>).
/// </summary>
public class CompanyData
{
    public List<Employee> Employees { get; set; }
    public string CompanyName { get; set; }

    public CompanyData(string name)
    {
        CompanyName = name;
        Employees = new List<Employee>();
    }

    // МЕТОД ГЛИБОКОГО КОПІЮВАННЯ (Deep Clone)
    public CompanyData DeepClone()
    {
        // 1. Поверхнева копія (MemberwiseClone)
        CompanyData clonedData = (CompanyData)this.MemberwiseClone();

        // 2. Створення нового, незалежного списку.
        clonedData.Employees = new List<Employee>();

        // 3. Копіювання кожного елемента (співробітника) окремо.
        foreach (Employee emp in this.Employees)
        {
            clonedData.Employees.Add((Employee)emp.Clone());
        }
        return clonedData;
    }
}


// ====================================================================
// КЛАС ФОРМИ (Form1)
// ====================================================================

namespace lab8._1.dot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            // InitializeComponent() генерується дизайнером і викликає ініціалізацію textBox1 та button1.
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Завантаження форми (залишаємо порожнім)
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Звіт виводимо у textBox1, який був ініціалізований у InitializeComponent().
            textBox1.Clear();
            string report = "=== Демонстрація інтерфейсів IComparable та ICloneable ===\r\n\r\n";

            // --------------------------------------------------------------------
            // ЧАСТИНА 1: Демонстрація ICloneable (ГЛИБОКЕ КОПІЮВАННЯ)
            // --------------------------------------------------------------------

            // 1. Створення оригінального об'єкта
            CompanyData originalCompany = new CompanyData("TechCorp Original");
            originalCompany.Employees.Add(new Employee(4, "Іван П.", 65000m));
            originalCompany.Employees.Add(new Employee(1, "Олена К.", 80000m));

            report += "--- 1. Оригінальний об'єкт (ID: 4, 1) ---\r\n";
            originalCompany.Employees.ForEach(emp => report += emp.ToString() + "\r\n");
            report += "\r\n";

            // 2. Створення глибокої копії
            CompanyData copiedCompany = originalCompany.DeepClone();

            // Змінюємо поле у КОПІЇ (Іван ID:4)
            copiedCompany.Employees[0].Salary = 99999m;

            report += "--- 2. Копія об'єкта (Змінено ЗП Івана на 99999) ---\r\n";
            report += $"Компанія: TechCorp Copy\r\n";
            copiedCompany.Employees.ForEach(emp => report += emp.ToString() + "\r\n");
            report += "\r\n";

            // 3. Перевірка Оригіналу
            report += "--- 3. Перевірка Оригіналу ---\r\n";
            report += "Оригінальна ЗП Івана (65000) НЕ змінилась, що доводить ГЛИБОКЕ копіювання.\r\n";
            originalCompany.Employees.ForEach(emp => report += emp.ToString() + "\r\n");
            report += "\r\n";

            // --------------------------------------------------------------------
            // ЧАСТИНА 2: Демонстрація IComparable (СОРТУВАННЯ)
            // --------------------------------------------------------------------

            report += "--- 4. Сортування Оригіналу (за EmployeeID) ---\r\n";

            // Виклик Sort() використовує Employee.CompareTo, оскільки клас реалізує IComparable<Employee>.
            originalCompany.Employees.Sort();

            report += "Порядок після Sort() (відсортовано за ID): \r\n";
            originalCompany.Employees.ForEach(emp => report += emp.ToString() + "\r\n");

            // Вивід звіту у TextBox
            textBox1.Text = report;
        }
    }
}