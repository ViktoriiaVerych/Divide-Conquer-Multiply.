using Conquer_the_second;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Enter an expression:");
        string input = Console.ReadLine();

        //splits the input into operands and operator
        string[] parts = input.Split(' ');
        if (parts.Length != 3)
        {
            Console.WriteLine("Invalid");
            return;
        }

        BigInteger operand1 = new BigInteger(parts[0]); //first
        string operation = parts[1];
        BigInteger operand2 = new BigInteger(parts[2]);//third

        BigInteger result;

        switch (operation)
        {
            case "+":
                result = operand1 + operand2;
                break;
            case "-":
                result = operand1 - operand2;
                break;
            case "*":
                result = operand1.Multiplication(operand2);
                break;
            default:
                Console.WriteLine("Invalid");
                return;
        }

        Console.WriteLine(result);
    }
}



// class Program
// {
//     static void Main(string[] args)
//     {
//         var x = new BigInteger("12");
//         var y = new BigInteger("4");
//         // //add
//         // Console.WriteLine(x + y);
//
//
//         //sub
//         var res = x - y;
//         Console.WriteLine(res);
//
//
//
//         //karatsuba
//         //var res3 = new BigInteger("5678").Multiplication(new BigInteger("1234"));
//
//         // Console.WriteLine(res3);
//     }
// }

