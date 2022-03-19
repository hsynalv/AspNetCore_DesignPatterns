using ClosedXML.Excel;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ChainOfResponsibility.COf_Responsibility
{
    public class ExcelProcessHandler<T> : ProcessHandler
    {
        private DataTable GetTable(object obj)
        {
            DataTable table = new();

            var type = typeof(T);

            type.GetProperties().ToList().ForEach(p => table.Columns.Add(p.Name, p.PropertyType));

            var list = obj as List<T>;

            list.ForEach(x =>
            {
                var values = type.GetProperties().Select(PI => PI.GetValue(x, null)).ToArray();

                table.Rows.Add(values);
            });

            return table;
        }
        public override object Handler(object obj)
        {
            var wb = new XLWorkbook();
            var ds = new DataSet();

            ds.Tables.Add(GetTable(obj));

            wb.Worksheets.Add(ds);

            var excelMemoryStream = new MemoryStream();

            wb.SaveAs(excelMemoryStream);

            return base.Handler(excelMemoryStream);
        }
    }
}
