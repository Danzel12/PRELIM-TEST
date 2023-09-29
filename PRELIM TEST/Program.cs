using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static Dictionary<string, string> encryptedMessages = new Dictionary<string, string>();
    static string encryptionCharset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    static string decryptionCharset = "zyxwvutsrqponmlkjihgfedcbaZYXWVUTSRQPONMLKJIHGFEDCBA";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("What would you like to do? (E for encrypt, D for decrypt, Q to quit)");
            string choice = Console.ReadLine().ToUpper();

            switch (choice)
            {
                case "E":
                    EncryptMessage();
                    break;
                case "D":
                    DecryptMessage();
                    break;
                case "Q":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select E, D, or Q.");
                    break;
            }
        }
    }

    // ... (the rest of your code remains the same)



    static void DecryptMessage()
    {
        Console.WriteLine("Enter the key to decrypt the message:");
        string key = Console.ReadLine();

        if (encryptedMessages.ContainsKey(key))
        {
            string encryptedMessage = encryptedMessages[key];
            string decryptedMessage = Decrypt(key, encryptedMessage); // Decrypt the message
            Console.WriteLine($"Decrypted Message: {decryptedMessage}");
            Console.WriteLine("The message is printed in the debug folder ;)");
        }
        else
        {
            Console.WriteLine("Key not found. Unable to decrypt.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }


    static void EncryptMessage()
    {
        Console.Clear();
        Console.WriteLine("What key would you like to set?");
        string key = Console.ReadLine();

        Console.WriteLine("What message would you like to say?");
        string message = Console.ReadLine();

        string encryptedMessage = Encrypt(key, message);

        // Save both the key and the encrypted message to a text file
        using (StreamWriter writer = File.AppendText("encrypted_messages.txt"))
        {
            writer.WriteLine($"Key: {key}, Encrypted Message: {encryptedMessage}");
        }

        encryptedMessages[key] = encryptedMessage;

        Console.WriteLine("Message encrypted successfully.");
        Console.Clear();

        Console.WriteLine("Nice! Press any key to continue...");
        Console.ReadKey();
    }


    static string Encrypt(string key, string message)
    {
        string uniqueKeyChars = new string(key.Distinct().ToArray());

        // Create the encryption key by combining uniqueKeyChars and the remaining characters in encryptionCharset
        string encryptionKey = uniqueKeyChars + new string(encryptionCharset.Except(uniqueKeyChars).ToArray());

        Dictionary<char, char> encryptionMap = new Dictionary<char, char>();
        for (int i = 0; i < encryptionCharset.Length; i++)
        {
            encryptionMap[encryptionCharset[i]] = encryptionKey[i];
        }

        string encryptedMessage = "";

        foreach (char c in message)
        {
            if (Char.IsLetter(c))
            {
                char encryptedChar = encryptionMap[c];
                encryptedMessage += encryptedChar;
            }
            else
            {
                encryptedMessage += c;
            }
        }

        return encryptedMessage;
    }

    static string Decrypt(string key, string encryptedMessage)
    {
        string uniqueKeyChars = new string(key.Distinct().ToArray());

        // Create the decryption key by combining uniqueKeyChars and the remaining characters in decryptionCharset
        string decryptionKey = uniqueKeyChars + new string(decryptionCharset.Except(uniqueKeyChars).ToArray());

        Dictionary<char, char> decryptionMap = new Dictionary<char, char>();
        for (int i = 0; i < decryptionCharset.Length; i++)
        {
            decryptionMap[decryptionCharset[i]] = decryptionKey[i];
        }

        string decryptedMessage = "";

        foreach (char c in encryptedMessage)
        {
            if (Char.IsLetter(c))
            {
                char decryptedChar = decryptionMap[c];
                decryptedMessage += decryptedChar;
            }
            else
            {
                decryptedMessage += c;
            }
        }

        return decryptedMessage;
    }
}
