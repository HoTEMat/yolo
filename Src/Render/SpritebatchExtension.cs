﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yolo {
    static class SpritebatchExtension {
        public static void DrawSprite(this SpriteBatch sb, Sprite sprite, Vector2 position) {
            sb.Draw(sprite.Texture, position, sprite.SourceRect, sprite.Tone, 0f, sprite.Origin, 1 / 16f, sprite.Effects, 0f);
        }
        public static void DrawSprite(this SpriteBatch sb, Sprite sprite, Vector3 position) {
            sb.Draw(sprite.Texture, new Vector2(position.X, position.Y), sprite.SourceRect, sprite.Tone, 0f, sprite.Origin, 1 / 16f, sprite.Effects, position.Z);
        }
    }
}