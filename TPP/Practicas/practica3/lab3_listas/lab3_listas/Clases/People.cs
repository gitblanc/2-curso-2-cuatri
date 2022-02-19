using System;

namespace Clases
{

    /// <summary>
    /// Person class used in different examples
    /// </summary>
    public class People
    {

        private String firstName, surname;

        public String FirstName
        {
            get { return firstName; }
        }
        public String Surname
        {
            get { return surname; }
        }

        private string idNumber;
        public string IDNumber
        {
            get { return idNumber; }
        }

        public override String ToString()
        {
            return String.Format("{0} {1}, with {2} ID number", firstName, surname, idNumber);
        }

        public People(String firstName, String surname, string idNumber)
        {
            this.firstName = firstName;
            this.surname = surname;
            this.idNumber = idNumber;
        }
    }
}

