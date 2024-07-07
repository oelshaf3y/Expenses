using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;
using Excel = Microsoft.Office.Interop.Excel;

namespace Expenses
{
    internal class Export
    {
        public List<Record> records { get; set; }
        string fullPathToFile { get; set; }
        public Export(List<Record> objects)
        {
            this.records = objects;
            SaveFileDialog dialog = new();
            dialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
            dialog.DefaultExt = "xlsx";
            dialog.Title = "Select a folder";
            dialog.AddExtension = true;

            // Show open folder dialog box
            bool? result = dialog.ShowDialog();

            // Process open folder dialog box results
            if (result == true)
            {
                // Get the selected folder
                fullPathToFile = dialog.FileName;
                //string folderNameOnly = dialog.SafeFolderName;
                exportData();
            }
        }
        private void exportData()
        {
            Excel.Application xlapp = new Excel.Application();
            xlapp.Visible = false;
            xlapp.DisplayAlerts = false;

            // Create a new workbook
            Excel.Workbook workbook = xlapp.Workbooks.Add(Type.Missing);
            Excel.Worksheet sheet = (Excel.Worksheet)workbook.Worksheets[1];
            sheet.Name = "Report";
            int listRow = 1;
            sheet.Cells[1, 1] = "Date";
            sheet.Cells[1, 2] = "Info";
            sheet.Cells[1, 3] = "Value";
            sheet.Cells[1, 4] = "List Row";
            for (int row = 2; row < records.Count + 2; row++)
            {
                Record record = records[row - 2];
                sheet.Cells[row, 1] = record.Date.ToString();
                sheet.Cells[row, 2] = record.Info.ToString();
                sheet.Cells[row, 3] = record.Value.ToString();
                if (record.GroceryList != null)
                {
                    if (listRow == 1)
                    {
                        sheet.Cells[listRow, 7] = "Info";
                        sheet.Cells[listRow, 8] = "Count";
                        sheet.Cells[listRow, 9] = "Price";
                        sheet.Cells[listRow, 10] = "Total";
                        listRow++;
                    }
                    sheet.Cells[row, 4] = listRow;
                    sheet.Cells[row, 5] = record.GroceryList.Name;
                    sheet.Cells[listRow, 7] = record.GroceryList.Name;
                    listRow++;
                    for (int i = 0; i < record.GroceryList.items.Count; i++)
                    {
                        listRow++;
                        ShopingItem item = record.GroceryList.items[i];
                        sheet.Cells[listRow, 7] = item.Name;
                        sheet.Cells[listRow, 8] = item.Count;
                        sheet.Cells[listRow, 9] = item.Value;
                        sheet.Cells[listRow, 10] = item.Count * item.Value;
                    }
                    listRow++;
                    sheet.Cells[listRow, 10] = record.GroceryList.Value;
                    listRow++;
                }
            }


            workbook.SaveAs(fullPathToFile);

            // Clean up
            workbook.Close(false, Type.Missing, Type.Missing);
            Marshal.ReleaseComObject(workbook);

            xlapp.Quit();
            Marshal.ReleaseComObject(xlapp);

            MessageBox.Show($"Excel file '{fullPathToFile}' created successfully.");
            //string jsonString;
            //JsonWriterOptions opt = new JsonWriterOptions
            //{
            //    Indented = true,
            //    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            //};
            //using (var stream = new MemoryStream())
            //{
            //    using (var writer = new Utf8JsonWriter(stream, opt))
            //    {
            //        JsonSerializer.Serialize(writer, records);
            //    }
            //    jsonString = Encoding.UTF8.GetString(stream.ToArray());
            //}
            //await File.WriteAllTextAsync(Path.Combine(fullPathToFolder, "records.json"), jsonString, Encoding.UTF8);
        }

    }
}
