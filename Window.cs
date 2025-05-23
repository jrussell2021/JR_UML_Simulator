﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;
using System.IO;

namespace UML_Simulator_SDL2
{
    internal class Window
    {
        static readonly Window instance = new Window();

        static Window()
        {
        }

        private Window()
        {
        }

        public static Window Instance
        {
            get
            {
                return instance;
            }
        }
        public void InitFont()
        {
            font = SDL_ttf.TTF_OpenFont("open-sans.regular.ttf", 80);

            sampleColour = new();
        }

        public void CreateWindow()
        {
            SDL.SDL_Init(SDL.SDL_INIT_VIDEO);

            LoadSettings();

            window = SDL.SDL_CreateWindow("UML Simulator", SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, width, height, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG);
            SDL_ttf.TTF_Init();

        }

        public void LoadSettings()
        {
            if (File.Exists("settings.txt"))
            {
                //Load data
                //FileStream settings = File.Open("/settings.txt", FileMode.Open);
                string text = File.ReadAllText("settings.txt");
                string[] values = text.Split(',');
                try
                {
                    width = int.Parse(values[0]);
                    height = int.Parse(values[1]);
                }
                catch(Exception e)
                {
                    width = 1920;
                    height = 1080;
                }
                
                //settings.Close();
            }
            else
            {
                //Create default file
                FileStream settings = File.Create("settings.txt");
                settings.Close();
                File.WriteAllText("settings.txt", "1920,1080");
                settings.Close();
            }
        }
        public IntPtr renderer;
        public IntPtr window;
        public bool running = true;
        public int width = 1920, height = 1080;
        public int screenPosX = 0, screenPosY = 0;
        public IntPtr font;

        public SDL.SDL_Color sampleColour;

    }
}
