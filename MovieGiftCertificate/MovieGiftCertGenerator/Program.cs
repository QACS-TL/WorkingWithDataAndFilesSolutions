using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;


namespace MovieGiftCertGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //The following code reads movie details in from a csv file and adds the data into a Word table
            List<NameAndValue> namesAndValues = null;
            using (var sr = new StreamReader("NamesAndValues.csv"))
            using (var reader = new CsvReader(sr, CultureInfo.InvariantCulture))
            {
                namesAndValues = reader.GetRecords<NameAndValue>().ToList();
                //foreach (NameAndValue nav in namesAndValues)
                //{
                //    Console.WriteLine($"{nav.Name} is {nav.Value}");
                //}
            }

            // Read a template document, replace some text, and save as another document
            
            DateTime expiryDate = DateTime.Now.AddMonths(6); 

            int currentCertCount = 1;
            foreach (NameAndValue nav in namesAndValues)
            {
                string newDocName = $"MovieGiftVoucher{currentCertCount}.docx";
                File.Copy("MovieGiftVoucherTemplate.docx", newDocName, true);
                ChangeTextInCell(newDocName, 1, 2, $"Spend up to £{nav.Value}.00 at your local cinema!");
                ChangeTextInCell(newDocName, 3, 2, nav.Name);
                ChangeTextInCell(newDocName, 5, 2, expiryDate.ToShortDateString());
                currentCertCount++;
            }
        }

        public static void ChangeTextInCell(string filepath, int rowpos, int cellpos, string text)
        {
            using (WordprocessingDocument doc =
                WordprocessingDocument.Open(filepath, true))
            {
                // Find the first table in the document.
                Table table =
                doc.MainDocumentPart.Document.Body.Elements<Table>().First();
                int maxNumberOfColumns = 2;


                TableRow row = table.Elements<TableRow>().ElementAt(rowpos);
                TableCell cell = row.Elements<TableCell>().ElementAt(cellpos);

                // Find the first paragraph in the table cell and add one if necessary.
                if (cell.Elements<Paragraph>().Count() == 0)
                {
                    var para = new Paragraph();
                    cell.Append(para);
                }

                Paragraph p = cell.Elements<Paragraph>().First();

                if (p.InnerText == String.Empty)
                {
                    string newText = text;
                    p.RemoveAllChildren();
                    p.AppendChild(new Run(new Text(newText)));
                }
                // Find the first run in the paragraph.
                Run r = p.Elements<Run>().First();

                // Set the text for the run.
                Text t = r.Elements<Text>().First();
                t.Text = text;
            }
        }
    }
}
