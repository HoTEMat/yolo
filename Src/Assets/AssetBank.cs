using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace yolo {
    public class AssetBank {

        public class TextureBank {
            public Texture2D Test { get; set; }
        }

        public class Sprites {

        }

        public TextureBank Textures { get; private set; }

        public void LoadContent(ContentManager Content) {
            Textures = new TextureBank();

            Textures.Test = Content.Load<Texture2D>("assetName");


        }
    }
}