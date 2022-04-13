using FlowModel.Parameters;
using FlowModel.InputData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private InputParameter _InputParameter = new InputParameter();
        public InputParameter InputData { get; set; }

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
            _VolumeFlowRate = (_GeometricParameters.Depth * _GeometricParameters.Width * _VariableParameters.CoverSpeed) / 2 * (0.125 * Square(_GeometricParameters.Depth / _GeometricParameters.Width) - 0.625 * _GeometricParameters.Depth / _GeometricParameters.Width + 1);
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
            _FirstPartCalc = (_EmpiricalCoef.TemperatureCoeffViscosity * _SpecificHeatFluxGamma + _GeometricParameters.Width * _EmpiricalCoef.HeatTransferCoeff) / _EmpiricalCoef.TemperatureCoeffViscosity * _SpecificHeatFluxAlpha;
        }

        /// <summary>
        /// Для упрощения расчетов вычислена часть из уравнения
        /// </summary>
        private double _SecondPartCalc;
        private void GetSecondPartCalc()
        {
            _SecondPartCalc = _SpecificHeatFluxAlpha / (_MaterialProperties.Density * _MaterialProperties.HeatCapacity * _VolumeFlowRate);
        }

        public void GetInputParameters()
        {
            double z = 0; //координата по длине канала
            int i = 0; // движение по массиву

            int size = (int)(_GeometricParameters.Length / _ParametersSolution.Step);

            GetVolumeFlowRate();
            GetSpecificHeatFluxAlpha();
            GetSpecificHeatFluxGamma();
            GetFirstPartCalc();
            GetSecondPartCalc();

            _InputParameter = new InputParameter();
            _InputParameter.ProcessStateParameters = new ProcessStateParameters[size];

            while(z <= _GeometricParameters.Length)
            {
                ProcessStateParameters processStateParameter = new ProcessStateParameters();

                processStateParameter.Coordinate = z;

                processStateParameter.Temperature = _EmpiricalCoef.CastingTemperature + 1 / _EmpiricalCoef.TemperatureCoeffViscosity * Math.Log(_FirstPartCalc * (1 - Math.Exp(0 - _EmpiricalCoef.TemperatureCoeffViscosity * _SecondPartCalc * z)) + Math.Exp(_EmpiricalCoef.TemperatureCoeffViscosity * (_MaterialProperties.MeltingPoint - _EmpiricalCoef.CastingTemperature - _SecondPartCalc * z)));

                processStateParameter.Viscosity = _EmpiricalCoef.ConsistencyCoeff * Math.Exp(0 - _EmpiricalCoef.TemperatureCoeffViscosity * (processStateParameter.Temperature - _EmpiricalCoef.CastingTemperature)) * Math.Pow(_VariableParameters.CoverSpeed / _GeometricParameters.Depth, _EmpiricalCoef.CurrentIndex - 1);

                _InputParameter.ProcessStateParameters[i] = processStateParameter;

                i++;
                z += _ParametersSolution.Step;
            }

            _InputParameter.Efficiency = _MaterialProperties.Density * _VolumeFlowRate;

            _InputParameter.TemperatureProduct = _InputParameter.ProcessStateParameters[_InputParameter.ProcessStateParameters.Length - 1].Temperature;

            _InputParameter.ViscosityProduct = _InputParameter.ProcessStateParameters[_InputParameter.ProcessStateParameters.Length - 1].Viscosity;

            InputData = _InputParameter;
        }

    }
}
