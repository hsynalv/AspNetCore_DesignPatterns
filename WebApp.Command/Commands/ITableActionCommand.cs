using Microsoft.AspNetCore.Mvc;

namespace WebApp.Command.Commands
{

    public interface ITableActionCommand
    {
        IActionResult Execute();
    }

    public enum EFileType
    {
        Excel = 1,
        Pdf = 2
    }
}
