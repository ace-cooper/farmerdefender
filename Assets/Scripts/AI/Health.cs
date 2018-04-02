using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Creature {
    public class Health : MonoBehaviour
    {
        public float dieTime = 10;
        private float timer;
        private bool dead = false;
        public int _hp=10;

        private float damageTimer = 0;
        private float damageAnimationTimer = 1.5f;
        enum damageState { none, forward, back }
        private damageState _damageAnimationState = damageState.none;
        private damageState damageAnimationState
        {
            get { return _damageAnimationState; }
            set
            {
                if (value != damageState.none) damageTimer = Time.time + damageAnimationTimer;
                _damageAnimationState = value; 
            }
        }

        private AIController _controller;
        private AIController controller {
            get
            {
                if (_controller == null) _controller = GetComponent<AIController>();

                return _controller;
            }
        }
        private Animator _animator;
        private Animator animator
        {
            get
            {
                if (_animator == null) _animator = GetComponent<Animator>();

                return _animator;
            }
        }

        private Material _material;
        private Material material
        {
            get
            {
                if (_material == null) _material = controller.body.GetComponent<Material>() ?? GetComponent<Material>();

                return _material;
            }
        }

        public bool dontDestroy = true;

        public bool isDead
        {
            get { return dead; }
        }
        public int hp
        {
            get { return _hp; }
            set
            {
                _hp = value;
                if (_hp <= 0) Die();
            }
        }
        void Awake()
        {

    
           
        }


        void FixedUpdate()
        {
            if (dead && timer <= Time.time) {

                if (controller!=null)
                {
                    controller.Destroy();
                } else {

                    if (dontDestroy)
                    {
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
            }

            //animate();
        }


        private void animate()
        {
            if (damageAnimationState != damageState.none && material != null)
            {
                Color primary = Color.white;
                Color secundary = Color.white;
                switch (damageAnimationState)
                {
                    case damageState.forward:
                        secundary = Color.red;
                        if (damageTimer < Time.time)
                        {
                            damageAnimationState = damageState.back;
                        }
                        break;
                    case damageState.back:
                        primary = Color.red;
                        if (damageTimer < Time.time)
                        {
                            damageAnimationState = damageState.none;
                        }
                        break;
                }

                float temp = damageTimer - damageAnimationTimer;
                material.color = Color.Lerp(primary, secundary, (Time.time - temp) * 100 / damageAnimationTimer);


            }
        }

        public void Damage(int d)
        {
            hp -= (hp - d < 0) ? hp : d;
            damageAnimationState = damageState.forward;

        }

        public void Die()
        {
            dead = true;
            timer = Time.time + dieTime;

            if (controller != null)
            {
                controller.core.Die(controller);
            }
            //animator.SetBool("isDead", true);
        }


        void OnEnable()
        {
            Initialize();
        }


        public void Initialize()
        {
            
            dead = false;
            if (controller != null)
            {
                hp = controller.profile.hp;
                controller.enabled = true;
            }
            if (animator!=null)
            {
                animator.SetBool("isDead", false);
            }
        }
    }
}