﻿namespace GeneticAlgorithmWEB.BLL
{
    // ����� ���������� �� ����������� ������� � �� ��������.
    public class Encryption
    {
        // ������ ����.
        private readonly int saltSize = 12;
        // ������ ����.
        private readonly int hashSize = 20;
        // ����� �������� ��� ��������.
        private readonly int iterations = 10000;

        // �������� ���� ������.
        // ��������� ������������ ����� ��������������� 
        // ���������� ���� � ��� ������ � ���� ������� ������.
        public byte[] CreatePassword(string password)
        {
            // �������� "����" ��� ������ 
            byte[] salt = new byte[saltSize];
            new RNGCryptoServiceProvider().GetBytes(salt);
            // ����������� ������ � �����
            return HashPassword(password, salt);
        }

        private byte[] HashPassword(string password, byte[] salt)
        {
            // ��������� ����.
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            // ����� �� ���� ��������� ����� ������.
            byte[] hash = pbkdf2.GetBytes(hashSize);
            // ��������� ������ ��� ���������.
            byte[] result = new byte[saltSize + hashSize];
            // ����������� ���� � ���� � ������ ������ ������.
            Array.Copy(salt, 0, result, 0, saltSize);
            Array.Copy(hash, 0, result, saltSize, hashSize);
            return result;
        }

        // �������� �������� ������.
        // ���������� ����� �� ���� ������ � ������ ��� ��������.
        public bool CheckPassword(byte[] realPassword, string check) {
            // ��������� ���� �� ������� ������.
            byte[] salt = GetSaltFromHash(realPassword);
            // ����������� ������ � ����������� �����
            byte[] hashedPassword = HashPassword(check, salt);
            if (hashedPassword.Length != realPassword.Length) {
                throw new ArgumentException("����� ��������� ���� �� �������� � ������ ���� �� ���� ������");
            }
            // ���������� �������� �� ������������ ������ � ����.
            for (int i = 0; i < hashedPassword.Length; i++) {
                if (hashedPassword[i] != realPassword[i]) {
                    return false;
                }
            }
            return true;
        }
        // ��������� ���� �� ������� ������.
        private byte[] GetSaltFromHash(byte[] hash)
        {
            byte[] salt = new byte[saltSize];
            Array.Copy(hash, 0, salt, 0, saltSize);
            return salt;
        }

    }
}
