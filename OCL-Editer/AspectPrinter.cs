/*** BNFC-Generated Pretty Printer and Abstract Syntax Viewer ***/

using System;
using System.Text; // for StringBuilder
using System.Collections;
using OCL.Absyn;

namespace OCL
{
    #region Aspect-printer class
    public class AspectPrinter
    {
        #region Misc rendering functions
        // You may wish to change these:
        private const int BUFFER_INITIAL_CAPACITY = 2000;
        private const int INDENT_WIDTH = 2;
        private const string LEFT_PARENTHESIS = "(";
        private const string RIGHT_PARENTHESIS = ")";
        private static System.Globalization.NumberFormatInfo InvariantFormatInfo = System.Globalization.NumberFormatInfo.InvariantInfo;

        private static int _n_ = 0;
        private static StringBuilder buffer = new StringBuilder(BUFFER_INITIAL_CAPACITY);

        private static ArrayList Trace = new ArrayList();
        private static int numberOfVar = 1;
        private static int rootIndex = -1;
        private static int numberOfRight = 0;
        private static ArrayList rootCode = new ArrayList();

        private static bool isLambdaExp = false;
        private static ArrayList Aspects = new ArrayList();
        private static Aspect Aspect = new Aspect();

        //You may wish to change render
        private static void Render(String s)
        {
            if (s == "(")
            {
                buffer.Append("\n");
                Indent();
                buffer.Append(s);
                _n_ = _n_ + INDENT_WIDTH;
                //buffer.Append("\n");
                //Indent();
            }
            else if (s == "a-(" || s == "[")
                buffer.Append(s);
            else if (s == "a-a)" || s == "]")
            {
                Backup();
                buffer.Append(s);
                buffer.Append(" ");
            }
            else if (s == ")")
            {
                int t;
                _n_ = _n_ - INDENT_WIDTH;
                for (t = 0; t < INDENT_WIDTH; t++)
                {
                    Backup();
                }
                buffer.Append(s);
                buffer.Append("\n");
                Indent();
            }
            else if (s == ",")
            {
                Backup();
                buffer.Append(s);
                buffer.Append(" ");
            }
            else if (s == ";")
            {
                Backup();
                buffer.Append(s);
                buffer.Append("\n");
                Indent();
            }
            else if (s == "") return;
            else
            {
                // Make sure escaped characters are printed properly!
                if (s.StartsWith("\"") && s.EndsWith("\""))
                {
                    buffer.Append('"');
                    StringBuilder sb = new StringBuilder(s);
                    // Remove enclosing citation marks
                    sb.Remove(0, 1);
                    sb.Remove(sb.Length - 1, 1);
                    // Note: we have to replace backslashes first! (otherwise it will "double-escape" the other escapes)
                    sb.Replace("\\", "\\\\");
                    sb.Replace("\n", "\\n");
                    sb.Replace("\t", "\\t");
                    sb.Replace("\"", "\\\"");
                    buffer.Append(sb.ToString());
                    buffer.Append('"');
                }
                else
                {
                    buffer.Append(s);
                }
                buffer.Append(" ");
            }
        }

        private static void PrintInternal(int n, int _i_)
        {
            buffer.Append(n.ToString(InvariantFormatInfo));
            buffer.Append(' ');
        }

        private static void PrintInternal(double d, int _i_)
        {
            buffer.Append(d.ToString(InvariantFormatInfo));
            buffer.Append(' ');
        }

        private static void PrintInternal(string s, int _i_)
        {
            Render(s);
        }

        private static void PrintInternal(char c, int _i_)
        {
            PrintQuoted(c);
        }


        private static void ShowInternal(int n)
        {
            Render(n.ToString(InvariantFormatInfo));
        }

        private static void ShowInternal(double d)
        {
            Render(d.ToString(InvariantFormatInfo));
        }

        private static void ShowInternal(char c)
        {
            PrintQuoted(c);
        }

        private static void ShowInternal(string s)
        {
            PrintQuoted(s);
        }


        private static void PrintQuoted(string s)
        {
            Render("\"" + s + "\"");
        }

        private static void PrintQuoted(char c)
        {
            // Makes sure the character is escaped properly before printing it.
            string str = c.ToString();
            if (c == '\n') str = "\\n";
            if (c == '\t') str = "\\t";
            Render("'" + str + "'");
        }

        private static void Indent()
        {
            int n = _n_;
            while (n > 0)
            {
                buffer.Append(' ');
                n--;
            }
        }

        private static void Backup()
        {
            if (buffer[buffer.Length - 1] == ' ')
            {
                buffer.Length = buffer.Length - 1;
            }
        }

        private static void Trim()
        {
            while (buffer.Length > 0 && buffer[0] == ' ')
                buffer.Remove(0, 1);
            while (buffer.Length > 0 && buffer[buffer.Length - 1] == ' ')
                buffer.Remove(buffer.Length - 1, 1);
        }

        private static ArrayList GetAndReset()
        {
            Trim();
            string strReturn = buffer.ToString();
            Reset();
            //return strReturn;
            /*foreach (Aspect a in Aspects)
                a.Print();*/
            return Aspects;

        }

        private static void Reset()
        {
            buffer.Remove(0, buffer.Length);
        }
        private static void Reset2()
        {
            Aspect.Code.Clear();
            rootCode.Clear();
            numberOfVar = 0;
            numberOfRight = 0;
            rootIndex = -1;
        }
        #endregion

        #region Print Entry Points
        public static ArrayList Print(OCL.Absyn.OCLfile cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.ListOCLPackage cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.OCLPackage cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.PackageName cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.OCLExpressions cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.ListConstrnt cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.Constrnt cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.ListConstrBody cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.ConstrBody cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.ContextDeclaration cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.ClassifierContext cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.OperationContext cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.Stereotype cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.OperationName cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.ListFormalParameter cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.FormalParameter cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.TypeSpecifier cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.CollectionType cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.ReturnType cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.OCLExpression cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.LetExpression cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.ListLetExpression cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.IfExpression cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.Expression cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.MessageArg cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.ListMessageArg cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.PropertyCall cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.PathName cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.PName cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.ListPName cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.PossQualifiers cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.Qualifiers cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.PossTimeExpression cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.PossPropCallParam cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.Declarator cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.DeclaratorVarList cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.ListIdent cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.PropertyCallParameters cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.ListExpression cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.PCPHelper cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.ListPCPHelper cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.OCLLiteral cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.SimpleTypeSpecifier cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.LiteralCollection cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.ListCollectionItem cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.CollectionItem cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.OCLNumber cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.LogicalOperator cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.CollectionKind cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.EqualityOperator cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.RelationalOperator cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.AddOperator cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.MultiplyOperator cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.UnaryOperator cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }

        public static ArrayList Print(OCL.Absyn.PostfixOperator cat)
        {
            PrintInternal(cat, 0);
            return GetAndReset();
        }
        #endregion

        #region (Internal) Print Methods
        private static void PrintInternal(OCL.Absyn.OCLfile p, int _i_)
        {
            Trace.Add("OCFfile");
            if (p is OCL.Absyn.OCLf)
            {
                OCL.Absyn.OCLf _oclf = (OCL.Absyn.OCLf)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_oclf.ListOCLPackage_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            // Trace Test
            // Aspect.ArrayPrint(Trace);

            Trace.Remove("OCFfile");
        }

        private static void PrintInternal(OCL.Absyn.ListOCLPackage p, int _i_)
        {
            Trace.Add("ListOCLPackage");
            for (int i = 0; i < p.Count; i++)
            {
                PrintInternal(p[i], 0);
                if (i < p.Count - 1)
                {
                    Render("");
                }
                else
                {
                    Render("");
                }
            }
            Trace.Remove("ListOCLPackage");
        }

        private static void PrintInternal(OCL.Absyn.OCLPackage p, int _i_)
        {
            Trace.Add("OCLPackage");
            if (p is OCL.Absyn.Pack)
            {
                OCL.Absyn.Pack _pack = (OCL.Absyn.Pack)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("package");
                PrintInternal(_pack.PackageName_, 0);
                PrintInternal(_pack.OCLExpressions_, 0);
                Render("endpackage");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("OCLPackage");
        }

        private static void PrintInternal(OCL.Absyn.PackageName p, int _i_)
        {
            Trace.Add("PackageName");
            if (p is OCL.Absyn.PackName)
            {
                OCL.Absyn.PackName _packname = (OCL.Absyn.PackName)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_packname.PathName_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("PackageName");
        }

        private static void PrintInternal(OCL.Absyn.OCLExpressions p, int _i_)
        {
            Trace.Add("OCLExpressions");
            if (p is OCL.Absyn.Constraints)
            {
                OCL.Absyn.Constraints _constraints = (OCL.Absyn.Constraints)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_constraints.ListConstrnt_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("OCLExpressions");
        }

        private static void PrintInternal(OCL.Absyn.ListConstrnt p, int _i_)
        {
            Trace.Add("ListConstrnt");
            for (int i = 0; i < p.Count; i++)
            {

                PrintInternal(p[i], 0);
                if (i < p.Count - 1)
                {
                    Render("");
                }
                else
                {
                    Render("");
                }
            }
            Trace.Remove("ListConstrnt");
        }

        private static void PrintInternal(OCL.Absyn.Constrnt p, int _i_)
        {
            Trace.Add("Constrnt");
            if (p is OCL.Absyn.Constr)
            {
                OCL.Absyn.Constr _constr = (OCL.Absyn.Constr)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_constr.ContextDeclaration_, 0);
                PrintInternal(_constr.ListConstrBody_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("Constrnt");
        }

        private static void PrintInternal(OCL.Absyn.ListConstrBody p, int _i_)
        {
            Trace.Add("ListConstrBody");
            for (int i = 0; i < p.Count; i++)
            {
                PrintInternal(p[i], 0);
                if (i < p.Count - 1)
                {
                    Render("");
                }
                else
                {
                    Render("");
                }

                // Aspect finish and append Aspect to Array Aspects
                Aspect.Id = Aspects.Count;
                if (Aspect.Type == "pre")
                {
                    Aspect.BeforeCode = Aspect.ArrayToString(Aspect.Code);
                    Aspect.AfterCode = "true";
                }
                else if (Aspect.Type == "post")
                {
                    Aspect.BeforeCode = "true";
                    Aspect.AfterCode = Aspect.ArrayToString(Aspect.Code);
                }
                else
                {
                    Aspect.BeforeCode = Aspect.ArrayToString(Aspect.Code);
                    Aspect.AfterCode = Aspect.ArrayToString(Aspect.Code);
                }

                //Aspect.Print();
                Aspect temp = new Aspect(Aspect.Id, Aspect.ConstraintName, Aspect.ContextName, Aspect.Type, Aspect.FunctionName, Aspect.Code, Aspect.BeforeCode, Aspect.AfterCode);
                Aspects.Add(temp);
                Reset2();
            }
            Trace.Remove("ListConstrBody");
        }

        private static void PrintInternal(OCL.Absyn.ConstrBody p, int _i_)
        {
            Trace.Add("ConstrBody");
            if (p is OCL.Absyn.CBDefNamed)
            {
                OCL.Absyn.CBDefNamed _cbdefnamed = (OCL.Absyn.CBDefNamed)p;

                Aspect.ConstraintName = _cbdefnamed.Ident_;

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("def");
                PrintInternal(_cbdefnamed.Ident_, 0);
                Render(":");
                PrintInternal(_cbdefnamed.ListLetExpression_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.CBDef)
            {
                OCL.Absyn.CBDef _cbdef = (OCL.Absyn.CBDef)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("def");
                Render(":");
                PrintInternal(_cbdef.ListLetExpression_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.CBNamed)
            {
                OCL.Absyn.CBNamed _cbnamed = (OCL.Absyn.CBNamed)p;

                Aspect.ConstraintName = _cbnamed.Ident_;

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_cbnamed.Stereotype_, 0);
                PrintInternal(_cbnamed.Ident_, 0);
                Render(":");
                PrintInternal(_cbnamed.OCLExpression_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.CB)
            {
                Aspect.ConstraintName = "";

                OCL.Absyn.CB _cb = (OCL.Absyn.CB)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_cb.Stereotype_, 0);
                Render(":");
                PrintInternal(_cb.OCLExpression_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("ConstrBody");
        }

        private static void PrintInternal(OCL.Absyn.ContextDeclaration p, int _i_)
        {
            Trace.Add("ContextDeclaration");
            if (p is OCL.Absyn.CDOper)
            {
                OCL.Absyn.CDOper _cdoper = (OCL.Absyn.CDOper)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("context");
                PrintInternal(_cdoper.OperationContext_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.CDClassif)
            {
                OCL.Absyn.CDClassif _cdclassif = (OCL.Absyn.CDClassif)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("context");
                PrintInternal(_cdclassif.ClassifierContext_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("ContextDeclaration");
        }

        private static void PrintInternal(OCL.Absyn.ClassifierContext p, int _i_)
        {
            Trace.Add("ClassifierContext");
            if (p is OCL.Absyn.CCType)
            {
                OCL.Absyn.CCType _cctype = (OCL.Absyn.CCType)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_cctype.Ident_1, 0);
                Render(":");
                PrintInternal(_cctype.Ident_2, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.CC)
            {
                OCL.Absyn.CC _cc = (OCL.Absyn.CC)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_cc.Ident_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("ClassifierContext");
        }

        private static void PrintInternal(OCL.Absyn.OperationContext p, int _i_)
        {
            Trace.Add("OperationContext");
            if (p is OCL.Absyn.OpC)
            {
                OCL.Absyn.OpC _opc = (OCL.Absyn.OpC)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_opc.Ident_, 0);

                Aspect.ContextName = _opc.Ident_;

                Render("::");
                PrintInternal(_opc.OperationName_, 0);
                Render("(");
                PrintInternal(_opc.ListFormalParameter_, 0);
                Render(")");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.OpCRT)
            {
                OCL.Absyn.OpCRT _opcrt = (OCL.Absyn.OpCRT)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_opcrt.Ident_, 0);

                Aspect.ContextName = _opcrt.Ident_;

                Render("::");
                PrintInternal(_opcrt.OperationName_, 0);
                Render("(");
                PrintInternal(_opcrt.ListFormalParameter_, 0);
                Render(")");
                Render(":");
                PrintInternal(_opcrt.ReturnType_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("OperationContext");
        }

        private static void PrintInternal(OCL.Absyn.Stereotype p, int _i_)
        {
            Trace.Add("Stereotype");
            if (p is OCL.Absyn.Pre)
            {
                OCL.Absyn.Pre _pre = (OCL.Absyn.Pre)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("pre");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);

                Aspect.Type = "pre";
            }
            else if (p is OCL.Absyn.Post)
            {
                OCL.Absyn.Post _post = (OCL.Absyn.Post)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("post");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);

                Aspect.Type = "post";
            }
            else if (p is OCL.Absyn.Inv)
            {
                OCL.Absyn.Inv _inv = (OCL.Absyn.Inv)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("inv");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);

                Aspect.Type = "inv";
            }
            Trace.Remove("Stereotype");
        }

        private static void PrintInternal(OCL.Absyn.OperationName p, int _i_)
        {
            Trace.Add("OperationName");
            if (p is OCL.Absyn.OpName)
            {
                OCL.Absyn.OpName _opname = (OCL.Absyn.OpName)p;

                Aspect.FunctionName = _opname.Ident_;

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_opname.Ident_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.Eq)
            {
                OCL.Absyn.Eq _eq = (OCL.Absyn.Eq)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("=");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.Add)
            {
                OCL.Absyn.Add _add = (OCL.Absyn.Add)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("+");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.Sub)
            {
                OCL.Absyn.Sub _sub = (OCL.Absyn.Sub)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("-");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.LST)
            {
                OCL.Absyn.LST _lst = (OCL.Absyn.LST)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("<");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.LSTE)
            {
                OCL.Absyn.LSTE _lste = (OCL.Absyn.LSTE)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("<=");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.GRT)
            {
                OCL.Absyn.GRT _grt = (OCL.Absyn.GRT)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render(">");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.GRTE)
            {
                OCL.Absyn.GRTE _grte = (OCL.Absyn.GRTE)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render(">=");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.Div)
            {
                OCL.Absyn.Div _div = (OCL.Absyn.Div)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("/");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.Mult)
            {
                OCL.Absyn.Mult _mult = (OCL.Absyn.Mult)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("*");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.NEq)
            {
                OCL.Absyn.NEq _neq = (OCL.Absyn.NEq)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("<>");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.Impl)
            {
                OCL.Absyn.Impl _impl = (OCL.Absyn.Impl)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("implies");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.Not)
            {
                OCL.Absyn.Not _not = (OCL.Absyn.Not)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("not");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.Or)
            {
                OCL.Absyn.Or _or = (OCL.Absyn.Or)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("or");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.Xor)
            {
                OCL.Absyn.Xor _xor = (OCL.Absyn.Xor)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("xor");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.And)
            {
                OCL.Absyn.And _and = (OCL.Absyn.And)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("and");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("OperationName");
        }

        private static void PrintInternal(OCL.Absyn.ListFormalParameter p, int _i_)
        {
            Trace.Add("ListFormalParameter");
            for (int i = 0; i < p.Count; i++)
            {
                PrintInternal(p[i], 0);
                if (i < p.Count - 1)
                {
                    Render(",");
                }
                else
                {
                    Render("");
                }
            }
            Trace.Remove("ListFormalParameter");
        }

        private static void PrintInternal(OCL.Absyn.FormalParameter p, int _i_)
        {
            Trace.Add("FormalParameter");
            if (p is OCL.Absyn.FP)
            {
                OCL.Absyn.FP _fp = (OCL.Absyn.FP)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_fp.Ident_, 0);
                Render(":");
                PrintInternal(_fp.TypeSpecifier_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("FormalParameter");
        }

        private static void PrintInternal(OCL.Absyn.TypeSpecifier p, int _i_)
        {
            Trace.Add("TypeSpecifier");
            if (p is OCL.Absyn.TSsimple)
            {
                OCL.Absyn.TSsimple _tssimple = (OCL.Absyn.TSsimple)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_tssimple.SimpleTypeSpecifier_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.TScoll)
            {
                OCL.Absyn.TScoll _tscoll = (OCL.Absyn.TScoll)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_tscoll.CollectionType_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("TypeSpecifier");
        }

        private static void PrintInternal(OCL.Absyn.CollectionType p, int _i_)
        {
            Trace.Add("CollectionType");
            if (p is OCL.Absyn.CT)
            {
                OCL.Absyn.CT _ct = (OCL.Absyn.CT)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_ct.CollectionKind_, 0);
                Render("(");
                PrintInternal(_ct.SimpleTypeSpecifier_, 0);
                Render(")");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("CollectionType");
        }

        private static void PrintInternal(OCL.Absyn.ReturnType p, int _i_)
        {
            Trace.Add("ReturnType");
            if (p is OCL.Absyn.RT)
            {
                OCL.Absyn.RT _rt = (OCL.Absyn.RT)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_rt.TypeSpecifier_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("ReturnType");
        }

        private static void PrintInternal(OCL.Absyn.OCLExpression p, int _i_)
        {
            Trace.Add("OCLExpression");
            if (p is OCL.Absyn.OCLExp)
            {
                OCL.Absyn.OCLExp _oclexp = (OCL.Absyn.OCLExp)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_oclexp.Expression_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.OCLExpLet)
            {
                OCL.Absyn.OCLExpLet _oclexplet = (OCL.Absyn.OCLExpLet)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_oclexplet.ListLetExpression_, 0);
                Render("in");
                PrintInternal(_oclexplet.Expression_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("OCLExpression");
        }

        private static void PrintInternal(OCL.Absyn.LetExpression p, int _i_)
        {
            Trace.Add("LetExpression");
            if (p is OCL.Absyn.LENoParam)
            {
                OCL.Absyn.LENoParam _lenoparam = (OCL.Absyn.LENoParam)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("let");
                PrintInternal(_lenoparam.Ident_, 0);
                Render("=");
                PrintInternal(_lenoparam.Expression_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.LENoParamType)
            {
                OCL.Absyn.LENoParamType _lenoparamtype = (OCL.Absyn.LENoParamType)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("let");
                PrintInternal(_lenoparamtype.Ident_, 0);
                Render(":");
                PrintInternal(_lenoparamtype.TypeSpecifier_, 0);
                Render("=");
                PrintInternal(_lenoparamtype.Expression_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.LE)
            {
                OCL.Absyn.LE _le = (OCL.Absyn.LE)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("let");
                PrintInternal(_le.Ident_, 0);
                Render("(");
                PrintInternal(_le.ListFormalParameter_, 0);
                Render(")");
                Render("=");
                PrintInternal(_le.Expression_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.LEType)
            {
                OCL.Absyn.LEType _letype = (OCL.Absyn.LEType)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("let");
                PrintInternal(_letype.Ident_, 0);
                Render("(");
                PrintInternal(_letype.ListFormalParameter_, 0);
                Render(")");
                Render(":");
                PrintInternal(_letype.TypeSpecifier_, 0);
                Render("=");
                PrintInternal(_letype.Expression_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("LetExpression");
        }

        private static void PrintInternal(OCL.Absyn.ListLetExpression p, int _i_)
        {
            Trace.Add("ListLetExpression");
            for (int i = 0; i < p.Count; i++)
            {
                PrintInternal(p[i], 0);
                if (i < p.Count - 1)
                {
                    Render("");
                }
                else
                {
                    Render("");
                }
            }
            Trace.Remove("ListLetExpression");
        }

        private static void PrintInternal(OCL.Absyn.IfExpression p, int _i_)
        {
            Trace.Add("IfExpression");
            if (p is OCL.Absyn.IfExp)
            {
                OCL.Absyn.IfExp _ifexp = (OCL.Absyn.IfExp)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("if");
                PrintInternal(_ifexp.Expression_1, 0);
                Render("then");
                PrintInternal(_ifexp.Expression_2, 0);
                Render("else");
                PrintInternal(_ifexp.Expression_3, 0);
                Render("endif");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("IfExpression");
        }

        private static void PrintInternal(OCL.Absyn.Expression p, int _i_)
        {
            Trace.Add("Expression");
            if (p is OCL.Absyn.EOpImpl)
            {
                OCL.Absyn.EOpImpl _eopimpl = (OCL.Absyn.EOpImpl)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_eopimpl.Expression_1, 0);
                Render("implies");
                PrintInternal(_eopimpl.Expression_2, 1);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.EOpLog)
            {
                OCL.Absyn.EOpLog _eoplog = (OCL.Absyn.EOpLog)p;
                if (_i_ > 1) Render(LEFT_PARENTHESIS);
                PrintInternal(_eoplog.Expression_1, 1);
                PrintInternal(_eoplog.LogicalOperator_, 0);
                PrintInternal(_eoplog.Expression_2, 2);
                if (_i_ > 1) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.EOpEq)
            {
                OCL.Absyn.EOpEq _eopeq = (OCL.Absyn.EOpEq)p;
                if (_i_ > 2) Render(LEFT_PARENTHESIS);
                PrintInternal(_eopeq.Expression_1, 2);
                PrintInternal(_eopeq.EqualityOperator_, 0);
                PrintInternal(_eopeq.Expression_2, 3);
                if (_i_ > 2) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.EOpRel)
            {
                OCL.Absyn.EOpRel _eoprel = (OCL.Absyn.EOpRel)p;
                if (_i_ > 3) Render(LEFT_PARENTHESIS);
                PrintInternal(_eoprel.Expression_1, 3);
                PrintInternal(_eoprel.RelationalOperator_, 0);
                PrintInternal(_eoprel.Expression_2, 4);
                if (_i_ > 3) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.EOpAdd)
            {
                OCL.Absyn.EOpAdd _eopadd = (OCL.Absyn.EOpAdd)p;
                if (_i_ > 4) Render(LEFT_PARENTHESIS);
                PrintInternal(_eopadd.Expression_1, 4);
                PrintInternal(_eopadd.AddOperator_, 0);
                PrintInternal(_eopadd.Expression_2, 5);
                if (_i_ > 4) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.EOpMul)
            {
                OCL.Absyn.EOpMul _eopmul = (OCL.Absyn.EOpMul)p;
                if (_i_ > 5) Render(LEFT_PARENTHESIS);
                PrintInternal(_eopmul.Expression_1, 5);
                PrintInternal(_eopmul.MultiplyOperator_, 0);
                PrintInternal(_eopmul.Expression_2, 6);
                if (_i_ > 5) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.EOpUn)
            {
                OCL.Absyn.EOpUn _eopun = (OCL.Absyn.EOpUn)p;
                if (_i_ > 6) Render(LEFT_PARENTHESIS);
                PrintInternal(_eopun.UnaryOperator_, 0);
                PrintInternal(_eopun.Expression_, 7);
                if (_i_ > 6) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.EExplPropCall)
            {
                if (rootIndex != -1) rootIndex = -1;
                OCL.Absyn.EExplPropCall _eexplpropcall = (OCL.Absyn.EExplPropCall)p;
                if (_i_ > 7) Render(LEFT_PARENTHESIS);
                PrintInternal(_eexplpropcall.Expression_, 7);
                PrintInternal(_eexplpropcall.PostfixOperator_, 0);
                PrintInternal(_eexplpropcall.PropertyCall_, 0);
                if (_i_ > 7) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.EMessage)
            {
                OCL.Absyn.EMessage _emessage = (OCL.Absyn.EMessage)p;
                if (_i_ > 7) Render(LEFT_PARENTHESIS);
                PrintInternal(_emessage.Expression_, 7);
                Render("^");
                PrintInternal(_emessage.PathName_, 0);
                Render("(");
                PrintInternal(_emessage.ListMessageArg_, 0);
                Render(")");
                if (_i_ > 7) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.EImplPropCall)
            {
                OCL.Absyn.EImplPropCall _eimplpropcall = (OCL.Absyn.EImplPropCall)p;
                if (_i_ > 8) Render(LEFT_PARENTHESIS);
                PrintInternal(_eimplpropcall.PropertyCall_, 0);
                if (_i_ > 8) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.ELitColl)
            {
                OCL.Absyn.ELitColl _elitcoll = (OCL.Absyn.ELitColl)p;
                if (_i_ > 8) Render(LEFT_PARENTHESIS);
                PrintInternal(_elitcoll.LiteralCollection_, 0);
                if (_i_ > 8) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.ELit)
            {
                OCL.Absyn.ELit _elit = (OCL.Absyn.ELit)p;
                if (_i_ > 8) Render(LEFT_PARENTHESIS);
                PrintInternal(_elit.OCLLiteral_, 0);
                if (_i_ > 8) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.EIfExp)
            {
                OCL.Absyn.EIfExp _eifexp = (OCL.Absyn.EIfExp)p;
                if (_i_ > 8) Render(LEFT_PARENTHESIS);
                PrintInternal(_eifexp.IfExpression_, 0);
                if (_i_ > 8) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.ENull)
            {
                OCL.Absyn.ENull _enull = (OCL.Absyn.ENull)p;
                if (_i_ > 8) Render(LEFT_PARENTHESIS);
                Render("null");
                if (_i_ > 8) Render(RIGHT_PARENTHESIS);
            }

            Trace.Remove("Expression");
        }

        private static void PrintInternal(OCL.Absyn.MessageArg p, int _i_)
        {
            Trace.Add("MessageArg");
            if (p is OCL.Absyn.MAExpr)
            {
                OCL.Absyn.MAExpr _maexpr = (OCL.Absyn.MAExpr)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_maexpr.Expression_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.MAUnspec)
            {
                OCL.Absyn.MAUnspec _maunspec = (OCL.Absyn.MAUnspec)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("?");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.MAUnspecTyped)
            {
                OCL.Absyn.MAUnspecTyped _maunspectyped = (OCL.Absyn.MAUnspecTyped)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("?");
                Render(":");
                PrintInternal(_maunspectyped.TypeSpecifier_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("MessageArg");
        }

        private static void PrintInternal(OCL.Absyn.ListMessageArg p, int _i_)
        {
            Trace.Add("ListMessageArg");
            for (int i = 0; i < p.Count; i++)
            {
                PrintInternal(p[i], 0);
                if (i < p.Count - 1)
                {
                    Render(",");
                }
                else
                {
                    Render("");
                }
            }
            Trace.Remove("ListMessageArg");
        }

        private static void PrintInternal(OCL.Absyn.PropertyCall p, int _i_)
        {
            Trace.Add("PropertyCall");
            if (p is OCL.Absyn.PCall)
            {
                OCL.Absyn.PCall _pcall = (OCL.Absyn.PCall)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_pcall.PathName_, 0);
                PrintInternal(_pcall.PossTimeExpression_, 0);
                PrintInternal(_pcall.PossQualifiers_, 0);
                PrintInternal(_pcall.PossPropCallParam_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("PropertyCall");
        }

        private static void PrintInternal(OCL.Absyn.PathName p, int _i_)
        {
            Trace.Add("PathName");
            if (p is OCL.Absyn.PathN)
            {
                OCL.Absyn.PathN _pathn = (OCL.Absyn.PathN)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_pathn.ListPName_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("PathName");
        }
        private static string ConvertToCS(String s)
        {
            switch (s.ToLower())
            {
                case "forall":
                    isLambdaExp = true;
                    return "TrueForAll";
                case "sum": return "Sum";
                case "notempty": return "Count() > 0";
                case "count": return "Count";
                case "size": return "Count";
                case "exists": return "Exists";
                case "includes": return "Exists";
                case "calender": return "DateTime.Today";
                case "year": return "Year";
                case "month": return "Month";
                case "day": return "Day";
                case "integer": return "int";
                case "double": return "double";
                case "abs": return "Abs";
                case "div":
                    Aspect.Code.RemoveAt(Aspect.Code.Count - 1);
                    return " / ";
                case "mod":
                    Aspect.Code.RemoveAt(Aspect.Code.Count - 1);
                    return " % ";
                case "concat": return "Concat";
                case "substring": return "Substring";
                case "toupper": return "toUpper";
                case "tolower": return "toLower";
                default: return s;
            }
        }
        private static void PrintInternal(OCL.Absyn.PName p, int _i_)
        {
            Trace.Add("PName");
            if (p is OCL.Absyn.PN)
            {
                OCL.Absyn.PN _pn = (OCL.Absyn.PN)p;
                if (Trace[Trace.Count - 4].Equals("PropertyCall"))
                    Aspect.Code.Add(ConvertToCS(_pn.Ident_));

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_pn.Ident_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("PName");
        }

        private static void PrintInternal(OCL.Absyn.ListPName p, int _i_)
        {
            Trace.Add("ListPName");
            for (int i = 0; i < p.Count; i++)
            {
                PrintInternal(p[i], 0);
                if (i < p.Count - 1)
                {
                    Render("::");
                }
                else
                {
                    Render("");
                }
            }
            Trace.Remove("ListPName");
        }

        private static void PrintInternal(OCL.Absyn.PossQualifiers p, int _i_)
        {
            Trace.Add("PossQualifiers");
            if (p is OCL.Absyn.NoQual)
            {
                OCL.Absyn.NoQual _noqual = (OCL.Absyn.NoQual)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.Qual)
            {
                OCL.Absyn.Qual _qual = (OCL.Absyn.Qual)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_qual.Qualifiers_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("PossQualifiers");
        }

        private static void PrintInternal(OCL.Absyn.Qualifiers p, int _i_)
        {
            Trace.Add("Qualifiers");
            if (p is OCL.Absyn.Quals)
            {
                OCL.Absyn.Quals _quals = (OCL.Absyn.Quals)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("[");
                PrintInternal(_quals.ListExpression_, 0);
                Render("]");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("Qualifiers");
        }

        private static void PrintInternal(OCL.Absyn.PossTimeExpression p, int _i_)
        {
            Trace.Add("PossTimeExpression");
            if (p is OCL.Absyn.NoTE)
            {
                OCL.Absyn.NoTE _note = (OCL.Absyn.NoTE)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.AtPre)
            {
                OCL.Absyn.AtPre _atpre = (OCL.Absyn.AtPre)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("@");
                Render("pre");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("PossTimeExpression");
        }

        private static void PrintInternal(OCL.Absyn.PossPropCallParam p, int _i_)
        {
            Trace.Add("PossPropCallParam");
            if (p is OCL.Absyn.NoPCP)
            {
                OCL.Absyn.NoPCP _nopcp = (OCL.Absyn.NoPCP)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.PCPs)
            {
                OCL.Absyn.PCPs _pcps = (OCL.Absyn.PCPs)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_pcps.PropertyCallParameters_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("PossPropCallParam");
        }

        private static void PrintInternal(OCL.Absyn.Declarator p, int _i_)
        {
            Trace.Add("Declarator");
            if (p is OCL.Absyn.Decl)
            {
                OCL.Absyn.Decl _decl = (OCL.Absyn.Decl)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_decl.DeclaratorVarList_, 0);
                Render("|");
                Aspect.Code.Add(" => ");

                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.DeclAcc)
            {
                OCL.Absyn.DeclAcc _declacc = (OCL.Absyn.DeclAcc)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_declacc.DeclaratorVarList_, 0);
                Render(";");
                PrintInternal(_declacc.Ident_, 0);
                Render(":");
                PrintInternal(_declacc.TypeSpecifier_, 0);
                Render("=");
                PrintInternal(_declacc.Expression_, 0);
                Render("|");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("Declarator");
        }

        private static void PrintInternal(OCL.Absyn.DeclaratorVarList p, int _i_)
        {
            Trace.Add("DeclaratorVarList");
            if (p is OCL.Absyn.DVL)
            {
                OCL.Absyn.DVL _dvl = (OCL.Absyn.DVL)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_dvl.ListIdent_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.DVLType)
            {
                OCL.Absyn.DVLType _dvltype = (OCL.Absyn.DVLType)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_dvltype.ListIdent_, 0);
                Render(":");
                PrintInternal(_dvltype.SimpleTypeSpecifier_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("DeclaratorVarList");
        }

        private static void PrintInternal(OCL.Absyn.ListIdent p, int _i_)
        {
            Trace.Add("ListIdent");
            for (int i = 0; i < p.Count; i++)
            {
                PrintInternal(p[i], 0);
                if (i < p.Count - 1)
                {
                    Render(",");
                }
                else
                {
                    Render("");
                }
            }
            Trace.Add("ListIdent");
        }

        private static void PrintInternal(OCL.Absyn.PropertyCallParameters p, int _i_)
        {
            Trace.Add("PropertyCallParameters");
            if (p is OCL.Absyn.PCPDecl)
            {
                OCL.Absyn.PCPDecl _pcpdecl = (OCL.Absyn.PCPDecl)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("(");
                Aspect.Code.Add("(");
                PrintInternal(_pcpdecl.Declarator_, 0);
                PrintInternal(_pcpdecl.ListExpression_, 0);
                Render(")");
                Aspect.Code.Add(")");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.PCP)
            {
                OCL.Absyn.PCP _pcp = (OCL.Absyn.PCP)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("(");
                Aspect.Code.Add("(");

                PrintInternal(_pcp.ListExpression_, 0);

                Render(")");
                Aspect.Code.Add(")");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.PCPNoDeclNoParam)
            {
                OCL.Absyn.PCPNoDeclNoParam _pcpnodeclnoparam = (OCL.Absyn.PCPNoDeclNoParam)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("(");
                Aspect.Code.Add("(");
                Render(")");
                Aspect.Code.Add(")");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.PCPConcrete)
            {
                OCL.Absyn.PCPConcrete _pcpconcrete = (OCL.Absyn.PCPConcrete)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("(");
                Aspect.Code.Add("(");

                PrintInternal(_pcpconcrete.Expression_, 0);
                PrintInternal(_pcpconcrete.ListPCPHelper_, 0);

                Render(")");
                for (int i = 0; i <= numberOfRight; i++)
                    Aspect.Code.Add(")");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("PropertyCallParameters");
        }

        private static void PrintInternal(OCL.Absyn.ListExpression p, int _i_)
        {
            Trace.Add("ListExpression");
            for (int i = 0; i < p.Count; i++)
            {
                PrintInternal(p[i], 0);
                if (i < p.Count - 1)
                {
                    Render(",");
                }
                else
                {
                    Render("");
                }
            }
            Trace.Remove("ListExpression");
        }

        private static void PrintInternal(OCL.Absyn.PCPHelper p, int _i_)
        {
            Trace.Add("PCPHelper");
            if (p is OCL.Absyn.PCPComma)
            {
                OCL.Absyn.PCPComma _pcpcomma = (OCL.Absyn.PCPComma)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render(",");
                if (rootCode.Count == 0 && rootIndex >= 0)
                {
                    ArrayList temp = new ArrayList();
                    for (int i = rootIndex; i < Aspect.Code.Count - 1; i++)
                        temp.Add(Aspect.Code[i]);
                    isLambdaExp = false;
                    rootCode = temp;
                }
                //Aspect.ArrayPrint(rootCode);
                if (rootCode.Count > 0)
                {
                    Aspect.Code.Add(" => ");
                    foreach (var r in rootCode)
                        Aspect.Code.Add(r);
                    numberOfRight++;
                    PrintInternal(_pcpcomma.Expression_, 0);
                    if (_i_ > 0) Render(RIGHT_PARENTHESIS);
                }
                else
                {
                    Aspect.Code.Add(", ");
                    PrintInternal(_pcpcomma.Expression_, 0);
                    if (_i_ > 0) Render(RIGHT_PARENTHESIS);
                }
            }
            else if (p is OCL.Absyn.PCPColon)
            {
                OCL.Absyn.PCPColon _pcpcolon = (OCL.Absyn.PCPColon)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render(":");
                PrintInternal(_pcpcolon.SimpleTypeSpecifier_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.PCPIterate)
            {
                OCL.Absyn.PCPIterate _pcpiterate = (OCL.Absyn.PCPIterate)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render(";");
                PrintInternal(_pcpiterate.Ident_, 0);
                Render(":");
                PrintInternal(_pcpiterate.TypeSpecifier_, 0);
                Render("=");
                PrintInternal(_pcpiterate.Expression_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.PCPBar)
            {
                OCL.Absyn.PCPBar _pcpbar = (OCL.Absyn.PCPBar)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("|");
                Aspect.Code.Add(" => ");
                PrintInternal(_pcpbar.Expression_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("PCPHelper");
        }

        private static void PrintInternal(OCL.Absyn.ListPCPHelper p, int _i_)
        {
            Trace.Add("ListPCPHelper");
            numberOfVar = p.Count;
            for (int i = 0; i < p.Count; i++)
            {
                PrintInternal(p[i], 0);
                if (i < p.Count - 1)
                {
                    Render("");
                }
                else
                {
                    Render("");
                }
            }

            rootIndex = -1;
            Trace.Remove("ListPCPHelper");
        }

        private static void PrintInternal(OCL.Absyn.OCLLiteral p, int _i_)
        {
            Trace.Add("OCLLiteral");
            if (p is OCL.Absyn.LitStr)
            {
                OCL.Absyn.LitStr _litstr = (OCL.Absyn.LitStr)p;
                Aspect.Code.Add(_litstr.String_);

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_litstr.String_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.LitNum)
            {
                OCL.Absyn.LitNum _litnum = (OCL.Absyn.LitNum)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_litnum.OCLNumber_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.LitBoolTrue)
            {
                OCL.Absyn.LitBoolTrue _litbooltrue = (OCL.Absyn.LitBoolTrue)p;
                Aspect.Code.Add(true);

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("true");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.LitBoolFalse)
            {
                OCL.Absyn.LitBoolFalse _litboolfalse = (OCL.Absyn.LitBoolFalse)p;
                Aspect.Code.Add(false);

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("false");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("OCLLiteral");
        }

        private static void PrintInternal(OCL.Absyn.SimpleTypeSpecifier p, int _i_)
        {
            Trace.Add("SimpleTypeSpecifier");
            if (p is OCL.Absyn.STSpec)
            {
                OCL.Absyn.STSpec _stspec = (OCL.Absyn.STSpec)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_stspec.PathName_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("SimpleTypeSpecifier");
        }

        private static void PrintInternal(OCL.Absyn.LiteralCollection p, int _i_)
        {
            Trace.Add("LiteralCollection");
            if (p is OCL.Absyn.LCollection)
            {
                OCL.Absyn.LCollection _lcollection = (OCL.Absyn.LCollection)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_lcollection.CollectionKind_, 0);
                Render("{");
                PrintInternal(_lcollection.ListCollectionItem_, 0);
                Render("}");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.LCollectionEmpty)
            {
                OCL.Absyn.LCollectionEmpty _lcollectionempty = (OCL.Absyn.LCollectionEmpty)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_lcollectionempty.CollectionKind_, 0);
                Render("{");
                Render("}");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("LiteralCollection");
        }

        private static void PrintInternal(OCL.Absyn.ListCollectionItem p, int _i_)
        {
            Trace.Add("ListCollectionItem");
            for (int i = 0; i < p.Count; i++)
            {
                PrintInternal(p[i], 0);
                if (i < p.Count - 1)
                {
                    Render(",");
                }
                else
                {
                    Render("");
                }
            }
            Trace.Remove("ListCollectionItem");
        }

        private static void PrintInternal(OCL.Absyn.CollectionItem p, int _i_)
        {
            Trace.Add("CollectionItem");
            if (p is OCL.Absyn.CI)
            {
                OCL.Absyn.CI _ci = (OCL.Absyn.CI)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_ci.Expression_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.CIRange)
            {
                OCL.Absyn.CIRange _cirange = (OCL.Absyn.CIRange)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_cirange.Expression_1, 0);
                Render("..");
                PrintInternal(_cirange.Expression_2, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("CollectionItem");
        }

        private static void PrintInternal(OCL.Absyn.OCLNumber p, int _i_)
        {
            Trace.Add("OCLNumber");
            if (p is OCL.Absyn.NumInt)
            {
                OCL.Absyn.NumInt _numint = (OCL.Absyn.NumInt)p;
                Aspect.Code.Add(_numint.Integer_);

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_numint.Integer_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.NumDouble)
            {
                OCL.Absyn.NumDouble _numdouble = (OCL.Absyn.NumDouble)p;
                Aspect.Code.Add(_numdouble.Double_);

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                PrintInternal(_numdouble.Double_, 0);
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("OCLNumber");
        }

        private static void PrintInternal(OCL.Absyn.LogicalOperator p, int _i_)
        {
            Trace.Add("LogicalOperator");
            if (p is OCL.Absyn.LAnd)
            {
                OCL.Absyn.LAnd _land = (OCL.Absyn.LAnd)p;
                Aspect.Code.Add(" & ");

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("and");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.LOr)
            {
                OCL.Absyn.LOr _lor = (OCL.Absyn.LOr)p;
                Aspect.Code.Add(" | ");

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("or");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.LXor)
            {
                OCL.Absyn.LXor _lxor = (OCL.Absyn.LXor)p;
                Aspect.Code.Add(" ^ ");

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("xor");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Add("LogicalOperator");
        }

        private static void PrintInternal(OCL.Absyn.CollectionKind p, int _i_)
        {
            Trace.Add("CollectionKind");
            if (p is OCL.Absyn.Set)
            {
                OCL.Absyn.Set _set = (OCL.Absyn.Set)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("Set");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.Bag)
            {
                OCL.Absyn.Bag _bag = (OCL.Absyn.Bag)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("Bag");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.Sequence)
            {
                OCL.Absyn.Sequence _sequence = (OCL.Absyn.Sequence)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("Sequence");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.Collection)
            {
                OCL.Absyn.Collection _collection = (OCL.Absyn.Collection)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("Collection");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("CollectionKind");
        }

        private static void PrintInternal(OCL.Absyn.EqualityOperator p, int _i_)
        {
            Trace.Add("EqualityOperator");
            if (p is OCL.Absyn.EEq)
            {
                OCL.Absyn.EEq _eeq = (OCL.Absyn.EEq)p;
                Aspect.Code.Add(" == ");

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("=");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.ENEq)
            {
                OCL.Absyn.ENEq _eneq = (OCL.Absyn.ENEq)p;
                Aspect.Code.Add(" != ");

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("<>");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("EqualityOperator");
        }

        private static void PrintInternal(OCL.Absyn.RelationalOperator p, int _i_)
        {
            Trace.Add("RelationalOperator");
            if (p is OCL.Absyn.RGT)
            {
                OCL.Absyn.RGT _rgt = (OCL.Absyn.RGT)p;
                Aspect.Code.Add(" > ");

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render(">");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.RGTE)
            {
                OCL.Absyn.RGTE _rgte = (OCL.Absyn.RGTE)p;
                Aspect.Code.Add(" >= ");

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render(">=");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.RLT)
            {
                OCL.Absyn.RLT _rlt = (OCL.Absyn.RLT)p;
                Aspect.Code.Add(" < ");

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("<");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.RLTE)
            {
                OCL.Absyn.RLTE _rlte = (OCL.Absyn.RLTE)p;
                Aspect.Code.Add(" <= ");

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("<=");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("RelationalOperator");
        }

        private static void PrintInternal(OCL.Absyn.AddOperator p, int _i_)
        {
            Trace.Add("AddOperator");
            if (p is OCL.Absyn.AAdd)
            {
                OCL.Absyn.AAdd _aadd = (OCL.Absyn.AAdd)p;
                Aspect.Code.Add(" + ");

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("+");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.ASub)
            {
                OCL.Absyn.ASub _asub = (OCL.Absyn.ASub)p;
                Aspect.Code.Add(" - ");

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("-");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("AddOperator");
        }

        private static void PrintInternal(OCL.Absyn.MultiplyOperator p, int _i_)
        {
            Trace.Add("MultiplyOperator");

            if (p is OCL.Absyn.MMult)
            {
                OCL.Absyn.MMult _mmult = (OCL.Absyn.MMult)p;
                Aspect.Code.Add(" * ");

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("*");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.MDiv)
            {
                OCL.Absyn.MDiv _mdiv = (OCL.Absyn.MDiv)p;
                Aspect.Code.Add(" / ");

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("/");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("MultiplyOperator");
        }

        private static void PrintInternal(OCL.Absyn.UnaryOperator p, int _i_)
        {
            Trace.Add("UnaryOperator");
            if (p is OCL.Absyn.UMin)
            {
                OCL.Absyn.UMin _umin = (OCL.Absyn.UMin)p;
                Aspect.Code.Add("-");

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("-");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.UNot)
            {
                OCL.Absyn.UNot _unot = (OCL.Absyn.UNot)p;
                Aspect.Code.Add("!");

                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("not");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            Trace.Remove("UnaryOperator");
        }

        private static void PrintInternal(OCL.Absyn.PostfixOperator p, int _i_)
        {
            Trace.Add("PostfixOperator");
            if (p is OCL.Absyn.PDot)
            {
                OCL.Absyn.PDot _pdot = (OCL.Absyn.PDot)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render(".");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            else if (p is OCL.Absyn.PArrow)
            {
                OCL.Absyn.PArrow _parrow = (OCL.Absyn.PArrow)p;
                if (_i_ > 0) Render(LEFT_PARENTHESIS);
                Render("->");
                if (_i_ > 0) Render(RIGHT_PARENTHESIS);
            }
            if (rootIndex == -1)
                rootIndex = Aspect.Code.Count - 1;
            Aspect.Code.Add(".");
            Trace.Remove("PostfixOperator");
        }
        #endregion

    }
    #endregion
}
