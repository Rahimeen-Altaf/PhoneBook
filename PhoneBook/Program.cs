using System;
using System.IO;
using System.Threading;

internal class Program
{
    static string path = @"contacts.txt";

    private static void Main(string[] args)
    {
        string[,] Contact = new string[100, 2];
        int i = 0;
        LoadContacts(ref Contact, ref i);
        Menu(ref Contact, ref i);
    }

    public static void LoadContacts(ref string[,] Contact, ref int i)
    {
        if (File.Exists(path))
        {
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string name = sr.ReadLine();
                    string phoneNumber = sr.ReadLine();
                    if (i < 100)
                    {
                        Contact[i, 0] = name;
                        Contact[i, 1] = phoneNumber;
                        i++;
                    }
                }
            }
        }
    }

    public static void SaveContacts(string[,] Contact, int i)
    {
        using (StreamWriter sw = new StreamWriter(path))
        {
            for (int j = 0; j < i; j++)
            {
                sw.WriteLine(Contact[j, 0]);
                sw.WriteLine(Contact[j, 1]);
            }
        }
    }

    public static void Menu(ref string[,] Contact, ref int i)
    {
        string choice;
        do
        {
            Console.Clear();
            Console.WriteLine("\t===================================");
            Console.WriteLine("\t       WELCOME TO PHONE BOOK       ");
            Console.WriteLine("\t===================================");
            Console.WriteLine("\n\t------Main Menu------");
            Console.WriteLine("\t1. Add Contact");
            Console.WriteLine("\t2. Delete Contact");
            Console.WriteLine("\t3. Update Contact");
            Console.WriteLine("\t4. Search Contact");
            Console.WriteLine("\t5. View All Contacts");
            Console.WriteLine("\t6. Exit");
            Console.Write("\tEnter Your Choice: ");
            choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddContact(ref Contact, ref i);
                    break;
                case "2":
                    DeleteContactMenu(ref Contact, ref i);
                    break;
                case "3":
                    UpdateContactMenu(ref Contact);
                    break;
                case "4":
                    SearchContactMenu(ref Contact);
                    break;
                case "5":
                    ViewAllContacts(ref Contact, i);
                    break;
                case "6":
                    SaveContacts(Contact, i);
                    break;
                default:
                    Console.WriteLine("Unknown Choice");
                    break;
            }
            Console.Write("\n\tPress any key to continue...");
            Console.ReadKey();
        } while (choice != "6");
        Console.WriteLine("\n\tThank you For Using Phone Book.");
    }

    public static void AddContact(ref string[,] Contact, ref int i)
    {
        Console.Clear();
        Console.WriteLine("\n\t==========================");
        Console.WriteLine("\n\t       ADD CONTACT        ");
        Console.WriteLine("\n\t==========================");

        char ch = 'y';
        do
        {
            if (i < 100)
            {
                Console.Write("\nEnter Name: ");
                Contact[i, 0] = Console.ReadLine();
                Console.Write("Enter Phone Number: ");
                Contact[i, 1] = Console.ReadLine();
                i++;
                Console.WriteLine("\nContact Added!");
            }
            else
            {
                Console.WriteLine("\nContact list is full. Cannot add more.");
            }
            Console.Write("\nWant to Add Another Contact? (Y/N): ");
            ch = char.ToUpper(Console.ReadKey().KeyChar);
        } while (ch == 'Y');
    }

    public static void DeleteContactMenu(ref string[,] Contact, ref int i)
    {
        Console.Clear();
        Console.WriteLine("\n\t==========================");
        Console.WriteLine("\n\t      DELETE CONTACT      ");
        Console.WriteLine("\n\t==========================");
        Console.WriteLine("\t1. Delete Contact By Name");
        Console.WriteLine("\t2. Delete Contact By Phone Number");
        Console.Write("\tEnter Your Choice: ");
        string choice1 = Console.ReadLine();
        switch (choice1)
        {
            case "1":
                DeleteContactByName(ref Contact, ref i);
                break;
            case "2":
                DeleteContactByPhoneNumber(ref Contact, ref i);
                break;
            default:
                Console.WriteLine("Unknown Choice");
                break;
        }
    }
    public static void DeleteContactByName(ref string[,] Contact, ref int i)
    {
        Console.WriteLine("\n\t==========================");
        Console.WriteLine("\n\t DELETE CONTACT BY NAME   ");
        Console.WriteLine("\n\t==========================");

        char ch = 'y';
        do
        {
            bool Flag = false;
            Console.Write("\nEnter Name: ");
            string delete = Console.ReadLine();

            Console.Write("Searching..");
            for (int j = 0; j < 7; j++)
            {
                Console.Write(".");
                Thread.Sleep(300);
            }

            for (int j = 0; j < Contact.GetLength(0); j++)
            {
                if (delete == Contact[j, 0])
                {
                    Flag = true;
                    i--;
                    break;
                }
            }
            if (Flag)
            {
                string[,] Temp = new string[Contact.GetLength(0) - 1, 2];
                for (int j = 0, k = 0; j < Contact.GetLength(0); j++)
                {
                    if (Contact[j, 0] != delete)
                    {
                        Temp[k, 0] = Contact[j, 0];
                        Temp[k, 1] = Contact[j, 1];
                        k++;
                    }
                }
                Contact = Temp;
                Console.WriteLine("\nDeletion Completed!");
            }
            else
            {
                Console.WriteLine("\nName Not Found!");
            }

            Console.WriteLine("\n\tWant to Delete Another Contact?");
            Console.WriteLine("\tPress Y for Yes.");
            Console.WriteLine("\tPress n for No.");
            ch = char.Parse(Console.ReadLine());
        }
        while (ch == 'Y' || ch == 'y');
        SaveContacts(Contact, i);
    }
    public static void DeleteContactByPhoneNumber(ref string[,] Contact, ref int i)
    {
        Console.WriteLine("\n\t=============================");
        Console.WriteLine("\n\tDELETE CONTACT BY PHONE NUMBER ");
        Console.WriteLine("\n\t==============================");

        char ch = 'y';
        do
        {
            bool Flag = false;
            Console.Write("\nEnter Number: ");
            string delete = Console.ReadLine();

            Console.Write("Searching..");
            for (int j = 0; j < 7; j++)
            {
                Console.Write(".");
                Thread.Sleep(300);
            }

            for (int j = 0; j < Contact.GetLength(0); j++)
            {
                if (delete == Contact[j, 1])
                {
                    Flag = true;
                    i--;
                    break;
                }
            }
            if (Flag)
            {
                string[,] Temp = new string[Contact.GetLength(0) - 1, 2];
                for (int j = 0, k = 0; j < Contact.GetLength(0); j++)
                {
                    if (Contact[j, 1] != delete)
                    {
                        Temp[k, 1] = Contact[j, 1];
                        Temp[k, 0] = Contact[j, 0];
                        k++;
                    }
                }
                Contact = Temp;
                Console.WriteLine("\nDeletion Completed!");
            }
            else
            {
                Console.WriteLine("\nPhone Number Not Found!");
            }

            Console.WriteLine("\n\tWant to Delete Another Contact?");
            Console.WriteLine("\tPress Y for Yes.");
            Console.WriteLine("\tPress n for No.");
            ch = char.Parse(Console.ReadLine());
        }
        while (ch == 'Y' || ch == 'y');
    }
    public static void UpdateContactMenu(ref string[,] Contact)
    {
        Console.Clear();
        Console.WriteLine("\n\t==========================");
        Console.WriteLine("\n\t      UPDATE CONTACT      ");
        Console.WriteLine("\n\t==========================");
        Console.WriteLine("\t1. Update Contact By Name");
        Console.WriteLine("\t2. Update Contact By Phone Number");
        Console.Write("\tEnter Your Choice: ");
        string choice1 = Console.ReadLine();
        switch (choice1)
        {
            case "1":
                UpdateContactByName(ref Contact);
                break;
            case "2":
                UpdateContactByPhoneNumber(ref Contact);
                break;
            default:
                Console.WriteLine("Unknown Choice");
                break;
        }
    }

    public static void UpdateContactByName(ref string[,] Contact)
    {
        Console.WriteLine("\n\t==========================");
        Console.WriteLine("\n\t  UPDATE CONTACT BY NAME  ");
        Console.WriteLine("\n\t==========================");

        char ch = 'y';
        do
        {
            bool flag = false;
            Console.Write("\nEnter Name: ");
            string n = Console.ReadLine();
            for (int i = 0; i < Contact.GetLength(0); i++)
            {
                if (n == Contact[i, 0])
                {
                    flag = true;
                    Console.Write("Edit Name: ");
                    Contact[i, 0] = Console.ReadLine();
                    Console.WriteLine("\nContact Updated!");
                    Console.WriteLine("Name: {0}", Contact[i, 0]);
                    Console.WriteLine("Phone Number: {0}", Contact[i, 1]);
                }
            }
            if (!flag)
            {
                Console.WriteLine("Contact Not Found!");
            }
            Console.WriteLine("\n\tWant to Update Another Contact?");
            Console.WriteLine("\tPress Y for Yes.");
            Console.WriteLine("\tPress n for No.");
            ch = char.Parse(Console.ReadLine());
        }
        while (ch == 'y' || ch == 'Y');
    }
    public static void UpdateContactByPhoneNumber(ref string[,] Contact)
    {
        Console.WriteLine("\n\t================================");
        Console.WriteLine("\n\t UPDATE CONTACT BY PHONE NUMBER ");
        Console.WriteLine("\n\t================================");

        char ch = 'y';
        do
        {
            bool flag = false;
            Console.Write("\nEnter Phone Number: ");
            string n = Console.ReadLine();
            for (int i = 0; i < Contact.GetLength(0); i++)
            {
                if (n == Contact[i, 1])
                {
                    flag = true;
                    Console.Write("Enter Phone Number: ");
                    Contact[i, 1] = Console.ReadLine();
                    Console.WriteLine("\nContact Updated!");
                    Console.WriteLine("Name: {0}", Contact[i, 0]);
                    Console.WriteLine("Phone Number: {0}", Contact[i, 1]);
                }
            }
            if (!flag)
            {
                Console.WriteLine("Contact Not Found!");
            }
            Console.WriteLine("\n\tWant to Update Another Contact?");
            Console.WriteLine("\tPress Y for Yes.");
            Console.WriteLine("\tPress n for No.");
            ch = char.Parse(Console.ReadLine());
        }
        while (ch == 'y' || ch == 'Y');
    }
    public static void SearchContactMenu(ref string[,] Contact)
    {
        Console.Clear();
        Console.WriteLine("\n\t==========================");
        Console.WriteLine("\n\t      SEARCH CONTACT      ");
        Console.WriteLine("\n\t==========================");
        Console.WriteLine("\t1. Search Contact By Name");
        Console.WriteLine("\t2. Search Contact By Phone Number");
        Console.Write("\tEnter Your Choice: ");
        string choice1 = Console.ReadLine();
        switch (choice1)
        {
            case "1":
                SearchContactByName(ref Contact);
                break;
            case "2":
                SearchContactByPhoneNumber(ref Contact);
                break;
            default:
                Console.WriteLine("Unknown Choice");
                break;
        }
    }
    public static void SearchContactByName(ref string[,] Contact)
    {
        Console.WriteLine("\n\t==========================");
        Console.WriteLine("\n\t  SEARCH CONTACT BY NAME  ");
        Console.WriteLine("\n\t==========================");

        char ch = 'y';
        do
        {
            bool flag = false;
            Console.Write("Enter Name: ");
            string n = Console.ReadLine();

            Console.Write("Searching..");
            for (int j = 0; j < 7; j++)
            {
                Console.Write(".");
                Thread.Sleep(300);
            }

            for (int i = 0; i < Contact.GetLength(0); i++)
            {
                if (n == Contact[i, 0])
                {
                    flag = true;
                    Console.WriteLine("\nContact Found!");
                    Console.WriteLine("\nName: {0}", Contact[i, 0]);
                    Console.WriteLine("Phone Number: {0}", Contact[i, 1]);
                }
            }
            if (!flag)
            {
                Console.WriteLine("\nContact Not Found!");
            }
            Console.WriteLine("\n\tWant to Search Another Contact?");
            Console.WriteLine("\tPress Y for Yes.");
            Console.WriteLine("\tPress n for No.");
            ch = char.Parse(Console.ReadLine());
        }
        while (ch == 'y' || ch == 'Y');
    }
    public static void SearchContactByPhoneNumber(ref string[,] Contact)
    {
        Console.WriteLine("\n\t================================");
        Console.WriteLine("\n\t SEARCH CONTACT BY PHONE NUMBER ");
        Console.WriteLine("\n\t================================");

        char ch = 'y';
        do
        {
            bool flag = false;
            Console.Write("\nEnter Phone Number: ");
            string n = Console.ReadLine();

            Console.Write("Searching..");
            for (int j = 0; j < 7; j++)
            {
                Console.Write(".");
                Thread.Sleep(300);
            }

            for (int i = 0; i < Contact.GetLength(0); i++)
            {
                if (n == Contact[i, 1])
                {
                    flag = true;
                    Console.WriteLine("\nContact Found!");
                    Console.WriteLine("\nName: {0}", Contact[i, 0]);
                    Console.WriteLine("Phone Number: {0}", Contact[i, 1]);
                }
            }
            if (!flag)
            {
                Console.WriteLine("\nContact Not Found!");
            }
            Console.WriteLine("\n\tWant to Search Another Contact?");
            Console.WriteLine("\tPress Y for Yes.");
            Console.WriteLine("\tPress n for No.");
            ch = char.Parse(Console.ReadLine());
        }
        while (ch == 'y' || ch == 'Y');
    }
    public static void ViewAllContacts(ref string[,] Contact, int i)
    {
        Console.Clear();
        Console.WriteLine("\n\t==========================");
        Console.WriteLine("\n\t       ALL CONTACTS       ");
        Console.WriteLine("\n\t==========================");
        if (i == 0)
        {
            Console.WriteLine("No Contacts Saved.");
        }
        else
        {
            for (int j = 0; j < i; j++)
            {
                Console.WriteLine("\nContact - {0}", j + 1);
                Console.WriteLine("Name: {0}", Contact[j, 0]);
                Console.WriteLine("Phone Number: {0}", Contact[j, 1]);
            }
        }
    }
}