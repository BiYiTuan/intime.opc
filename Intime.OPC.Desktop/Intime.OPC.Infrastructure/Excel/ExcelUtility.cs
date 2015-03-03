using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace Intime.OPC.Infrastructure.Excel
{
    public static class ExcelUtility
    {
        /// <summary>
        /// Export to excel
        /// </summary>
        /// <typeparam name="T">Type argument</typeparam>
        /// <param name="enumerable">IEnumerable instance</param>
        /// <param name="columnDefinitions">Column definitions</param>
        /// <param name="reportName">Report Name</param>
        /// <param name="openAfterCreated">Open excel after created</param>
        public static void Export<T>(IEnumerable<T> enumerable, IList<ColumnDefinition<T>> columnDefinitions,string reportName, bool openAfterCreated = true)
        {
            var outputDir = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(outputDir,string.Format("{0}_{1}.xlsx", reportName, Guid.NewGuid()));

            FileInfo file = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(reportName);

                //Draw the head
                for (int i = 1; i <= columnDefinitions.Count;i++ )
                {
                    worksheet.Cells[1, i].Value = columnDefinitions[i-1].ColumnName;
                }

                //Draw the content
                int rowIndex = 2;
                foreach (var obj in enumerable)
                {
                    int columnIndex = 1;
                    foreach (var columnDefinition in columnDefinitions)
                    {
                        var propertyValue = columnDefinition.PropertySelector(obj);

                        worksheet.Cells[rowIndex, columnIndex].Value = propertyValue;
                        Format(worksheet.Cells[rowIndex, columnIndex], propertyValue);
                        columnIndex++;
                    }
                    rowIndex++;
                }

                //Format as table
                var tableRange = worksheet.Cells[1, 1, rowIndex - 1, columnDefinitions.Count];
                var table = worksheet.Tables.Add(tableRange,reportName);
                table.TableStyle = TableStyles.Light11;

                //Set the width from the content of the range
                worksheet.Cells.AutoFitColumns(0);

                //Set some document properties
                package.Workbook.Properties.Title = reportName;
                package.Workbook.Properties.Author = "Intime";
                package.Workbook.Properties.Comments = string.Format("{0} exported by Intime OPC", reportName);
                //Set some extended property values
                package.Workbook.Properties.Company = "银泰商业";
                package.Save();

                if (openAfterCreated)
                {
                    Process p = new Process();
                    p.StartInfo.FileName = @filePath;
                    p.Start();
                }
            }
        }

        private static void Format(ExcelRange range, dynamic propertyValue)
        {
            if (propertyValue is string)
            {
                range.Style.Numberformat.Format = "@";
            }
            else if (propertyValue is DateTime)
            {
                range.Style.Numberformat.Format = "yyyy/m/d h:mm; @";
            }
        }
    }
}
