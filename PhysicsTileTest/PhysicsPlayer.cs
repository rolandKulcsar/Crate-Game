using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Common.PolygonManipulation;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsTileTest
{
    class PhysicsPlayer
    {
        protected const float unitToPixel = 100.0f;
        protected const float pixelToUnit = 1 / unitToPixel;
        World world;
        uint[] data;
        Texture2D texture;
        Body body;
        Vector2 origin;
        Vector2 centerScreen;
        const float scale = 1f;
        protected Vector2 size;
        public Vector2 Size
        {
            get { return size * unitToPixel; }
            set { size = value * pixelToUnit; }
        }

        public Vector2 Position
        {
            get { return body.Position * unitToPixel; }
            set { body.Position = value * pixelToUnit; }
        }

        public PhysicsPlayer(World _world)
        {
            world = _world;
        }

        public void CreateBodyFromImage(Texture2D _texture)
        {
            //Load the passed texture.
            texture = _texture;

            //Use an array to hold the textures data.
            uint[] data = new uint[texture.Width * texture.Height];

            //Transfer the texture data into the array.
            texture.GetData(data);

            //Find the verticals that make up the outline of the passed texture shape.
            Vertices vertices = PolygonTools.CreatePolygon(data, texture.Width);

            //For now we need to scale the vertices (result is in pixels, we use meters)
            Vector2 scale = new Vector2(0.5f, 0.5f);
            vertices.Scale(ref scale);

            //Partition the concave polygon into a convex one.
            var decomposedVertices = FarseerPhysics.Common.Decomposition.Triangulate.ConvexPartition(vertices, FarseerPhysics.Common.Decomposition.TriangulationAlgorithm.Bayazit);

            //Create a single body, that has multiple fixtures to the polygon shapes.
            body = BodyFactory.CreateCompoundPolygon(world, decomposedVertices, 1f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 scale = new Vector2(Size.X / (float)texture.Width, Size.Y / (float)texture.Height);
            spriteBatch.Draw(texture, Position, null, Color.White, body.Rotation, new Vector2(texture.Width / 2.0f, texture.Height / 2.0f), scale, SpriteEffects.None, 0);
        }

    }
}