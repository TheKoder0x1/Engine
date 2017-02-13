﻿using Engine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Engine.Events.MouseEvent;
using Engine.Managers.CamManage;
using Engine.Managers.Behaviour;
using Engine.Managers.EntityRelated;
using Engine;

namespace ADS.Entities
{
   public class steermind : Mind
    {
        

        private static List<steermind> neighbours = new List<steermind>();
        IEntity target;
        
        IMind b;

        public steermind()
        {
            MouseHandler.Instance.MouseMoved += onMouseMoved;
        }

        public override void Initialize(Vector2 Position)
        {
            texPath = "virus1";
            base.Initialize(Position);
            target = EntityManager.Instance.getPlayer();
            neighbours.Add(this);
        }

        public void onMouseMoved(object Sender, MouseEventArgs m)
        {
            Vector2 world = new Vector2(m.X, m.Y);
          //  target = CameraManager.Instance.getWorldPosition(world);

        }

        public override void Unload()
        {
            neighbours.Remove(this);
            base.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            b = BehaviourManager.Instance.getMind(0);


            if (b != null)
            {
                Velocity = Vector2.Normalize(b.Position - _pos) * 0.2f;
                _pos = _pos + Velocity;

            }
          
         

            base.Update(gameTime);
        }
    }
}
