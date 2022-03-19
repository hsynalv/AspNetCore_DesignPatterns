﻿using System.IO;
using System.IO.Compression;

namespace ChainOfResponsibility.COf_Responsibility
{
    public class ZipFileProcessHandler<T> : ProcessHandler
    {
        public override object Handler(object obj)
        {
            var excelMemoryStream = obj as MemoryStream;

            excelMemoryStream.Position = 0;

            using (var zipStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                {
                    var zipFile = archive.CreateEntry($"{typeof(T).Name}.xlsx");

                    using (var zipEntry = zipFile.Open())
                    {
                        excelMemoryStream.CopyTo(zipEntry);
                    }
                }
                return base.Handler(zipStream);
            }
        }
    }
}
