namespace GeneticAlgorithmWEB.BLL
{
    // Класс отвечающий за хеширование паролей и их проверку.
    public class Encryption
    {
        // Размер соли.
        private readonly int saltSize = 12;
        // Размер хеша.
        private readonly int hashSize = 20;
        // Число итераций для задержки.
        private readonly int iterations = 10000;

        // Создание хеша пароля.
        // Результат представляет собой последовательно 
        // записанные соль и хеш пароля в виде массива байтов.
        public byte[] CreatePassword(string password)
        {
            // Создание "соли" для пароля 
            byte[] salt = new byte[saltSize];
            new RNGCryptoServiceProvider().GetBytes(salt);
            // Хеширование пароля с солью
            return HashPassword(password, salt);
        }

        private byte[] HashPassword(string password, byte[] salt)
        {
            // Генерация хеша.
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            // Выбор из хеша заданного числа байтов.
            byte[] hash = pbkdf2.GetBytes(hashSize);
            // Выделение памяти под результат.
            byte[] result = new byte[saltSize + hashSize];
            // КОпирование соли и хеша в единый массив данных.
            Array.Copy(salt, 0, result, 0, saltSize);
            Array.Copy(hash, 0, result, saltSize, hashSize);
            return result;
        }

        // Проверка верности пароля.
        // Передаются байты из базы данных и пароль для проверки.
        public bool CheckPassword(byte[] realPassword, string check) {
            // Выделение соли из массива байтов.
            byte[] salt = GetSaltFromHash(realPassword);
            // Хеширование пароля с сохраненной солью
            byte[] hashedPassword = HashPassword(check, salt);
            if (hashedPassword.Length != realPassword.Length) {
                throw new ArgumentException("Длина реального хеша не совпадет с длиной хеша из базы данных");
            }
            // ПОбайтовая проверка на соответствие пароля и хеша.
            for (int i = 0; i < hashedPassword.Length; i++) {
                if (hashedPassword[i] != realPassword[i]) {
                    return false;
                }
            }
            return true;
        }
        // Выделение хеша из массива байтов.
        private byte[] GetSaltFromHash(byte[] hash)
        {
            byte[] salt = new byte[saltSize];
            Array.Copy(hash, 0, salt, 0, saltSize);
            return salt;
        }

    }
}
