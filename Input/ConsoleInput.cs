using ConsolePixie.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsolePixie.Setup;

namespace ConsolePixie.Input
{
    public class ConsoleInput
    {
        private ImageProcessor _imageProcessor;

        public ConsoleInput()
        {
            _imageProcessor = new ImageProcessor();
            Program.stateUpdate = Update;
        }

        private void Update()
        {
            //Console.WriteLine("State has updated to {0}...", Program.CurrentState);

            if (Program.CurrentState == States.Start)
            {
                Console.Clear();
                CommonText.ShowInitText();
            }

            if (Program.CurrentState == States.Settings)
                Console.WriteLine("\n--------- НАСТРОЙКИ ---------\n");

            Input();
        }

        public void LoadImage()
        {    
            string? imagePath = Console.ReadLine();

            _imageProcessor.LoadImage(imagePath);
            //_imageProcessor.BuildImage(false);
        }

        public void Input()
        {
            string? str;

            switch (Program.CurrentState)
            {
                case States.Start:
                    _imageProcessor.DisposeImage();
                    Console.Write("Введите команду /start, чтобы начать: ");
                    str = Console.ReadLine();
                    if (!CheckCommands(str))
                        Input();
                    break;
                case States.WaitForImage:
                    _imageProcessor.DisposeImage();
                    Console.Write("Введите путь до изображения: ");
                    str = Console.ReadLine();
                    if (!CheckCommands(str))
                        _imageProcessor.LoadImage(str);
                    break;
                case States.ImageLoaded:
                    Console.WriteLine("Работа с изображением {0}...", _imageProcessor.ImageName);
                    Console.Write("Ожидание комманды: ");
                    str = Console.ReadLine();
                    if (!CheckCommands(str))
                        WaitForBuildCommands(str);
                    break;
                case States.ShowingResult:
                    Program.CurrentState = States.WaitForImage;
                    break;
                case States.Settings:
                    Console.Write("Введите цвет: ");
                    str = Console.ReadLine();
                    if (!CheckCommands(str))
                        Input();
                    break;
            }
        }

        private void WaitForBuildCommands(string str)
        {
            if (Program.CurrentState == States.ImageLoaded)
            {
                switch (str)
                {
                    case "/build":
                        _imageProcessor.BuildImage(true);
                        Program.CurrentState = States.ImageLoaded;
                        break;
                    case "/build2":
                        _imageProcessor.BuildImage(false);
                        Program.CurrentState = States.ImageLoaded;
                        break;
                    default:
                        Program.CurrentState = States.ImageLoaded;
                        break;
                }
            }
        }

        public bool CheckCommands(string str)
        {
            if (Program.CurrentState == States.Settings)
            {
                switch (str)
                {
                    case "/back":
                        Program.CurrentState = Program.LastState;
                        return true;
                    case "white":
                        Settings.SetNewColor(str);
                        Update();
                        return true;
                    case "yellow":
                        Settings.SetNewColor(str);
                        Update();
                        return true;
                    case "cyan":
                        Settings.SetNewColor(str);
                        Update();
                        return true;
                    case "blue":
                        Settings.SetNewColor(str);
                        Update();
                        return true;
                    case "magenta":
                        Settings.SetNewColor(str);
                        Update();
                        return true;
                }
            }

            switch (str)
            {
                case "/start":
                    _imageProcessor.DisposeImage();
                    Console.WriteLine();
                    Program.CurrentState = States.WaitForImage;
                    return true;
                case "/stop":
                    Program.CurrentState = States.Start;
                    return true;
                case "/cancel":
                    Console.WriteLine();
                    if (Program.CurrentState == States.Start) return false;
                    if (Program.CurrentState == States.Settings) Program.CurrentState = Program.LastState;
                    Program.CurrentState = States.WaitForImage;
                    return true;
                case "/clear":
                    Console.Clear();
                    Update();
                    return true;
                case "/settings":
                    if (Program.CurrentState != States.Settings) Program.LastState = Program.CurrentState;
                    Program.CurrentState = States.Settings;
                    return true;
                case "/help":
                    CommonText.ShowInitText();
                    Update();
                    return true;
                case "/exit":
                    Environment.Exit(0);
                    return true;
                default: return false;
            }
        }
    }
}
