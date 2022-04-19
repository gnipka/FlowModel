using FlowModel.OutputData;
using FlowModel.Parameters;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace FlowModel
{
    public static class ExportExcel
    {
        private static Microsoft.Office.Interop.Excel.Application? _Excel;
        private static Workbook? _Workbook;
        private static Worksheet? _Worksheet;

        public static void Export(OutputParameter outputParameter, EmpiricalCoeff empiricalCoeff, GeometricParameters geometricParameters, MaterialProperties materialProperties, ParametersSolution parametersSolution, VariableParameters variableParameters)
        {
            _Excel = new Microsoft.Office.Interop.Excel.Application();
            _Workbook = _Excel.Workbooks.Add();
            //_Workbook = _Excel.Workbooks.Open(file.FullName);
            _Worksheet = (Worksheet)_Workbook.ActiveSheet;
            _Worksheet.Columns.AutoFit();
            _Worksheet.Columns.EntireColumn.ColumnWidth = 25;


            // Добавление названий столбцов
            _Worksheet.Range["A1"].Offset[0, 0].Value = "Координата по длине канала, м";
            _Worksheet.Range["A1"].Offset[0, 1].Value = "Температура, С";
            _Worksheet.Range["A1"].Offset[0, 2].Value = "Вязкость, Па*с";

            //прорисовка границ
            _Worksheet.Range["A1"].Offset[0, 0].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["A1"].Offset[0, 1].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["A1"].Offset[0, 2].Borders.LineStyle = XlAboveBelow.xlBelowAverage;


            for (int i = 1; i < outputParameter.ProcessStateParameters.Length + 1; i++)
            {
                //заполнение данными
                _Worksheet.Range["A1"].Offset[i, 0].Value = outputParameter.ProcessStateParameters[i - 1].Coordinate;
                _Worksheet.Range["A1"].Offset[i, 1].Value = outputParameter.ProcessStateParameters[i - 1].Temperature;
                _Worksheet.Range["A1"].Offset[i, 2].Value = outputParameter.ProcessStateParameters[i - 1].Viscosity;

                //прорисовка границ
                _Worksheet.Range["A1"].Offset[i, 0].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
                _Worksheet.Range["A1"].Offset[i, 1].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
                _Worksheet.Range["A1"].Offset[i, 2].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            }

            _Worksheet.Range["D1"].Value = "Критериальные показатели";
            _Worksheet.Range["D1"].Font.Bold = true;
            _Worksheet.Range["D1"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Range["D2"].Value = "Производительность канала, кг/ч";
            _Worksheet.Range["D2"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["E2"].Value = outputParameter.Efficiency;
            _Worksheet.Range["E2"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Range["D3"].Value = "Температура, С";
            _Worksheet.Range["D3"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["E3"].Value = outputParameter.TemperatureProduct;
            _Worksheet.Range["E3"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Range["D4"].Value = "Вязкость продукта канала, Па*с";
            _Worksheet.Range["D4"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["E4"].Value = outputParameter.ViscosityProduct;
            _Worksheet.Range["E4"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Range["D6"].Value = "Геометрические параметры";
            _Worksheet.Range["D6"].Font.Bold = true;
            _Worksheet.Range["D6"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["D7"].Value = "Ширина, м";
            _Worksheet.Range["D7"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["E7"].Value = geometricParameters.Width;
            _Worksheet.Range["E7"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["D8"].Value = "Длина, м";
            _Worksheet.Range["D8"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["E8"].Value = geometricParameters.Length;
            _Worksheet.Range["E8"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["D9"].Value = "Глубина, м";
            _Worksheet.Range["D9"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["E9"].Value = geometricParameters.Depth;
            _Worksheet.Range["E9"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Range["D11"].Value = "Параметры свойств материала";
            _Worksheet.Range["D11"].Font.Bold = true;
            _Worksheet.Range["D11"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["D12"].Value = "Плотность, кг/м^3";
            _Worksheet.Range["D12"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["E12"].Value = materialProperties.Density;
            _Worksheet.Range["E12"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["D13"].Value = "Удельная теплоемкость, Дж/(кг*С)";
            _Worksheet.Range["D13"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["E13"].Value = materialProperties.HeatCapacity;
            _Worksheet.Range["E13"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["D14"].Value = "Температура плавления, С";
            _Worksheet.Range["D14"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["E14"].Value = materialProperties.MeltingPoint;
            _Worksheet.Range["E14"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Range["D16"].Value = "Варьируемые параметры";
            _Worksheet.Range["D16"].Font.Bold = true;
            _Worksheet.Range["D16"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["D17"].Value = "Скорость крышки, м/с";
            _Worksheet.Range["D17"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["E17"].Value = variableParameters.CoverSpeed;
            _Worksheet.Range["E17"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["D18"].Value = "Температура крышки, С";
            _Worksheet.Range["D18"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["E18"].Value = variableParameters.CoverTemperature;
            _Worksheet.Range["E18"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Range["D20"].Value = "Эмпирические коэффициенты математической модели";
            _Worksheet.Range["D20"].Font.Bold = true;
            _Worksheet.Range["D20"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["D21"].Value = "Коэффициент консистенции при температуре приведения, Па*с^n";
            _Worksheet.Range["D21"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["E21"].Value = empiricalCoeff.ConsistencyCoeff;
            _Worksheet.Range["E21"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["D22"].Value = "Температурный коэффициент вязкости, 1/C";
            _Worksheet.Range["D22"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["E22"].Value = empiricalCoeff.TemperatureCoeffViscosity;
            _Worksheet.Range["E22"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["D23"].Value = "Температура приведения, С";
            _Worksheet.Range["D23"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["E23"].Value = empiricalCoeff.CastingTemperature;
            _Worksheet.Range["E23"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["D24"].Value = "Индекс течения";
            _Worksheet.Range["D24"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["E24"].Value = empiricalCoeff.CurrentIndex;
            _Worksheet.Range["E24"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["D25"].Value = "Коэффициент теплоотдачи от крышки канала к материалу, Вт/(м^2*C)";
            _Worksheet.Range["D25"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["E25"].Value = empiricalCoeff.HeatTransferCoeff;
            _Worksheet.Range["E25"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Range["D27"].Value = "Параметры метода решения модели";
            _Worksheet.Range["D27"].Font.Bold = true;
            _Worksheet.Range["D27"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["D28"].Value = "Шаг расчета по длине канала, м";
            _Worksheet.Range["D28"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["E28"].Value = parametersSolution.Step;
            _Worksheet.Range["E28"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            
            Microsoft.Office.Interop.Excel.ChartObjects xlCharts = (Microsoft.Office.Interop.Excel.ChartObjects)_Worksheet.ChartObjects(Type.Missing);
            Microsoft.Office.Interop.Excel.ChartObject myChart = (Microsoft.Office.Interop.Excel.ChartObject)xlCharts.Add(1000, 10, 300, 250);
            Microsoft.Office.Interop.Excel.Chart chartPage = myChart.Chart;
            object misValue = Missing.Value;

            SeriesCollection seriesCollection = (SeriesCollection)chartPage.SeriesCollection(Type.Missing);
            Series series = seriesCollection.NewSeries();
            series.XValues = _Worksheet.get_Range("A2", "A47");
            series.Values = _Worksheet.get_Range("B2", "B47");

            //chartPage.SetSourceData(chartRange, misValue);
            chartPage.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlXYScatterLines;
            chartPage.HasLegend = false;
            myChart.Name = "График зависимости температуры от времени";
        }

        public static void SaveAndClose(string fileName)
        {
            _Excel.Visible = false;
            _Workbook.Activate();
            _Workbook.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, Missing.Value,
    Missing.Value, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
    Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, true,
    Missing.Value, Missing.Value, Missing.Value);
            _Workbook.Close();
            _Excel.Quit();
        }

        public static void Save(string fileName)
        {
            _Excel.Visible = true;
            _Workbook.Activate();            
            _Workbook.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, Missing.Value,
    Missing.Value, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
    Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, true,
    Missing.Value, Missing.Value, Missing.Value);
        }
    }
}
