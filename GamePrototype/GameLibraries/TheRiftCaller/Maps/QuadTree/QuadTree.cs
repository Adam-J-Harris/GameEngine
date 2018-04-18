/*
MIT License

Copyright (c) 2016 Duncan Baldwin

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
using Engine.Managers;
using Engine.Maps.QuadTree;
using Engine.Maps.QuadTree.Interfaces;
using Engine.Objects.Entities.Interfaces;
using Engine.Screens.Interfaces;
using Microsoft.Xna.Framework;
using TheRiftCaller.Objects.Entities;
using TheRiftCaller.Objects.Entities.Statics;
using TheRiftCaller.Screens;

namespace TheRiftCaller.Maps.Quadtrees
{
    public class QuadTree : IQuadTree
    {
        private IQuadNode mRootNode;
        private int mMaxObjectsPerLeaf = 256;

        private IDictionary<int, IQuadNode> mAllNodes = new Dictionary<int, IQuadNode>();
        private IDictionary<IEntity, IQuadNode> objectToNodeLookup = new Dictionary<IEntity, IQuadNode>();

        private IQuadNode currentPlayerNode;

        public QuadTree(Rectangle pBounds)
        {
            mRootNode = new QuadNode(pBounds);
            mRootNode.getSetID = 1;

            AddToNodePopulus(mRootNode);
        }

        public QuadTree(int x, int y, int width, int height)
        {
            mRootNode = new QuadNode(new Rectangle(x, y, width, height));
            mRootNode.getSetID = 1;

            AddToNodePopulus(mRootNode);

        }

        public void ClearQuadTree()
        {
            foreach (IQuadNode node in mAllNodes.Values)
            {
                node.getContents.Clear();
            }

            objectToNodeLookup.Clear();
            mRootNode = mAllNodes[1];
            mAllNodes.Clear();
            AddToNodePopulus(mRootNode);
            //objectToNodeLookup.Clear();
        }

        public void SetUpQuadTree()
        {
            foreach (IEntity e in EntityM.getInstance.getDictionary.Values)
            {
                InsertObjectToNode(e, mRootNode);
            }

            IDictionary<string, ICollidable> passMe = new Dictionary<string, ICollidable>();
            List<IEntity> values = GetChildObjects(mAllNodes[1]);
            foreach (IEntity entityRef in values)
            {
                if (entityRef.getSetID != "Player1" && entityRef is IStructure)
                {
                    passMe.Add(entityRef.getSetID, (ICollidable)entityRef);
                }
            }
            //CollisionM.getInstance.BreakStaticHitboxes();
            CollisionM.getInstance.SetUpStaticHitboxes(passMe);

            passMe.Clear();

            foreach (IEntity entityRef in objectToNodeLookup[EntityM.getInstance.getEntity("Player1")].getContents.Values)
            {
                if (entityRef.getSetID != "Player1" && entityRef is ICollidable)
                {
                    passMe.Add(entityRef.getSetID, (ICollidable)entityRef);
                }
            }

            CollisionM.getInstance.getSetDictionary = passMe;
            CollisionM.getInstance.ResetStaticHitboxes();
            CollisionM.getInstance.SetUpMovingEntities();
        }

        /// <summary>
        /// Adds the IQuadNode node to the dictionary which contains all of the nodes in the tree
        /// </summary>
        /// <param name="node">IQuadNode to add</param>
        public void AddToNodePopulus(IQuadNode node)
        {
            mAllNodes.Add(node.getSetID, node);
        }

        /// <summary>
        /// Should be implemented for an Adaptive QuadTree design
        /// </summary>
        public void CheckNodes(IQuadNode node)
        {
            CheckChildNodes(node);

            //IDictionary<string, ICollidable> passMe = new Dictionary<string, ICollidable>();
            //IList<IEntity> values = GetChildObjects(mRootNode);
            //foreach (IEntity entityRef in values)
            //{
            //    if (entityRef.getSetID != "Player1" && entityRef is Wall)
            //    {
            //        passMe.Add(entityRef.getSetID, (ICollidable)entityRef);
            //    }
            //}
            //CollisionM.getInstance.BreakStaticHitboxes();
            //CollisionM.getInstance.SetUpStaticHitboxes(passMe);

            //IDictionary<string, ICollidable> passMe = new Dictionary<string, ICollidable>();

            //foreach (ICollidable c in objectToNodeLookup.Keys)
            //{
            //    if (c.getSetID == "Player1")
            //    {
            //        passMe.Concat(objectToNodeLookup[c].getSetParent.getContents);
            //        break;
            //    }
            //}

            //CollisionM.getInstance.BreakStaticHitboxes();
            //CollisionM.getInstance.SetUpStaticHitboxes(passMe);
        }

        /// <summary>
        /// Splits a IQuadNode into 4 more QuadNodes
        /// </summary>
        /// <param name="node">IQuadNode to split</param>
        public void SplitNode(IQuadNode node)
        {
            node.Split();

            foreach (IQuadNode child in node.getChildNodes.Values)
            {
                child.getSetParent = node;
                AddToNodePopulus(child);
            }
        }

        /// <summary>
        /// Returns a IQuadNode object
        /// </summary>
        /// <param name="id">Id of the IQuadNode - 1 : 11 : 243</param>
        /// <returns>IQuadNode</returns>
        public IQuadNode SearchByNodeID(int id)
        {
            IQuadNode returnMe = new QuadNode();

            if (mAllNodes.ContainsKey(id))
            {
                returnMe = mAllNodes[id];
            }

            return returnMe;
        }

        /// <summary>
        /// Clears all of the contents inside the IQuadNode
        /// </summary>
        /// <param name="node">IQuadNode to be wiped</param>
        public void ClearObjectsFromNode(IQuadNode node)
        {
            List<IEntity> quadObjects = new List<IEntity>(node.getContents.Values);
            foreach (IEntity quadObject in quadObjects)
            {
                RemoveObjectFromNode(quadObject);
            }
        }

        /// <summary>
        /// Removes an IEntity object from a IQuadNode's contents dictionary
        /// </summary>
        /// <param name="e">IEntity object to be removed</param>
        public void RemoveObjectFromNode(IEntity e)
        {
            IQuadNode node = objectToNodeLookup[e];
            node.getContents.Remove(e.getSetID);
            objectToNodeLookup.Remove(e);

            if (e is ICollidable && e.getSetName == "Player")
            {
                ICollidable c = (ICollidable)e;

                c.BoundsChanged -= new EventHandler(quadObject_BoundsChanged);
            }
        }

        /// <summary>
        /// Checks for whether it needs to split a QuadNode and cleans up contents lists
        /// </summary>
        /// <param name="e">IEntity to be inserted</param>
        /// <param name="node">IQuadNode which object will be inserted to</param>
        public void InsertObjectToNode(IEntity e, IQuadNode node)
        {
            ICollidable c;
            if (node.getChildNodes.Count == 0 && node.getContents.Count + 1 > mMaxObjectsPerLeaf)
            {
                SplitNode(node);

                List<IEntity> childObjects = new List<IEntity>(node.getContents.Values);
                List<IEntity> childrenToRelocate = new List<IEntity>();

                foreach (IEntity childObject in childObjects)
                {
                    foreach (IQuadNode childNode in node.getChildNodes.Values)
                    {
                        if (childNode == null)
                            continue;

                        if (childObject is ICollidable)
                        {
                            c = (ICollidable)childObject;

                            if (childNode.getBounds.Contains(c.getSetHitbox))
                            {
                                childrenToRelocate.Add(childObject);
                                break;
                            }
                        }
                        // If object is a floor
                        else
                        {
                            if (childNode.getBounds.Contains(childObject.getSetPointLocation))
                            {
                                childrenToRelocate.Add(childObject);
                                break;
                            }
                        }
                    }
                }

                foreach (IEntity childObject in childrenToRelocate)
                {
                    RemoveObjectFromNode(childObject);
                    InsertObjectToNode(childObject, node);
                }
            }

            foreach (IQuadNode childNode in node.getChildNodes.Values)
            {
                if (childNode != null)
                {
                    if (e is ICollidable)
                    {
                        c = (ICollidable)e;

                        if (childNode.getBounds.Contains(c.getSetHitbox))
                        {
                            InsertObjectToNode(e, childNode);
                            return;
                        }
                    }
                    // If object is a floor
                    else
                    {
                        if (childNode.getBounds.Contains(e.getSetLocation))
                        {
                            InsertObjectToNode(e, childNode);
                            return;
                        }
                    }
                }
            }

            AddObjectToNode(node, e);
        }

        /// <summary>
        /// Adds an IEntity object to a IQuadNode's contents dictionary
        /// </summary>
        /// <param name="node">IQuadNode which object will be added to</param>
        /// <param name="e">IEntity object to be added</param>
        public void AddObjectToNode(IQuadNode node, IEntity e)
        {
            node.getContents.Add(e.getSetID, e);
            objectToNodeLookup.Add(e, node);

            if (e is ICollidable)
            {
                ICollidable c = (ICollidable)e;
                
                c.BoundsChanged += new EventHandler(quadObject_BoundsChanged);
            }
        }

        public void Insert(IEntity quadObject)
        {
            bool containsObject = false;
            //mRootNode = mAllNodes[1];

            while (!containsObject)
            {
                if (mRootNode.getSetParent != null)
                {
                    foreach (IQuadNode qn in mRootNode.getSetParent.getChildNodes.Values)
                    {
                        mRootNode = qn;

                        if (quadObject is ICollidable)
                        {
                            ICollidable c = (ICollidable)quadObject;

                            c.SetUpHitbox();

                            if (mRootNode.getBounds.Contains(c.getSetHitbox))
                            {
                                containsObject = true;
                                break;
                            }
                        }
                        // Object is not ICollidable
                        else
                        {
                            if (mRootNode.getBounds.Contains(quadObject.getSetPointLocation))
                            {
                                containsObject = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    containsObject = true;
                }

                if (!containsObject)
                {
                    mRootNode = mRootNode.getSetParent;
                }
            }

            InsertObjectToNode(quadObject, mRootNode);
        }

        public void Remove(IEntity quadObject)
        {
            if (!objectToNodeLookup.ContainsKey(quadObject))
            {
                throw new KeyNotFoundException("QuadObject not found in dictionary for removal");
            }

            IQuadNode containingNode = objectToNodeLookup[quadObject];
            RemoveObjectFromNode(quadObject);

            if (containingNode.getSetParent != null)
            {
                //CheckNodes(containingNode.getSetParent);
            }
            
            ContentsHasChanged(containingNode);
        }

        public void quadObject_BoundsChanged(object sender, EventArgs e)
        {
            ICollidable quadObject = sender as ICollidable;
            
            if (!objectToNodeLookup[quadObject].getBounds.Intersects(quadObject.getSetHitbox))
            {
                RemoveObjectFromNode(quadObject);
                Insert(quadObject);

                mRootNode = objectToNodeLookup[quadObject];

                Console.WriteLine("Change bounds to this node: " + mRootNode.getSetID);
                BoundsHaveChanged(quadObject, mRootNode);                
            }
        }

        public void BoundsHaveChanged(IEntity quadObject, IQuadNode node)
        {
            IDictionary<string, ICollidable> passMe = new Dictionary<string, ICollidable>();
            List<IEntity> values = GetChildObjects(mRootNode);
            foreach (IEntity entityRef in values)
            {
                if (entityRef.getSetID != "Player1" && (entityRef is IStructure || entityRef is ISmartObject))
                {
                    passMe.Add(entityRef.getSetID, (ICollidable)entityRef);
                }
            }
            //CollisionM.getInstance.BreakStaticHitboxes();
            //CollisionM.getInstance.SetUpStaticHitboxes(passMe);
            CollisionM.getInstance.getSetDictionary = passMe;
            
        }

        private void ContentsHasChanged(IQuadNode node)
        {
            IDictionary<string, ICollidable> passMe = new Dictionary<string, ICollidable>();
            List<IEntity> values = GetChildObjects(node);
            foreach (IEntity entityRef in values)
            {
                if (entityRef.getSetID != "Player1" && (entityRef is IStructure || entityRef is ISmartObject))
                {
                    passMe.Add(entityRef.getSetID, (ICollidable)entityRef);
                }
            }

            //CollisionM.getInstance.ResetStaticHitboxes();
            //CollisionM.getInstance.SetUpStaticHitboxes(passMe);
            CollisionM.getInstance.getSetDictionary = passMe;
            CollisionM.getInstance.ResetStaticHitboxes();
        }

        private List<IEntity> GetChildObjects(IQuadNode node)
        {
            List<IEntity> results = new List<IEntity>();

            results.AddRange(node.getContents.Values);

            foreach (IQuadNode childNode in node.getChildNodes.Values)
            {
                if (childNode != null)
                {
                    results.AddRange(GetChildObjects(childNode));
                }
            }

            return results;
        }

        private void CheckChildNodes(IQuadNode node)
        {
            if (node.getContentsCount <= mMaxObjectsPerLeaf)
            {
                // Move child objects into this node, and delete sub nodes
                IList<IEntity> subChildObjects = GetChildObjects(node);

                foreach (IEntity childObject in subChildObjects)
                {
                    if (!node.getContents.Values.Contains(childObject) && node.getSetID != 1)
                    {
                        RemoveObjectFromNode(childObject);
                        AddObjectToNode(node, childObject);
                    }
                }

                if (node.getSetParent != null)
                {
                    //CheckChildNodes(node.getSetParent);
                    CheckNodes(node.getSetParent);
                }
                else
                {
                    // Its the root node, see if we're down to one quadrant, with none in local storage - if so, ditch the other three
                    int numQuadrantsWithObjects = 0;
                    IQuadNode nodeWithObjects = null;
                    foreach (IQuadNode childNode in node.getChildNodes.Values)
                    {
                        if (childNode != null && childNode.getContentsCount > 0)
                        {
                            numQuadrantsWithObjects++;
                            nodeWithObjects = childNode;
                            if (numQuadrantsWithObjects > 1) break;
                        }
                    }
                    if (numQuadrantsWithObjects == 1)
                    {
                        foreach (IQuadNode childNode in node.getChildNodes.Values)
                        {
                            if (childNode != nodeWithObjects)
                                childNode.getSetParent = null;
                        }
                        mRootNode = nodeWithObjects;
                    }
                }
            }
        }

        /// <summary>
        /// Updates the QuadTree and also checks for QuadNode contents movements
        /// </summary>
        public void Update()
        {
            //mRootNode = objectToNodeLookup[EntityM.getInstance.getEntity("Player1")];
            //Console.WriteLine(currentPlayerNode.getSetID + " : currentPlayerNode");
            //Console.WriteLine(mRootNode.getSetID + " : mRootNode");

            //if (EntityM.removedObj)
            //{
            //    ClearQuadTree();
            //    SetUpQuadTree();
            //    EntityM.removedObj = false;
            //}
        }

        public IDictionary<int, IQuadNode> getAllNodes
        {
            get { return mAllNodes; }
        }
    }
}
