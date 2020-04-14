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
        public static List<T> Read<T>(string fullPath)
        {
            StringBuilder sb = new StringBuilder();
            var listToReturn = new List<T>();

            string sFileExtension = Path.GetExtension(fullPath).ToLower();
            ISheet sheet;

            using (var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
            {
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

                PropertyInfo[] properties = typeof(T).GetProperties();
                Dictionary<int, PropertyInfo> dictionaryOfColumns = new Dictionary<int, PropertyInfo>();
//                sb.Append("<table class='table'><tr>");
                for (int columnIndex = 0; columnIndex < cellCount; columnIndex++)
                {
                    ICell cell = headerRow.GetCell(columnIndex);
                    if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;

                    PropertyInfo property = properties
                        .Where(w => w.CustomAttributes
                                            .Any(x=>x.ConstructorArguments
                                                        .Where(ca=>ca.Value.Equals(cell.ToString())).Any())).FirstOrDefault();

                    if (property != null)
                        dictionaryOfColumns.Add(columnIndex, property);
//                    sb.Append("<th>" + cell.ToString() + "</th>");
                }


                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;
                    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;

                    object item = Activator.CreateInstance(typeof(T));

                    for (int cellIndex = row.FirstCellNum; cellIndex < cellCount; cellIndex++)
                    {
                        if (row.GetCell(cellIndex) != null)
                        {
                            if (dictionaryOfColumns.ContainsKey(cellIndex))
                            {
                                var objectColumn = item.GetType().GetProperty(dictionaryOfColumns[cellIndex].Name);
                                objectColumn.SetValue(item, row.GetCell(cellIndex).ToString());
                            }
                        }
                    }
                    listToReturn.Add((T)item);
                }
            }
            return listToReturn;
        }
        public static MemoryStream Create<T>(string path, string fileName, string sheetName, List<T> list)
        {
            if (list == null || list.Count <= 0) return null;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string sFileName = $"{fileName}_{System.DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";

            var memory = new MemoryStream();

            using (var fs = new FileStream(Path.Combine(path, sFileName), FileMode.Create, FileAccess.Write))
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
            using (var stream = new FileStream(Path.Combine(path, sFileName), FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            return memory;

        }
        public static void DeleteFile(string fullPath)
        {
            File.Delete(fullPath);
        }

    }

}
