using System.Threading.Tasks;

namespace Common.Rcp
{
    public abstract class ServiceCommand
    {
        public abstract string Name { get; }
        public abstract Task<string> Execute(object jsonValue);
    }
}
