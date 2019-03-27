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
        string Todo(string cadena, int idThread);
        bool IsTime();
        int AmountOfStepsOrMls();
    }

    public enum ETypeWork
    {
        LOOPING,
        SLEEPING
    }

    public static class ETypeWorkExtension
    {
        public static int Factor(this ETypeWork t)
        {
            return t == ETypeWork.LOOPING ? 1 : 10;
        }

        public static string Unit(this ETypeWork t)
        {
            return t == ETypeWork.LOOPING ? "steps" : "mls";
        }
    }

    public enum ETypeImpl
    {
        ASYNC,
        SYNC
    }

    public enum EMyTypeImpl
    {
        ASYNC,
        SYNC,
        AWAITER
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

        public static List<ECallNext> HowToBeCalled(this ETypeImpl imp)
        {
            switch (imp)
            {
                case ETypeImpl.SYNC:
                    return (new List<ECallNext>() { ECallNext.WAIT_FIRST, ECallNext.WAIT_AFTER });
                case ETypeImpl.ASYNC:
                    {
                        var result = new List<ECallNext>();
                        foreach (var aux in Enum.GetValues(typeof(ECallNext)))
                        {
                            result.Add((ECallNext)aux);
                        }
                        return result;

                    }
            }

            return null;
        }
    }
}