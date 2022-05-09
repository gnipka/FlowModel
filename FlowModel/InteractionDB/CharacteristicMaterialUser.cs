namespace FlowModel.InteractionDB
{
    internal class CharacteristicMaterialUser
    {
        /// <summary>
        /// ID свойства материала
        /// </summary>
        public int ID_characteristic { get; set; }

        /// <summary>
        /// наименование свойства материала
        /// </summary>
        public string Name_characteristic { get; set; }

        /// <summary>
        /// ID единицы измерения
        /// </summary>
        public string Name_unit { get; set; }
    }
}
