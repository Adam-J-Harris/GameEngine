/*
MIT License

Copyright (c) 2016 Duncan Baldwin & Adam Harris

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using System;
using System.Collections.Generic;
using Engine.Managers;
using Engine.Maps;
using Engine.Maps.QuadTree.Interfaces;
using Engine.Objects.Entities.Interfaces;
using Engine.Objects.Minds.Interfaces;
using Engine.Screens.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sandbox.Screens;

namespace PengineProjects
{
    public class SceneMSandbox : Game
    {
        private static SceneMSandbox instance;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static int ScreenWidth;
        public static int ScreenHeight;

        public static GameTime timer;

        private ICamera camera;

        private SceneMSandbox()

            : base()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.IsFullScreen = false;

            Content.RootDirectory = "Content";

            // Pass content to contentManager instance
            ImageM.getInstance.getContent(Content);
            SoundM.getInstance.getContent(Content);

            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;

            this.IsMouseVisible = true;
        }

        // Singleton Code
        public static SceneMSandbox getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SceneMSandbox();
                }
                return instance;
            }
        }

        // Overrides the Initialize function inside of the Game class
        protected override void Initialize()
        {
            // Sets the variables to the size of the screen in use
            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;

            IScreen gameScreen = new GameScreen(ScreenWidth, ScreenHeight);

            DisplayM.getInstance.Initialise(gameScreen);

            base.Initialize();
        }

        // Overrides the LoadContent function inside of the Game class
        protected override void LoadContent()
        {
            // Set up spritebatch
            spriteBatch = new SpriteBatch(GraphicsDevice);

            DisplayM.getInstance.LoadContent();

            SetCamera();

            base.LoadContent();
        }

        private void SetCamera()
        {
            camera = (ICamera)MindM.getInstance.getMind("CameraMind1");
            camera.SetViewport(GraphicsDevice.Viewport);
        }

        // Overrides the UnloadContent inside of the Game class
        protected override void UnloadContent()
        {
            DisplayM.getInstance.UnloadContent();

            base.UnloadContent();
        }

        // Overrides the Update inside of the Game class
        protected override void Update(GameTime gameTime)
        {
            // If the user presses "Escape" the game will exit
            // If the user presses "Back" the game will exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                UnloadContent();
                Exit();
            }

            // If user changes screen size, game window will change size also
            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;

            timer = gameTime;

            DisplayM.getInstance.Update(timer);
            CollisionM.getInstance.Update();
            MindM.getInstance.Update();
            InputM.getInstance.Update();

            // call Game update method
            base.Update(gameTime);
        }

        // Overrides the Draw method inside the Game class
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.getSetTransform);

            int i = 0;
            foreach (IQuadNode node in DisplayM.getInstance.getCurrentScene.getThisAsGameScreen.getSetMap.getSetQuadTree.getAllNodes.Values)
            {
                i++;
                node.Draw(spriteBatch, node.getBounds, 2, Color.Black);
            }

            foreach (KeyValuePair<string, IEntity> ent in EntityM.getInstance.getDictionary)
            {
                ent.Value.Draw(spriteBatch);
            }

            EntityM.getInstance.getEntity("Player1").Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}