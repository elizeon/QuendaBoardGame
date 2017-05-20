using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General 2D Game obhect
/// </summary>
public class GameObject2D : MonoBehaviour {


    public CatGame catGame { get; private set; }


    // Use this for initialization
    void Start ()
    {
        catGame = FindObjectOfType<CatGame>();

    }
    

    /// <summary>
    /// Max X value of sprite
    /// </summary>
    public float maxX { get; private set; }
    /// <summary>
    /// Max Y value of sprite
    /// </summary>
    public float maxY { get; private set; }

    /// <summary>
    /// Default AnimatedSprite to revert to once finished playing a temp animation.
    /// If null, will be set to the first AnimatedSprite you add.
    /// </summary>
    public string defaultAnim { get; set; }
    /// <summary>
    /// Whether the object should collide with anything.
    /// </summary>
    public bool collisions = true;
    /// <summary>
    /// Whether the object should be drawn.
    /// </summary>
    public bool visible = true;
    /// <summary>
    /// Whether the object should update each frame.
    /// </summary>
    public bool active = true;

    /// <summary>
    /// Max HP of gameobject
    /// </summary>
    public float maxHP = 0;

    private string m_type;
    /// <summary>
    /// Gameobject's type
    /// </summary>
    public string type
    {
        get { return m_type; }
        set { m_type = value; }
    }

    private string m_id;
    /// <summary>
    /// Gameobject's unique ID
    /// </summary>
    public string id
    {
        get { return m_id; }
        set { m_id = value; }
    }
    
    /// <summary>
    /// The 2D position of the gameobject
    /// </summary>
    public Vector2 pos2D
    {
        get
        {
            return new Vector2(transform.position.x,transform.position.y);
        }
        private set
        {
            transform.position = new Vector3(value.x, value.y, transform.position.z);
        }
    }

    public void Set(string newid, string newtype, float maxHP)
    {
        Set(newid, newtype);
        hp = maxHP;
    }
    public void Set(string newid, string newtype)
    {
        id = newid;
        type = newtype;
    }
    public virtual void SetPos2D(Vector2 posToSet)
    {
        pos2D = posToSet;
    }

    /// <summary>
    /// Rotation in radians of the gameobject
    /// </summary>
    public float rotation { get; protected set; }

        /// <summary>
        /// Direction
        /// </summary>
        public Vector2 direction { get; protected set; }


        private Vector2 m_scale = new Vector2(1, 1);
        /// <summary>
        /// Scale of the object. Assumes that sprites in animation set for this object are uniform and do not require individual scaling.
        /// </summary>
        public Vector2 scale { get { return m_scale; } set { m_scale = value; } }


        private List<GameObject2D> m_collisionEvents = new List<GameObject2D>();
        /// <summary>
        /// Collision events this update frame 
        /// </summary>
        public List<GameObject2D> collisionEvents { get { return m_collisionEvents; } }

        public const int COLTRIGGERSIZE = 1000;
    
    /*
        /// <summary>
        /// Enter the size you want the sprite to be and the scale will be set accordingly.
        /// The object must have an animatedSprite active, ie with SetAnim().
        /// </summary>
        /// <param name="newSprSize"></param>
        public void ScaleToSpriteSize(Vector2 newSprSize)
        {
            Vector2 new1 = new Vector2(newSprSize.x / spriteSize.x, newSprSize.y / spriteSize.y);
            m_scale.x = new1.x;
            m_scale.y = new1.y;

        }
        */
        /// <summary>
        /// Last position the gameobject was not colliding with anything.
        /// </summary>
        private Vector2 m_lastPosNoCol = new Vector2();

        /// <summary>
        /// Last pos where the object was not colliding
        /// </summary>
        public Vector2 lastPosNoCol
        {
            get { return m_lastPosNoCol; }
            set { m_lastPosNoCol = value; }
        }
        /// <summary>
        /// Update the 2D game object.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update()
        {
            if (!usingCustomBBox)
            {
                m_boundingBox = new Rectangle((int)(pos2D.x - maxX / 2), (int)(pos2D.y - maxY / 2), (int)(maxX), (int)(maxY));

            }

            /*

            animSprite.Update(gameTime);

            for (int i = 0; i < m_collisionEvents.Count; i++)
            {

                if (m_collisionEvents[i].type == "end" && this.type == "player")
                {
                    if (!Game1.levelStart)
                    {
                        ProcessCollisionEvent(gameTime, m_collisionEvents[i]);

                    }
                }
                else
                {
                    ProcessCollisionEvent(gameTime, m_collisionEvents[i]);

                }



            }
            */


        }

        private bool usingCustomBBox = false;

        /// <summary>
        /// Set the animation to that with the string key given.
        /// </summary>
        /// <param name="anim">String key of animation to set the object to.</param>
        public void SetAnim(string anim)
        {
        /*
            if (animSprites.ContainsKey(anim))
            {
                animSprite = animSprites[anim];
            }
            */

        }

        /*
        /// <summary>
        /// Play animation of this string once, then revert to default.
        /// </summary>
        /// <param name="anim">String key of animation to set the object to.</param>
        public void PlayAnim(string anim)
        {
            if (animSprites.ContainsKey(anim))
            {
                animSprite = animSprites[anim];
                animSprite.PlayOnce();
            }

        }
        */

        /// <summary>
        /// Returns size of the current sprite, taking scale into account.
        /// </summary>
        /*
        public Vector2 spriteSize
        {
            get
            {

                //return new Vector2(animSprite.width * scale.X, animSprite.height * scale.Y);

            }
        }
    */
    /*

        public virtual void Render(SpriteBatch sprBatch)
        {
            if (visible)
            {
                if (animSprite != null)
                {
                    animSprite.Draw(sprBatch, new Vector2(pos2D.X - (spriteSize.X) / 2, pos2D.Y - (spriteSize.Y) / 2), rotation, new Vector2(scale.X, scale.Y));
                    //Render2D.Instance.DrawRectangle(sprBatch, boundingBox, Color.White);

                }
            }


        }
        */

        /// <summary>
        /// Bounding box for collisions
        /// </summary>
        public Rectangle boundingBox

        {
            get
            {

                return m_boundingBox;


            }
            set
            { m_boundingBox = value; }

        }
        private Rectangle m_boundingBox;




        /// <summary>
        /// Hit points
        /// </summary>
        public float hp { get; set; }
        /// <summary>
        /// Time limit in seconds between hits
        /// </summary>
        public float hitLimitSeconds = 1;
        /// <summary>
        /// Last time the gameobject took a hit
        /// </summary>
        public float lastTimeHitTaken { get; set; }



        /// <summary>
        /// Register hit on gameobject
        /// </summary>
        /// <param name="gameTime">time</param>
        /// <param name="hpTaken">hp taken</param>
        public void RegisterHit(float hpTaken)
        {
        float totalTimeSec = Time.timeSinceLevelLoad;
            if (totalTimeSec - lastTimeHitTaken > hitLimitSeconds)
            {
                //Console.WriteLine(m_id + " takes " + hpTaken + " damage.");
                TakeDamage(hpTaken);
                lastTimeHitTaken = totalTimeSec;
            }
        }
        /// <summary>
        /// Take damage
        /// </summary>
        /// <param name="hpTaken">hp taken</param>
        public void TakeDamage(float hpTaken)
        {
            hp -= hpTaken;
            if (hp <= 0)
            {
                TriggerDeath();
            }
        }

        /// <summary>
        /// Set custom bounding box
        /// Will not move with object
        /// </summary>
        /// <param name="rect">bounding box rectangle</param>
        public void SetCustomBoundingBox(Rectangle rect)
        {
            m_boundingBox = rect;
            usingCustomBBox = true;
            //Console.WriteLine(this.id + " now using custom bounding box.\n");
        }

        /// <summary>
        /// Creates custom bounding box centred on the objects current origin.
        /// Note the bounding box will not move with the object.
        /// </summary>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        public void SetCustomBoundingBoxFromOrigin(float iwidth, float iheight)
        {
            SetCustomBoundingBox(new Rectangle((int)(pos2D.x - iwidth / 2), (int)(pos2D.y - iheight / 2), (int)(iwidth), (int)(iheight)));

        }


        /// <summary>
        /// Remove object
        /// </summary>
        public virtual void TriggerDeath()
        {
            //Console.WriteLine(id + " died.");
            visible = false;
            collisions = false;
            active = false;
            SetCustomBoundingBox(default(Rectangle));
        }

        /// <summary>
        /// Returns the object to life. Any custom bounding box will be erased and must be re-assigned.
        /// </summary>
        public virtual void TriggerLife()
        {
            //Console.WriteLine(id + " returned to life.");
            visible = true;
            collisions = true;
            active = true;
            hp = maxHP;
            usingCustomBBox = false;
        }

        /// <summary>
        /// Clear collision events for object
        /// </summary>
        public void ClearCollisionEvents()
        {
            m_collisionEvents = new List<GameObject2D>();
        }




    }
