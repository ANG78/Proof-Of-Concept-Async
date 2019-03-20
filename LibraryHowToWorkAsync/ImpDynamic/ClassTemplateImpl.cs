using System.Reflection;

namespace HowToWorkAsync.ImpDynamic
{

    public abstract class ClassBaseImpl : IGetLevel
    {
        protected IStrategyTodo WhatTodo;
        protected IGenerateSerie generator { get; set; }
        public uint Level { get;  set; }

        public ClassBaseImpl(IGenerateSerie gen, IStrategyTodo pProcesamiento)
        {
            WhatTodo = pProcesamiento;
            generator = gen;
        }

        public string Ident()
        {
            return "0" + Level + ((this is IGetString) ? "-Main" : "-MainAsync");
        }

        protected string TaskBase(string cadena, uint numProc)
        {
            return WhatTodo.Todo(cadena);
        }

        protected int TaskBaseNumeroIteracionesOTiempo(string cadena, uint numProc)
        {
            return WhatTodo.AmountOfStepsOrMls();
        }

        protected string GetNameMethod(MethodBase nameMetodo, bool obtenerNombreDesdeDeclaracion = false)
        {
            if (!obtenerNombreDesdeDeclaracion)
                return nameMetodo.Name;
            else
                return nameMetodo.DeclaringType.Name.Substring(0, nameMetodo.DeclaringType.Name.IndexOf(">")).Replace("<", "");
        }

        public string ObtenerLiteralSerie(MethodBase metodo, uint i, bool obtenerNombreDesdeDeclaracion = false)
        {
            var nameReflection = GetNameMethod(metodo, obtenerNombreDesdeDeclaracion);
            string codFuncion = "0" + i + "-";
            return codFuncion + nameReflection;
        }

        public string MyWork(string literal)
        {
            return TaskBase("0" + Level + "-" + literal, Level);
        }

        public void GenerateHeaderAndFoot(string literal)
        {
            generator.WriteLineReport("00" + Level + "-" + literal, (int)Level);
        }

        public void GenerateLostPoint(string literal)
        {
            generator.WriteLineReport("0" + Level + "-" + literal + "LOST", (int)Level);
        }

    }

    public abstract class ClassTemplateImpl : ClassBaseImpl  /*IStrategyVisitable,*/
    {
        protected IGetBase Next { get; private set; }
        protected IUseMethod Method;

        public ClassTemplateImpl(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
            : base(gen, pProcesamiento)
        {
            Next = pNext;
            Method = pMethod;
            Level = (uint)pMethod.Level;
        }
        //public abstract void Accept(IStrategyVisitor ac);

    }

    
}

