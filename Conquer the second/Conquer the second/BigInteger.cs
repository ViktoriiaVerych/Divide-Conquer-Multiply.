namespace Conquer_the_second;

public class BigInteger
{
    //constructor
    private int[] _numbers; //array of integers, stores individual digits of the big integer
    public static BigInteger operator +(BigInteger a, BigInteger b) => a.Add(b);
    public static BigInteger operator -(BigInteger a, BigInteger b) => a.Sub(b);
  
    public BigInteger(string value)
    {
        int n = 0;
        if (value != null)
        {
            _numbers = new int[value.Length];
            foreach (var dig in value)
            {
                _numbers[n] = Int32.Parse(dig.ToString());
                n++;
                //takes a string _value_
                //initilization _numbers array with converting each character
                //of the input string into an integer and storing in the array
            }
        }

    }

    public override string ToString() //overrides the default to provide a string of the BigInteger,
        //adds all digit in the _numbers array to form a normal number in string representation

    {
        var num = "";
        foreach (var dig in _numbers)
        {
            num += dig;
        }

        return num;
    }

    public BigInteger Add(BigInteger another)
    {
        int firstLenghtE = _numbers.Length + 1;
        int secondLenghtE = another._numbers.Length + 1;
        int firstArrayLenght = _numbers.Length - 1;
        int secondArrayLenght = another._numbers.Length - 1;
        while (firstArrayLenght != -1 && secondArrayLenght != -1) //indicating that all digits have been processed
        {
            if (firstArrayLenght >=
                secondArrayLenght) //checks if the index of the last digit in the _numbers array of the current is >= to the index of the last digit in the _numbers  of the another BigInteger
            {
                var adding =
                    _numbers[firstArrayLenght] + another._numbers[secondArrayLenght]; //adds corresponding digits
                if (adding < 10) //chekcks if the sum of the digits is less than 10
                {
                    _numbers[firstArrayLenght] =
                        adding; //updates the digits in the _numbers array of the current BigInteger with the sum
                }
                else //if the sum >=10
                {
                    var first = adding
                        .ToString()[0]; //assigns it the first character of the string representation of the adding sum
                    var second = adding.ToString()[1]; //assigns it the second character

                    if (firstArrayLenght - 1 ==
                        -1) //checks if the carryover extends beyond the current _numbers array, a new digit needs to be added
                    {
                        int[] temporaryArray = new int[firstLenghtE];
                        int n = firstLenghtE;
                        foreach (var dig in _numbers)
                        {
                            temporaryArray[n - 1] =
                                _numbers[n - 2]; //copies each digit from the _numbers array to the temporaryArray,
                            n--; //shifting them one position to the right

                        }

                        _numbers = temporaryArray; //assigns the temporaryArray back to the _numbers array
                        firstArrayLenght++;
                    }

                    _numbers[firstArrayLenght - 1] =
                        Int32.Parse(first.ToString()) +
                        _numbers[firstArrayLenght - 1]; //adds digits in the _numbers array
                    _numbers[firstArrayLenght] = Int32.Parse(second.ToString()); //assigns to the position in the array
                }
            }

            if (firstArrayLenght <
                secondArrayLenght) //checks if the index of the last digit in the _numbers array of the current BigInteger is less
                //than the index of the last digit in the _numbers array of the another BigInteger 
            {
                var addition =
                    _numbers[firstArrayLenght] + another._numbers[secondArrayLenght]; //adds corresponding digits
                if (addition < 10)
                {
                    another._numbers[secondArrayLenght] =
                        addition; //updates the digit in the _numbers array of the another BigInteger object with the sum
                }
                else
                {
                    var first = addition
                        .ToString()[
                            0]; //assigns it the first character of the string representation of the addition sum | carryover digit
                    var second =
                        addition.ToString()[
                            1]; //assigns it the second character | represents the digit to be updated in the _numbers array
                    if (secondArrayLenght - 1 == -1) //checks if the carryover extends beyond the _numbers array
                    {
                        int[] tempArray = new int[secondLenghtE];
                        int n = secondLenghtE;

                        foreach (var dig in _numbers)
                        {
                            tempArray[n - 1] =
                                _numbers
                                    [n - 2]; //copies each digit from the _numbers array to the tempArray, shifting them one position to the right
                            n--;
                        }

                        another._numbers =
                            tempArray; //assigns the tempArray back to the _numbers array of the another BigInteger
                        secondLenghtE++;

                    }

                    another._numbers[secondArrayLenght - 1] =
                        Int32.Parse(first.ToString()) + another._numbers[secondArrayLenght - 1];
                    //adds the carryover digit to the digit at secondArrayLenght - 1 in the _numbers array of the another BigInteger
                    another._numbers[secondArrayLenght] = Int32.Parse(second.ToString());
                    //assigns the digit represented by second to the position at secondArrayLenght in the _numbers array of the another
                }
            }

            firstArrayLenght--;
            secondArrayLenght--; //decrement to move to the next digit from right to left
        }

        return firstArrayLenght >= secondArrayLenght ? new BigInteger(string.Join("", _numbers)) : another;
        //returns a new BigInteger if the current BigInteger has more digits than the another object
        //otherwise, it returns the another itself.
    }

    public BigInteger Sub(BigInteger another)
    {
        int firstLengthE = _numbers.Length + 1; // Довжина першого масиву з додатковим розрядом
        int secondLengthE = another._numbers.Length + 1; // Довжина другого масиву з додатковим розрядом
        int firstArrayLength = _numbers.Length - 1; // Довжина першого масиву без додаткового розряду
        int secondArrayLength = another._numbers.Length - 1; // Довжина другого масиву без додаткового розряду
        bool borrow = false; // Флаг позикового додатку (позичення)

        while (firstArrayLength != -1 && secondArrayLength != -1)
        {
            if (firstArrayLength >= secondArrayLength)
            {
                int difference = _numbers[firstArrayLength] -
                                 (borrow
                                     ? another._numbers[secondArrayLength] + 1
                                     : another._numbers[secondArrayLength]);
                // Віднімаємо відповідні розряди з можливим позиченням

                if (difference >= 0)
                {
                    _numbers[firstArrayLength] = difference; // Записуємо різницю в перший масив
                    borrow = false; // Встановлюємо флаг позикового додатку в false
                }
                else
                {
                    _numbers[firstArrayLength] = difference + 10; // Записуємо різницю + 10 в перший масив
                    borrow = true; // Встановлюємо флаг позикового додатку в true
                }
            }
            else
            {
                int difference = (borrow ? _numbers[firstArrayLength] - 1 : _numbers[firstArrayLength]) -
                                 another._numbers[secondArrayLength];
                // Віднімаємо відповідні розряди з можливим позиченням

                if (difference >= 0)
                {
                    another._numbers[secondArrayLength] = difference; // Записуємо різницю в другий масив
                    borrow = false; // Встановлюємо флаг позикового додатку в false
                }
                else
                {
                    another._numbers[secondArrayLength] = difference + 10; // Записуємо різницю + 10 в другий масив
                    borrow = true; // Встановлюємо флаг позикового додатку в true
                }
            }

            firstArrayLength--; // Зменшуємо індекс першого розряду
            secondArrayLength--; // Зменшуємо індекс другого розряду
        }

        return firstArrayLength >= secondArrayLength ? new BigInteger(string.Join("", _numbers)) : another;
        // Повертаємо новий BigInteger, якщо перший масив має більшу довжину, інакше повертаємо another.
    }


    public BigInteger Multiplication(BigInteger second)
    {
        var firstArrayLength = _numbers.Length;
        var secondArrayLength = second._numbers.Length;
        var a = Int32.Parse(string.Join("", _numbers[0], _numbers[1]));

        var b = Int32.Parse(string.Join("", _numbers[2], _numbers[3]));
        var c = Int32.Parse(string.Join("", second._numbers[0], second._numbers[1]));
        var d = Int32.Parse(string.Join("", second._numbers[2], second._numbers[3]));
        var x = Math.Pow(10, firstArrayLength / 2) * a + b;
        var y = Math.Pow(10, secondArrayLength / 2) * c + d;
        var res = x * y;
        return new BigInteger(res.ToString());
    }

    
    
    
}




            
                