using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TourManagementSystem.ManagerView.Model;

namespace TourManagementSystem.Global.Model
{
    public static class GlobalFunction
    {
        /*
         * 
         */

        /*
         * Base64Encode to encode text by Base64
         */
        public static string Base64Encode(string plainText)
        {
            if (plainText != String.Empty)
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                return System.Convert.ToBase64String(plainTextBytes);
            }
            return String.Empty;
        }

        /*
         * Security Password with MD5
         */
        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        /*
         * Convert Image from byte[] to bitmap
         */
        public static BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        public static bool IsValidEmail(string email)
        {
            if (email.Trim().EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<bool> IsSendEmail(string text)
        {
            await Task.Delay(6000);
            string from = "pplthdt.uit.team3@gmail.com";
            string pass = "UIT@team3";
            string to = "19522074@gm.uit.edu.vn";
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(from);
                mail.To.Add(to);
                mail.Subject = "Feedback";
                mail.Body = string.Format("Feedback from TourX : {0}", text);

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(from, pass);
                    smtp.EnableSsl = true;
                    try
                    {
                        smtp.Send(mail);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        return false;
                    }
                }
            }
        }

        //Chuyển từ datagrid sang datatable
        public static DataTable ToDataTable<T>(List<T> items, List<string> headerList)
        {
            var dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            int index = 0;
            foreach (var prop in properties)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(headerList[index], type);
                index++;
            }
            foreach (var item in items)
            {
                var values = new object[properties.Length];
                for (var i = 0; i < properties.Length; i++)
                {
                    //inserting property values to data table rows
                    values[i] = properties[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check data table
            return dataTable;
        }

        public static DataTable DataGridtoDataTable(DataGrid dg)
        {
            dg.SelectAllCells();
            dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dg);
            dg.UnselectAllCells();
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            string[] Lines = result.Split(new string[] { "\r\n", "\n" },
            StringSplitOptions.None);
            string[] Fields;
            Fields = Lines[0].Split(new char[] { ',' });
            int Cols = Fields.GetLength(0);
            DataTable dt = new DataTable();
            //1st row must be column names; force lower case to ensure matching later on.
            for (int i = 0; i < Cols; i++)
                dt.Columns.Add(Fields[i].ToUpper(), typeof(string));
            DataRow Row;
            for (int i = 1; i < Lines.GetLength(0) - 1; i++)
            {
                Fields = Lines[i].Split(new char[] { ',' });
                Row = dt.NewRow();
                for (int f = 0; f < Cols; f++)
                {
                    Row[f] = Fields[f];
                }
                dt.Rows.Add(Row);
            }
            return dt;

        }

        //Lưu datagrid thàng pdf (name: UserName, title: Title for Header, message: message after doing this code)
        //List<T> DataGrid.ItemSource.Cast<T>();
        //headerList: list header
        public static void ExportPDF<T>(List<T> items, List<string> headerList, string title, string name, ref string message)
        {
            DataTable data = ToDataTable(items, headerList);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pdf Files|*.pdf";
            saveFileDialog.Title = "Save Pdf file";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                Document document = new Document(PageSize.A4);

                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();

                //Report Header
                BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font fntHead = new Font(bfntHead, 40, 1, BaseColor.GRAY);
                Paragraph prgHeading = new Paragraph();
                prgHeading.Alignment = Element.ALIGN_CENTER;
                prgHeading.Add(new Chunk(title, fntHead));
                document.Add(prgHeading);

                //Author
                Paragraph prgAuthor = new Paragraph();
                BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font fntAuthor = new Font(btnAuthor, 8, 2, BaseColor.BLACK);
                prgAuthor.Alignment = Element.ALIGN_RIGHT;
                prgAuthor.Add(new Chunk(String.Format("Author : {0}", name), fntAuthor));
                prgAuthor.Add(new Chunk("\nRun Date : " + DateTime.Now.ToShortDateString(), fntAuthor));
                document.Add(prgAuthor);

                //Add a line seperation
                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                document.Add(p);

                //Add line break
                document.Add(new Chunk("\n", fntHead));

                //Write the table
                PdfPTable table = new PdfPTable(data.Columns.Count);
                //Table header
                BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font fntColumnHeader = new Font(btnColumnHeader, 10, 1, BaseColor.WHITE);
                Font fntColumnData = new Font(btnColumnHeader, 10, 1, BaseColor.BLACK);
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    PdfPCell cell = new PdfPCell();
                    cell.BackgroundColor = BaseColor.GRAY;
                    String header = data.Columns[i].ColumnName.ToUpper();
                    string[] split_string = header.Split('_');
                    String name_temp = "";
                    foreach (var item in split_string)
                        name_temp = item + " ";

                    cell.AddElement(new Chunk(String.Format("{0}", name_temp), fntColumnHeader));
                    table.AddCell(cell);
                }
                //table Data
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    for (int j = 0; j < data.Columns.Count; j++)
                    {
                        DateTime date = DateTime.Now;
                        if (data.Rows[i][j].GetType() == date.GetType())
                        {
                            date = (DateTime)data.Rows[i][j];
                            PdfPCell cell_data = new PdfPCell();
                            string data_table = date.Day + "/" + date.Month + "/" + date.Year;
                            cell_data.AddElement(new Chunk(data_table, fntColumnData));
                            table.AddCell(cell_data);
                        }
                        else
                        {
                            PdfPCell cell_data = new PdfPCell();
                            string data_table = data.Rows[i][j].ToString();
                            cell_data.AddElement(new Chunk(data_table, fntColumnData));
                            table.AddCell(cell_data);
                        }
                    }
                }

                document.Add(table);
                document.Close();
                writer.Close();
                fs.Close();
                message = "Export data to " + saveFileDialog.FileName + " successful";
            }
        }

        //Lưu datagrid thành excel (title: Title for Header, message: message after doing this code)
        //List<T> DataGrid.ItemSource.Cast<T>();
        public static void ExportExcel<T>(List<T> items, List<string> headerList, string title, ref string message)
        {
            var data = ToDataTable(items, headerList);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files(.xlsx)| *.xlsx";
            saveFileDialog.Title = "Save Excel file";

            if (saveFileDialog.ShowDialog() == true)
            {
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    workbook.Worksheets.Add(data, title);
                    workbook.SaveAs(saveFileDialog.FileName);
                }
                message = "Export data to " + saveFileDialog.FileName + " successful";
            }
        }


    }
}
