using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

namespace less_6
{
    class Program
    {
        static private string file = @"tasks.json";
        static private List<Todo> toDoList = new List<Todo>();
        static private string taskTitle = String.Empty;
        static private string userChoice = String.Empty;
        static private int taskId;

        static void Main(string[] args)
        {
            FileInfo fileInfo = new FileInfo(file);
            if (fileInfo.Exists && fileInfo.Length > 5)
            {
                ReadTasksFromFile(file);
            }
            else
            {
                Console.WriteLine("Список задач пуст! Добавьте новую задачу:");
                taskTitle = Console.ReadLine();
                AddTaskToList(taskTitle);
            }
            do
            {
                PrintTasksToConsole(toDoList);
                Console.WriteLine($"\nДобавить новую задачу \t\t- нажмите '1'\nОтметить задачу завершенной \t- нажмите '2'\nУдалить задачу \t\t\t- нажмите '3'\nВыйти из программы \t\t- нажмите '4'");
                userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        Console.WriteLine($"Напишите задачу: ");
                        taskTitle = Console.ReadLine();
                        AddTaskToList(taskTitle);
                        break;
                    case "2":
                        Console.Write($"Укажите номер выполненной задачи: ");
                        taskId = Convert.ToInt32(Console.ReadLine());
                        markTaskAsDone(taskId);
                        break;
                    case "3":
                        Console.Write("Укажите номер удаляемой задачи: ");
                        taskId = Convert.ToInt32(Console.ReadLine());
                        DeleteTaskFromList(taskId);
                        break;
                    default:
                        break;
                }
            } while (userChoice != "4");
            SaveToDoListToFile();
        }

        static void AddTaskToList(string taskTitle)
        {
            Todo toDo = new Todo(taskTitle);
            Todo str = JsonSerializer.Deserialize<Todo>(JsonSerializer.Serialize(toDo));
            toDoList.Add(str);
        }
        private static void DeleteTaskFromList(int taskId)
        {
            toDoList.Remove(toDoList[taskId]);
        }
        private static void markTaskAsDone(int taskId)
        {
            toDoList[taskId].IsDone = true;
        }
        private static void SaveToDoListToFile()
        {
            string[] vs = new string[toDoList.Count];
            for (int i = 0; i < vs.Length; i++)
            {
                vs[i] = JsonSerializer.Serialize(toDoList[i]);
            }
            File.WriteAllLines(file, vs);
        }
        private static void PrintTasksToConsole(List<Todo> toDoList)
        {
            Console.Clear();
            for (int i = 0; i < toDoList?.Count; i++)
            {
                Console.WriteLine("{0} {1} {2}", i, toDoList[i].IsDone ? "[x]" : "[ ]", toDoList[i].Title);
            }
        }
        static void ReadTasksFromFile(string file)
        {
            string[] str = File.ReadAllLines(file);
            for (int i = 0; i < str.Length; i++)
            {
                Todo toDo = JsonSerializer.Deserialize<Todo>(str[i]);
                toDoList.Add(toDo);
            }
        }
    }
}