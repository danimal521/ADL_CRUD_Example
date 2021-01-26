using System;
namespace CRUDExample
{
    class Program
    {
        static void Main(string[] args)
        {
            ADLCRUD.UploadFile(Guid.NewGuid().ToString() + ".txt", @"c:\site\Kudos.txt");
            //ADLCRUD.ReadFile("d38dc541-f753-4e8e-81be-b374ba9882c1.txt", @"c:\site\Kudos2.txt");


        }
    }
}
