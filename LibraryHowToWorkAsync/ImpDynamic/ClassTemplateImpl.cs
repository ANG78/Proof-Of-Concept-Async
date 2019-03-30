using System.Reflection;
using System.Threading.Tasks;

namespace HowToWorkAsync.ImpDynamic
{

    public abstract class ClassBaseImpl
    {
        
        protected IGenerateSerie generator { get; set; }
        public uint Level { get; set; }
        protected abstract string IdSerie { get; }

        public ClassBaseImpl(IGenerateSerie gen)
        {
            generator = gen;
        }

        public string Ident()
        {
            return "0" + Level + ((this is IGetString) ? "-Main" : "-MainAsync");
        }

       

        protected string GetNameMethod(MethodBase nameMetodo)
        {
            if (nameMetodo.Name.Contains("MoveNext"))
            {
                return "0" + Level + nameMetodo.DeclaringType.Name.Substring(0, nameMetodo.DeclaringType.Name.IndexOf(">")).Replace("<", "") + " " + IdSerie;
            }
            else
            {
                return "0" + Level + nameMetodo.Name + " " + IdSerie;
            }
        }

        public void GenerateHeaderAndFoot(string literal, int idThread)
        {
            generator.FillingOutTheReport(ETypePoint.START_END, "0" + literal +  " S/E", (int)Level, idThread);
        }

        public void GenerateLostPoint(string literal, int idThread)
        {
            generator.FillingOutTheReport(ETypePoint.LOST, "0"+ literal + "LOST", (int)Level, idThread);
        }

        protected string GenerateHeaderAndFoot(MethodBase param, int idThread)
        {
            var nameReflection = GetNameMethod(param);
            GenerateHeaderAndFoot(nameReflection, idThread);
            return nameReflection;
        }

        protected void GeranateFoot(string stringSerie, int idThread)
        {
            GenerateHeaderAndFoot(stringSerie, idThread);
        }
    }

    public abstract class ClassTemplateImpl : ClassBaseImpl
    {
        protected IUseMethod Method;
        private string idSerie;

        public ClassTemplateImpl(IGenerateSerie gen, IUseMethod pMethod)
            : base(gen)
        {
            Method = pMethod;
            Level = (uint)pMethod.Level;
            pMethod.Implementation = (IGetBase)this;
            idSerie = Method.IdMethod;
        }

        protected override string IdSerie { get { return idSerie;} }
        
       
    }


}

