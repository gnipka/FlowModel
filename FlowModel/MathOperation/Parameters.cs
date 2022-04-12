using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowModel.MathOperation
{
    /// <summary>
    /// Геометрические параметры канала
    /// </summary>
    public struct GeometricParameters
    {
        /// <summary>
        /// Ширина
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Глубина
        /// </summary>
        public double Depth { get; set; }

        /// <summary>
        /// Длина
        /// </summary>
        public double Length { get; set; }
    }

    /// <summary>
    /// Свойства материала
    /// </summary>
    public struct MaterialProperties
    {
        /// <summary>
        /// Плотность
        /// </summary>
        public double Density { get; set; }

        /// <summary>
        /// Удельная теплоемкость
        /// </summary>
        public double HeatCapacity { get; set; }

        /// <summary>
        /// Температура плавления
        /// </summary>
        public double MeltingPoint { get; set; }
    }

    /// <summary>
    /// Варьируемые параметры
    /// </summary>
    public struct VariableParameters
    {
        /// <summary>
        /// Скорость крышки
        /// </summary>
        public double CoverSpeed { get; set; }

        /// <summary>
        /// Температура крышки
        /// </summary>
        public double CoverTemperature { get; set; }
    }

    /// <summary>
    /// Эмпирические коэффициенты
    /// </summary>
    public struct EmpiricalCoeff
    {
        /// <summary>
        /// Коэффициент консистенции при температуре приведения
        /// </summary>
        public double ConsistencyCoeff { get; set; }

        /// <summary>
        /// Температурный коэффициент вязкости
        /// </summary>
        public double TemperatureCoeffViscosity { get; set; }

        /// <summary>
        /// Температура приведения
        /// </summary>
        public double CastingTemperature { get; set; }

        /// <summary>
        /// Индекс течения
        /// </summary>
        public int CurrentIndex { get; set; }

        /// <summary>
        /// Коэффициент теплоотдачи от крышки канала к материалу
        /// </summary>
        public double HeatTransferCoeff { get; set; }
    }
}
