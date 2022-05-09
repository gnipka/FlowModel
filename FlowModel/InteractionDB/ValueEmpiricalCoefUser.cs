namespace FlowModel.InteractionDB
{
    internal class ValueEmpiricalCoefUser
    {
        /// <summary>
        /// ID значения эмпирического коэффициента
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Наименование материала
        /// </summary>
        public string Name_material { get; set; }

        /// <summary>
        /// Наименование эмпирического коэффициента
        /// </summary>
        public string Name_empirical_coef { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public double Value_empirical_coef { get; set; }
    }
}
