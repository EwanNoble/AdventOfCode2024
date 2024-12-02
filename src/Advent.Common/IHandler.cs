using System.CommandLine;

namespace Advent.Common
{
    public interface IHandler
    {
        public Command CreateHandlerCommand();
    }
}