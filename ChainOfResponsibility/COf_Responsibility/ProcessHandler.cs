namespace ChainOfResponsibility.COf_Responsibility
{
    public abstract class ProcessHandler : IProcessHandler
    {

        private IProcessHandler nextProcessHandler;
        public virtual object Handler(object obj)
        {
            if(nextProcessHandler != null)
                return nextProcessHandler.Handler(obj);
            return null;
        }

        public IProcessHandler SetNext(IProcessHandler processHandler)
        {
            nextProcessHandler = processHandler;
            return nextProcessHandler;
        }
    }
}
