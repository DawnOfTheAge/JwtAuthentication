// See https://aka.ms/new-console-template for more information

using Password_Security;
using System.Text;

const int SALT_SIZE = 24; // size in bytes
const int HASH_SIZE = 24; // size in bytes
const int ITERATIONS = 100000; // number of pbkdf2 iterations

string password = "!passWord$";
byte[] bytesPassword = Encoding.ASCII.GetBytes(password);
byte[] bytesSalt = Utils.GenerateSalt(SALT_SIZE);

byte[] hash = Utils.GenerateHash(bytesPassword, bytesSalt, ITERATIONS, HASH_SIZE);

Console.WriteLine($"Password[{password}] Salt[{BitConverter.ToString(bytesSalt).Replace("-", string.Empty)}] Hash[{BitConverter.ToString(hash).Replace("-", string.Empty)}]");
Console.ReadKey();

