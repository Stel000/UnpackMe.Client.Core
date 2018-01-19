using System;
using System.IO;
using System.Linq;
using UnpackMe.SDK.Core;
using UnpackMe.SDK.Core.Models;

namespace UnpackMe.Client.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            using (UnpackMeClient unpackMeClient = new UnpackMeClient("http://unpackmeurl"))
            {
                unpackMeClient.Authenticate("login", "password");

                // Retrieve the list of available commands for your login/password
                CommandModel[] commands = unpackMeClient.GetAvailableCommands();
                CommandModel decryptCommand = commands.SingleOrDefault(x => x.CommandTitle == "Pangya TH *.iff decrypt");
                string rootPath = @"C:\UnpackMe\pangya.iff";
                DirectoryInfo root = new DirectoryInfo(rootPath);
                if(root.Exists)
                {
                    FileInfo[] fileList = root.GetFiles();
                }
                foreach (FileInfo f in fileList)
                {
                    string[] fileNameList = f.FullName
                }
                foreach (string fileName in fileNameList)
                {
                    // Open the file to unpack
                    FileStream fileStream = new FileStream(
                        fileName,
                        FileMode.Open
                    );

                // Create an unpack task with the file
                    string taskId = unpackMeClient.CreateTaskFromCommandId(decryptCommand.CommandId, fileStream);

                // Check for unpack status
                    TaskModel task;
                    string taskStatus;
                    do
                    {
                        task = unpackMeClient.GetTaskById(taskId);
                        taskStatus = task.TaskStatus;

                        System.Threading.Thread.Sleep(1000);

                        Console.WriteLine(taskStatus);

                    } while (taskStatus != "completed");

                    // When unpacked file is ready, Save the result into a file
                    unpackMeClient.SaveTaskFileTo(
                        taskId,
                        fileName + ".dec"
                    );

                    Console.WriteLine("should be unpacked now o_O");
                }
                Console.WriteLine("所有文件已完成转换");
            }
        }
    }
}