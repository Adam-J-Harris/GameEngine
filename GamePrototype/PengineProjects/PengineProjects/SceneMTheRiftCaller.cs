using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Managers;
using Engine.Maps;
using Engine.Objects.Entities.Interfaces;
using Engine.Objects.Minds.Interfaces;
using Engine.Screens.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Engine.Maps.QuadTree.Interfaces;
using Microsoft.Xna.Framework.Media;
using TheRiftCaller.Screens;
using TheRiftCaller.Managers;

namespace PengineProjects
{
    public class SceneMTheRiftCaller : Game
    {
        private static SceneMTheRiftCaller instance;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static int ScreenWidth;
        public static int ScreenHeight;

        public static GameTime timer;

        private ICamera camera;

        private Texture2D pixel;
        private bool run = true;
        private Vector2 temp;

        private SceneMTheRiftCaller()

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
        public static SceneMTheRiftCaller getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SceneMTheRiftCaller();
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

            //MediaPlayer.Play(SoundM.getInstance.getSong("Creeping_Death"));
            //MediaPlayer.Volume = 0.3f;

            IScreen gameScreen = new GameScreen(8192, 4096);

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

        // Overrides the UnloadContent inside of the Game class
        protected override void UnloadContent()
        {
            DisplayM.getInstance.UnloadContent();

            base.UnloadContent();
        }

        public void SetCamera()
        {
            camera = (ICamera)MindM.getInstance.getMind("CameraMind1");
            camera.SetViewport(GraphicsDevice.Viewport);
        }

        // Overrides the Update inside of the Game class
        protected override void Update(GameTime gameTime)
        {
            // If user changes screen size, game window will change size also
            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;

            timer = gameTime;

            DisplayM.getInstance.Update(timer);
            CollisionM.getInstance.Update();
            MindM.getInstance.Update();
            InputM.getInstance.Update();

            // If the user presses "Escape" the game will exit
            // If the user presses "Back" the game will exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                UnloadContent();
                Exit();
            }

            // call Game update method
            base.Update(gameTime);
        }

        public bool CastRay(SpriteBatch sb, Vector2 origin, Vector2 target, int thicknessOfBorder, Color borderColor)
        {
            bool returnMe = false;

            origin.X += 20;
            origin.Y += 20;

            target.X += 20;
            target.Y += 20;

            if (run)
            {
                temp = origin;

                pixel = new Texture2D(sb.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                pixel.SetData(new[] { Color.White });
                run = false;


                while (returnMe == false)
                {
                    // May be jumping over the walls, try incrementing in smaller values
                    temp += (target - origin) / 10;

                    Rectangle tempR = new Rectangle((int)temp.X, (int)temp.Y, 1, 1);

                    if (CollisionM.getInstance.CheckRayCollision(tempR))
                    {
                        returnMe = true;

                        // Draw line
                        //DrawLine(sb, origin, temp);
                    }
                }
            }

            //CollisionM.getInstance.CheckRayCollision(tempR);

            // Draw line
            //DrawLine(sb, origin, temp);

            run = true;

            return returnMe;
        }

        public void DrawLine(SpriteBatch sb, Vector2 origin, Vector2 target)
        {
            float angle = (float)Math.Atan2(origin.Y - target.Y, origin.X - target.X);
            float distance = Vector2.Distance(origin, target);

            sb.Draw(pixel, new Rectangle((int)target.X, (int)target.Y, (int)distance, 1), null, Color.Blue, angle, Vector2.Zero, SpriteEffects.None, 0);


        }

        // Overrides the Draw method inside the Game class
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.getSetTransform);

            // Draws the IQuadTree IQuadNodes as BlueViolet Rectangles
            foreach (IQuadNode node in DisplayM.getInstance.getCurrentScene.getThisAsGameScreen.getSetMap.getSetQuadTree.getAllNodes.Values)
            {
                if (node.getBounds.Intersects(camera.getSetBounds))
                {

                    foreach (IEntity e in node.getContents.Values)
                    {
                        if (e.getSetName != "Ghost")// || e.getSetName != "Patient")
                        {
                            e.Draw(spriteBatch);
                        }
                        
                    }
                    //node.Draw(spriteBatch, node.getBounds, 5, Color.BlueViolet);
                }

            }

            EntityM.getInstance.getEntity("Player1").Draw(spriteBatch);

            SetCamera();

            if (DimensionM.getInstance.getDimension == "R")
            {
                EntityM.getInstance.getEntity("Patient1").Draw(spriteBatch);
            }
            else if (DimensionM.getInstance.getDimension == "P")
            {
                EntityM.getInstance.getEntity("Ghost1").Draw(spriteBatch);
            }
            
            EntityM.getInstance.getEntity("Filter1").Draw(spriteBatch);

            

            if (EntityM.getInstance.getEntity("BigNote1") != null)
            {
                EntityM.getInstance.getEntity("BigNote1").Draw(spriteBatch);
            }
            
            //CastRay(spriteBatch, EntityM.getInstance.getEntity("Player1").getSetLocation, EntityM.getInstance.getEntity("Patient1").getSetLocation, 2, Color.Blue);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
