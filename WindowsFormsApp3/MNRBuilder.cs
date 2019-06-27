using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{

    class MNRProgram
    {
        public List<string> Prog { get; private set; }
        public int NumberOfComands { get; private set; }
        public String[] Register = new String[100];
        public int NumberVal=0;
 
        public MNRProgram(int var)
        {
            NumberOfComands = 0;
            Prog = new List<string>();
            if (var > 1) Register[0] = "x";
            if (var > 2) Register[1] = "y";
            if (var > 3) Register[2] = "z";
            
        }

        public void J(int n, int m, int k)
        {
            NumberOfComands++;
            string comand = NumberOfComands.ToString() + ". J(" + n.ToString() + "," + m.ToString() + "," + k.ToString() + ")";
            Prog.Add(comand);
        }
        public void Z(int n)
        {
            NumberOfComands++;
            string comand = NumberOfComands.ToString() + ". Z(" + n.ToString() + ")";
            Prog.Add(comand);
        }
        public void S(int n)
        {
            NumberOfComands++;
            string comand = NumberOfComands.ToString() + ". S(" + n.ToString() + ")";
            Prog.Add(comand);
        }

        public void T(int n, int m)
        {
            NumberOfComands++;
            string comand = NumberOfComands.ToString() + ". T(" + n.ToString() + "," + m.ToString() + ")";
            Prog.Add(comand);
        }
        
        public void PrinReg()
        {
            string regist = "| ";
            int i = 0;
            while (Register[i] != null)
            {
                regist += i + ". " + Register[i] + " | ";
                i++;
            }
            Prog.Add(" ");
            Prog.Add("Стан регістрів на даний момент");
            Prog.Add(regist);
            Prog.Add(" ");

        }
        public string GetProgram()
        {
            string result = "";
            if (Prog.Count == 0)
            {
                for (int i = 0; i < NumberVal; i++)
                {
                    S(0);
                }
            }
            else
            {
                int i = 0;
                while (Register[i] != null)
                {
                    i++;
                };
                T(i - 1, 0);
            }
            foreach (string com in Prog)
            {
                result += com + Environment.NewLine;
            }
            return result;
        }
    }

    class Value
    {
        public int index;
        public bool isIndex;
        public int val;
        public bool isVal;
        public int get
        {
            get
            {
                if (isVal) return val;
                return index;
            }
        }
        public Value(int i, string type)
        {
            if (type == "index")
            {
                index = i;
                isIndex = true;
                isVal = false;

            }
            else
            {
                val = i;
                isVal = true;
                isIndex = false;
            }
        }

    }


    class MNRBuilder
    {
        String[] Register;
        int First_free;
        MNRProgram Program ;

        public MNRBuilder(MNRProgram Prog, int var)
        {

            First_free = var;
            Program = Prog;
            Register = Prog.Register;
        }
        private string get(int x) { return x.ToString(); }
        private string get(Value x)
        {
            if (x.isIndex) return Register[x.index];
            return x.ToString();
        }

        public Value Value(int val)
        {
            Program.NumberVal = val;
            return new Value(val, "value");
        }

        public Value Sig(Value x)
        {
            string res_string = "sig(" + Register[x.get] + ")";
            int res_index = Array.IndexOf(Register, res_string);
            if (res_index != -1)
            {
                //якщо вже було підраховано
                return new Value(res_index, "index");
            }
            else
            {
                if (x.isVal)
                {
                    if (x.val == 0) { Program.NumberVal = 0; return new Value(0, "val"); }
                    Program.NumberVal = 1;
                    return new Value(1, "val");
                }
                else
                {
                    Program.J(x.index, First_free, Program.NumberOfComands + 3);
                    Program.S(First_free);
                    Register[First_free] = res_string;
                    Program.PrinReg();
                    First_free += 1;
                    return new Value(First_free - 1, "index");
                }
            }
        }


        public Value Nsig(Value x)  
        {
            string res_string = "nsig(" + Register[x.get] + ")";
            int res_index = Array.IndexOf(Register, res_string);
            if (res_index != -1)
            {
                //якщо вже було підраховано
                return new Value(res_index, "index");
            }
            else
            {
                //якщо ще не пораховано
                if (x.isVal)
                {
                    if (x.val == 0) { Program.NumberVal = 1; return new Value(1, "val"); }
                    Program.NumberVal = 0;  
                    return new Value(0, "val"); ;
                }
                else
                {
                    Program.J(x.index, First_free, Program.NumberOfComands + 3);
                    Program.J(0, 0, Program.NumberOfComands + 3);
                    Program.S(First_free);
                    Register[First_free] = res_string;
                    Program.PrinReg();
                    First_free += 1;
                    return new Value(First_free - 1, "index");
                }
            }
        }


        public Value Inc(Value x)
        {
            string res_string = "inc(" + Register[x.get] + ")";
            int res_index = Array.IndexOf(Register, res_string);
            if (res_index != -1)
            {
                //якщо вже було підраховано
                return new Value(res_index, "index");
            }
            else
            {
                //якщо ще не пораховано
                if (x.isVal)
                {
                    Program.NumberVal = x.val + 1;
                    return new Value(x.val + 1, "val"); ;
                }
                else
                {
                    Program.T(x.index, First_free);
                    Program.S(First_free);
                    Register[First_free] = res_string;
                    Program.PrinReg();
                    First_free += 1;
                    return new Value(First_free - 1, "index");
                }
            }
        }


        public Value Dec(Value x)
        {
            string res_string = "dec(" + Register[x.get] + ")";
            int res_index = Array.IndexOf(Register, res_string);
            if (res_index != -1)
            {
                //якщо вже було підраховано
                return new Value(res_index, "index");
            }
            else
            {
                //якщо ще не пораховано
                if (x.isVal)
                {
                    if (x.val > 0) { Program.NumberVal = x.val - 1; return new Value(x.val - 1, "val"); }
                    Program.NumberVal = 0;
                    return new Value(0, "val"); ;
                }
                else
                {
                    Program.S(First_free + 1);
                    Program.J(x.index, First_free + 1, Program.NumberOfComands + 4);
                    Program.S(First_free);
                    Program.S(First_free + 1);
                    Program.J(0, 0, Program.NumberOfComands - 3);
                    Program.Z(First_free + 1);
                    Register[First_free] = res_string;
                    Program.PrinReg();
                    First_free += 1;
                    return new Value(First_free - 1, "index");
                }
            }
        }


        public Value Sum(Value x, Value y)
        {
            string res_string = get(x) + "+"+get(y);
            int res_index = Array.IndexOf(Register, res_string);
            if (res_index != -1)
            {
                return new Value(res_index, "index");
            }
            else
            {
                res_string = get(y) + "+" + get(x);
                res_index = Array.IndexOf(Register, res_string);
                if(res_index != -1)
                {
                    return new Value(res_index, "index");
                }
                else
                {
                    if (x.isVal && y.isVal) return Sum(x.val, y.val);
                    if (x.isVal && y.isIndex) return Sum(y, x.val);
                    if (x.isIndex && y.isVal) return Sum(x, y.val);
                    if (x.isIndex && y.isIndex)
                    {
                        Program.T(x.index, First_free);
                        Program.J(y.index, First_free + 1, Program.NumberOfComands + 5);
                        Program.S(First_free);
                        Program.S(First_free + 1);
                        Program.J(0, 0, Program.NumberOfComands - 2);
                        Program.Z(First_free + 1);
                        Register[First_free] = res_string;
                        Program.PrinReg();
                        First_free++;
                        return new Value(First_free - 1, "index");
                    }
                }
            }
            return x;
        }
        public Value Sum(Value x, int y)
        {

            Program.T(x.index, First_free);
            for (int i=0; i<y; i++)
            {
                Program.S(First_free);
            }
            Register[First_free] = Register[x.get] + "+" +y ;
            Program.PrinReg();
            First_free++;
            return new Value(First_free - 1, "index");
        }
        public Value Sum(int x, int y) { Program.NumberVal = x + y; return new Value(x + y, "val"); }



        public Value Sub(Value x, Value y)
        {
            string res_string = get(x) + "-" + get(y);
            int res_index = Array.IndexOf(Register, res_string);
            if (res_index != -1)
            {
                return new Value(res_index, "index");
            }
            else
            {

                if (x.isVal && y.isVal) return Sub(x.val, y.val);
                if (x.isVal && y.isIndex) return Sub(x.val, y);
                if (x.isIndex && y.isVal) return Sub(x, y.val);
                if (x.isIndex && y.isIndex)
                {
                    Program.T(y.index, First_free+1);
                    Program.J(x.index, First_free+1, Program.NumberOfComands + 5);
                    Program.S(First_free+1);
                    Program.S(First_free);
                    Program.J(0, 0, Program.NumberOfComands - 2);
                    Program.Z(First_free + 1);
                    Register[First_free] = res_string;
                    Program.PrinReg();
                    First_free++;
                    return new Value(First_free - 1, "index");

                }
            }
            return x;
        }

        public Value Sub(Value x, int y)
        {

            for (int i = 0; i < y; i++)
            {
                Program.S(First_free+1);
            }
            Program.J(x.index, First_free + 1, Program.NumberOfComands+ 5);
            Program.S(First_free);
            Program.S(First_free + 1);
            Program.J(0, 0, Program.NumberOfComands - 2);
            Program.Z(First_free + 1);
            Register[First_free] =  Register[x.get]+ "-" +y ;
            Program.PrinReg();
            First_free++;
            return new Value(First_free - 1, "index");
        }
        public Value Sub(int x, Value y)
        {

            for (int i = 0; i < x; i++)
            {
                Program.S(First_free + 1);
            }
            Program.J(y.index, First_free + 1, Program.NumberOfComands + 5);
            Program.S(First_free);
            Program.S(First_free + 1);
            Program.J(0, 0, Program.NumberOfComands - 2);
            Program.Z(First_free + 1);
            Register[First_free] = get(x) + "-" + get(y);
            Program.PrinReg();
            First_free++;
            return new Value(First_free - 1, "index");
        }
        public Value Sub(int x, int y) {
            if (x < y)
            {
                Program.J(0, 0, Program.NumberOfComands + 1);
                return new Value(0, "val");
            }
            Program.NumberVal = x - y;
            return new Value(x - y, "val");
        }



        public Value SemiSub(Value x, Value y)
        {
            string res_string = get(x) + "_" + get(y);
            int res_index = Array.IndexOf(Register, res_string);
            if (res_index != -1)
            {
                return new Value(res_index, "index");
            }
            else
            {

                if (x.isVal && y.isVal) return SemiSub(x.val, y.val);
                if (x.isVal && y.isIndex) return SemiSub(x.val, y);
                if (x.isIndex && y.isVal) return SemiSub(x, y.val);
                if (x.isIndex && y.isIndex)
                {
                    Program.T(y.index, First_free + 1);
                    SemiSubMain(x, y);
                    return new Value(First_free - 1, "index");

                }
            }
            return x;
        }
        public void  SemiSubMain(Value x, Value y)
        {

            Program.J(x.index, First_free + 1, Program.NumberOfComands + 7);
            Program.J(x.index, First_free, Program.NumberOfComands + 5);
            Program.S(First_free + 1);
            Program.S(First_free);
            Program.J(0, 0, Program.NumberOfComands - 3);
            Program.Z(First_free);
            Program.Z(First_free + 1);
            Register[First_free] = get(x) + "_" + get(y);
            Program.PrinReg();
            First_free++;
        }
        public Value SemiSub(Value x, int y)
        {

            for (int i = 0; i < y; i++)
            {
                Program.S(First_free + 1);
            }
            SemiSubMain(x, new Value(First_free + 1, "index"));
            return new Value(First_free - 1, "index");
        }
        public Value SemiSub(int x, Value y)
        {
            Program.T(y.index, First_free + 1);
            for (int i = 0; i < x; i++)
            {
                Program.S(First_free + 2);
            }
            Program.J(First_free+2, First_free + 1, Program.NumberOfComands + 7);
            Program.J(First_free+2, First_free, Program.NumberOfComands + 5);
            Program.S(First_free + 1);
            Program.S(First_free);
            Program.J(0, 0, Program.NumberOfComands - 3);
            Program.Z(First_free);
            Program.Z(First_free + 1);
            Program.Z(First_free + 2);
            Register[First_free] = get(x) + "_" + get(y);
            Program.PrinReg();
            First_free++;
            return new Value(First_free - 1, "index");
        }
        public Value SemiSub(int x, int y)
        {
            if (x < y)
            {
                Program.NumberVal = 0;
                return new Value(0, "val");
            }
            Program.NumberVal = x - y;
            return new Value(x - y, "val");
        }

        public Value Number(int x)
        {
            for (int i=0; i<x; i++)
            {
                Program.S(First_free);
            }
            First_free++;
            return new Value(First_free - 1, "index");
        }

        public Value Mult(Value x, Value y)
        {
            return x;
        }
        //public Value Mult(int x, Value y);
        //public Value Mult(Value x, int y);
        public int Mult(int x, int y)
        {
            if (x * y <= 0) { Program.NumberVal = 0; return 0; }
            Program.NumberVal = x * y;
            return x * y;
        }

        public Value Div(Value x, Value y)
        {
            return x;
        }
        //public Value Div(int x, Value y);
        //public Value Div(Value x, int y);
        public int Div(int x, int y)
        {
            if (x % y != 0) { return 0; }
            return x / y;
        }

        public Value Pow(Value x, Value y)
        {
            return x;
        }
        //public Value Pow(int x, Value y);
        //public Value Pow(Value x, int y);
        public int Pow(int x, int y)
        {
            double res = System.Math.Pow(x, y);
            if (res >= 0 && res % 1 == 0) { return (int)res; }
            return 0;
        }

        public Value Max(Value x, Value y)
        {
            string res_string ="max(" + get(x) + "," + get(y) +")";
            int res_index = Array.IndexOf(Register, res_string);
            if (res_index != -1)
            {
                return new Value(res_index, "index");
            }
            else
            {

                if (x.isVal && y.isVal) return Max(x.val, y.val);
                if (x.isVal && y.isIndex) return Max(x, y);
                if (x.isIndex && y.isVal) return Max(y, x);
                if (x.isIndex && y.isIndex)
                {
                    Program.J(x.index, First_free,Program.NumberOfComands+5);
                    Program.J(y.index, First_free, Program.NumberOfComands + 5);
                    Program.S(First_free);
                    Program.J(0, 0, Program.NumberOfComands - 2);
                    Program.T(y.index, First_free);
                    Register[First_free] = res_string;
                    Program.PrinReg();
                    First_free++;
                    return new Value(First_free - 1, "index");

                }
            }
            return x;
        }
        //public Value Max(int x, Value y);
        
        public Value Max(Value x, int y)
        {
            for(int i=0; i < y; i++)
            {
                Program.S(First_free + 1);
            }
            Program.J(x.index, First_free, Program.NumberOfComands + 5);
            Program.J(First_free+1, First_free, Program.NumberOfComands + 5);
            Program.S(First_free);
            Program.J(0, 0, Program.NumberOfComands - 2);
            Program.T(First_free+1, First_free);
            Program.Z(First_free + 1);
            Register[First_free] ="max("+ get(x) + "," + get(y)+")";
            Program.PrinReg();
            First_free++;
            return new Value(First_free-1, "index");

        }
        public Value Max(int x, int y)
        {
            int res = System.Math.Max(x, y);
            if (res < 0) res = 0;
            Program.NumberVal = res;
            return new Value(res,"val");
        }

        public Value Min(Value x, Value y)
        {
            return x;
        }
        //public Value Min(int x, Value y);
        //public Value Min(Value x, int y);
        public int Min(int x, int y)
        {
            int res = System.Math.Min(x, y);
            if (res > 0) { Program.NumberVal = res; return res; }
            Program.NumberVal = 0;
            return 0;
        }



        public Value GetRegIndex(string variable)
        {

            int value = Array.IndexOf(Register, variable.ToString());

            if (value == -1)
            {
                Register[First_free] = variable.ToString();
                First_free++;

                return new Value(First_free-1, "index");
            }
            else
            {
                return new Value(value, "index");
            }
        }



    }
}
