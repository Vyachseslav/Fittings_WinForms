
namespace Fittings.Model
{
    /// <summary>
    /// Модель для окна выбора фиттинга.
    /// </summary>
    public class SelectFittingModel
    {
        /// <summary>
        /// Id фиттинга.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название фиттинга.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена фиттинга за единицу.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Название группы.
        /// </summary>
        public string Group { get; set; }
    }
}
