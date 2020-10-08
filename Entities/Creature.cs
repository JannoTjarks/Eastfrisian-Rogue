using System;
using System.Collections.Generic;
using System.Text;

namespace EastfrisianRogue.Entities
{
    public class Creature : Entity
    {
        private int _hitpoints;
        private string _behaviour;
        private bool _isDead;
        private int _damageAmount;

        public int Hitpoints { get => _hitpoints; }
        public string Behaviour { get => _behaviour; }
        public bool IsDead { get => _isDead; }
        public int DamageAmount { get => this._damageAmount; set => this._damageAmount = value; }

        public Creature(string type, char glyph, ConsoleColor color, int x, int y, string behaviour, int hitpoints, int damageAmount) : base(type, glyph, color, x, y)
        {
            _behaviour = behaviour;
            _hitpoints = hitpoints;
            _isDead = false;
            _damageAmount = damageAmount;
        }

        public void Move(World world, int dx, int dy)
        {
            if (world.IsBlocked(_x + dx, _y + dy) != true)
            {
                this._x += dx;
                this._y += dy;
            }
            else 
            {
                Creature creature = world.GetCreatureAt(_x + dx, _y + dy);

                if(creature != null)
                {
                    AttackCreature(creature);
                }
            }
        }

        public void Damage(int amount) 
        {
            if(_hitpoints - amount > 0)
            {
                _hitpoints -= amount;
            }
            else
            {
                _hitpoints = 0;
                _isDead = true;
            }
        }

        public void AttackCreature(Creature creature) 
        {
            if (creature != this)
            {
                creature.Damage(_damageAmount);
            }            
        }
    }
}
