namespace Conquer_the_second;

public class BigInteger
{
    private int[] _numbers;
    
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
                }
            }}
        public override string ToString()
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
            while (firstArrayLenght != -1 && secondArrayLenght != -1)
            {
                if (firstArrayLenght >= secondArrayLenght)
                {
                    var adding = _numbers[firstArrayLenght] + another._numbers[secondArrayLenght];
                    if (adding < 10)
                    {
                        _numbers[firstArrayLenght] = adding;
                    }
                    else
                    {
                        var first = adding.ToString()[0];
                        var second = adding.ToString()[1];

                        if (firstArrayLenght - 1 == -1)
                        {
                            int[] temporaryArray = new int[firstLenghtE];
                            int n = firstLenghtE;
                            foreach (var dig in _numbers)
                            {
                                temporaryArray[n - 1] = _numbers[n - 2];
                                n--;
                            }
                            _numbers = temporaryArray;
                            firstArrayLenght++;
                        }

                        _numbers[firstArrayLenght - 1] = Int32.Parse(first.ToString()) + _numbers[firstArrayLenght - 1];
                        _numbers[firstArrayLenght] = Int32.Parse(second.ToString());
                    }
                }

                if (firstArrayLenght < secondArrayLenght)
                {
                    var addition = _numbers[firstArrayLenght] + another._numbers[secondArrayLenght];
                    if (addition < 10)
                    {
                        another._numbers[secondArrayLenght] = addition;
                    }
                    else
                    {
                        var first = addition.ToString()[0];
                        var second = addition.ToString()[1];
                        if (secondArrayLenght - 1 == -1)
                        {
                            int[] tempArray = new int[secondLenghtE];
                            int n = secondLenghtE;

                            foreach (var dig in _numbers)
                            {
                                tempArray[n - 1] = _numbers[n - 2];
                                n--;
                            }

                            another._numbers = tempArray;
                            secondLenghtE++;

                        }
                        another._numbers[secondArrayLenght - 1] = Int32.Parse(first.ToString()) + another._numbers[secondArrayLenght - 1];
                        another._numbers[secondArrayLenght] = Int32.Parse(second.ToString());
                    }
                }
                firstArrayLenght--;
                secondArrayLenght--;
            }

            return firstArrayLenght >= secondArrayLenght ? new BigInteger(string.Join("", _numbers)) : another;
        }
        
        public BigInteger Sub(BigInteger another)
        {
            // return new BigInteger, result of current - another
            
            
            return (null); //   !!!just for my checking | you can erase it!!!
        }
        
        
        
}