namespace Gerard.Model
{
    public class IssueNonLivingObject
    {
        public string JiraLink { get; set; }

        public string Number { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// № помещения
        /// </summary>
        public string RoomNumber { get; set; }

        /// <summary>
        /// Общая площадь
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// Начальная стоимость
        /// </summary>
        public string InitialCost { get; set; }

        /// <summary>
        /// Обеспечение заявки
        /// </summary>
        public string EnsureBid { get; set; }

        /// <summary>
        /// Назначение помещения
        /// </summary>
        public string RoomFunction { get; set; }
    }
}
