using System;
using System.Globalization;
using Stack = Additional_for_Conquer.Stack;
using Queue = Additional_for_Conquer.Queue;
//1!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


namespace Additional_for_Conquer
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter expression: ");
                var input = Console.ReadLine();
                var tokens = Token(input);
                var postfixTokens = InfixToPostfix(tokens);
                var result = Calculate(postfixTokens);
                Console.Write("Result: ");
                Console.WriteLine(result);
                Console.Write("Calculate again? [Yes/No]: ");
                var input2 = Console.ReadLine();
                if (input2.Equals("No", StringComparison.OrdinalIgnoreCase))
                    break;
            }
        }

        static Queue<string> Token(string expression)//belongs to the class itself rather than an instance of the class
        {
            var tokenized = new Queue<string>();
            var buffer = new Queue<string>();
            for (int i = 0; i < expression.Length; i++)
            {
                if (char.IsDigit(expression[i]) || expression[i] == ',' || expression[i] == '.')
                {
                    buffer.Enqueue(expression[i].ToString());
                }
                else if (expression[i] == ' ')
                {
                    if (buffer.Count > 0)
                        tokenized.Enqueue(combineNumber(buffer));//characters in buffer combined into one number
                }
                else if (expression[i] == '(')
                {
                    tokenized.Enqueue(expression[i].ToString()); //enqueued directly into the tokenized queue
                }
                else
                {
                    if (buffer.Count > 0)//the closing parenthesis
                        tokenized.Enqueue(combineNumber(buffer));//combined into a single number and enqueued into the tokenized queue
                    tokenized.Enqueue(expression[i].ToString());//the operator  is enqueued as a separate token into the tokenized queue
                }
            }

            if (buffer.Count > 0)
                tokenized.Enqueue(combineNumber(buffer));
            return tokenized;//queue containing all the tokens from the expression is returned as the result 
        }

        static Queue<string> InfixToPostfix(Queue<string> tokenized)
        {
            var operatorStack = new Stack<string>();
            var output = new Queue<string>();//the resulting postfix tokens
            while (tokenized.Count > 0)
            {
                var token = tokenized.Dequeue();
                bool checkIfNumber = long.TryParse(token,  out _);
                if (checkIfNumber)
                {
                    output.Enqueue(token);//enqueued directly into the output
                }
                else if (isOperator(token))
                {
                    while (operatorStack.Count > 0 && (checkPriority(operatorStack.Peek()) > checkPriority(token) ||
                           (checkPriority(operatorStack.Peek()) == checkPriority(token) && token != "^")))
                    {
                        if (operatorStack.Peek() != "(")
                            output.Enqueue(operatorStack.Pop());
                        else
                            break;
                    }

                    operatorStack.Push(token);//to maintain the order of precedence
                }
                else if (token == "(")
                {
                    operatorStack.Push(token);
                }
                else if (token == ")")
                {
                    while (operatorStack.Peek() != "(")
                    {
                        output.Enqueue(operatorStack.Pop());//dequeues operators from the operatorStack and enqueues them
                                                            //into the output queue until an opening parenthesis is encountered
                    }

                    operatorStack.Pop();
                }
            }

            while (operatorStack.Count > 0)//check for remaining operators
            {
                output.Enqueue(operatorStack.Pop());
            }

            return output;//tokens in postfix
        }

        static string Calculate(Queue<string> postfixTokenList)
        {
            var buffer = new Stack<string>();// for intermediate results
            while (postfixTokenList.Count > 0)
            {
                var token = postfixTokenList.Dequeue();
                if (long.TryParse(token, out _))//its number(
                {
                    buffer.Push(token);//since an operand un the expression
                }
                else//not a number -> operator
                {//pops 2 elements from buffer, repr. the operands for the operator
                    var secondNum = buffer.Pop();
                    var firstNum = buffer.Pop();
                    //calculation + pushed back to the buffer
                    buffer.Push(PerformOperation(firstNum, secondNum, token));
                }
            }

            var result = buffer.Pop();//popped + converted to a string
            return result;
        }

        static int checkPriority(string operation)
        {
            if (operation == "+" || operation == "-")
            {
                return 0;
            }
            else if (operation == "*" || operation == "/")
            {
                return 1;
            }
            else return 2;
        }

        static bool isOperator(string oper)
        {
            if (oper == "+" || oper == "-" || oper == "/" || oper == "*" || oper == "^")
                return true;
            return false;
        }

        static string combineNumber(Queue<string> queue)
        {
            string result = "";
            while (queue.Count > 0)
            {
                result += queue.Dequeue();
            }

            return result;
        }

        static string PerformOperation(string firstNum, string secondNum, string oper)
        {
            long result = 0;
            switch (oper)
            {
                case "+":
                    result = Add(firstNum, secondNum);
                    break;
                case "-":
                    result = Subtract(firstNum, secondNum);
                    break;
                case "/":
                    result = Divide(firstNum, secondNum);
                    break;
                case "*":
                    result = Multiply(firstNum, secondNum);
                    break;
                case "^":
                    result = Power(firstNum, secondNum);
                    break;
                default:
                    throw new Exception("Illegal operation");
            }

            return result.ToString();
        }

        static long Add(string firstNum, string secondNum)
        {
            long a = long.Parse(firstNum);
            long b = long.Parse(secondNum);
            return a + b;
        }

        static long Subtract(string firstNum, string secondNum)
        {
            long a = long.Parse(firstNum);
            long b = long.Parse(secondNum);
            return a - b;
        }

        static long Multiply(string firstNum, string secondNum)
        {
            long a = long.Parse(firstNum);
            long b = long.Parse(secondNum);
            return KaratsubaMultiplication(a, b);
        }

        static long KaratsubaMultiplication(long x, long y)
        {
            if (x < 10 || y < 10)
            {
                return x * y;
            }
            //splitting into two halves
            int maxLength = Math.Max(x.ToString().Length, y.ToString().Length);
            int halfCutMaxLength = maxLength / 2;

            long a = x / (long)Math.Pow(10, halfCutMaxLength);//higher digits
            long b = x % (long)Math.Pow(10, halfCutMaxLength);//lower digits
            long c = y / (long)Math.Pow(10, halfCutMaxLength);//same
            long d = y % (long)Math.Pow(10, halfCutMaxLength);//same
            
            long bd = KaratsubaMultiplication(b, d);
            long ab_cd = KaratsubaMultiplication((b + a), (d + c));
            long ac2 = KaratsubaMultiplication(a, c);

            return (ac2 * (long)Math.Pow(10, halfCutMaxLength * 2)) + ((ab_cd - ac2 - bd) * (long)Math.Pow(10, halfCutMaxLength)) + bd;
        }//a*c -> multiplied by 10 raised to the power of halfMaxLength multiplied by 2 
        //(ab_cd - ac2 - bd) * (long)Math.Pow(10, halfCutMaxLength) ->  recursive multiplication of the sum of lower and higher digits
        static long Divide(string firstNum, string secondNum)
        {
            long a = long.Parse(firstNum);
            long b = long.Parse(secondNum);
            return a / b;
        }

        static long Power(string firstNum, string secondNum)
        {
            long a = long.Parse(firstNum);
            long b = long.Parse(secondNum);
            return (long)Math.Pow(a, b);
        }
    }
}
