using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Credit Card Interest Calculator");
        Console.WriteLine("===============================");

        // Get monthly interest rate
        Console.Write("Enter monthly interest rate (e.g., 1.5 for 1.5%): ");
        double monthlyInterestRate = Convert.ToDouble(Console.ReadLine()) / 100;

        // Get principal amount
        Console.Write("Enter principal amount ($): ");
        double principal = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter the Downpayment amount ($): ");
        double downpayment = Convert.ToDouble(Console.ReadLine());
        
        if(downpayment > 0)
        {
            principal -= downpayment;
        }

        // Get desired monthly payment
        Console.Write("Enter desired monthly payment ($): ");
        double monthlyPayment = Convert.ToDouble(Console.ReadLine());

        // Validate inputs
        if (monthlyInterestRate <= 0)
        {
            Console.WriteLine("Interest rate must be greater than 0.");
            return;
        }

        if (principal <= 0)
        {
            Console.WriteLine("Principal amount must be greater than 0.");
            return;
        }

        if (monthlyPayment <= 0)
        {
            Console.WriteLine("Monthly payment must be greater than 0.");
            return;
        }

        // Check if monthly payment is sufficient to cover interest
        double minimumPayment = principal * monthlyInterestRate;
        if (monthlyPayment <= minimumPayment)
        {
            Console.WriteLine($"Monthly payment of ${monthlyPayment:F2} is too low.");
            Console.WriteLine($"Minimum payment to reduce balance: ${minimumPayment + 0.01:F2}");
            return;
        }

        // Calculate payoff details
        int months = 0;
        double totalInterestPaid = 0;
        double remainingBalance = principal;
        double totalPaid = 0;

        Console.WriteLine("\nAmortization Schedule:");
        Console.WriteLine("Month | Payment | Interest | Principal | Remaining Balance");
        Console.WriteLine("--------------------------------------------------");

        while (remainingBalance > 0)
        {
            months++;
            
            // Calculate interest for this month
            double interestThisMonth = remainingBalance * monthlyInterestRate;
            totalInterestPaid += interestThisMonth;
            
            // Calculate principal portion of payment
            double principalThisMonth = monthlyPayment - interestThisMonth;
            
            // Adjust for final payment
            if (principalThisMonth > remainingBalance)
            {
                principalThisMonth = remainingBalance;
                monthlyPayment = principalThisMonth + interestThisMonth;
            }
            
            // Update remaining balance
            remainingBalance -= principalThisMonth;
            totalPaid += monthlyPayment;
            
            // Display this month's payment details
            Console.WriteLine($"{months,5} | ${monthlyPayment,7:F2} | ${interestThisMonth,8:F2} | ${principalThisMonth,9:F2} | ${remainingBalance,16:F2}");

            // Break if it's taking too long (safety check)
            if (months > 1000)
            {
                Console.WriteLine("Payment too small, will take over 1000 months to pay off.");
                break;
            }
        }

        // Display summary
        Console.WriteLine("\nPayment Summary:");
        Console.WriteLine("===============================");
        Console.WriteLine($"Total months to pay off: {months}");
        Console.WriteLine($"Total amount paid + Downpayment: ${totalPaid + downpayment:F2}");
        Console.WriteLine($"Total interest paid: ${totalInterestPaid:F2}");
        Console.WriteLine($"Original principal: ${principal + downpayment:F2}");
        Console.WriteLine($"Interest to principal ratio: {(totalInterestPaid / principal):P2}");
    }
}

