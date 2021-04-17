using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace yolo {
    public class AssetBank {

        ContentManager content;

        public TextureBank Textures { get; } = new TextureBank();
        public class TextureBank {
            public Texture2D Dev { get; private set; }

            public void LoadContent(AssetBank assets) {

            }
        }

        public SpriteBank Sprites { get; } = new SpriteBank();
        public class SpriteBank {
            public Sprite RoadSprite = new Sprite();

            public void LoadContent(AssetBank assets) {

            }
        }


        public void LoadContent(ContentManager Content) {
            Textures = new TextureBank();

            Textures.Test = Content.Load<Texture2D>("assetName");


        }
    }
}