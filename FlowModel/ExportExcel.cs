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
            

            _Worksheet.Range["A1"].ColumnWidth = 31;
            _Worksheet.Range["B1"].ColumnWidth = 18;
            _Worksheet.Range["C1"].ColumnWidth = 29;
            _Worksheet.Range["D1"].ColumnWidth = 15;
            _Worksheet.Range["E1"].ColumnWidth = 15;
            _Worksheet.Range["F1"].ColumnWidth = 29;
            _Worksheet.Range["G1"].ColumnWidth = 10;
            // Добавление названий столбцов
            _Worksheet.Range["C1"].Offset[0, 0].Value = "Координата по длине канала, м";
            _Worksheet.Range["C1"].Font.Bold = true;            
            _Worksheet.Range["C1"].Offset[0, 1].Value = "Температура, С";
            _Worksheet.Range["D1"].Font.Bold = true;
            _Worksheet.Range["C1"].Offset[0, 2].Value = "Вязкость, Па*с";
            _Worksheet.Range["E1"].Font.Bold = true;

            //прорисовка границ
            _Worksheet.Range["C1"].Offset[0, 0].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["C1"].Offset[0, 1].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["C1"].Offset[0, 2].Borders.LineStyle = XlAboveBelow.xlBelowAverage;


            for (int i = 1; i < outputParameter.ProcessStateParameters.Length + 1; i++)
            {
                //заполнение данными
                _Worksheet.Range["C1"].Offset[i, 0].Value = outputParameter.ProcessStateParameters[i - 1].Coordinate;
                _Worksheet.Range["C1"].Offset[i, 1].Value = outputParameter.ProcessStateParameters[i - 1].Temperature;
                _Worksheet.Range["C1"].Offset[i, 2].Value = outputParameter.ProcessStateParameters[i - 1].Viscosity;

                //прорисовка границ
                _Worksheet.Range["C1"].Offset[i, 0].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
                _Worksheet.Range["C1"].Offset[i, 1].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
                _Worksheet.Range["C1"].Offset[i, 2].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            }

            _Worksheet.Range["F1"].Value = "Критериальные показатели";
            _Worksheet.Range["F1"].Font.Bold = true;
            _Worksheet.Range["F1"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["G1"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Range["F2"].Value = "Производительность канала, кг/ч";
            _Worksheet.Range["F2"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["G2"].Value = outputParameter.Efficiency;
            _Worksheet.Range["G2"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Range["F3"].Value = "Температура, С";
            _Worksheet.Range["F3"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["G3"].Value = outputParameter.TemperatureProduct;
            _Worksheet.Range["G3"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Range["F4"].Value = "Вязкость продукта канала, Па*с";
            _Worksheet.Range["F4"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["G4"].Value = outputParameter.ViscosityProduct;
            _Worksheet.Range["G4"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Range[_Worksheet.Cells[1, 1], _Worksheet.Cells[1, 2]].Merge();
            _Worksheet.Range[_Worksheet.Cells[1, 1], _Worksheet.Cells[1, 2]].Value = "Входные параметры";
            _Worksheet.Range[_Worksheet.Cells[1, 1], _Worksheet.Cells[1, 2]].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range[_Worksheet.Cells[1, 1], _Worksheet.Cells[1, 2]].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            _Worksheet.Range[_Worksheet.Cells[1, 1], _Worksheet.Cells[1, 2]].Font.Bold = true;

            _Worksheet.Range["A2"].Value = "Геометрические параметры";
            _Worksheet.Range["A2"].Font.Bold = true;
            _Worksheet.Range["A2"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["B2"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["A3"].Value = "Ширина, м";
            _Worksheet.Range["A3"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["B3"].Value = geometricParameters.Width;
            _Worksheet.Range["B3"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["A4"].Value = "Длина, м";
            _Worksheet.Range["A4"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["B4"].Value = geometricParameters.Length;
            _Worksheet.Range["B4"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["A5"].Value = "Глубина, м";
            _Worksheet.Range["A5"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["B5"].Value = geometricParameters.Depth;
            _Worksheet.Range["B5"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Range["A7"].Value = "Параметры свойств материала";
            _Worksheet.Range["A7"].Font.Bold = true;
            _Worksheet.Range["A7"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["B7"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["A8"].Value = "Плотность, кг/м^3";
            _Worksheet.Range["A8"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["B8"].Value = materialProperties.Density;
            _Worksheet.Range["B8"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["A9"].Value = "Удельная теплоемкость, Дж/(кг*С)";
            _Worksheet.Range["A9"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["B9"].Value = materialProperties.HeatCapacity;
            _Worksheet.Range["B9"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["A10"].Value = "Температура плавления, С";
            _Worksheet.Range["A10"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["B10"].Value = materialProperties.MeltingPoint;
            _Worksheet.Range["B10"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Range["A12"].Value = "Варьируемые параметры";
            _Worksheet.Range["A12"].Font.Bold = true;
            _Worksheet.Range["A12"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["B12"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["A13"].Value = "Скорость крышки, м/с";
            _Worksheet.Range["A13"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["B13"].Value = variableParameters.CoverSpeed;
            _Worksheet.Range["B13"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["A14"].Value = "Температура крышки, С";
            _Worksheet.Range["A14"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["B14"].Value = variableParameters.CoverTemperature;
            _Worksheet.Range["B14"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Range["A16"].Value = "Эмпирические коэффициенты математической модели";
            _Worksheet.Range["A16"].Font.Bold = true;
            _Worksheet.Range["A16"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["B16"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["A17"].Value = "Коэффициент консистенции при температуре приведения, Па*с^n";
            _Worksheet.Range["A17"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["B17"].Value = empiricalCoeff.ConsistencyCoeff;
            _Worksheet.Range["B17"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["A17"].Value = "Температурный коэффициент вязкости, 1/C";
            _Worksheet.Range["A17"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["B18"].Value = empiricalCoeff.TemperatureCoeffViscosity;
            _Worksheet.Range["B18"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["A19"].Value = "Температура приведения, С";
            _Worksheet.Range["A19"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["B19"].Value = empiricalCoeff.CastingTemperature;
            _Worksheet.Range["B19"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["A20"].Value = "Индекс течения";
            _Worksheet.Range["A20"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["B20"].Value = empiricalCoeff.CurrentIndex;
            _Worksheet.Range["B20"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["A21"].Value = "Коэффициент теплоотдачи от крышки канала к материалу, Вт/(м^2*C)";
            _Worksheet.Range["A21"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["B21"].Value = empiricalCoeff.HeatTransferCoeff;
            _Worksheet.Range["B21"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;

            _Worksheet.Range["A23"].Value = "Параметры метода решения модели";
            _Worksheet.Range["A23"].Font.Bold = true;
            _Worksheet.Range["A23"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["B23"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["A24"].Value = "Шаг расчета по длине канала, м";
            _Worksheet.Range["A24"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;
            _Worksheet.Range["B24"].Value = parametersSolution.Step;
            _Worksheet.Range["B24"].Borders.LineStyle = XlAboveBelow.xlBelowAverage;           
            
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
