namespace HairyNerdStudios.GameJams.MiniGameJam96.Unity.Behaviours
{
    using UnityEngine;

    [AddComponentMenu("HairyNerd/MGJ96/Reaper")]
    public class ReaperBehaviour : ActorBehaviour
    {
        #region Members

        public float MovementStep = 0.16f;

        public float MovementCooldownRate = 1;

        public float FrozenSeconds = 1;

        protected float MovementTimer { get; set; }

        protected float FrozenTimer { get; set; }

        protected HeroBehaviour Hero { get; set; }

        #endregion

        #region Public Methods

        public void Freeze()
        {
            FrozenTimer = FrozenSeconds;
        }

        #endregion

        #region Protected Methods

        protected void Move()
        {
            int x = 0;
            int y = 0;

            float dx = Hero.transform.position.x - transform.position.x;
            float dy = Hero.transform.position.y - transform.position.y;


            if (dx > 0.08f)
            {
                x = 1;
            }
            else if (dx < -0.08f)
            {
                x = -1;
            }
            
            if (dy > 0.08f)
            {
                y = 1;
            }
            else if (dy < -0.08f)
            {
                y = -1;
            }

            SetDirection(x, y);

            var newPos = transform.position + new Vector3(x * MovementStep, y * MovementStep, 0);
            transform.position = newPos;
        }

        #endregion

        #region Unity        

        protected void Update()
        {
            if (FrozenTimer > 0)
            {
                FrozenTimer -= Time.deltaTime;
                if (FrozenTimer < 0)
                {
                    FrozenTimer = 0;
                }
            }

            if (FrozenTimer > 0)
            {
                return;
            }

            if (MovementTimer < 1)
            {
                MovementTimer += Time.deltaTime * MovementCooldownRate;

                if (MovementTimer >= 1)
                {
                    Move();
                    MovementTimer = 0;
                }
            }

        }

        protected override void Awake()
        {
            base
                .Awake();

            Hero = FindObjectOfType<HeroBehaviour>();
        }

        #endregion
    }
}
