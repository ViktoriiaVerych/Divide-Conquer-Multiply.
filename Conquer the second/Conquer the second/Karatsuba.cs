namespace Conquer_the_second;
using System;

class Karatsuba
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter two numbers:");
        string first = Console.ReadLine();
        string second = Console.ReadLine();
        Console.WriteLine(Multiply(first, second));
        Console.ReadLine();
    }

    static string Multiply(string first, string second)
    {
        int cutPos = GetCutPosition(first, second);//splitting the input numbers into parts
        //split into 4 parts
        string a = GetFirstPart(first, cutPos);
        string b = GetSecondPart(first, cutPos);
        string c = GetFirstPart(second, cutPos);
        string d = GetSecondPart(second, cutPos);
        //recursive multiplications
        string ac = Multiply(a, c);
        string bd = Multiply(b, d);
        string ab_cd = Multiply(StringAddition(a, b), StringAddition(c, d));
        //calculates the values and the padding/adding (the sym of lengths of b and d)
        return CalculateResult(ac, bd, ab_cd, b.Length + d.Length);
    }
    //combines the calculated values and returns the final result as a string

    static string CalculateResult(string ac, string bd, string ab_cd, int padding)
    {
        string term0 = StringSubtraction(StringSubtraction(ab_cd, ac), bd);//subtracting ac and bd from ab_cd
        string term1 = term0.PadRight(term0.Length + padding / 2, '0');//for aligning the result, zeros is determined by padding
        string term2 = ac.PadRight(ac.Length + padding, '0');//similar as the previous 
        return StringAddition(StringAddition(term1, term2), bd);//combining
    }

    static string GetFirstPart(string str, int cutPos)
    {
        return str.Remove(str.Length - cutPos);//the first part of string
    }

    static string GetSecondPart(string str, int cutPos)
    {
        return str.Substring(str.Length - cutPos);//the first part of string
    }

    static int GetCutPosition(string first, string second)
        //calculates the cut position for splitting the numbers based on their lengths
    {
        int min = Math.Min(first.Length, second.Length);
        if (min == 1) 
            return 1;
        if (min / 2 == 0) //if even
            return min / 2;
        return min / 2 + 1;//
    }

    static string StringAddition(string a, string b)
    {
        string result = "";//stores the result of addition
        if (a.Length > b.Length)
        {
            Swap(ref a, ref b);
        }
        //for aligning. add leading zeros to a so that it matches the length of b
        a = a.PadLeft(b.Length, '0');
        
        int length = a.Length;
        int carry = 0, res;//stores current digit result
        
        //from the rightmost to the leftmost 
        for (int i = length - 1; i >= 0; i--)
        {
            //extracts the corresponding digits and converts into integers via int.Parse
            int num1 = int.Parse(a.Substring(i, 1));
            int num2 = int.Parse(b.Substring(i, 1));
            res = (num1 + num2 + carry) % 10;//the digits are aaded with the carry to the previous iteration
            carry = (num1 + num2 + carry) / 10;//divided by 10 to obtain the result digit (res)
            result = result.Insert(0, res.ToString());//inserted at he beginning of the result //addition in the correct order
        }
        if (carry != 0)//if carry is left
            result = result.Insert(0, carry.ToString());
        return SanitizeResult(result);//remove leading zeros and handle the case when the result is 0
    }

    static string StringSubtraction(string a, string b)
    {
        bool resultNegative = false;
        string result = "";
        if (StringIsSmaller(a, b))//if a is smaller than b, swap
        {
            Swap(ref a, ref b);
            resultNegative = true;
        }
        b = b.PadLeft(a.Length, '0');//add zeros to align
        int length = a.Length;//as it is equal to b
        int carry = 0, res;
        for (int i = length - 1; i >= 0; i--)//from the rightmost to the leftmost
        {
            bool nextCarry = false;
            int num1 = int.Parse(a.Substring(i, 1));//extracts the corresponding digits
            int num2 = int.Parse(b.Substring(i, 1));
            if (num1 - carry < num2)
            {
                num1 += + 10;//borrows 10 from the next higher digit by adding 10
                nextCarry = true;
            }
            res = (num1 - num2 - carry);
            result = result.Insert(0, res.ToString());//digits are added in the correct order
            if (nextCarry)//borrow occurred in the subtraction
                carry = 1;
            else
                carry = 0;
        }
        result = SanitizeResult(result);
        if (resultNegative)
            return result.Insert(0, "-");//remove leading zeros 
        return result;
    }

    static bool StringIsSmaller(string a, string b)
    {
        if (a.Length < b.Length)
            return true;
        if (a.Length > b.Length)
            return false;
        //proceeds to compare the digits of both numbers to determine which one is smaller
        char[] arrayA = a.ToCharArray();
        char[] arrayB = b.ToCharArray();
        //comparing corresponding letters
        for (int i = 0; i < arrayA.Length; i++)
        {
            if (arrayA[i] < arrayB[i])
                return true;
            if (arrayA[i] > arrayB[i])
                return false;
        }
        return false;//are equal
    }

    static void Swap(ref string a, ref string b)
    {
        string temp = a;//temporary string
        //swappping
        a = b;
        b = temp;
    }

    static string SanitizeResult(string result)
    {
        result = result.TrimStart(new char[] { '0' });//removes zeros
        if (result.Length == 0)//result after removing zeros is zero
            result = "0";
        return result;
    }
}
