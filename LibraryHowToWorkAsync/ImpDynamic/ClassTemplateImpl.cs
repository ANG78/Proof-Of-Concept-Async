﻿using System.Reflection;
using System.Threading.Tasks;

namespace HowToWorkAsync.ImpDynamic
{

    public abstract class ClassBaseImpl
    {
        protected IStrategyTodo WhatTodo;
        protected IGenerateSerie generator { get; set; }
        public uint Level { get; set; }

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

        protected string GetNameMethod(MethodBase nameMetodo)
        {
            if (nameMetodo.Name.Contains("MoveNext"))
            {
                return nameMetodo.DeclaringType.Name.Substring(0, nameMetodo.DeclaringType.Name.IndexOf(">")).Replace("<", "");
            }
            else
            {
                return nameMetodo.Name;
            }
        }

        public string ObtenerLiteralSerie(MethodBase metodo, uint i)
        {
            var nameReflection = GetNameMethod(metodo);
            string codFuncion = "0" + i + "-";
            return codFuncion + nameReflection;
        }

        public void GenerateHeaderAndFoot(string literal)
        {
            generator.FillingOutTheReport("00" + Level + "-" + literal, (int)Level);
        }

        public void GenerateLostPoint(string literal)
        {
            generator.FillingOutTheReport("0" + Level + "-" + literal + "LOST", (int)Level);
        }

    }

    public abstract class ClassTemplateImpl : ClassBaseImpl
    {
        protected IGetBase Next { get; private set; }
        protected IUseMethod Method;

        public ClassTemplateImpl(IGenerateSerie gen, IStrategyTodo pProcesamiento, IUseMethod pMethod, IGetBase pNext)
            : base(gen, pProcesamiento)
        {
            Next = pNext;
            Method = pMethod;
            Level = (uint)pMethod.Level;
            pMethod.Implementation = (IGetBase)this;
        }

        public string MyWork(string literal)
        {
            return TaskBase("0" + Level + "-" + literal, Level);            
        }

        public abstract string MyWorkDescription();
        public abstract string CallNextDescription();
        public abstract string HowToGetResultNextDescription();
    }


}

