using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalaizerClass
{
    public class AnalaizerClass
    {
        //private static int erposition = 0;

        public string expression = "";

        //public static bool ShowMessage = true;

        public string lastError { get; private set; }

        public AnalaizerClass(string expression)
        {
            this.expression = expression;
            lastError = "";
        }

        public void CheckCurrency()
        {
            if(expression.Length>65536)
            {
                lastError = "Error 07: Too long expression(>65536)!";
                throw new Exception(lastError);
            }
            CheckBrackets();
            CheckSymbols();
        }

        public void CheckBrackets()
        {
            int left = 0;
            int right = 0;
            for (int i = 0; i < expression.Length; i++)
            {
                char current = expression[i];
                if (current == '(') left++;
                if (current == ')') right++;
                if (right > left)
                {
                    lastError = "Error 01: at " + (i + 1) + " : Wrong brackets structure!";
                    throw new Exception(lastError);
                }
                if (left - right > 3)
                {
                    lastError = "Error 01: at " + (i + 1) + " : Wrong brackets structure!";
                    throw new Exception(lastError);
                }
            }
            if (left != right)
            {
                lastError = "Error 03: Incorrect syntax!";
                throw new Exception(lastError);
            }
        }

        public void CheckSymbols()
        {
            for (int i = 0; i < expression.Length; i++)
            {
                char current = expression[i];
                if (Char.IsDigit(current) || Char.IsWhiteSpace(current) || current == '+' ||
                    current == '-' || current == '*' || current == '/' || current == '(' ||
                    current == ')' || current == 'm' || current == 'p')
                {
                    if ((current == 'm') && (i < expression.Length - 2) &&
                        (expression[i + 1] == 'o') && (expression[i + 2] == 'd')) i += 2;
                }
                else
                {
                    lastError = "Error 02: at " + (i + 1) + " : Unknown operator!";
                    throw new Exception(lastError);
                }
            }
        }
        public string ExpSpaces()
        {
            string expression_with_spaces = "";
            for (int i = 0; i < expression.Length; i++)
            {
                char current = expression[i];

                //if(Char.IsWhiteSpace(current))
                //{
                //    if(!expression_with_spaces.EndsWith(" "))
                //    {
                //        expression_with_spaces += ' ';
                //    }
                //    continue;
                //}

                if(current == '+' || current == '-' || current == '*' ||
                    current == '/')
                {
                    if((i+1<expression.Length)&& (expression[i+1] == '+' || expression[i + 1] == '-' ||
                        expression[i + 1] == '*' || expression[i + 1] == '/'))
                    {
                        lastError = "Error 04: at " + (i + 1) + " : Two operators in a row!";
                        throw new Exception(lastError);
                    }
                    if(i+1>=expression.Length)
                    {
                        lastError = "Error 05: Not finished expression!";
                        throw new Exception(lastError);
                    }
                    if (!expression_with_spaces.EndsWith(" "))
                    {
                        expression_with_spaces += ' ';
                    }
                    expression_with_spaces += current;
                    expression_with_spaces += ' ';
                    continue;
                }

                if(current=='(' || current==')')
                {
                    if (!expression_with_spaces.EndsWith(" "))
                    {
                        expression_with_spaces += ' ';
                    }
                    expression_with_spaces += current;
                    expression_with_spaces += ' ';
                    continue;
                }

                if(current=='m')
                {
                    if(i+1>=expression.Length)
                    {
                        lastError = "Error 05: Not finished expression!";
                        throw new Exception(lastError);
                    }
                    if(!Char.IsDigit(expression[i+1])&&(expression[i+1]!='o'))
                    {
                        lastError = "Error 03: Incorrect syntax!";
                        throw new Exception(lastError);
                    }
                    if(Char.IsDigit(expression[i+1]))
                    {
                        if (!expression_with_spaces.EndsWith(" "))
                        {
                            expression_with_spaces += ' ';
                        }
                        expression_with_spaces += '-';
                    }
                    if(expression[i+1]=='o'&&expression[i+2]=='d')
                    {
                        if (!expression_with_spaces.EndsWith(" "))
                        {
                            expression_with_spaces += ' ';
                        }
                        expression_with_spaces += "mod";
                        expression_with_spaces += ' ';
                        i += 2;
                    }
                    continue;
                }

                if(current=='p')
                {
                    if (i + 1 >= expression.Length)
                    {
                        lastError = "Error 05: Not finished expression!";
                        throw new Exception(lastError);
                    }
                    if (!Char.IsDigit(expression[i + 1]))
                    {
                        lastError = "Error 03: Incorrect syntax!";
                        throw new Exception(lastError);
                    }
                    continue;
                }

                while(true)
                {
                    expression_with_spaces += expression[i];
                    if ((i + 1 < expression.Length) && Char.IsDigit(expression[i + 1]))
                    {
                        i++;
                    }
                    else
                    {
                        if (!expression_with_spaces.EndsWith(" "))
                        {
                            expression_with_spaces += ' ';
                        }
                        break;
                    }
                }
            }

            if (expression_with_spaces.EndsWith(" "))
            {
                expression_with_spaces = expression_with_spaces.Remove(expression_with_spaces.Length - 1);
            }
            return expression_with_spaces;
        }

        public string Format()
        {
            string expression_with_spaces = ExpSpaces();
            string[] elements = expression_with_spaces.Split(' ');
            if (elements.Length > 30)
            {
                lastError = "Error 08: The amount of numbers and operators is more than 30!";
                throw new Exception(lastError);
            }
            for (int i = 0; i < elements.Length; i++)
            {
                string current = elements[i];
                if(current=="mod")
                {
                    if(i==0)
                    {
                        lastError = "Error 03: Incorrect syntax!";
                        throw new Exception(lastError);
                    }
                    else if(i+1>=elements.Length)
                    {
                        lastError = "Error 05: Not finished expression!";
                        throw new Exception(lastError);
                    }
                    else
                    {
                        if (!Int64.TryParse(elements[i + 1], out _) && (elements[i + 1][0] !='('))
                        {
                            lastError = "Error 03: Incorrect syntax!";
                            throw new Exception(lastError);
                        }
                    }
                    continue;
                }

                if(Int64.TryParse(current, out Int64 n))
                {
                    if((n >= -2147483648) && (n <= 2147483647))
                    {
                        if ((i + 1 < elements.Length) && (elements[i + 1][0] == '('))
                        {
                            lastError = "Error 03: Incorrect syntax!";
                            throw new Exception(lastError);
                        }
                        if ((i + 1 < elements.Length) && (Int64.TryParse(elements[i+1], out _)))
                        {
                            lastError = "Error 03: Incorrect syntax!";
                            throw new Exception(lastError);
                        }
                    }
                    else
                    {
                        lastError = "Error 06: Too small or too big value for int!";
                        throw new Exception(lastError);
                    }
                    continue;
                }

                if(current[0]=='(')
                {
                    if(!(Int64.TryParse(elements[i+1], out _))&&(elements[i+1][0]!='('))
                    {
                        lastError = "Error 03: Incorrect syntax!";
                        throw new Exception(lastError);
                    }
                }
                if(current[0]==')')
                {
                    if((i+1<elements.Length)&&(
                        (Int64.TryParse(elements[i + 1], out _)) ||
                        (elements[i + 1][0] == '('))
                        )
                    {
                        lastError = "Error 03: Incorrect syntax!";
                        throw new Exception(lastError);
                    }
                    continue;
                }
                if(current[0]=='+'|| current[0] == '-' || current[0] == '*' || current[0] == '/')
                {
                    if(i+1>=elements.Length)
                    {
                        lastError = "Error 05: Not finished expression!";
                        throw new Exception(lastError);
                    }
                    else
                    {
                        if(!Int64.TryParse(elements[i+1], out _)&& (elements[i + 1][0] != '('))
                        {
                            lastError = "Error 03: Incorrect syntax!";
                            throw new Exception(lastError);
                        }
                    }
                }
            }
            return expression_with_spaces;
        }

        public bool Oper (char ch)
        {
            string operand = "+-/*m()";
            //for (int i = 0; i < operand.Length; i++)
            //{
            //    if (ch == operand[i])
            //    {
            //        return true;
            //    }
            //}
            //return false;
            return operand.Contains(ch);
        }

        private byte orderOfOperations(string line)
        {
            switch (line[0])
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 3;
                case '*': return 4;
                case '/': return 4;
                case 'm': return 5;
                default: return 6;
            }
        }

        public string PolishInverse (string str)
        {
            string[] temp = str.Split(' ');
            string output = "";
            Stack<string> stack = new Stack<string>();
            for (int i = 0; i < temp.Length; i++)
            {
                if(Int64.TryParse(temp[i], out _))
                {
                    output += temp[i] + ' ';
                    continue;
                }
                if(Oper(temp[i][0]))
                {
                    if(temp[i][0] == '(')
                    {
                        stack.Push(temp[i]);
                    }
                    else if(temp[i][0] == ')')
                    {
                        string s = stack.Pop();
                        while(s[0] != '(')
                        {
                            output += s + ' ';
                            s = stack.Pop();
                        }
                    }
                    else
                    {
                        if(stack.Count > 0)
                        {
                            if(orderOfOperations(stack.Peek()) >= orderOfOperations(temp[i]))
                            {
                                output += stack.Pop() + ' ';
                            }
                        }
                        stack.Push(temp[i]);
                    }
                }
            }
            while(stack.Count > 0)
            {
                output += stack.Pop() + ' ';
            }
            if (output.EndsWith(" "))
            {
                output = output.Remove(output.Length - 1);
            }
            if (output.StartsWith(" "))
            {
                output = output.Remove(0, 1);
            }
            return output;
        }
        public string RunEstimate(string str)
        {
            Int64 result = 0;
            string[] temp = str.Split(' ');
            Stack<Int64> stack = new Stack<Int64>();
            for (int i = 0; i < temp.Length; i++)
            {
                if(Int64.TryParse(temp[i], out Int64 num))
                {
                    stack.Push(num);
                }
                else if(Oper(temp[i][0]))
                {
                    Int64 right = stack.Pop();
                    Int64 left = stack.Pop();
                    switch (temp[i][0])
                    {
                        case '+': result = Cc.CalcClass.Add(left, right);
                            break;
                        case '-': result = Cc.CalcClass.Sub(left, right);
                            break;
                        case '*':
                            result = Cc.CalcClass.Mult(left, right);
                            break;
                        case '/':
                            if(right == 0)
                            {
                                lastError = "Error 09: Division by zero!";
                                throw new Exception(lastError);
                            }
                            result = Cc.CalcClass.Div(left, right);
                            break;
                        case 'm': result = Cc.CalcClass.Mod(left, right);
                            break;
                    }
                    stack.Push(result);
                }
            }
            return stack.Peek().ToString();
        }
        public string Estimate()
        {
            CheckCurrency();
            string str = Format();
            string polska = PolishInverse(str);
            return RunEstimate(polska);
        }
    }
}
