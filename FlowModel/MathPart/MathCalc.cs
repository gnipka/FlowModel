using FlowModel.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlowModel.OutputData;
using System.Diagnostics;

namespace FlowModel.MathPart
{
    public class MathCalc
    {
        private EmpiricalCoeff _EmpiricalCoef;
        private GeometricParameters _GeometricParameters;
        private MaterialProperties _MaterialProperties;
        private ParametersSolution _ParametersSolution;
        private VariableParameters _VariableParameters;

        public MathCalc(EmpiricalCoeff empiricalCoeff, GeometricParameters geometricParameters, MaterialProperties materialProperties, ParametersSolution parametersSolution, VariableParameters variableParameters)
        {
            _EmpiricalCoef = empiricalCoeff;
            _GeometricParameters = geometricParameters;
            _MaterialProperties = materialProperties;
            _ParametersSolution = parametersSolution;
            _VariableParameters = variableParameters;
        }

        private OutputParameter _InputParameter = new OutputParameter();
        public OutputParameter InputData { get; set; }

        private double Square(double number) => number * number;

        /// <summary>
        /// Объемный расход потока материала
        /// </summary>
        private double _VolumeFlowRate;

        /// <summary>
        /// Расчет объемного расхода потока материала
        /// </summary>
        private void GetVolumeFlowRate()
        {
            _VolumeFlowRate = ((_GeometricParameters.Depth * _GeometricParameters.Width * _VariableParameters.CoverSpeed) / 2) * (0.125 * Square(_GeometricParameters.Depth / _GeometricParameters.Width) -( 0.625 * _GeometricParameters.Depth / _GeometricParameters.Width) + 1);
        }

        /// <summary>
        /// Удельный тепловой поток альфа
        /// </summary>
        private double _SpecificHeatFluxAlpha;

        /// <summary>
        /// Расчет удельного теплового потока альфа
        /// </summary>
        private void GetSpecificHeatFluxAlpha()
        {
            _SpecificHeatFluxAlpha = _GeometricParameters.Width * _EmpiricalCoef.HeatTransferCoeff * (1 / _EmpiricalCoef.TemperatureCoeffViscosity - _VariableParameters.CoverTemperature + _EmpiricalCoef.CastingTemperature);
        }

        // <summary>
        /// Удельный тепловой поток альфа
        /// </summary>
        private double _SpecificHeatFluxGamma;

        /// <summary>
        /// Расчет удельного теплового потока альфа
        /// </summary>
        private void GetSpecificHeatFluxGamma()
        {
            _SpecificHeatFluxGamma = _GeometricParameters.Depth * _GeometricParameters.Width * _EmpiricalCoef.ConsistencyCoeff * Math.Pow(_VariableParameters.CoverSpeed / _GeometricParameters.Depth, _EmpiricalCoef.CurrentIndex + 1);
        }

        /// <summary>
        /// Для упрощения расчетов вычислена часть из уравнения
        /// </summary>
        private double _FirstPartCalc;
        private void GetFirstPartCalc()
        {
            _FirstPartCalc = (_EmpiricalCoef.TemperatureCoeffViscosity * _SpecificHeatFluxGamma + _GeometricParameters.Width * _EmpiricalCoef.HeatTransferCoeff) / (_EmpiricalCoef.TemperatureCoeffViscosity * _SpecificHeatFluxAlpha);
        }

        /// <summary>
        /// Для упрощения расчетов вычислена часть из уравнения
        /// </summary>
        private double _SecondPartCalc;
        private void GetSecondPartCalc()
        {
            _SecondPartCalc = _SpecificHeatFluxAlpha / (_MaterialProperties.Density * _MaterialProperties.HeatCapacity * _VolumeFlowRate);
        }

        public void GetOutputParameters()
        {
            var sw = new Stopwatch();
            double z = 0; //координата по длине канала
            int i = 0; // движение по массиву

            int size = (int)(_GeometricParameters.Length / _ParametersSolution.Step);

            sw.Start();
            GetVolumeFlowRate();
            GetSpecificHeatFluxAlpha();
            GetSpecificHeatFluxGamma();
            GetFirstPartCalc();
            GetSecondPartCalc();

            _InputParameter = new OutputParameter();
            _InputParameter.ProcessStateParameters = new ProcessStateParameters[size + 1];

            while(Math.Round(z, 2) <= _GeometricParameters.Length)

            {
                ProcessStateParameters processStateParameter = new ProcessStateParameters();

                processStateParameter.Coordinate = Math.Round(z, 2);

                processStateParameter.Temperature = Math.Round(_EmpiricalCoef.CastingTemperature + (1 / _EmpiricalCoef.TemperatureCoeffViscosity) * Math.Log(_FirstPartCalc * (1 - Math.Exp(0 - _EmpiricalCoef.TemperatureCoeffViscosity * _SecondPartCalc * z)) + Math.Exp(_EmpiricalCoef.TemperatureCoeffViscosity * (_MaterialProperties.MeltingPoint - _EmpiricalCoef.CastingTemperature - _SecondPartCalc * z))), 2);

                processStateParameter.Viscosity = Math.Round(_EmpiricalCoef.ConsistencyCoeff * Math.Exp(0 - _EmpiricalCoef.TemperatureCoeffViscosity * (processStateParameter.Temperature - _EmpiricalCoef.CastingTemperature)) * Math.Pow(_VariableParameters.CoverSpeed / _GeometricParameters.Depth, _EmpiricalCoef.CurrentIndex - 1), 2);

                _InputParameter.ProcessStateParameters[i] = processStateParameter;

                i++;
                z += _ParametersSolution.Step;
            }

            _InputParameter.Efficiency = Math.Round(_MaterialProperties.Density * _VolumeFlowRate * 3600);

            sw.Stop();

            _InputParameter.TemperatureProduct = Math.Round(_InputParameter.ProcessStateParameters[_InputParameter.ProcessStateParameters.Length - 1].Temperature, 2);

            _InputParameter.ViscosityProduct = Math.Round(_InputParameter.ProcessStateParameters[_InputParameter.ProcessStateParameters.Length - 1].Viscosity, 2);

            _InputParameter.Time = sw.Elapsed;

            InputData = _InputParameter;
        }

    }
}
