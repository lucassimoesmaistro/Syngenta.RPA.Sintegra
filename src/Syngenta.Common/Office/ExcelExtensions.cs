using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Syngenta.Common.Office
{
    public static class ExcelExtensions
    {
        private readonly static string rootFolder = Path.Combine(Directory.GetCurrentDirectory(), "Excel");
        public static string Read(string fileName, string sheetName)
        {
            string folderName = "Upload";
            //string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(rootFolder, folderName);
            StringBuilder sb = new StringBuilder();
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            using (FileStream file = new FileStream($@"{rootFolder}\{fileName}", FileMode.Open, FileAccess.Read))
            {
                string sFileExtension = Path.GetExtension(file.Name).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file.Name);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }
                    IRow headerRow = sheet.GetRow(0); //Get Header Row
                    int cellCount = headerRow.LastCellNum;
                    sb.Append("<table class='table'><tr>");
                    for (int j = 0; j < cellCount; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        sb.Append("<th>" + cell.ToString() + "</th>");
                    }
                    sb.Append("</tr>");
                    sb.AppendLine("<tr>");
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                                sb.Append("<td>" + row.GetCell(j).ToString() + "</td>");
                        }
                        sb.AppendLine("</tr>");
                    }
                    sb.Append("</table>");
                }
            }
            return sb.ToString();
        }
        public static MemoryStream Create<T>(string fileName, string sheetName, List<T> list)
        {
            if (list == null || list.Count <= 0) return null;

            if (!Directory.Exists(rootFolder))
                Directory.CreateDirectory(rootFolder);

            string sFileName = $"{fileName}_{System.DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";

            var memory = new MemoryStream();

            using (var fs = new FileStream(Path.Combine(rootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet(sheetName);

                int coluna = 0;
                var row = excelSheet.CreateRow(coluna);
                foreach (PropertyInfo property in typeof(T).GetProperties())
                {
                    row.CreateCell(coluna).SetCellValue(property.Name);
                    coluna++;
                }

                int linha = 1;
                foreach (var item in list)
                {
                    row = excelSheet.CreateRow(linha);

                    coluna = 0;
                    foreach (PropertyInfo property in typeof(T).GetProperties())
                    {
                        try
                        {
                            if (property.PropertyType.Equals(typeof(int)))
                                row.CreateCell(coluna).SetCellValue(Convert.ToInt32(item.GetType().GetProperty(property.Name).GetValue(item, null)));
                            else if (property.PropertyType.Equals(typeof(long)))
                                row.CreateCell(coluna).SetCellValue(Convert.ToInt64(item.GetType().GetProperty(property.Name).GetValue(item, null)));
                            else if (property.PropertyType.Equals(typeof(DateTime)))
                            {
                                DateTime dateV;
                                DateTime.TryParse(item.GetType().GetProperty(property.Name).GetValue(item, null).ToString(), out dateV);
                                if (dateV != DateTime.MinValue)
                                {
                                    ICellStyle _dateCellStyle = workbook.CreateCellStyle();
                                    _dateCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("dd/MM/yyyy");

                                    ICell cell = row.CreateCell(coluna);
                                    cell.SetCellValue(dateV);
                                    cell.CellStyle = _dateCellStyle;
                                }
                                else
                                    row.CreateCell(coluna).SetCellValue("");
                            }
                            else if (property.PropertyType.Equals(typeof(string)))
                                row.CreateCell(coluna).SetCellValue(item.GetType().GetProperty(property.Name).GetValue(item, null).ToString());
                            else if (property.PropertyType.Equals(typeof(bool)))
                                row.CreateCell(coluna).SetCellValue(Convert.ToBoolean(item.GetType().GetProperty(property.Name).GetValue(item, null)));
                            else
                                row.CreateCell(coluna).SetCellValue(item.GetType().GetProperty(property.Name).GetValue(item, null).ToString());
                        }
                        catch
                        {
                            //TODO: tratar Exceção
                            //row.CreateCell(coluna).SetCellValue("erro");
                        }

                        coluna++;
                    }
                    linha++;
                }

                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(rootFolder, sFileName), FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            return memory;

        }

        private static void DeleteFile()
        {
            System.Threading.Thread thread = new System.Threading.Thread(CheckOldFiles);
            thread.IsBackground = true;
            thread.Start();
        }

        private static void CheckOldFiles()
        {
            DirectoryInfo folder = new DirectoryInfo(rootFolder);
            FileInfo[] files = folder.GetFiles();
            Array.Sort(files, delegate (FileInfo a, FileInfo b) { return DateTime.Compare(a.CreationTime, b.CreationTime); });

            foreach (FileInfo arquivo in files)
            {
                if (arquivo.LastWriteTime.Date.ToUniversalTime() < DateTime.Now.Date.ToUniversalTime())
                    File.Delete(arquivo.FullName);
            }
        }

    }

}
