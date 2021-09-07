using ConsoleTables;
using System;

namespace EmployeePayProject
{  
    class Program
    {
        static decimal Tax(decimal GrossPay)
        {
            const decimal personalRelief = 2400;
            decimal payableTax = Convert.ToDecimal(0) - personalRelief;
            decimal TaxableIncome = GrossPay - (GrossPay * Convert.ToDecimal(0.03));

            if (TaxableIncome < Convert.ToDecimal(24000))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Gross pay is less than the minimum taxable income");
                Console.ForegroundColor = ConsoleColor.White;
                payableTax = Convert.ToDecimal(0);
            }
            if (TaxableIncome >= Convert.ToDecimal(24000))
            {
                payableTax += Convert.ToDecimal(0.1 * 24000);
                TaxableIncome -= 24000;
            }
            if (TaxableIncome > Convert.ToDecimal(24000))
            {
                payableTax += Convert.ToDecimal(0.25 * 8333);
                TaxableIncome -= 8333;
            }
            if (TaxableIncome > Convert.ToDecimal(32333))
            {
                payableTax += Convert.ToDecimal(0.3) * TaxableIncome;
            }

            return payableTax;
        }
        static string GenderName(string name)
        {
            if(name == "F") 
                {
                    name = "Female";
                }
            else 
                {
                    name = "Male";
                }
            return name;
        }
        static string EmailVendor(string email) 
        {
            char[] separators = { '@', '.' };
            var emailproviderfinder = email.Split(separators);
            return emailproviderfinder[2];
        }

        static void Main(string[] args)
        {
            int i = 0;
            string[] details = System.IO.File.ReadAllLines(@"F:\C#\EmployeePay.txt");

            //creating an array of objects
            Data[] individuals = new Data[details.Length];

            //intitializing the array of objects
            for (int x = 0; x < details.Length; x++)
            {
                individuals[x] = new Data();
            }

            foreach (string line in details)
            {
                var stringedLine = line.Split(",");

                //assigning the data to the objects                
                individuals[i].FirstName = stringedLine[0];
                individuals[i].LastName = stringedLine[1];

                individuals[i].Gender = stringedLine[2];
                individuals[i].Gender = GenderName(individuals[i].Gender);

                individuals[i].Email = stringedLine[3];
                individuals[i].Email = EmailVendor(individuals[i].Email);                

                individuals[i].SalaryPerMonth = Convert.ToDecimal(stringedLine[4]);
                individuals[i]._Tax = Tax(individuals[i].SalaryPerMonth);
                i++;
            }

            var table = new ConsoleTable("Full Name", "Gender", "Email Provider", "GrossPay", "PAYE");
            for (int z = 0; z < details.Length; z++)
            {
                table.AddRow(individuals[z].FirstName + " " + individuals[z].LastName, individuals[z].Gender, individuals[z].Email, Decimal.Round(individuals[z].SalaryPerMonth, 2), Decimal.Round(individuals[z]._Tax, 2));
            }
            table.Write();
        
        }
    }
}
