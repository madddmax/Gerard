namespace Gerard.Model
{
    public class IssueLivingObject
    {
        public string JiraLink { get; set; }

        public string Number { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Этаж
        /// </summary>
        public string Floor { get; set; }

        /// <summary>
        /// № кв.
        /// </summary>
        public string FlatNumber { get; set; }

        /// <summary>
        /// Кол-во комнат
        /// </summary>
        public string RoomsCount { get; set; }

        /// <summary>
        /// Площадь
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// Начальная стоимость
        /// </summary>
        public string InitialCost { get; set; }

        /// <summary>
        /// Шаг аукциона
        /// </summary>
        public string AuctionStep { get; set; }
    }
}
