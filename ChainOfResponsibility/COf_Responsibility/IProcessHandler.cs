using System;

namespace ChainOfResponsibility.COf_Responsibility
{
    public interface IProcessHandler
    {
        IProcessHandler SetNext(IProcessHandler processHandler);
        Object Handler(Object obj);

    }
}
