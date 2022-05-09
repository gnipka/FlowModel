namespace FlowModel.InteractionDB
{
    internal class ValueCharacteristicMaterialUser
    {
        /// <summary>
        /// ID для значения характеристики
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование материала
        /// </summary>
        public string Name_material { get; set; }

        /// <summary>
        /// Наименование харакстеристики материала
        /// </summary>
        public string Name_characteristic { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public double Value_characteristic { get; set; }
    }
}
