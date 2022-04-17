namespace RC.Core.DomainObjects
{
    public class NationalId
    {
        public const int MaxLength = 11;
        public string Number { get; private set; }

        protected NationalId()
        {
        }

        public NationalId(string numero)
        {
            if (!IsValid(numero)) throw new DomainException("Invalid National Id");
            Number = numero;
        }

        public static bool IsValid(string nationalId)
        {
            nationalId = new string(nationalId.Where(char.IsDigit).ToArray());

            if (nationalId.Length > 11)
                return false;

            while (nationalId.Length != 11)
                nationalId = '0' + nationalId;

            var igual = true;
            for (var i = 1; i < 11 && igual; i++)
                if (nationalId[i] != nationalId[0])
                    igual = false;

            if (igual || nationalId == "12345678909")
                return false;

            var numeros = new int[11];

            for (var i = 0; i < 11; i++)
                numeros[i] = int.Parse(nationalId[i].ToString());

            var soma = 0;
            for (var i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            var resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }
    }
}