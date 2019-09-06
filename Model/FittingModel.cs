

namespace Fittings.Model
{
    /// <summary>
    /// Модель комплектующего для справочника.
    /// </summary>
    public class FittingModel
    {
        /// <summary>
        /// Наименование фиттинга.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена фиттинга за 1 шт.
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// Конструктор по-умолчанию.
        /// </summary>
        public FittingModel() { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Название фиттинга.</param>
        /// <param name="price">Цена фиттинга (за 1 шт.).</param>
        public FittingModel(string name, string price)
        {
            this.Name = name;
            this.Price = price;
        }
    }
}
