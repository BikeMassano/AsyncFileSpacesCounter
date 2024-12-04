using AsyncTxtReader.Interfaces;

namespace AsyncTxtReader.Services
{
    class SpaceTxtCounter : ISpaceCounter
    {
        /// <summary>
        /// Метод не нуждается в асинхронности:
        /// 1. Недорогая операция.
        /// 2. Не является IO операцией
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public int CountSpaces(string text)
        {
            return text.Count(c => c == ' ');
        }
    }
}
