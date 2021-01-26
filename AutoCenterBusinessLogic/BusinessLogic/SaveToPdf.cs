using AutoCenterBusinessLogic.HelperModels;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoCenterBusinessLogic.BusinessLogic
{
    class SaveToPdf
    {
        public static void CreateDoc(PdfInfoClient info)
        {
            Document document = new Document();
            DefineStyles(document);
            Section section = document.AddSection();
            Paragraph paragraph = section.AddParagraph(info.Title);
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = "NormalTitle";
            foreach (var order in info.Orders)
            {
                var orderLabel = section.AddParagraph("Заказ №" + order.Id + "от" + order.DateCreate);
                orderLabel.Style = "NormalTitle";
                var CarLabel = section.AddParagraph("Автомобили с комплектацией:");
                CarLabel.Style = "NormalTitle";

                foreach (var cars in order.OrderCars)
                {
                    var CarTable = document.LastSection.AddTable();
                    List<string> headerWidths2 = new List<string> { "1cm", "3cm", "3cm", "3cm", "3cm" };
                    foreach (var elem in headerWidths2)
                    {
                        CarTable.AddColumn(elem);
                    }
                    CreateRow(new PdfRowParameters
                    {
                        Table = CarTable,
                        Texts = new List<string> { "№", "Автомобиль", "Год выпуска", "Количество", "Цена" },
                        Style = "NormalTitle",
                        ParagraphAlignment = ParagraphAlignment.Center
                    });
                    CreateRow(new PdfRowParameters
                    {
                        Table = CarTable,
                        Texts = new List<string> { cars.CarId.ToString(), cars.CarName, cars.Year.ToString(), cars.Count.ToString(), cars.Price.ToString() },
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Left
                    });
                    foreach (var car in info.Cars)
                    {
                        if (car.Id != cars.CarId)
                        {
                            continue;
                        }

                        var SpareTable = document.LastSection.AddTable();
                        List<string> headerWidths3 = new List<string> { "1cm", "3cm", "3cm" };
                        foreach (var elem in headerWidths3)
                        {
                            SpareTable.AddColumn(elem);
                        }
                        CreateRow(new PdfRowParameters
                        {
                            Table = SpareTable,
                            Texts = new List<string> { "№", "Деталь", "Количество" },
                            Style = "NormalTitle",
                            ParagraphAlignment = ParagraphAlignment.Center
                        });

                        foreach (var det in car.CarSpares)
                        {
                            CreateRow(new PdfRowParameters
                            {
                                Table = SpareTable,
                                Texts = new List<string> { det.Key.ToString(), det.Value.Item1, det.Value.Item2.ToString() },
                                Style = "Normal",
                                ParagraphAlignment = ParagraphAlignment.Left
                            });

                        }
                        section.AddParagraph();
                    }
                }
            }
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            renderer.RenderDocument();
            renderer.PdfDocument.Save(info.FileName);
        }

        public static void CreateDoc(PdfInfo info)
        {
            Document document = new Document();
            DefineStyles(document);
            Section section = document.AddSection();
            Paragraph paragraph = section.AddParagraph(info.Title);
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = "NormalTitle";

            var servicesLabel = section.AddParagraph("Пополнения:");
            servicesLabel.Style = "NormalTitle";
            foreach (var reqs in info.Requests)
            {
                foreach (var req in reqs)
                {
                    var orderLabel = section.AddParagraph("Заявка" + req.RequestName + " от " + req.DateCreate.ToShortDateString());
                    orderLabel.Style = "NormalTitle";
                    orderLabel.Format.SpaceBefore = "1cm";
                    orderLabel.Format.SpaceAfter = "0,25cm";
                    var servicesLabel2 = section.AddParagraph("Детали:");
                    servicesLabel2.Style = "NormalTitle";
                    var reqTable = document.LastSection.AddTable();
                    List<string> headerWidths = new List<string> { "1cm", "3cm", "3cm" };
                    foreach (var elem in headerWidths)
                    {
                        reqTable.AddColumn(elem);
                    }
                    CreateRow(new PdfRowParameters
                    {
                        Table = reqTable,
                        Texts = new List<string> { "№", "Деталь", "Количество" },
                        Style = "NormalTitle",
                        ParagraphAlignment = ParagraphAlignment.Center
                    });
                    int i = 1;
                    int col = 0;
                    foreach (var Spare in req.SpareRequests)
                    {
                        CreateRow(new PdfRowParameters
                        {
                            Table = reqTable,
                            Texts = new List<string> { i.ToString(), Spare.Value.Item1, Spare.Value.Item2.ToString() },
                            Style = "Normal",
                            ParagraphAlignment = ParagraphAlignment.Left
                        });
                        i++;
                        col += Spare.Value.Item2;
                    }

                    CreateRow(new PdfRowParameters
                    {
                        Table = reqTable,
                        Texts = new List<string> { "", "Общее количество деталей:", col.ToString() },
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Left
                    });
                }
            }

            var CarLabel = section.AddParagraph("Автомобили с комплектацией:");
            CarLabel.Style = "NormalTitle";

            foreach (var car in info.Cars)
            {
                var CarTable = document.LastSection.AddTable();
                List<string> headerWidths2 = new List<string> { "1cm", "3cm", "3cm", "6cm", "4cm" };
                foreach (var elem in headerWidths2)
                {
                    CarTable.AddColumn(elem);
                }
                CreateRow(new PdfRowParameters
                {
                    Table = CarTable,
                    Texts = new List<string> { "№", "Авто", "Год выпуска", "Цена авто без комплектации", "Полная цена" },
                    Style = "NormalTitle",
                    ParagraphAlignment = ParagraphAlignment.Center
                });
                CreateRow(new PdfRowParameters
                {
                    Table = CarTable,
                    Texts = new List<string> { car.Id.ToString(), car.CarName, car.Year.ToString(), car.Price.ToString(), car.FullPrice.ToString() },
                    Style = "Normal",
                    ParagraphAlignment = ParagraphAlignment.Left
                });

                var SpareTable = document.LastSection.AddTable();
                List<string> headerWidths3 = new List<string> { "1cm", "3cm", "3cm", "3cm" };
                foreach (var elem in headerWidths3)
                {
                    SpareTable.AddColumn(elem);
                }
                CreateRow(new PdfRowParameters
                {
                    Table = SpareTable,
                    Texts = new List<string> { "№", "Деталь", "Количество", "Цена" },
                    Style = "NormalTitle",
                    ParagraphAlignment = ParagraphAlignment.Center
                });
                foreach (var det in car.CarSpares)
                {
                    CreateRow(new PdfRowParameters
                    {
                        Table = SpareTable,
                        Texts = new List<string> { det.Key.ToString(), det.Value.Item1, det.Value.Item2.ToString(), det.Value.Item3.ToString() },
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Left
                    });
                }
                section.AddParagraph();
            }

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            renderer.RenderDocument();
            renderer.PdfDocument.Save(info.FileName);
        }

        private static void DefineStyles(Document document)
        {
            Style style = document.Styles["Normal"];
            style.Font.Name = "Times New Roman";
            style.Font.Size = 14;
            style = document.Styles.AddStyle("NormalTitle", "Normal");
            style.Font.Bold = true;
        }
        private static void CreateRow(PdfRowParameters rowParameters)
        {
            Row row = rowParameters.Table.AddRow();
            for (int i = 0; i < rowParameters.Texts.Count; ++i)
            {
                FillCell(new PdfCellParameters
                {
                    Cell = row.Cells[i],
                    Text = rowParameters.Texts[i],
                    Style = rowParameters.Style,
                    BorderWidth = 0.5,
                    ParagraphAlignment = rowParameters.ParagraphAlignment
                });
            }
        }
        private static void FillCell(PdfCellParameters cellParameters)
        {
            cellParameters.Cell.AddParagraph(cellParameters.Text);
            if (!string.IsNullOrEmpty(cellParameters.Style))
            {
                cellParameters.Cell.Style = cellParameters.Style;
            }
            cellParameters.Cell.Borders.Left.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Right.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Top.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Bottom.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Format.Alignment = cellParameters.ParagraphAlignment;
            cellParameters.Cell.VerticalAlignment = VerticalAlignment.Center;
        }
    }
}