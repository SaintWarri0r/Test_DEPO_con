﻿using Microsoft.VisualBasic.FileIO;
using System;
using System.Data;
using System.Data.SqlClient;




public class Company
{
    public string Company_Name { get; set; }
    public string Company_INN { get; set; }
    public string Company_Legal_Address { get; set; }
    public string Company_Actual_Address { get; set; }
}
public class Employee
{
    public string Employee_Company_ID { get; set; }
    public string Employee_Surname { get; set; }
    public string Employee_Name { get; set; }
    public string? Employee_Patronymic { get; set; }
    public string Employee_Date_Of_Birth { get; set; }
    public string Employee_Passport_Series { get; set; }
    public string Employee_Passport_Number { get; set; }
}

class Con_Test_DEPO
{
    
    public static void Main()
    {


        Console.SetCursorPosition((Console.WindowWidth - 7) / 2, Console.CursorTop);
        Console.WriteLine("TEST DB\n");

        //Проверка подключения к БД--------------------------------------------------------------------------------------------
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Test_DEPO_DB;Trusted_Connection=True;";
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        Console.WriteLine("Подключение открыто");
        // Вывод информации о подключении
        Console.WriteLine("Свойства подключения:");
        Console.WriteLine($"\tСтрока подключения: {connection.ConnectionString}");
        Console.WriteLine($"\tБаза данных: {connection.Database}");
        Console.WriteLine($"\tСервер: {connection.DataSource}");
        Console.WriteLine($"\tВерсия сервера: {connection.ServerVersion}");
        Console.WriteLine($"\tСостояние: {connection.State}");
        Console.WriteLine($"\tWorkstationld: {connection.WorkstationId}");
        connection.Close();
        //-------------------------------------------------------------------------------------------------------------

        Console.WriteLine("\n\nWhat did you want to do?\n------------------------------------------------------\n" +
                    "1 - Show list of companies\n" +
                    "2 - Show list of Employee\n" +
                    "3 - Add Company\n" +
                    "4 - Add Employee\n" +
                    "5 - Export DB\n" +
                    "6 - Import DB");

        while (true)
        {
            
            switch (Console.ReadLine())
            {
                case "1":// Вывод списка компаний
                    connection.Open();
                    string Show_Company_List = "SELECT Company_Name FROM Company";
                    SqlCommand Show_Company_command = new SqlCommand(Show_Company_List,connection);
                    SqlDataReader Show_Company_reader = Show_Company_command.ExecuteReader();
                    foreach (var row in Show_Company_reader)
                    {
                        string Company_NAME = Show_Company_reader.GetValue(0).ToString();
                        Console.WriteLine(Company_NAME);
                    }
                    connection.Close();
                    Console.WriteLine("\n\nWhat did you want to do?\n------------------------------------------------------\n" +
                     "1 - Show list of companies\n" +
                     "2 - Show list of Employee\n" +
                     "3 - Add Company\n" +
                     "4 - Add Employee\n" +
                     "5 - Export DB\n" +
                     "6 - Import DB");
                    break;

                case "2":// Вывод списка сотрудников
                    connection.Open();
                    string Show_Employee_List = "SELECT * FROM Employee";
                    SqlCommand Show_Employee_command = new SqlCommand(Show_Employee_List, connection);
                    SqlDataReader Show_Employee_reader = Show_Employee_command.ExecuteReader();
                    foreach (var row in Show_Employee_reader)
                    {
                        string Employee_Surname = Show_Employee_reader.GetValue(2).ToString();
                        string Employee_Name = Show_Employee_reader.GetValue(3).ToString();
                        string Employee_Patronymic = Show_Employee_reader.GetValue(4).ToString();
                        Console.WriteLine($"{Employee_Surname} {Employee_Name} {Employee_Patronymic}");

                    }
                    connection.Close();
                    Console.WriteLine("\n\nWhat did you want to do?\n------------------------------------------------------\n" +
                    "1 - Show list of companies\n" +
                    "2 - Show list of Employee\n" +
                    "3 - Add Company\n" +
                    "4 - Add Employee\n" +
                    "5 - Export DB\n" +
                    "6 - Import DB");
                    break;

                case "3":// Добавление новой компании

                    Company new_company = new Company();
                    bool stopper = false;
                    while (stopper == false)
                    {
                        Console.WriteLine("Введите название компании: ");
                        string temp_name = Console.ReadLine();
                        if (temp_name == "")
                        {
                            Console.WriteLine("ОШИБКА!");
                        }
                        else
                        {
                            new_company.Company_Name = temp_name;
                            stopper = true;
                        }
                    }
                    stopper = false;

                    while (stopper == false)
                    {
                        Console.WriteLine("Введите ИНН компании: ");
                        string temp_name = Console.ReadLine();
                        if (temp_name == "" || temp_name.Length < 10)
                        {
                            Console.WriteLine("ОШИБКА!");
                        }
                        else
                        {
                            new_company.Company_INN = temp_name;
                            stopper = true;
                        }
                    }
                    stopper = false;

                    while (stopper == false)
                    {
                        Console.WriteLine("Введите юр.адрес компании: ");
                        string temp_name = Console.ReadLine();
                        if (temp_name == "")
                        {
                            Console.WriteLine("ОШИБКА!");
                        }
                        else
                        {
                            new_company.Company_Legal_Address = temp_name;
                            stopper = true;
                        }
                    }
                    stopper = false;

                    while (stopper == false)
                    {
                        Console.WriteLine("Введите факт.адрес компании: ");
                        string temp_name = Console.ReadLine();
                        if (temp_name == "")
                        {
                            Console.WriteLine("ОШИБКА!");
                        }
                        else
                        {
                            new_company.Company_Actual_Address = temp_name;
                            stopper = true;
                        }
                    }
                    stopper = false;

                    connection.Open();
                    string Add_Company = "INSERT INTO Company " +
                        "(" +
                        "Company_Name," +
                        "Company_INN," +
                        "Company_Legal_Address," +
                        "Company_Actual_Address" +
                        ") VALUES" +
                        "(" +
                        $"N\'{new_company.Company_Name}\'," +
                        $"N\'{new_company.Company_INN}\'," +
                        $"N\'{new_company.Company_Legal_Address}\'," +
                        $"N\'{new_company.Company_Actual_Address}\'" +
                        ");";
                    SqlCommand ADD_Company_command = new SqlCommand(Add_Company, connection);
                    SqlDataReader ADD_Company_reader = ADD_Company_command.ExecuteReader();
                    connection.Close();
                    Console.WriteLine("\nA new company added sucesfull");
                    Console.WriteLine("\n\nWhat did you want to do?\n------------------------------------------------------\n" +
                    "1 - Show list of companies\n" +
                    "2 - Show list of Employee\n" +
                    "3 - Add Company\n" +
                    "4 - Add Employee\n" +
                    "5 - Export DB\n" +
                    "6 - Import DB");
                    break;

                case "4"://Добавление нового сотрудника
                   Employee new_employee = new Employee();
                    stopper = false;
                    while (stopper == false)
                    {
                        Console.WriteLine("Введите ID компании, в которой работает сотрудник: ");
                        string temp_name = Console.ReadLine();
                        if (temp_name == "")
                        {
                            Console.WriteLine("ОШИБКА!");
                        }
                        else
                        {
                            new_employee.Employee_Company_ID = temp_name;
                            stopper = true;
                        }
                    }
                    stopper = false;

                    while (stopper == false)
                    {
                        Console.WriteLine("Введите фамилию сотрудника: ");
                        string temp_name = Console.ReadLine();
                        if (temp_name == "")
                        {
                            Console.WriteLine("ОШИБКА!");
                        }
                        else
                        {
                            new_employee.Employee_Surname = temp_name;
                            stopper = true;
                        }
                    }
                    stopper = false;

                    while (stopper == false)
                    {
                        Console.WriteLine("Введите имя сотрудника: ");
                        string temp_name = Console.ReadLine();
                        if (temp_name == "")
                        {
                            Console.WriteLine("ОШИБКА!");
                        }
                        else
                        {
                            new_employee.Employee_Name = temp_name;
                            stopper = true;
                        }
                    }
                    stopper = false;

                    while (stopper == false)
                    {
                        Console.WriteLine("Введите отчество сотрудника: ");
                        string temp_name = Console.ReadLine();
                        if (temp_name == "")
                        {
                            new_employee.Employee_Patronymic = null;
                            stopper = true;
                        }
                        else
                        {
                            new_employee.Employee_Patronymic = temp_name;
                            stopper = true;
                        }
                    }
                    stopper = false;

                    while (stopper == false)
                    {
                        Console.WriteLine("Введите дату рождения сотрудника в формате YYYY.MM.DD : ");
                        string temp_name = Console.ReadLine();
                        if (DateTime.TryParse(temp_name, out DateTime result) == false)
                        {
                            Console.WriteLine("ОШИБКА!");
                        }
                        else
                        {
                            new_employee.Employee_Date_Of_Birth = temp_name;
                            stopper = true;
                        }
                    }
                    stopper = false;

                    while (stopper == false)
                    {
                        Console.WriteLine("Введите серию паспорта сотрудника: ");
                        string temp_name = Console.ReadLine();
                        
                        if (temp_name == "")
                        {
                            Console.WriteLine("ОШИБКА!");
                        }
                        else
                        {
                            new_employee.Employee_Passport_Series = temp_name;
                            stopper = true;
                        }
                    }
                    stopper = false;
                    while (stopper == false)
                    {
                        Console.WriteLine("Введите номер паспорта сотрудника ");
                        string temp_name = Console.ReadLine();
                        if (temp_name == "")
                        {
                            Console.WriteLine("ОШИБКА!");
                        }
                        else
                        {
                            new_employee.Employee_Passport_Number = temp_name;
                            stopper = true;
                        }
                    }
                    stopper = false;

                    connection.Open();
                    string Add_empoloyee = "INSERT INTO Employee " +
                        "(" +
                        "Employee_Company_ID ," +
                        "Employee_Surname ," +
                        "Employee_Name ," +
                        "Employee_Patronymic, " +
                        "Employee_Date_Of_Birth, " +
                        "Employee_Passport_Series, " +
                        "Employee_Passport_Number " +
                        ") VALUES" +
                        "(" +
                        $"N\'{new_employee.Employee_Company_ID}\'," +
                        $"N\'{new_employee.Employee_Surname}\'," +
                        $"N\'{new_employee.Employee_Name}\'," +
                        $"N\'{new_employee.Employee_Patronymic}\'," +
                        $"N\'{new_employee.Employee_Date_Of_Birth}\'," +
                        $"N\'{new_employee.Employee_Passport_Series}\'," +
                        $"N\'{new_employee.Employee_Passport_Number}\'" +
                        ");";
                    SqlCommand ADD_Employee_command = new SqlCommand(Add_empoloyee, connection);
                    SqlDataReader Add_empoloyee_reader = ADD_Employee_command.ExecuteReader();
                    connection.Close();
                    Console.WriteLine("\nA new employee added sucesfull");
                    Console.WriteLine("\n\nWhat did you want to do?\n------------------------------------------------------\n" +
                    "1 - Show list of companies\n" +
                    "2 - Show list of Employee\n" +
                    "3 - Add Company\n" +
                    "4 - Add Employee\n" +
                    "5 - Export DB\n" +
                    "6 - Import DB");
                    break;

                case "5"://Экспорт в формате csv
                    DateTime current_time = DateTime.Now;
                    var CSV_Timestamp = current_time.ToFileTimeUtc();
                    string path = @"C:\Users\SaintWarrior\Desktop\DEPO_test";
                    Console.WriteLine($"enter the path to save\ne.g {path}");
                    path = Console.ReadLine();
                    string Exporting_Table_Name = null;
                    Console.WriteLine("Which table do you want to export?\n" +
                        "1 - Employee\n" +
                        "2 - Company");
                    if (Console.ReadLine() == "1")
                    {
                        Exporting_Table_Name = "Employee";
                        path = Path.Combine(path,$"{Exporting_Table_Name}_" + CSV_Timestamp + ".csv");
                    }
                    else
                    {
                        Exporting_Table_Name = "Company";
                        path = Path.Combine(path,$"{Exporting_Table_Name}_" + CSV_Timestamp + ".csv");
                    }

                    //File.Create(path);
                    
                    connection.Open();

                    SqlCommand sqlCmd = new SqlCommand(
                        $"Select * from {Exporting_Table_Name}", connection);
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    StreamWriter sw = new StreamWriter(path,true,System.Text.Encoding.UTF8);
                    object[] output = new object[reader.FieldCount];

                    for (int i = 0; i < reader.FieldCount; i++)

                        output[i] = reader.GetName(i);

                    sw.WriteLine(string.Join(",", output));

                    while (reader.Read())
                    {
                        reader.GetValues(output);
                        sw.WriteLine(string.Join(",", output));
                    }
                    
                    sw.Close();
                    reader.Close();
                    connection.Close();
                    Console.WriteLine($"Table {Exporting_Table_Name} has been successfully saved at {path}");
                    Console.WriteLine("\n\nWhat did you want to do?\n------------------------------------------------------\n" +
                    "1 - Show list of companies\n" +
                    "2 - Show list of Employee\n" +
                    "3 - Add Company\n" +
                    "4 - Add Employee\n" +
                    "5 - Export DB\n" +
                    "6 - Import DB");
                    break;
                case "6"://Import CSV to DB
                    string Importing_Table_Name = null;
                    Console.WriteLine("which database do you want to import the data into?\n" +
                        "1 - Employee\n" +
                        "2 - Company");
                    if (Console.ReadLine() == "1")
                    {
                        Importing_Table_Name = "Employee";
                    }
                    else
                    {
                        Importing_Table_Name = "Company";
                    }
                    string base_path = @"C:\Users\SaintWarrior\Desktop\DEPO_test";
                    Console.WriteLine("Enter the name of the imported file");
                    string name_of_file = Console.ReadLine();
                    string import_path = Path.Combine(@base_path, name_of_file + ".csv");
                    DataTable csvData = new DataTable();

                    using (TextFieldParser csvReader = new TextFieldParser(import_path))
                    {
                        csvReader.SetDelimiters(new string[] { "," });
                        csvReader.HasFieldsEnclosedInQuotes = true;
                        string[] colFields = csvReader.ReadFields();
                        foreach (string column in colFields)
                        {
                            DataColumn datecolumn = new DataColumn(column);
                            datecolumn.AllowDBNull = true;
                            csvData.Columns.Add(datecolumn);
                        }
                        while (!csvReader.EndOfData)
                        {
                            string[] fieldData = csvReader.ReadFields();
                            //Making empty value as null
                            for (int i = 0; i < fieldData.Length; i++)
                            {
                                if (fieldData[i] == "")
                                {
                                    fieldData[i] = null;
                                }
                            }
                            csvData.Rows.Add(fieldData);
                        }
                    }
                    
                    connection.Open();
                    using (SqlBulkCopy s = new SqlBulkCopy(connection))
                    {
                        s.DestinationTableName = Importing_Table_Name;
                        foreach (var column in csvData.Columns)
                            s.ColumnMappings.Add(column.ToString(), column.ToString());
                        s.WriteToServer(csvData);
                    }
                    connection.Close();

                    Console.WriteLine("Data imported succesfully!");
                    Console.WriteLine("\n\nWhat did you want to do?\n------------------------------------------------------\n" +
                    "1 - Show list of companies\n" +
                    "2 - Show list of Employee\n" +
                    "3 - Add Company\n" +
                    "4 - Add Employee\n" +
                    "5 - Export DB\n" +
                    "6 - Import DB");
                    break;
            }


        }
    }
}
