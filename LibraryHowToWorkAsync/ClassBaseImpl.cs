using System;
using System.Reflection;

namespace HowToWorkAsync
{

    public class ClassBaseImpl
    {

        protected readonly IGenerateSerie _generator;
        protected readonly IStrategyTodo[] _strategyTodo;

        public ClassBaseImpl(IGenerateSerie gen, IStrategyTodo[] procesamientos)
        {
            _generator = gen;
            _strategyTodo = procesamientos;
        }

        protected string TaskBase(string cadena, uint numProc)
        {
            if ( numProc< _strategyTodo.Length)
                return _strategyTodo[numProc].Todo(cadena);

            throw new Exception(@"Mal definido la peticion " + cadena + " y  " + numProc);
        }

        protected int TaskBaseInt(string cadena, int numProc)
        {
            if (numProc < _strategyTodo.Length)
                return numProc;

            throw new Exception(@"Mal definido la peticion " + cadena + " y  " + numProc);
        }

        protected int TaskBaseNumeroIteracionesOTiempo(string cadena, uint numProc)
        {
            if (numProc < _strategyTodo.Length)
                return _strategyTodo[numProc].AmountOfStepsOrMls();

            throw new Exception(@"Mal definido la peticion " + cadena + " y  " + numProc);
        }

      

        protected string Adaptador(MethodBase nameMetodo, bool obtenerNombreDesdeDeclaracion = false)
        {
            if (!obtenerNombreDesdeDeclaracion)
                return nameMetodo.Name;
            else
                return nameMetodo.DeclaringType.Name.Substring(0, nameMetodo.DeclaringType.Name.IndexOf(">")).Replace("<", "");
        }

        public string ObtenerLiteralSerie(MethodBase metodo, uint i, bool obtenerNombreDesdeDeclaracion = false)
        {
            var nameReflection = Adaptador(metodo, obtenerNombreDesdeDeclaracion);
            string codFuncion = "0" + i + "-";
            return codFuncion + nameReflection;
        }
    }
}
