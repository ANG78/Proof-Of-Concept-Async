using System;
using System.Collections.Generic;

namespace HowToWorkAsync
{


    public interface ICallNextDescription
    {
        string PreDescription();
        string PostDescription();
    }


    public interface IProcessReportStrategy
    {
        void Execute(Report report);
        void WriteToFile(Report informe, string path);
    }

    public delegate void EventNextMethodWasChanged(ETypeImpl newType);

    public interface IGenerateSerie
    {
        string FillingOutTheReport(ETypePoint type, string metod, int iLevel, int idThread, bool esTiempo = false);
        Report GenateReport();
    }

    public interface IStrategyTodo
    {
        string Description();
        string Todo(string cadena, int idThread);
        bool IsTime();
        int AmountOfStepsOrMls();
    }

    public enum ETypeDoIndependentWork
    {
        LOOPING,
        SLEEPING
    }

    public static class ETypeWorkExtension
    {
        public static int Factor(this ETypeDoIndependentWork t)
        {
            return t == ETypeDoIndependentWork.LOOPING ? 1 : 10;
        }

        public static string Unit(this ETypeDoIndependentWork t)
        {
            return t == ETypeDoIndependentWork.LOOPING ? "steps" : "mls";
        }
    }

    public enum ETypeImpl
    {
        ASYNC,
        SYNC
    }
    
    public enum EStrategyDoIndependentWork
    {
        NORMAL,
        WRAPPER_ASYNC,
        WRAPPER_ASYNC_AWAITER
    }

    public enum ECallNext
    {
        WAIT_FIRST,
        WAIT_AFTER,
        AWAITER_AFTER,
        NOT_WAIT
    }

    public static class ETypeImplExtension
    {

        public static List<ECallNext> HowToCallTheNextOne(this ETypeImpl imp, ETypeImpl impNext)
        {
            switch (imp)
            {
                case ETypeImpl.SYNC:
                    {
                        return (new List<ECallNext>() { ECallNext.WAIT_FIRST, ECallNext.WAIT_AFTER });
                    }
                case ETypeImpl.ASYNC:
                    {
                        if (impNext == ETypeImpl.ASYNC)
                        {
                            var result = new List<ECallNext>();
                            foreach (var aux in Enum.GetValues(typeof(ECallNext)))
                            {
                                result.Add((ECallNext)aux);
                            }
                            return result;
                        }
                        else
                        {
                            return (new List<ECallNext>() { ECallNext.WAIT_FIRST, ECallNext.WAIT_AFTER });
                        }
                        
                    }
            }

            return null;
        }

        public static List<EStrategyDoIndependentWork> DoMyWork(this ETypeImpl imp)
        {
            switch (imp)
            {
                case ETypeImpl.SYNC:
                    return (new List<EStrategyDoIndependentWork>() { EStrategyDoIndependentWork.NORMAL });
                case ETypeImpl.ASYNC:
                    {
                        var result = new List<EStrategyDoIndependentWork>();
                        foreach (var aux in Enum.GetValues(typeof(EStrategyDoIndependentWork)))
                        {
                            result.Add((EStrategyDoIndependentWork)aux);
                        }
                        return result;
                    }
            }

            return null;
        }
    }
}