using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace yolo {
    public class AssetBank {

        public TextureBank Textures { get; } = new TextureBank();
        
        public class TextureBank {
            public Texture2D RoadSprite { get; set; }
            
            public void LoadContent(ContentManager content) {
            
            }
        }

        public SpriteBank Sprites { get; } = new SpriteBank();
        public class SpriteBank {
            public Sprite RoadSprite = new Sprite {
                Texture = 
        }


        public void LoadContent(ContentManager Content) {
            Textures = new TextureBank();

            Textures.Test = Content.Load<Texture2D>("assetName");


        }
    }
}