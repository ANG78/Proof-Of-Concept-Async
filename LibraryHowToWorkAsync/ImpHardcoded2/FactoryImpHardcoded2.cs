using System;

namespace HowToWorkAsync.ImpHardcoded2
{
    public class FactoryImpHardcoded2
    {
        public static IGetString GetInstance(IGenerateSerie gen, ETipoImplementacion tipo, IStrategyTodo main, IStrategyTodo proc1, IStrategyTodo proc2, IStrategyTodo proc3)
        {
            var procesamientos = new IStrategyTodo[] { main, proc1, proc2, proc3 };
            switch (tipo)
            {

                case ETipoImplementacion.Imp_AM1_AM2_AM3_WF:
                    return new Imp_AM1_AM2_AM3(gen, procesamientos);

                case ETipoImplementacion.Imp_AM1_AM2_AM3_WA:
                    return new Imp_AM1_AM2_AM3_Paralela(gen, procesamientos);

                case ETipoImplementacion.Imp_AM1_M2_M3:
                    return new Imp_AM1_M2_M3_Paralela(gen, procesamientos);

                case ETipoImplementacion.Imp_AM1W_AM2W_AM3W_WA:
                    return new Imp_AM1W_AM2W_AM3W_Paralela(gen, procesamientos);

                case ETipoImplementacion.Imp_AM1W_AM2W_AM3W_WF:
                    return new Imp_AM1W_AM2W_AM3W(gen, procesamientos);

                case ETipoImplementacion.Imp_AM1_AM2_AM3W:
                    return new Imp_AM1_AM2_AM3W(gen, procesamientos);

                case ETipoImplementacion.Imp_M1_M2_M3_WF:
                    return new Imp_M1_M2_M3_Lineal(gen, procesamientos);

                case ETipoImplementacion.Imp_M1_M2_M3_WA:
                    return new Imp_M1_M2_M3_Paralela(gen, procesamientos);

                case ETipoImplementacion.Imp_AM1W_AM2_AM3W:
                    return new Imp_AM1W_AM2_AM3W(gen, procesamientos);

                case ETipoImplementacion.Imp_AM1_AM2_AM3_WithAwaitC:
                    return new Imp_AM1_AM2_AM3_WithAwaitC(gen, 25, procesamientos);

                default:
                    throw new Exception("tipo de implementacion no encontrado");
            }

        }
    }
}
