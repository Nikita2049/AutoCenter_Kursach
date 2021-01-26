using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using AutoCenterBusinessLogic.HelperModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoCenterBusinessLogic.BusinessLogic
{
    static class SaveToWord
    {
        public static void CreateDoc(WordInfo info)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(info.FileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());
                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<string> { info.Title },
                    TextProperties = new WordParagraphProperties
                    {
                        Bold = true,
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));
                Table table = new Table();
                TableProperties tblProp = new TableProperties(
                    new TableBorders(
                        new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                        new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                        new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                        new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                        new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                        new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 }
                    )
                );
                table.AppendChild<TableProperties>(tblProp);
                TableRow headerRow = new TableRow();
                TableCell headerNameCell = new TableCell(new Paragraph(new Run(new Text(" Название заявки "))));
                TableCell headerDateCell = new TableCell(new Paragraph(new Run(new Text(" Дата создания"))));
                TableCell headerSpareCell = new TableCell(new Paragraph(new Run(new Text(" Название детали"))));
                TableCell headerCountCell = new TableCell(new Paragraph(new Run(new Text(" Количество "))));
                headerRow.Append(headerNameCell);
                headerRow.Append(headerDateCell);
                headerRow.Append(headerSpareCell);
                headerRow.Append(headerCountCell);
                table.Append(headerRow);
                int i = 0;
                string name = "";
                foreach (var req in info.SpareRequests)
                {
                    if (name != req.RequestName)
                    {
                        if (i != 0)
                        {
                            TableRow service3Row = new TableRow();
                            TableCell nul5Cell = new TableCell(new Paragraph(new Run(new Text())));
                            service3Row.Append(nul5Cell);
                            TableCell nul6Cell = new TableCell(new Paragraph(new Run(new Text())));
                            service3Row.Append(nul6Cell);
                            TableCell itCell = new TableCell(new Paragraph(new Run(new Text(" Итоговое количество: "))));
                            TableCell coCell = new TableCell(new Paragraph(new Run(new Text(i.ToString()))));
                            service3Row.Append(itCell);
                            service3Row.Append(coCell);
                            table.Append(service3Row);
                            i = 0;
                        }
                        TableRow serviceRow = new TableRow();
                        TableCell nameCell = new TableCell(new Paragraph(new Run(new Text(req.RequestName))));
                        TableCell dateCreateCell = new TableCell(new Paragraph(new Run(new Text(req.DateCreate.ToString()))));
                        serviceRow.Append(nameCell);
                        serviceRow.Append(dateCreateCell);
                        TableCell nul4Cell = new TableCell(new Paragraph(new Run(new Text())));
                        serviceRow.Append(nul4Cell);
                        TableCell nul3Cell = new TableCell(new Paragraph(new Run(new Text())));
                        serviceRow.Append(nul3Cell);
                        table.Append(serviceRow);
                    }
                    TableRow service2Row = new TableRow();
                    TableCell nulCell = new TableCell(new Paragraph(new Run(new Text())));
                    service2Row.Append(nulCell);
                    TableCell nul2Cell = new TableCell(new Paragraph(new Run(new Text())));
                    service2Row.Append(nul2Cell);
                    TableCell SpareCell = new TableCell(new Paragraph(new Run(new Text(req.SpareName))));
                    TableCell countCell = new TableCell(new Paragraph(new Run(new Text(req.Count.ToString()))));
                    i += req.Count;
                    service2Row.Append(SpareCell);
                    service2Row.Append(countCell);
                    table.Append(service2Row);
                    name = req.RequestName;
                }
                TableRow service4Row = new TableRow();
                TableCell nul7Cell = new TableCell(new Paragraph(new Run(new Text())));
                service4Row.Append(nul7Cell);
                TableCell nul8Cell = new TableCell(new Paragraph(new Run(new Text())));
                service4Row.Append(nul8Cell);
                TableCell it2Cell = new TableCell(new Paragraph(new Run(new Text(" Итоговое количество: "))));
                TableCell co2Cell = new TableCell(new Paragraph(new Run(new Text(i.ToString()))));
                service4Row.Append(it2Cell);
                service4Row.Append(co2Cell);
                table.Append(service4Row);
                docBody.Append(table);
                docBody.AppendChild(CreateSectionProperties());
                wordDocument.MainDocumentPart.Document.Save();
            }
        }

        public static void CreateDoc(WordInfoAvto info)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(info.FileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());
                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<string> { info.Title },
                    TextProperties = new WordParagraphProperties
                    {
                        Bold = true,
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));
                Table table = new Table();
                TableProperties tblProp = new TableProperties(
                    new TableBorders(
                        new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                        new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                        new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                        new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                        new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                        new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 }
                    )
                );
                table.AppendChild<TableProperties>(tblProp);
                TableRow headerRow = new TableRow();
                TableCell headerNumberCell = new TableCell(new Paragraph(new Run(new Text("№"))));
                TableCell headerNameCell = new TableCell(new Paragraph(new Run(new Text("Название"))));
                TableCell headerDescCell = new TableCell(new Paragraph(new Run(new Text("Описание"))));
                TableCell headerPriceCell = new TableCell(new Paragraph(new Run(new Text("Цена"))));
                headerRow.Append(headerNumberCell);
                headerRow.Append(headerNameCell);
                headerRow.Append(headerDescCell);
                headerRow.Append(headerPriceCell);
                table.Append(headerRow);
                int i = 1;
                foreach (var car in info.Cars)
                {
                    TableRow serviceRow = new TableRow();
                    TableCell numberCell = new TableCell(new Paragraph(new Run(new Text(i.ToString()))));
                    TableCell nameCell = new TableCell(new Paragraph(new Run(new Text(car.CarName))));
                    TableCell descCell = new TableCell(new Paragraph(new Run(new Text(car.Year.ToString()))));
                    TableCell priceCell = new TableCell(new Paragraph(new Run(new Text(car.Price.ToString()))));
                    serviceRow.Append(numberCell);
                    serviceRow.Append(nameCell);
                    serviceRow.Append(descCell);
                    serviceRow.Append(priceCell);
                    table.Append(serviceRow);
                    i++;
                }
                docBody.Append(table);
                docBody.AppendChild(CreateSectionProperties());
                wordDocument.MainDocumentPart.Document.Save();
            }
        }

        private static SectionProperties CreateSectionProperties()
        {
            SectionProperties properties = new SectionProperties();
            PageSize pageSize = new PageSize
            {
                Orient = PageOrientationValues.Portrait
            };
            properties.AppendChild(pageSize);
            return properties;
        }

        private static Paragraph CreateParagraph(WordParagraph paragraph)
        {
            if (paragraph != null)
            {
                Paragraph docParagraph = new Paragraph();
                docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));
                foreach (var run in paragraph.Texts)
                {
                    Run docRun = new Run();
                    RunProperties properties = new RunProperties();
                    properties.AppendChild(new FontSize
                    {
                        Val = paragraph.TextProperties.Size
                    });
                    if (paragraph.TextProperties.Bold)
                    {
                        properties.AppendChild(new Bold());
                    }
                    docRun.AppendChild(properties);
                    docRun.AppendChild(new Text
                    {
                        Text = run,
                        Space = SpaceProcessingModeValues.Preserve
                    });
                    docParagraph.AppendChild(docRun);
                }
                return docParagraph;
            }
            return null;
        }

        private static ParagraphProperties
        CreateParagraphProperties(WordParagraphProperties paragraphProperties)
        {
            if (paragraphProperties != null)
            {
                ParagraphProperties properties = new ParagraphProperties();
                properties.AppendChild(new Justification()
                {
                    Val = paragraphProperties.JustificationValues
                });
                properties.AppendChild(new SpacingBetweenLines
                {
                    LineRule = LineSpacingRuleValues.Auto
                });
                properties.AppendChild(new Indentation());
                ParagraphMarkRunProperties paragraphMarkRunProperties = new ParagraphMarkRunProperties();
                if (!string.IsNullOrEmpty(paragraphProperties.Size))
                {
                    paragraphMarkRunProperties.AppendChild(new FontSize
                    {
                        Val = paragraphProperties.Size
                    });
                }
                if (paragraphProperties.Bold)
                {
                    paragraphMarkRunProperties.AppendChild(new Bold());
                }
                properties.AppendChild(paragraphMarkRunProperties);
                return properties;
            }
            return null;
        }
    }
}