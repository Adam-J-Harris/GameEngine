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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Devices.Interfaces;
using Engine.Events;
using Engine.Managers;
using Engine.Maps;
using Engine.Objects.Entities.Interfaces;
using Engine.Objects.Minds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheRiftCaller.Managers;
using Engine.CollisionDetection;
using Engine.AI.StateMachine.Interfaces;
using TheRiftCaller.AI.StateMachine;
using Engine.AI.Behaviours.Interfaces;
using TheRiftCaller.AI.Behaviours.WalkingStates;
using TheRiftCaller.Physics;
using TheRiftCaller.Lighting;
using TheRiftCaller.Objects.Entities.Statics;

namespace TheRiftCaller.Objects.Minds
{
    public class PlayerMind : AMind
    {
        private Vector2 mPos;

        private IDkeyboard keyboardDevice;
        private IDmouse mouseDevice;
        
        private Keys[] exclude = new Keys[4] { Keys.W, Keys.A, Keys.S, Keys.D };

        public static IDictionary<string, ISmartObject> inventory;

        private bool smartObjCollide = false;
        private ISmartObject smartObj;

        private bool smartStructCollide = false;
        private ISmartStructure smartStruct;

        private bool physicsCollide = false;

        private float mass = 82.5f;
        private float inVerseMass;

        public PlayerMind()
        {
            getSetName = "PlayerMind";

            keyboardDevice = (IDkeyboard)InputM.getInstance.getKeyboard;
            keyboardDevice.AddListener(newKeyboardInput);
            keyboardDevice.AddKeyUpListener(newKeyUp);

            mouseDevice = (IDmouse)InputM.getInstance.getMouse;

            CollisionM.getInstance.AddListener(newCollision);

            inventory = new Dictionary<string, ISmartObject>();

            getSetStateMachine = new FSM();
        }

        /// <summary>
        /// Set/Initialise the values of the class
        /// </summary>
        public override void SetValues()
        {
            base.SetValues();

            mPos = getSetEntity.getSetLocation;
            getSetSpeed = 2f;

            inVerseMass = 1 / mass;

            //IList<IState> states = new List<IState>() { BehaviourM.getInstance.GetState("WalkRight"), BehaviourM.getInstance.GetState("WalkLeft") };
            //getSetStateMachine.ApplyStates(states);

            getSetStateMachine.getSetCurrentState = BehaviourM.getInstance.GetState("Idle");
        }

        private void UpdateRays()
        {
            //List<IRay> rays = new List<IRay>();
             
        }

        /// <summary>
        /// Updates the values of this class and sends information back to the getSetEntity
        /// </summary>
        public override void Update()
        {
            if (DisplayM.timer.TotalGameTime.Seconds > startTime + timeDelay)
            {
                canAction = true;
            }
            
            //mouseDevice.getRotationFromEntity(getSetEntity);

            Vector2 p1Velocity = keyboardDevice.GetKeyboardInputDirection(PlayerIndex.One);

            getSetStateMachine.getSetCurrentState.RunBehaviour(this, p1Velocity);
            getSetMoveVector.Normalize();

            if (physicsCollide)
            {
                Vector2 temp = getSetMoveVector;

                if (getSetStateMachine.getSetPreviousState == BehaviourM.getInstance.GetState("WalkUp"))
                {
                    temp.Y = 1;
                }
                else if (getSetStateMachine.getSetPreviousState == BehaviourM.getInstance.GetState("WalkDown"))
                {
                    temp.Y = -1;
                }
                else if (getSetStateMachine.getSetPreviousState == BehaviourM.getInstance.GetState("WalkLeft"))
                {
                    temp.X = 1;
                }
                else if (getSetStateMachine.getSetPreviousState == BehaviourM.getInstance.GetState("WalkRight"))
                {
                    temp.X = -1;
                }
                //getSetMoveVector *= -1;

                getSetMoveVector = temp;
            }

            // Force equals Mass multiplied by Acceleration
            getSetAcceleration += PhysicsM.getInstance.getSetInputForce * inVerseMass;//* mePhysical.getSetMass;
            // Speed/Velocity equals acceleration multiplied by delta time in seconds
            getSetSpeed += getSetAcceleration; //* (float)DisplayM.timer.ElapsedGameTime.TotalSeconds;
            getSetSpeed *= PhysicsM.damping;

            getSetAcceleration = 0;

            if (getSetSpeed < 0)
            {
                getSetSpeed = 0;
            }
            float rotation = 0.0f;

            if (rotation > Math.PI || rotation < -Math.PI)
            {
                rotation *= -1;
            }

            mPos += getSetMoveVector * getSetSpeed; //* (float)DisplayM.timer.ElapsedGameTime.TotalSeconds;

            physicsCollide = false;

            //getSetMoveVector = Vector2.Zero;

            ICollidable c = (ICollidable)getSetEntity;
            c.RaiseBoundsChanged();

            getSetLocation = mPos;
            base.Update(mPos);
        }

        /// <summary>
        /// Determines what happens if the ICollidable getSetEntity is intersecting another entity's hitbox
        /// </summary>
        /// <param name="source">.</param>
        /// <param name="data">CollisionEvent which contains the entity being collided with</param>
        public virtual void newCollision(object source, CollisionEvent data)
        {
            ICollidable cRef = (ICollidable)getSetEntity;

            if (data.collidable is ISmartObject)
            {
                smartObj = (ISmartObject)data.collidable;
                smartObjCollide = true;

                
            }
            else if (data.collidable is ISmartStructure)
            {
                smartStruct = (ISmartStructure)data.collidable;
                smartStructCollide = true;

                if (cRef.getSetCenter.X < data.collidable.getSetCenter.X)
                {
                    data.mtv.X *= -1;
                }

                if (cRef.getSetCenter.Y < data.collidable.getSetCenter.Y)
                {
                    data.mtv.Y *= -1;
                }

                getSetMoveVector = data.mtv * (getSetSpeed);
                //mPos += getSetMoveVector;

                physicsCollide = true;
                //getSetSpeed *= 0.1f;
            }
            else
            {
                if (cRef.getSetCenter.X < data.collidable.getSetCenter.X)
                {
                    data.mtv.X *= -1;
                }

                if (cRef.getSetCenter.Y < data.collidable.getSetCenter.Y)
                {
                    data.mtv.Y *= -1;
                }

                getSetMoveVector = data.mtv * (getSetSpeed);
                //mPos += getSetMoveVector;

                physicsCollide = true;
                //getSetSpeed *= 0.1f;
            }

            
        }

        /// <summary>
        /// Determines what happens when a keyboard key is pressed, excludes WASD from the keysArray, Upon action completion adds a delay for the next action
        /// </summary>
        /// <param name="source">.</param>
        /// <param name="data">KeyboardEvent which contains all currently pressed keys in an Keys[]</param>
        public virtual void newKeyboardInput(object source, KeyboardEvent data)
        {
            foreach (Keys key in data.keysArray)
            {
                int index = Array.IndexOf(exclude, key);

                if (canAction && index == -1)
                {
                    if (key == Keys.E)
                    {
                        DimensionM.getInstance.ChangeDimension();
                        //Console.WriteLine("Change dimension");
                    }
                    else if (key == Keys.Space)
                    {
                       
                            
                        if (smartObjCollide)
                        {

                            if (smartObj is IBigNote)
                            {
                                smartObj.getSetLocation = new Vector2(-1000, -1000);
                            }
                            //inventory.Add(smartObj.getSetID, smartObj);
                            //InventoryM.getInstance.inv.Add(smartObj.getSetID, smartObj);

                            smartObj.Interact();
                            smartObjCollide = false;
                        }
                        else if (smartStructCollide)
                        {
                            smartStruct.Interact();
                            smartStructCollide = false;
                        }
                    }

                    StartDelay(1);
                }
                            
            }

            smartObjCollide = false;
            smartStructCollide = false;

        }

        public virtual void newKeyUp(object source, KeyUpEvent data)
        {
            
        }

        public override void AddCollisionListener()
        {
            CollisionM.getInstance.AddListener(newCollision);
        }

        public override void RemoveCollisionListener()
        {
            CollisionM.getInstance.RemoveListener(newCollision);
        }

    }

}
