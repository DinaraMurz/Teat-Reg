using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;

namespace TestWorkRegistration
{
    public class User
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public string Password { get; set; }



        #region PhoneNumber

        public string DelNoDigits(string s)
        {
            //string Result = new string('\0', s.Length);
            //fixed (char* _PString = s)
            //{
            //    fixed (char* _PResult = Result)
            //    {
            //        char* PString, PResult; char c; int Len = 0;
            //        for (PString = _PString, PResult = _PResult; (c = *PString) != 0; PString++)
            //        {
            //            if ((c >= '0') && (c <= '9'))
            //            {
            //                *PResult++ = c;
            //                Len++;
            //            }
            //        }
            //        return Result.Substring(0, Len);
            //    }
            //}
            return "";
        }
        
        public String ReplaceCharInString(String str, int index, Char newSymb)
        {
            return str.Remove(index, 1).Insert(index, newSymb.ToString());
        }

        public bool IsPhoneNumberCorrect(string PhoneNumber)
        {
            string PhoneNumberNoDigit = DelNoDigits(PhoneNumber);

            if (PhoneNumberNoDigit.Length < 9)
            {
                return false;
            }

            Int64 PhoneNumberInt = Convert.ToInt64(PhoneNumberNoDigit);

            for (int i = 0; i < PhoneNumber.Length; i++)
            {
                if(PhoneNumber[0] == '+')
                {
                    PhoneNumberNoDigit = ReplaceCharInString(PhoneNumber, 0, Convert.ToChar(Convert.ToInt32(PhoneNumberNoDigit[0]) + 1));
                }
            }
            return true;
        }


        #endregion

        #region Email
        public bool IsEmailUnique (string Email)
        {
            return true;
        }

        public void EmailRegirtratition(User user, string fileName = "clients.txt")
        {
            Console.WriteLine("Введите свой email:");
            user.Email = Convert.ToString(Console.ReadLine());
            switch (user.IsEmailUnique(user.Email))
            {
                case true:
                    //FileStream file = new FileStream(fileName, FileMode.Append);
                    //StreamWriter f_fin = new StreamWriter(file);
                    //f_fin.Write(name + "\n");
                    //f_fin.Close();



                    break;
                case false:
                    Console.WriteLine("Введен неверный email");
                    break;
            }

        }
        #endregion

        public bool IsPasswordCorrect (string Password)
        {
            if (Password.Length >= 6)
                return true;
            else
                return false;
        }

        //public string ReturnUserStr(User user)
        //{
        //    string UserStr;
        //    UserStr = $"Email:{user.Email} PhoneNumber:{user.PhoneNumber} FullName:{user.FullName} email:{user.Email}"
        //    return UserStr
        //}

        public void EnterUser(User user)
        {

            Console.WriteLine("Введите");
            Console.WriteLine("Email:");
            user.Email = Convert.ToString(Console.ReadLine());

            Console.WriteLine("PhoneNumber:");
            user.PhoneNumber = Convert.ToString(Console.ReadLine());

            Console.WriteLine("FullName:");
            user.FullName = Convert.ToString(Console.ReadLine());

            Console.WriteLine("Age:");
            user.Age = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Password:");
            user.Password = Convert.ToString(Console.ReadLine());
            
        }

        public bool IsUserCorrect(User user)
        {
            if (IsEmailUnique(user.Email))
                return false;
            if (IsPasswordCorrect(user.Password))
                return false;
            if(IsPhoneNumberCorrect(user.PhoneNumber))
                return false;
            return true;
        }

        public void GetUser(List<User> listOfUser)
        {
            string path = "ListOfUser.txt";
            //FileStream outFile = File.Create(path);
            //XmlSerializer formatter = new XmlSerializer(GetType());
            //formatter.Serialize(outFile, listOfUser);

            FileStream fsdis = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryFormatter bfdis = new BinaryFormatter();
            listOfUser = (List<User>)bfdis.Deserialize(fsdis);
            fsdis.Close();
        }

        public void SaveUser(List<User> listOfUser)
        {
            string path = "ListOfUser.txt";
            //FileStream outFile = File.Create(path);
            //XmlSerializer formatter = new XmlSerializer(GetType());
            //formatter.Serialize(outFile, listOfUser);

            XmlSerializer ser = new XmlSerializer(typeof(BindingList<User>));
            FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            ser.Serialize(file, listOfUser);
            file.Close();
        }
    }
}
